using System.ComponentModel.DataAnnotations;

namespace Material.Application.DTOs
{
    public class ProductResponseDTO
    {
        public ProductResponseDTO(Guid id, string code, string name, bool active)
        {
            Id = id;
            Code = code;
            Name = name;
            Active = active;
        }

        [Required(ErrorMessage = "Campo obrigatório não fornecido")]
        public Guid Id { get; set; }

        [MaxLength(20, ErrorMessage = "Valor não deve ser maior que 20 caracteres")]
        [Required(ErrorMessage = "Campo obrigatório não fornecido")]
        public string Code { get; set; }

        [MaxLength(140, ErrorMessage = "Valor não deve ser maior que 140 caracteres")]
        [Required(ErrorMessage = "Campo obrigatório não fornecido")]
        public string Name { get; set; }

        [MaxLength(140, ErrorMessage = "Valor não deve ser maior que 140 caracteres")]
        [Required(ErrorMessage = "Campo obrigatório não fornecido")]
        public bool Active { get; set; }
    }
}
