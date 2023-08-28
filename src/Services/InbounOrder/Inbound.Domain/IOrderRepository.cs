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
        Task<Product?> GetProductByCodeAsync(string code, CancellationToken cancellationToken = default);
        Task<Product?> GetProductByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Package?> GetPackageByProductIdAsync(Guid productId, string type, int capacity, CancellationToken cancellationToken = default);
        Task<Package?> GetPackageByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Barcode?> GetBarcodeByCodeAsync(string code, CancellationToken cancellationToken = default);
        Task CreateProductAsync(Product product, CancellationToken cancellationToken);
        Task AddPackageAsync(Package package, CancellationToken cancellationToken = default);
        Task AddBarcodeAsync(Barcode barcode, CancellationToken cancellationToken = default);
    }
}
