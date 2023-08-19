using Configuration.Domain;
using Core.Communication.Mediator;
using Core.Messages;
using Core.Messages.CommonMessages.Notifications;
using MediatR;

namespace Configuration.Application.Commands
{
    public class WarehouseCommandHandler : IRequestHandler<CreateWarehouseCommand, bool>,
                                           IRequestHandler<UpdateWarehouseCommand, bool>
    {
        private readonly IWarehouseRepository _warehouseRepository;
        public readonly IMediatorHandler _mediatorHandler;

        public WarehouseCommandHandler(IWarehouseRepository warehouseRepository, IMediatorHandler mediatorHandler)
        {
            _warehouseRepository = warehouseRepository;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<bool> Handle(CreateWarehouseCommand request, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(request))
            {
                return false;
            }

            var warehouse = await _warehouseRepository.GetWarehouseByCodeAsync(request.Code, cancellationToken);

            if (warehouse is not null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("Code", "Depósito já cadastrado"));

                return false;
            }

            var warehouseModel = new Warehouse(request.Code, request.Name);

            await _warehouseRepository.CreateWarehouseAsync(warehouseModel);

            return await _warehouseRepository.UnityOfWork.Commit();
        }

        public async Task<bool> Handle(UpdateWarehouseCommand request, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(request))
            {
                return false;
            };

            var warehouse = await _warehouseRepository.GetWarehouseByIdAsync(request.ID, cancellationToken);

            if (warehouse is null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("ID", "Depósito não encontrado"));

                return false;
            }

            warehouse.UpdateName(request.Name);

            warehouse.UpdateCode(request.Code);

            await _warehouseRepository.UpdateAsync(warehouse);

            return await _warehouseRepository.UnityOfWork.Commit();
        }

        private bool ValidateCommand(Command message)
        {
            if (message.IsValid())
            {
                return true;
            }
            foreach (var error in message.ValidationResult.Errors)
            {
                _mediatorHandler.PublishNotification(new DomainNotification(message.MessageType, error.ErrorMessage));
            }

            return false;
        }
    }
}
