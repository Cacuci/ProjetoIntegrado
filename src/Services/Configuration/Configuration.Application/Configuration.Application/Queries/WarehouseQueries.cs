using Configuration.Application.Queries.DTOs.Warehouse;
using Configuration.Domain;

namespace Configuration.Application.Queries
{
    public class WarehouseQueries : IWarehouseQueries
    {
        private readonly IWarehouseRepository _warehouseRepository;

        public WarehouseQueries(IWarehouseRepository warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;
        }

        public async Task<IEnumerable<WarehouseResponseDTO>> GetAllWarehouseAsync(CancellationToken cancellationToken)
        {
            var warehouses = await _warehouseRepository.GetAllWarehouseAsync(cancellationToken);

            return warehouses.Select(warehouse => new WarehouseResponseDTO(warehouse.Id, warehouse.Code, warehouse.Name));
        }

        public async Task<WarehouseResponseDTO?> GetWarehouseByCodeAsync(string code, CancellationToken cancellationToken)
        {
            var warehouse = await _warehouseRepository.GetWarehouseByCodeAsync(code, cancellationToken);

            if (warehouse is null)
            {
                return null;
            }

            return new WarehouseResponseDTO(warehouse.Id, warehouse.Code, warehouse.Name);
        }

        public async Task<WarehouseResponseDTO?> GetWarehouseByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var warehouse = await _warehouseRepository.GetWarehouseByIdAsync(id, cancellationToken);

            if (warehouse is null)
            {
                return null;
            }

            return new WarehouseResponseDTO(warehouse.Id, warehouse.Code, warehouse.Name);
        }
    }
}
