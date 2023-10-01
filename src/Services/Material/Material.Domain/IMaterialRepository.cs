using Core.Data;

namespace Material.Domain
{
    public interface IMaterialRepository : IRepository<Product>
    {
        Task AddProductAsync(Product product, CancellationToken cancellationToken);
        Task AddProductRangeAsync(IEnumerable<Product> products, CancellationToken cancellationToken);
        Task UpdateProduct(Product product);
        Task UpdateProductRange(IEnumerable<Product> products);
        Task<Product?> GetProductByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<Product?> GetProductByCodeAsync(string code, CancellationToken cancellationToken);
        Task<IEnumerable<Product>> GetProductRangeAsync(IEnumerable<string> codes, CancellationToken cancellationToken);
        Task<IEnumerable<Product>> GetAllProductAsync(CancellationToken cancellationToken);
    }
}
