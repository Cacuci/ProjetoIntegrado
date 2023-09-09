using Inbound.Application.DTOs;
using Inbound.Domain;

namespace Inbound.Application.Queries
{
    public class OrderQueries : IOrderQueries
    {
        private readonly IOrderRepository _orderRepository;

        public OrderQueries(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<OrderResponseDTO>> GetAllOrderAsync(CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetAllOrdersAsync(cancellationToken);

            var result = orders.Select(order => new OrderResponseDTO
            (
                orderId: order.Id,
                number: order.Number,
                warehouseCode: order.WarehouseCode,
                dateCreated: order.DateRegister,
                documents: order.Documents.Select(document =>
                new OrderDocumentResponseDTO(
                    id: document.Id,
                    number: document.Number,
                    items: document.Items.Select(item =>
                    {
                        var product = _orderRepository.GetProductByIdAsync(item.ProductId).Result;

                        var package = _orderRepository.GetPackageByIdAsync(item.PackageId).Result;

                        var result = new OrderItemResponseDTO(id: item.Id,
                                                              productCode: product?.Code,
                                                              productName: product?.Name,
                                                              typepackage: package?.Type,
                                                              quantityPackage: package.Capacity,
                                                              quantity: item.Quantity);

                        result.AddBarcodeRange(package.Barcodes.Select(barcode => new BarcodeResponseDTO(barcode.Code, barcode.Active)));

                        return result;
                    })))));

            return result;
        }

        public async Task<OrderResponseDTO?> GetOrderByNumberAsync(string number, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrderByNumberAsync(number, cancellationToken);

            if (order is null)
            {
                return null;
            }

            return new OrderResponseDTO(orderId: order.Id,
                                        number: order.Number,
                                        warehouseCode: order.WarehouseCode,
                                        dateCreated: order.DateRegister,
                                        documents: order.Documents.Select(document => new OrderDocumentResponseDTO
                                        (
                                            id: document.Id,
                                            number: document.Number,
                                            items: document.Items.Select(item =>
                                            {
                                                var product = _orderRepository.GetProductByIdAsync(item.ProductId).Result;

                                                var package = _orderRepository.GetPackageByIdAsync(item.PackageId).Result;

                                                var result = new OrderItemResponseDTO(id: item.Id,
                                                                                          productCode: product?.Code,
                                                                                          productName: product?.Name,
                                                                                          typepackage: package?.Type,
                                                                                          quantityPackage: package.Capacity,
                                                                                          quantity: item.Quantity);

                                                result.AddBarcodeRange(package.Barcodes.Select(barcode => new BarcodeResponseDTO(barcode.Code, barcode.Active)));

                                                return result;
                                            }))));
        }

        public async Task<OrderResponseDTO?> GetOrderByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id, cancellationToken);

            if (order is null)
            {
                return null;
            }

            return new OrderResponseDTO(orderId: order.Id,
                                        number: order.Number,
                                        warehouseCode: order.WarehouseCode,
                                        dateCreated: order.DateRegister,
                                        documents: order.Documents.Select(document => new OrderDocumentResponseDTO
                                        (
                                            id: document.Id,
                                            number: document.Number,
                                            items: document.Items.Select(item =>
                                            {
                                                var product = _orderRepository.GetProductByIdAsync(item.ProductId).Result;

                                                var package = _orderRepository.GetPackageByIdAsync(item.PackageId).Result;

                                                var result = new OrderItemResponseDTO(id: item.Id,
                                                                                          productCode: product?.Code,
                                                                                          productName: product?.Name,
                                                                                          typepackage: package?.Type,
                                                                                          quantityPackage: package.Capacity,
                                                                                          quantity: item.Quantity);

                                                result.AddBarcodeRange(package.Barcodes.Select(barcode => new BarcodeResponseDTO(barcode.Code, barcode.Active)));

                                                return result;
                                            }))));
        }
    }
}
