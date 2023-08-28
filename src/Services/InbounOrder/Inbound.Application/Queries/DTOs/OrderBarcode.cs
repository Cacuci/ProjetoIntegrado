using System.ComponentModel.DataAnnotations;

namespace Inbound.Application.Queries.DTOs
{
    public class OrderBarcode
    {
        [MaxLength(20, ErrorMessage = "Valor não deve ser maior que 20 caracteres")]
        [Required(ErrorMessage = "Campo obrigatório não fornecido")]
        public string Code { get; set; }
    }
}
