using System.ComponentModel.DataAnnotations;

namespace Identity.Models
{
    public class UserModel
    {
        [Required(ErrorMessage = "Campo obrigatório não fornecido")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Campo obrigatório não fornecido")]
        public string Password { get; set; }
    }
}
