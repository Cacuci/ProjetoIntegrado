﻿using System.ComponentModel.DataAnnotations;

namespace Configuration.Application.DTOs.User
{
    public class UserUpdateRequestDTO
    {
        /// <summary>
        /// Nome do usuário
        /// </summary>
        [Required(ErrorMessage = "Campo obrigatório não fornecido")]
        [MaxLength(256, ErrorMessage = "Valor não deve ser maior que 256 caracteres")]
        public string Name { get; set; }
    }
}
