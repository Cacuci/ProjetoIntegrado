using Inbound.Application.Queries.DTOs;

namespace Inbound.Application.Queries
{
    public interface IOrderQueries
    {
        public Task<OrderResponseDTO?> GetOrderByIdAsync(Guid id, CancellationToken cancellationToken);
        public Task<OrderResponseDTO?> GetOrderByNumberAsync(string code, CancellationToken cancellationToken);
        public Task<IEnumerable<OrderResponseDTO>> GetorderAllAsync(CancellationToken cancellationToken);
    }
}
