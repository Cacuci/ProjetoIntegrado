using Inbound.Application.Queries.DTOs;
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

        public async Task<IEnumerable<OrderResponseDTO>> GetorderAllAsync(CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetAllOrdersAsync(cancellationToken);

            return orders.Select(order => new OrderResponseDTO(orderId: order.Id,
                                                               number: order.Number,
                                                               warehouseCode: order.WarehouseCode,
                                                               dateCreated: order.DateCreated,
                                                               documents: order.Documents.Select(document =>
                                                               new OrderDocumentResponseDTO(id: document.Id,
                                                                                            number: document.Number,
                                                                                            items: document.Items.Select(item => new OrderItemResponseDTO(id: item.Id,
                                                                                                                                                          productCode: item.ProductCode,
                                                                                                                                                          typepackage: item.TypePackage,
                                                                                                                                                          quantityPackage: item.QuantityPackage,
                                                                                                                                                          quantity: item.Quantity))))));
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
                                        dateCreated: order.DateCreated,
                                        documents: order.Documents.Select(document =>
                                        new OrderDocumentResponseDTO(id: document.Id,
                                                                         number: document.Number,
                                                                         items: document.Items.Select(item => new OrderItemResponseDTO(id: item.Id,
                                                                                                                                           productCode: item.ProductCode,
                                                                                                                                           typepackage: item.TypePackage,
                                                                                                                                           quantityPackage: item.QuantityPackage,
                                                                                                                                           quantity: item.Quantity)))));
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
                                        dateCreated: order.DateCreated,
                                        documents: order.Documents.Select(document =>
                                        new OrderDocumentResponseDTO(id: document.Id,
                                                                         number: document.Number,
                                                                         items: document.Items.Select(item => new OrderItemResponseDTO(id: item.Id,
                                                                                                                                           productCode: item.ProductCode,
                                                                                                                                           typepackage: item.TypePackage,
                                                                                                                                           quantityPackage: item.QuantityPackage,
                                                                                                                                           quantity: item.Quantity)))));
        }
    }
}
