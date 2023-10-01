using System.ComponentModel.DataAnnotations;

namespace Inbound.Application.DTOs
{
    public class OrderItemRequestDTO
    {
        /// <summary>
        /// Código do produto
        /// </summary>
        [MaxLength(20, ErrorMessage = "Valor não deve ser maior que 20 caracteres")]
        [Required(ErrorMessage = "Campo obrigatório não fornecido")]
        public string ProductCode { get; set; }

        /// <summary>
        /// Descrição do produto
        /// </summary>
        [MaxLength(140, ErrorMessage = "Valor não deve ser maior que 140 caracteres")]
        [Required(ErrorMessage = "Campo obrigatório não fornecido")]
        public string ProductName { get; set; }

        /// <summary>
        /// Tipo da embalagem (KG, CX, FD e etc)
        /// </summary>
        [StringLength(3, ErrorMessage = "Valor não deve ser maior que 3 caracteres")]
        [Required(ErrorMessage = "Campo obrigatório não fornecido")]
        public string TypePackage { get; set; }

        /// <summary>
        /// Capacidade da embalagem
        /// </summary>
        [Range(1, 32767, ErrorMessage = "Valor deve ser entre 1 e 32.767")]
        [Required(ErrorMessage = "Campo obrigatório não fornecido")]
        public int QuantityPackage { get; set; }

        /// <summary>
        /// Quantidade do item
        /// </summary>
        [Range(1, 9999999999.999, ErrorMessage = "Valor deve ser entre 1 e 9.999.999.999,999")]
        [Required(ErrorMessage = "Campo obrigatório não fornecido")]
        public decimal Quantity { get; set; }

        /// <summary>
        /// Lista de código de barras que identica o(s) SKU(s)
        /// </summary>
        [Required(ErrorMessage = "Campo obrigatório não fornecido")]
        public IEnumerable<BarcodeRequestDTO> Barcodes { get; set; }
    }
}
