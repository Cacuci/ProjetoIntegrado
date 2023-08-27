using Core.Communication.Mediator;
using Core.Messages;
using Core.Messages.CommonMessages.Notifications;
using Inbound.Domain;
using MediatR;

namespace Inbound.Application.Commands
{
    public class OrderCommandHandler : IRequestHandler<CreateOrderCommand, bool>,
                                       IRequestHandler<UpdateOrderCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMediatorHandler _mediatorHandler;

        public OrderCommandHandler(IOrderRepository orderRepository, IMediatorHandler mediatorHandler)
        {
            _orderRepository = orderRepository;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<bool> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(request))
            {
                return false;
            }

            var order = await _orderRepository.GetOrderByNumberAsync(request.Order.Number, cancellationToken);

            if (order is not null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("Order", "Ordem já transmitida"));

                return false;
            }

            var orderModel = new Order(request.Order.Number, request.Order.WarehouseCode, request.Order.DateCreated);

            orderModel.AddDocumentRange(request.Order.Documents.Select(document =>
            {
                var documentModel = new OrderDocument(orderModel.Id, document.Number);

                documentModel.AddItemRange(document.Items.Select(item => new OrderItem(documentModel.Id, item.ProductCode, item.TypePackage, item.QuantityPackage, item.Quantity)));

                return documentModel;
            }));

            await _orderRepository.CreateOrderAsync(orderModel, cancellationToken);

            return await _orderRepository.UnityOfWork.Commit();
        }

        public async Task<bool> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(request))
            {
                return false;
            }

            var order = await _orderRepository.GetOrderByNumberAsync(request.Order.Number, cancellationToken);

            if (order is null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("Order", "Ordem não encontrada"));

                return false;
            }

            order.ClearDocuments();

            order.AddDocumentRange(request.Order.Documents.Select(document =>
            {
                var documentModel = new OrderDocument(order.Id, document.Number);

                documentModel.AddItemRange(document.Items.Select(item => new OrderItem(documentModel.Id, item.ProductCode, item.TypePackage, item.QuantityPackage, item.Quantity)));

                return documentModel;
            }));

            return await _orderRepository.UnityOfWork.Commit();
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
