using Material.Application.DTOs;

namespace Material.Application.Queries
{
    public interface IMaterialQueries
    {
        public Task<ProductResponseDTO?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        public Task<ProductResponseDTO?> GetByCodeAsync(string code, CancellationToken cancellationToken);
        public Task<IEnumerable<ProductResponseDTO>> GetByCodeRangeAsync(IEnumerable<string> codes, CancellationToken cancellationToken);
        public Task<IEnumerable<ProductResponseDTO>> GetAllAsync(CancellationToken cancellationToken);
    }
}
