using Core.Data;

namespace Inbound.Domain
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task CreateOrderAsync(Order order, CancellationToken cancellationToken);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(string number, CancellationToken cancellationToken);
        Task<IEnumerable<Order?>> GetAllOrdersAsync(CancellationToken cancellationToken = default);
        Task<Order?> GetOrderByNumberAsync(string number, CancellationToken cancellationToken = default);
        Task<Order?> GetOrderByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
