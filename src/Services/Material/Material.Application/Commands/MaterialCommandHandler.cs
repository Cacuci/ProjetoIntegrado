using Core.Communication.Mediator;
using Core.Extensions;
using Core.Messages;
using Core.Messages.CommonMessages.Notifications;
using Material.Application.Events;
using Material.Domain;
using MediatR;

namespace Material.Application.Commands
{
    public class MaterialCommandHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IMaterialRepository _materialRepository;
        private readonly IMediatorHandler _mediatorHandler;

        public MaterialCommandHandler(IMaterialRepository materialRepository, IMediatorHandler mediatorHandler)
        {
            _materialRepository = materialRepository;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(request))
            {
                return false;
            }

            var products = await _materialRepository.GetProductRangeAsync(request.Products.Select(item => item.Code), cancellationToken);

            if (products.Any())
            {
                foreach (var item in products)
                {
                    var product = request.Products.FirstOrDefault(product => product.Code == item.Code);

                    if (product != null)
                    {
                        item.Update(product.Name, product.Active);
                    }
                }

                await _materialRepository.UpdateProductRange(products);
            }

            var newProducts = request.Products.ExceptBy(products.Select(c => new { c.Code }), d => new { d.Code })
                                              .Select(e => new Product(e.Code, e.Name, e.Active));

            if (newProducts.Any())
            {
                await _materialRepository.AddProductRangeAsync(newProducts, cancellationToken);
            }

            _ = products.Union(newProducts);

            products.ForEach(item => item.AddEvent(new UpdatedProductEvent(item.Id, item.Code, item.Name, item.Active)));

            return await _materialRepository.UnityOfWork.Commit();
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
