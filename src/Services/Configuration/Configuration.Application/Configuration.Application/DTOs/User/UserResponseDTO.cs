using System.ComponentModel.DataAnnotations;

namespace Configuration.Application.DTOs.User
{
    public class UserResponseDTO
    {
        public string ID { get; set; }

        [MaxLength(256, ErrorMessage = "Valor não deve ser maior que 256 caracteres")]
        public string Name { get; set; }

        [MaxLength(256, ErrorMessage = "Valor não deve ser maior que 256 caracteres")]
        public string Email { get; set; }

        public UserResponseDTO(string id, string name, string email)
        {
            ID = id;
            Name = name;
            Email = email;
        }
    }
}
