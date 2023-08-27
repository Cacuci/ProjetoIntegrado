using Core.Data;
using Inbound.Domain;
using Inbound.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Inbound.Infrastructure.Repository
{
    public class OrderRepository : IOrderRepository
    {
        public IUnityOfWork UnityOfWork => _context;

        private readonly InboundDataContext _context;

        public OrderRepository(InboundDataContext context)
        {
            _context = context;
        }

        public async Task CreateOrderAsync(Order order, CancellationToken cancellationToken)
        {
            await _context.Orders.AddAsync(order, cancellationToken);
        }

        public async Task DeleteOrderAsync(string number, CancellationToken cancellationToken)
        {

            await _context.Orders.Where(c => c.Number == number).ExecuteDeleteAsync(cancellationToken);
        }

        public async Task<IEnumerable<Order?>> GetAllOrdersAsync(CancellationToken cancellationToken = default)
        {
            var orders = await _context.Orders.AsNoTracking().ToListAsync(cancellationToken);

            return orders;
        }

        public async Task<Order?> GetOrderByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(order => order.Id == id, cancellationToken);

            return order;
        }

        public async Task<Order?> GetOrderByNumberAsync(string number, CancellationToken cancellationToken = default)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(order => order.Number == number, cancellationToken);

            return order;
        }

        public Task UpdateOrderAsync(Order order)
        {
            _context.Orders.Update(order);

            return Task.CompletedTask;
        }

        public void Dispose() => _context.Dispose();
    }
}
