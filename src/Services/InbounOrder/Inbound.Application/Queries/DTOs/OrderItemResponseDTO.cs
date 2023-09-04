using System.ComponentModel.DataAnnotations;

namespace Inbound.Application.Queries.DTOs
{
    public class OrderItemResponseDTO
    {
        public OrderItemResponseDTO(Guid id, string productCode, string productName, string typepackage, int quantityPackage, decimal quantity)
        {
            Id = id;
            ProductCode = productCode;
            ProductName = productName;
            TypePackage = typepackage;
            QuantityPackage = quantityPackage;
            Quantity = quantity;
        }

        public Guid Id { get; private set; }

        [MaxLength(20, ErrorMessage = "Valor não deve ser maior que 20 caracteres")]
        [Required(ErrorMessage = "Campo obrigatório não fornecido")]
        public string ProductCode { get; private set; }

        [MaxLength(140, ErrorMessage = "Valor não deve ser maior que 140 caracteres")]
        [Required(ErrorMessage = "Campo obrigatório não fornecido")]
        public string ProductName { get; private set; }

        [StringLength(3, ErrorMessage = "Valor não deve ser maior que 3 caracteres")]
        [Required(ErrorMessage = "Campo obrigatório não fornecido")]
        public string TypePackage { get; private set; }

        [Range(1, 32767, ErrorMessage = "Valor deve ser entre 1 e 32.767")]
        [Required(ErrorMessage = "Campo obrigatório não fornecido")]
        public int QuantityPackage { get; private set; }

        [Range(1, 9999999999.999, ErrorMessage = "Valor deve ser entre 1 e 9.999.999.999,999")]
        [Required(ErrorMessage = "Campo obrigatório não fornecido")]
        public decimal Quantity { get; private set; }

        private readonly List<BarcodeResponseDTO> _barcodes = new();

        [Required(ErrorMessage = "Campo obrigatório não fornecido")]
        public IReadOnlyCollection<BarcodeResponseDTO> Barcodes => _barcodes;

        public void AddBarcode(BarcodeResponseDTO barcode)
        {
            _barcodes.Add(barcode);
        }

        public void AddBarcodeRange(IEnumerable<BarcodeResponseDTO> barcodes)
        {
            _barcodes.AddRange(barcodes);
        }
    }
}
