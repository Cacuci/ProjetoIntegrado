using Core.Data;

namespace Material.Domain
{
    public interface IMaterialRepository : IRepository<Product>
    {
        Task AddAsync(Product product, CancellationToken cancellationToken);
        Task AddRangeAsync(IEnumerable<Product> products, CancellationToken cancellationToken);
        Task Update(Product product);
        Task UpdateRange(IEnumerable<Product> products);
        Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<Product?> GetByCodeAsync(string code, CancellationToken cancellationToken);
        Task<IEnumerable<Product>> GetByRange(IEnumerable<Product> products);
        Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken);
    }
}
