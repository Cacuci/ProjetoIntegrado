using System.ComponentModel.DataAnnotations;

namespace Configuration.API.DTOs.User
{
    public class UserRequestDTO
    {
        [Required(ErrorMessage = "Campo obrigatório não fornecido")]
        [MaxLength(256, ErrorMessage = "Valor não deve ser maior que 256 caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Campo obrigatório não fornecido")]
        [MaxLength(256, ErrorMessage = "Valor não deve ser maior que 256 caracteres")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo obrigatório não fornecido")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
