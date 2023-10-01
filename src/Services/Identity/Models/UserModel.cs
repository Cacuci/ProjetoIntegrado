using System.ComponentModel.DataAnnotations;

namespace Identity.Models
{
    public class UserModel
    {
        /// <summary>
        /// Nome do usuário
        /// </summary>
        [Required(ErrorMessage = "Campo obrigatório não fornecido")]
        public string UserName { get; set; }

        /// <summary>
        /// Senha. Deve 12 caracteres, incluindo maiusculas, minúsculas, caracteres especias e números 
        /// </summary>
        [Required(ErrorMessage = "Campo obrigatório não fornecido")]
        public string Password { get; set; }
    }
}
