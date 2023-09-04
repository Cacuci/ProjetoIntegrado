using System.ComponentModel.DataAnnotations;

namespace Inbound.Application.Queries.DTOs
{
    public class OrderRequestDTO
    {
        [Required(ErrorMessage = "Campo obrigatório não fornecido")]
        [MaxLength(20, ErrorMessage = "Valor não deve ser maior que 20 caracteres")]
        public string Number { get; set; }

        [Required(ErrorMessage = "Campo obrigatório não fornecido")]
        [MaxLength(7, ErrorMessage = "Valor não deve ser maior que 7 caracteres")]
        public string WarehouseCode { get; set; }

        [Required(ErrorMessage = "Campo obrigatório não fornecido")]
        public DateTime? DateCreated { get; set; } = DateTime.UtcNow;

        [Required(ErrorMessage = "Campo obrigatório não fornecido")]
        public IEnumerable<OrderDocumentRequestDTO> Documents { get; set; }
    }
}
