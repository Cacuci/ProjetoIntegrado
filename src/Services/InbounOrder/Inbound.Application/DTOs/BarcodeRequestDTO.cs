using System.ComponentModel.DataAnnotations;

namespace Inbound.Application.DTOs
{
    public class BarcodeRequestDTO
    {
        /// <summary>
        /// Cõdigo de barras que identica o SKU
        /// </summary>
        [MaxLength(20, ErrorMessage = "Valor não deve ser maior que 20 caracteres")]
        [Required(ErrorMessage = "Campo obrigatório não fornecido")]
        public string Code { get; set; }
    }
}
