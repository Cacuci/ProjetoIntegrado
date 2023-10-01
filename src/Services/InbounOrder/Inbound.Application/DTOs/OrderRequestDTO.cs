using System.ComponentModel.DataAnnotations;

namespace Inbound.Application.DTOs
{
    public class OrderRequestDTO
    {
        /// <summary>
        /// Número da ordem
        /// </summary>
        [Required(ErrorMessage = "Campo obrigatório não fornecido")]
        [MaxLength(20, ErrorMessage = "Valor não deve ser maior que 20 caracteres")]
        public string Number { get; set; }

        /// <summary>
        /// Código da loja de destino
        /// </summary>
        [Required(ErrorMessage = "Campo obrigatório não fornecido")]
        [MaxLength(7, ErrorMessage = "Valor não deve ser maior que 7 caracteres")]
        public string WarehouseCode { get; set; }

        /// <summary>
        /// Lista de documentos
        /// </summary>
        [Required(ErrorMessage = "Campo obrigatório não fornecido")]
        public IEnumerable<OrderDocumentRequestDTO> Documents { get; set; }
    }
}
