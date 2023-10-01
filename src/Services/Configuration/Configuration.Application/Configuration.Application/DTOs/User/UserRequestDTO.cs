using System.ComponentModel.DataAnnotations;

namespace Configuration.Application.DTOs.User
{
    public class UserRequestDTO
    {
        /// <summary>
        /// Nome do usuário
        /// </summary>
        [Required(ErrorMessage = "Campo obrigatório não fornecido")]
        [MaxLength(256, ErrorMessage = "Valor não deve ser maior que 256 caracteres")]
        public string Name { get; set; }

        /// <summary>
        /// Email unico de identificação
        /// </summary>
        [Required(ErrorMessage = "Campo obrigatório não fornecido")]
        [MaxLength(256, ErrorMessage = "Valor não deve ser maior que 256 caracteres")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        /// <summary>
        /// Senha. Deve 12 caracteres, incluindo maiusculas, minúsculas, caracteres especias e números 
        /// </summary>
        [Required(ErrorMessage = "Campo obrigatório não fornecido")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
