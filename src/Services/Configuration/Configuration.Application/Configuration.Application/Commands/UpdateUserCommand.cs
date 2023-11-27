﻿using Core.Messages;
using FluentValidation;

namespace Configuration.Application.Commands
{
    public class UpdateUserCommand : Command
    {
        public string ID { get; private set; }
        public string Name { get; private set; }

        public UpdateUserCommand(string iD, string name)
        {
            ID = iD;
            Name = name;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateUserValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }

    internal class UpdateUserValidation : AbstractValidator<UpdateUserCommand>
    {
        //VALIDAÇÃO DOS CAMPOS DO COMANDO PODE SER FEITA AQUI ATRAVÉS DO FLUENT VALIDATION
    }
}
