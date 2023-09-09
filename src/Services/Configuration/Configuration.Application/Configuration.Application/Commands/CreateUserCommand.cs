﻿using Core.Messages;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace Configuration.Application.Commands
{
    public class CreateUserCommand : Command
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }

        public CreateUserCommand(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
        }

        public override bool IsValid()
        {
            ValidationResult = new CreateUserValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }

    public class CreateUserValidation : AbstractValidator<CreateUserCommand>
    {
        //VALIDAÇÃO DOS CAMPOS DO COMANDO PODE SER FEITA AQUI ATRAVÉS DO FLUENT VALIDATION
    }
}
