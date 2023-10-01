using Inbound.Application.DTOs;

namespace Inbound.Application.Queries
{
    public interface IOrderQueries
    {
        public Task<OrderResponseDTO?> GetOrderByIdAsync(Guid id, CancellationToken cancellationToken);
        public Task<OrderResponseDTO?> GetOrderByNumberAsync(string number, CancellationToken cancellationToken);
        public Task<IEnumerable<OrderResponseDTO>> GetAllOrderAsync(CancellationToken cancellationToken);
    }
}
