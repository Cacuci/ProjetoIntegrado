using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Material.Application.DTOs
{
    public class ProductRequestDTO
    {
        [MaxLength(20, ErrorMessage = "Valor não deve ser maior que 20 caracteres")]
        [Required(ErrorMessage = "Campo obrigatório não fornecido")]
        public string Code { get; set; }

        [MaxLength(140, ErrorMessage = "Valor não deve ser maior que 140 caracteres")]
        [Required(ErrorMessage = "Campo obrigatório não fornecido")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Campo obrigatório não fornecido")]
        [DefaultValue(true)]
        public bool Active { get; set; } = true;
    }
}
