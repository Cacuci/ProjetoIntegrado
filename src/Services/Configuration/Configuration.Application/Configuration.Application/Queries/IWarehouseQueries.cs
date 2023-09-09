using Configuration.Application.Queries.DTOs.Warehouse;

namespace Configuration.Application.Queries
{
    public interface IWarehouseQueries
    {
        public Task<WarehouseResponseDTO?> GetWarehouseByIdAsync(Guid id, CancellationToken cancellationToken);
        public Task<WarehouseResponseDTO?> GetWarehouseByCodeAsync(string code, CancellationToken cancellationToken);
        public Task<IEnumerable<WarehouseResponseDTO>> GetAllWarehouseAsync(CancellationToken cancellationToken);
    }
}
