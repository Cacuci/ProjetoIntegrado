using Configuration.Application.DTOs.Warehouse;

namespace Configuration.Application.Queries
{
    public interface IWarehouseQueries
    {
        public Task<WarehouseResponseDTO?> GetWarehouseByIDAsync(Guid id, CancellationToken cancellationToken);
        public Task<WarehouseResponseDTO?> GetWarehouseByCodeAsync(string code, CancellationToken cancellationToken);
        public Task<IEnumerable<WarehouseResponseDTO>> GetWarehouseAllAsync(CancellationToken cancellationToken);
    }
}
