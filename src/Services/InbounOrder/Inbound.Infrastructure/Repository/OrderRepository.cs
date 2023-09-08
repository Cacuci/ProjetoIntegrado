using Core.Data;
using Inbound.Domain;
using Inbound.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Inbound.Infrastructure.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly InboundDataContext _context;

        public IUnityOfWork UnityOfWork => _context;

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

            if (orders.Any())
            {
                foreach (var order in orders)
                {
                    await _context.Entry(order).Collection(order => order.Documents).LoadAsync(cancellationToken);

                    foreach (var document in order.Documents)
                    {
                        await _context.Entry(document).Collection(document => document.Items).LoadAsync(cancellationToken);
                    }
                }
            }

            return orders;
        }

        public async Task<Order?> GetOrderByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(order => order.Id == id, cancellationToken);

            if (order is not null)
            {
                await _context.Entry(order).Collection(order => order.Documents).LoadAsync(cancellationToken);

                foreach (var document in order.Documents)
                {
                    await _context.Entry(document).Collection(document => document.Items).LoadAsync(cancellationToken);
                }
            }

            return order;
        }

        public async Task<Order?> GetOrderByNumberAsync(string number, CancellationToken cancellationToken = default)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(order => order.Number == number, cancellationToken);

            if (order is not null)
            {
                await _context.Entry(order).Collection(order => order.Documents).LoadAsync(cancellationToken);

                foreach (var document in order.Documents)
                {
                    await _context.Entry(document).Collection(document => document.Items).LoadAsync(cancellationToken);
                }
            }

            return order;
        }

        public Task UpdateOrder(Order order)
        {
            _context.Orders.Update(order);

            return Task.CompletedTask;
        }

        public async Task<Product?> GetProductByCodeAsync(string code, CancellationToken cancellationToken = default)
        {
            var product = await _context.Products.FirstOrDefaultAsync(product => product.Code == code, cancellationToken);

            if (product is not null)
            {
                await _context.Entry(product).Collection(product => product.Packages).LoadAsync(cancellationToken);

                foreach (var package in product.Packages)
                {
                    await _context.Entry(package).Collection(package => package.Barcodes).LoadAsync(cancellationToken);
                }
            }

            return product;
        }

        public async Task<Product?> GetProductByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var product = await _context.Products.FirstOrDefaultAsync(product => product.Id == id, cancellationToken);

            return product;
        }


        public async Task<Package?> GetPackageByProductIdAsync(string type, int capacity, CancellationToken cancellationToken = default)
        {
            var package = await _context.Packages.FirstOrDefaultAsync(package => package.Type == type &&
                                                                      package.Capacity == capacity, cancellationToken);

            return package;
        }

        public async Task<Package?> GetPackageByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var package = await _context.Packages.FirstOrDefaultAsync(package => package.Id == id, cancellationToken);

            if (package is not null)
            {
                await _context.Entry(package).Collection(package => package.Barcodes).LoadAsync();
            }

            return package;
        }

        public async Task<Barcode?> GetBarcodeByCodeAsync(string code, CancellationToken cancellationToken = default)
        {
            var barcode = await _context.Barcodes.FirstOrDefaultAsync(barcode => barcode.Code == code, cancellationToken);

            return barcode;
        }

        public async Task CreateProductAsync(Product product, CancellationToken cancellationToken)
        {
            await _context.Products.AddAsync(product, cancellationToken);
        }

        public async Task<Package?> GetPackageByProductIdAsync(Guid productId, string type, int capacity, CancellationToken cancellationToken = default)
        {
            var package = await _context.Packages.FirstOrDefaultAsync(package => package.ProductId == productId &&
                                                                                 package.Type == type &&
                                                                                 package.Capacity == capacity, cancellationToken: cancellationToken);

            return package;
        }

        public async Task AddPackageAsync(Package package, CancellationToken cancellationToken = default)
        {
            await _context.Packages.AddAsync(package, cancellationToken);
        }

        public async Task AddBarcodeRangeAsync(IEnumerable<Barcode> barcodes, CancellationToken cancellationToken = default)
        {
            await _context.Barcodes.AddRangeAsync(barcodes, cancellationToken);
        }

        public Task UpdateBarcodeRange(IEnumerable<Barcode> barcodes)
        {
            _context.Barcodes.UpdateRange(barcodes);

            return Task.CompletedTask;
        }

        public Task RemoveRangeDocument(IEnumerable<OrderDocument> documents)
        {
            _context.OrderDocuments.RemoveRange(documents);

            return Task.CompletedTask;
        }

        public Task RemoveRangeItem(IEnumerable<OrderItem> items)
        {
            _context.OrderItems.RemoveRange(items);

            return Task.CompletedTask;
        }

        public void Dispose() => _context.Dispose();
    }
}
