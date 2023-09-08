using Core.Communication.Mediator;
using Core.Messages;
using Core.Messages.CommonMessages.Notifications;
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

            var products = await _materialRepository.GetByRange(request.Products.Select(product => Product.ProductFactory(product.Code, product.Name)));

            if (products.Any())
            {
                foreach (var item in products)
                {
                    var product = request.Products.SingleOrDefault(product => product.Code == item.Code);

                    if (product != null)
                    {
                        item.Update(product.Name, product.Active);
                    }
                }

                await _materialRepository.UpdateRange(products);
            }

            var newProducts = request.Products.ExceptBy(products.Select(c => new { c.Code }), d => new { d.Code })
                                              .Select(e => new Product(e.Code, e.Name, e.Active));

            if (newProducts.Any())
            {
                await _materialRepository.AddRangeAsync(newProducts, cancellationToken);
            }

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
