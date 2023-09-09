using System.ComponentModel.DataAnnotations;

namespace Inbound.Application.DTOs
{
    public class OrderDocumentRequestDTO
    {
        [MaxLength(20, ErrorMessage = "Valor não deve ser maior que 20 caracteres")]
        [Required(ErrorMessage = "Campo obrigatório não fornecido")]
        public string Number { get; set; }

        [Required(ErrorMessage = "Campo obrigatório não fornecido")]
        public IEnumerable<OrderItemRequestDTO> Items { get; set; }
    }
}
