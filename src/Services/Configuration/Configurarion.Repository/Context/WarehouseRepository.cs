using Configuration.Domain;
using Core.Data;
using Microsoft.EntityFrameworkCore;

namespace Configuration.Repository.Context
{
    public class WarehouseRepository : IWarehouseRepository
    {
        public IUnityOfWork UnityOfWork => _context;

        private readonly ConfigurationDataContext _context;

        public WarehouseRepository(ConfigurationDataContext context)
        {
            _context = context;
        }

        public async Task CreateWarehouseAsync(Warehouse warehouse)
        {
            await _context.Warehouses.AddAsync(warehouse);
        }

        public async Task<IEnumerable<Warehouse>> GetWarehouseAllAsync(CancellationToken cancellationToken)
        {
            var warehouses = await _context.Warehouses.AsNoTracking().ToListAsync(cancellationToken);

            return warehouses;
        }

        public async Task<Warehouse?> GetWarehouseByCodeAsync(string id, CancellationToken cancellationToken)
        {
            var warehouse = await _context.Warehouses.FirstOrDefaultAsync(warehouse => warehouse.Code == id, cancellationToken);

            return warehouse;
        }

        public async Task<Warehouse?> GetWarehouseByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var warehouse = await _context.Warehouses.FindAsync(id, cancellationToken);

            return warehouse;
        }

        public Task UpdateAsync(Warehouse warehouse)
        {
            _context.Warehouses.Update(warehouse);

            return Task.CompletedTask;
        }


        public void Dispose() => _context.Dispose();
    }
}
