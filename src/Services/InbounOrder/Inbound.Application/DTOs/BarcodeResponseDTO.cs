using System.ComponentModel.DataAnnotations;

namespace Inbound.Application.DTOs
{
    public class BarcodeResponseDTO
    {
        public BarcodeResponseDTO(string code, bool active)
        {
            Code = code;
            Active = active;
        }

        [MaxLength(20, ErrorMessage = "Valor não deve ser maior que 20 caracteres")]
        [Required(ErrorMessage = "Campo obrigatório não fornecido")]
        public string Code { get; set; }

        public bool Active { get; set; }
    }
}
