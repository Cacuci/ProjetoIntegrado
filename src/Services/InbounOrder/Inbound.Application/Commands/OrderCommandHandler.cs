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

            order = Order.OrderFactory(request.Order.Number, request.Order.WarehouseCode);

            foreach (var document in request.Order.Documents)
            {
                var documentModel = new OrderDocument(order.Id, document.Number);

                foreach (var item in document.Items)
                {
                    var product = await _orderRepository.GetProductByCodeAsync(item.ProductCode, cancellationToken);

                    Package? package;

                    if (product is null)
                    {
                        product = Product.ProductFactory(item.ProductCode, item.ProductName);

                        package = Package.PackageFactory(product.Id, item.TypePackage, item.QuantityPackage);

                        package.AddBarcodeRange(item.Barcodes.Select(barcode => Barcode.BarcodeFactory(package.Id, barcode.Code)));

                        product.AddPackage(package);

                        await _orderRepository.CreateProductAsync(product, cancellationToken);
                    }
                    else
                    {
                        package = Package.PackageFactory(product.Id, item.TypePackage, item.QuantityPackage);

                        if (!product.PackageExists(package))
                        {
                            await _orderRepository.AddPackageAsync(package, cancellationToken);

                            product.AddPackage(package);
                        }
                        else
                        {
                            package = product.GetPackage(package);
                        }

                        if (package is null)
                        {
                            await _mediatorHandler.PublishNotification(new DomainNotification("Package", $"Falha ao localizar a embalagem {item.TypePackage} {item.QuantityPackage} do item {item.ProductCode} {item.ProductName}"));

                            return false;
                        }

                        package.DisableAllBarcode();

                        var existsBarcodes = package.GetAllExistingBarcode(item.Barcodes.Select(barcode => new Barcode(package.Id, barcode.Code)));

                        package.ActivateBarcodeRange(existsBarcodes);

                        await _orderRepository.UpdateBarcodeRange(package.Barcodes);

                        var newbarcodes = package.GetAllNewBarcode(item.Barcodes.Select(barcode => new Barcode(package.Id, barcode.Code)));

                        if (newbarcodes.Any())
                        {
                            package.AddBarcodeRange(newbarcodes);

                            await _orderRepository.AddBarcodeRangeAsync(newbarcodes, cancellationToken);
                        }
                    }

                    documentModel.AddItem(OrderItem.OrderItemFactory(documentModel.Id, product.Id, package.Id, item.Quantity));
                }

                order.AddDocument(documentModel);
            }

            await _orderRepository.CreateOrderAsync(order, cancellationToken);

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

            await _orderRepository.RemoveRangeDocument(order.Documents);

            order.ClearDocuments();

            foreach (var document in request.Order.Documents)
            {
                var documentModel = new OrderDocument(order.Id, document.Number);

                foreach (var item in document.Items)
                {
                    var product = await _orderRepository.GetProductByCodeAsync(item.ProductCode, cancellationToken);

                    Package? package;

                    if (product is null)
                    {
                        product = Product.ProductFactory(item.ProductCode, item.ProductName);

                        package = Package.PackageFactory(product.Id, item.TypePackage, item.QuantityPackage);

                        package.AddBarcodeRange(item.Barcodes.Select(barcode => Barcode.BarcodeFactory(package.Id, barcode.Code)));

                        product.AddPackage(package);

                        await _orderRepository.CreateProductAsync(product, cancellationToken);
                    }
                    else
                    {
                        package = Package.PackageFactory(product.Id, item.TypePackage, item.QuantityPackage);

                        if (!product.PackageExists(package))
                        {
                            await _orderRepository.AddPackageAsync(package, cancellationToken);

                            product.AddPackage(package);
                        }
                        else
                        {
                            package = product.GetPackage(package);
                        }

                        if (package is null)
                        {
                            await _mediatorHandler.PublishNotification(new DomainNotification("Package", $"Falha ao localizar a embalagem {item.TypePackage} {item.QuantityPackage} do item {item.ProductCode} {item.ProductName}"));

                            return false;
                        }

                        package.DisableAllBarcode();

                        var existsBarcodes = package.GetAllExistingBarcode(item.Barcodes.Select(barcode => new Barcode(package.Id, barcode.Code)));

                        package.ActivateBarcodeRange(existsBarcodes);

                        await _orderRepository.UpdateBarcodeRange(package.Barcodes);

                        var newbarcodes = package.GetAllNewBarcode(item.Barcodes.Select(barcode => new Barcode(package.Id, barcode.Code)));

                        if (newbarcodes.Any())
                        {
                            package.AddBarcodeRange(newbarcodes);

                            await _orderRepository.AddBarcodeRangeAsync(newbarcodes, cancellationToken);
                        }
                    }

                    documentModel.AddItem(OrderItem.OrderItemFactory(documentModel.Id, product.Id, package.Id, item.Quantity));
                }

                order.AddDocument(documentModel);
            }

            await _orderRepository.UpdateOrder(order);

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
