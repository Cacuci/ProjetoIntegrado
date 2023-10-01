using System.ComponentModel.DataAnnotations;

namespace Inbound.Application.DTOs
{
    public class OrderDocumentRequestDTO
    {
        /// <summary>
        /// Número do documento
        /// </summary>
        [MaxLength(20, ErrorMessage = "Valor não deve ser maior que 20 caracteres")]
        [Required(ErrorMessage = "Campo obrigatório não fornecido")]
        public string Number { get; set; }

        /// <summary>
        /// Lista de itens (Produtos)
        /// </summary>
        [Required(ErrorMessage = "Campo obrigatório não fornecido")]
        public IEnumerable<OrderItemRequestDTO> Items { get; set; }
    }
}
