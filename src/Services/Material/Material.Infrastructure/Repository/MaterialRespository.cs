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

        public async Task AddAsync(Product product, CancellationToken cancellationToken)
        {
            await _context.Products.AddAsync(product, cancellationToken);
        }

        public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken)
        {
            var products = await _context.Products.AsNoTracking().ToListAsync(cancellationToken);

            return products;
        }

        public async Task<Product?> GetByCodeAsync(string code, CancellationToken cancellationToken)
        {
            var product = await _context.Products.SingleOrDefaultAsync(product => product.Code == code, cancellationToken);

            return product;
        }

        public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var product = await _context.Products.SingleOrDefaultAsync(product => product.Id == id, cancellationToken);

            return product;
        }

        public Task Update(Product product)
        {
            _context.Products.Update(product);

            return Task.CompletedTask;
        }

        public Task<IEnumerable<Product>> GetByRange(IEnumerable<Product> products)
        {
            var result = _context.Products.AsEnumerable()
                                          .Join(products,
                                                c => c.Code,
                                                d => d.Code,
                                                (c, d) => new Product(d.Code, d.Name, d.Active));

            return Task.FromResult(result);

        }

        public Task UpdateRange(IEnumerable<Product> products)
        {
            _context.Products.UpdateRange(products);

            return Task.CompletedTask;
        }

        public async Task AddRangeAsync(IEnumerable<Product> products, CancellationToken cancellationToken)
        {
            await _context.Products.AddRangeAsync(products, cancellationToken);
        }

        public void Dispose() => _context.Dispose();
    }
}
