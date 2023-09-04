using System.ComponentModel.DataAnnotations;

namespace Inbound.Application.Queries.DTOs
{
    public class OrderDocumentResponseDTO
    {
        public OrderDocumentResponseDTO(Guid id, string number, IEnumerable<OrderItemResponseDTO> items)
        {
            Id = id;
            Number = number;
            Items = items;
        }

        [Required(ErrorMessage = "Campo obrigatório não fornecido")]
        public Guid Id { get; private set; }

        [MaxLength(20, ErrorMessage = "Valor não deve ser maior que 20 caracteres")]
        [Required(ErrorMessage = "Campo obrigatório não fornecido")]
        public string Number { get; private set; }

        [Required(ErrorMessage = "Campo obrigatório não fornecido")]
        public IEnumerable<OrderItemResponseDTO> Items { get; set; }
    }
}
