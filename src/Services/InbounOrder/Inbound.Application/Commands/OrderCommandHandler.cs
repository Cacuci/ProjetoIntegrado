using Core.Communication.Mediator;
using Core.Messages;
using Core.Messages.CommonMessages.Notifications;
using Inbound.Domain;
using Inbound.Domain.Comparer;
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

            order = new Order(request.Order.Number, request.Order.WarehouseCode, request.Order.DateCreated);

            var documents = request.Order.Documents.Select(async document =>
            {
                var documentModel = new OrderDocument(order.Id, document.Number);

                foreach (var item in document.Items)
                {
                    var product = await _orderRepository.GetProductByCodeAsync(item.ProductCode, cancellationToken);

                    Package? package;

                    if (product is null)
                    {
                        product = new Product(item.ProductCode, item.ProductName);

                        package = new Package(product.Id, item.TypePackage, item.QuantityPackage);

                        package.AddBarcodeRange(item.Barcodes.Select(barcode => new Barcode(package.Id, barcode.Code)));

                        product.AddPackage(package);

                        await _orderRepository.CreateProductAsync(product, cancellationToken);
                    }
                    else
                    {
                        package = new Package(product.Id, item.TypePackage, item.QuantityPackage);

                        if (!product.PackageExists(package))
                        {
                            await _orderRepository.AddPackageAsync(package, cancellationToken);

                            product.AddPackage(package);
                        }

                        var barcodes = item.Barcodes.Select(barcode => new Barcode(package.Id, barcode.Code))
                                                    .Except(product.Packages.SelectMany(package => package.Barcodes), new BarcodeComparer());

                        package.AddBarcodeRange(barcodes);

                    }

                    documentModel.AddItem(new OrderItem(documentModel.Id, product.Id, package.Id, item.Quantity));
                }

                return documentModel;
            });

            order.AddDocumentRange(documents.Select(document => document.Result));

            await _orderRepository.CreateOrderAsync(order, cancellationToken);

            return await _orderRepository.UnityOfWork.Commit();
        }

        public async Task<bool> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(request))
            {
                return false;
            }

            //var order = await _orderRepository.GetOrderByNumberAsync(request.Order.Number, cancellationToken);

            //if (order is null)
            //{
            //    await _mediatorHandler.PublishNotification(new DomainNotification("Order", "Ordem não encontrada"));

            //    return false;
            //}

            //order.ClearDocuments();

            //order.AddDocumentRange(request.Order.Documents.Select(document =>
            //{
            //    var documentModel = new OrderDocument(order.Id, document.Number);

            //    document.Items.ForEach(async item =>
            //    {
            //        var productModel = await _orderRepository.GetProductByCodeAsync(item.ProductCode, cancellationToken);

            //        if (productModel is null)
            //        {
            //            productModel = new Product(item.ProductCode, item.ProductName);
            //        }

            //        var packageModel = await _orderRepository.GetPackageByProductIdAsync(item.TypePackage, item.QuantityPackage, cancellationToken);

            //        if (packageModel is null)
            //        {
            //            packageModel = new Package(item.TypePackage, item.QuantityPackage);
            //        }

            //        item.Barcodes.ForEach(async barcode =>
            //        {
            //            var barcodeModel = await _orderRepository.GetBarcodeByCodeAsync(barcode.Code, cancellationToken);

            //            if (barcodeModel is null)
            //            {
            //                barcodeModel = new Barcode(barcode.Code);
            //            }

            //            packageModel.AddBarcode(barcodeModel);
            //        });

            //        documentModel.AddItem(new OrderItem(documentModel.Id, productModel.Id, packageModel.Id, item.Quantity));
            //    });

            //    return documentModel;
            //}));

            //await _orderRepository.UpdateOrderAsync(order);

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
