using System.ComponentModel.DataAnnotations;

namespace Inbound.Application.Queries.DTOs
{
    public class OrderItemRequestDTO
    {
        [MaxLength(20, ErrorMessage = "Valor não deve ser maior que 20 caracteres")]
        [Required(ErrorMessage = "Campo obrigatório não fornecido")]
        public string ProductCode { get; set; }

        [MaxLength(140, ErrorMessage = "Valor não deve ser maior que 140 caracteres")]
        [Required(ErrorMessage = "Campo obrigatório não fornecido")]
        public string ProductName { get; set; }

        [StringLength(3, ErrorMessage = "Valor não deve ser maior que 3 caracteres")]
        [Required(ErrorMessage = "Campo obrigatório não fornecido")]
        public string TypePackage { get; set; }

        [Range(1, 32767, ErrorMessage = "Valor deve ser entre 1 e 32.767")]
        [Required(ErrorMessage = "Campo obrigatório não fornecido")]
        public int QuantityPackage { get; set; }

        [Range(1, 9999999999.999, ErrorMessage = "Valor deve ser entre 1 e 9.999.999.999,999")]
        [Required(ErrorMessage = "Campo obrigatório não fornecido")]
        public decimal Quantity { get; set; }

        [Required(ErrorMessage = "Campo obrigatório não fornecido")]
        public IEnumerable<BarcodeRequestDTO> Barcodes { get; set; }
    }
}
