using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Material.Application.DTOs
{
    public class ProductRequestDTO
    {
        /// <summary>
        /// Código do produto
        /// </summary>
        [MaxLength(20, ErrorMessage = "Valor não deve ser maior que 20 caracteres")]
        [Required(ErrorMessage = "Campo obrigatório não fornecido")]
        public string Code { get; set; }

        /// <summary>
        /// Descrição do produto
        /// </summary>
        [MaxLength(140, ErrorMessage = "Valor não deve ser maior que 140 caracteres")]
        [Required(ErrorMessage = "Campo obrigatório não fornecido")]
        public string Name { get; set; }

        /// <summary>
        /// Está ativo?
        /// </summary>
        [Required(ErrorMessage = "Campo obrigatório não fornecido")]
        [DefaultValue(true)]
        public bool Active { get; set; } = true;
    }
}
