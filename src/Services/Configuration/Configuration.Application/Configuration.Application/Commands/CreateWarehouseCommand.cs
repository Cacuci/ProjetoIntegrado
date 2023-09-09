using Core.Messages;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace Configuration.Application.Commands
{
    public class CreateWarehouseCommand : Command
    {
        public string Code { get; private set; }
        public string Name { get; private set; }

        public CreateWarehouseCommand(string code, string name)
        {
            Code = code;
            Name = name;
        }

        public override bool IsValid()
        {
            ValidationResult = new CreateWarehouseValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }

    public class CreateWarehouseValidation : AbstractValidator<CreateWarehouseCommand>
    {
        //VALIDAÇÃO DOS CAMPOS DO COMANDO PODE SER FEITA AQUI ATRAVÉS DO FLUENT VALIDATION
    }
}
