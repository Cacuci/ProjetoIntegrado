using Core.Data;
using Material.Domain;
using Material.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Material.Infrastructure.Repository
{
    public class MaterialRespository : IMaterialRepository
    {
        private readonly MaterialDataContext _context;

        public IUnityOfWork UnityOfWork => _context;

        public MaterialRespository(MaterialDataContext context)
        {
            _context = context;
        }

        public async Task AddProductAsync(Product product, CancellationToken cancellationToken)
        {
            await _context.Products.AddAsync(product, cancellationToken);
        }

        public async Task<IEnumerable<Product>> GetAllProductAsync(CancellationToken cancellationToken)
        {
            var products = await _context.Products.AsNoTracking().ToListAsync(cancellationToken);

            return products;
        }

        public async Task<Product?> GetProductByCodeAsync(string code, CancellationToken cancellationToken)
        {
            var product = await _context.Products.SingleOrDefaultAsync(product => product.Code == code, cancellationToken);

            return product;
        }

        public async Task<Product?> GetProductByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var product = await _context.Products.SingleOrDefaultAsync(product => product.Id == id, cancellationToken);

            return product;
        }

        public Task UpdateProduct(Product product)
        {
            _context.Products.Update(product);

            return Task.CompletedTask;
        }

        public async Task<IEnumerable<Product>> GetProductRangeAsync(IEnumerable<string> codes, CancellationToken cancellationToken)
        {
            var products = Enumerable.Empty<Product>().ToList();

            foreach (var code in codes)
            {
                var product = await _context.Products.SingleOrDefaultAsync(c => c.Code == code, cancellationToken);

                if (product is not null)
                {
                    products.Add(product);
                }
            }

            return products;
        }

        public Task UpdateProductRange(IEnumerable<Product> products)
        {
            _context.Products.UpdateRange(products);

            return Task.CompletedTask;
        }

        public async Task AddProductRangeAsync(IEnumerable<Product> products, CancellationToken cancellationToken)
        {
            await _context.Products.AddRangeAsync(products, cancellationToken);
        }

        public void Dispose() => _context.Dispose();
    }
}
