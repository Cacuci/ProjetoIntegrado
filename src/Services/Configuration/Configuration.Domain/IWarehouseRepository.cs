using Core.Data;

namespace Configuration.Domain
{
    public interface IWarehouseRepository : IRepository<Warehouse>
    {
        Task CreateWarehouseAsync(Warehouse warehouse, CancellationToken cancellationToken);
        Task UpdateAsync(Warehouse warehouse);
        Task<Warehouse?> GetWarehouseByCodeAsync(string code, CancellationToken cancellationToken = default);
        Task<Warehouse?> GetWarehouseByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Warehouse>> GetAllWarehouseAsync(CancellationToken cancellationToken = default);
    }
}
