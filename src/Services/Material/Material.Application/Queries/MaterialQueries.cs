using Material.Application.DTOs;
using Material.Domain;

namespace Material.Application.Queries
{
    public class MaterialQueries : IMaterialQueries
    {
        private readonly IMaterialRepository _materialRepository;

        public MaterialQueries(IMaterialRepository materialRepository)
        {
            _materialRepository = materialRepository;
        }

        public async Task<IEnumerable<ProductResponseDTO>> GetAllAsync(CancellationToken cancellationToken)
        {
            var products = await _materialRepository.GetAllAsync(cancellationToken);

            return products.Select(product => new ProductResponseDTO(product.Id, product.Code, product.Name, product.Active));
        }

        public async Task<ProductResponseDTO?> GetByCodeAsync(string code, CancellationToken cancellationToken)
        {
            var product = await _materialRepository.GetByCodeAsync(code, cancellationToken);

            if (product is null)
            {
                return null;
            }

            return new ProductResponseDTO(product.Id, product.Code, product.Name, product.Active);
        }

        public async Task<IEnumerable<ProductResponseDTO>> GetByCodeRangeAsync(IEnumerable<Product> products, CancellationToken cancellationToken)
        {
            var result = await _materialRepository.GetByRange(products);

            return result.Select(product => new ProductResponseDTO(product.Id, product.Code, product.Name, product.Active));
        }

        public async Task<ProductResponseDTO?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var product = await _materialRepository.GetByIdAsync(id, cancellationToken);

            if (product is null)
            {
                return null;
            }

            return new ProductResponseDTO(product.Id, product.Code, product.Name, product.Active);
        }
    }
}
