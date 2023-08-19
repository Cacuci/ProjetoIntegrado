using Core.Messages;
using FluentValidation;

namespace Configuration.Application.Commands
{
    public class UpdateWarehouseCommand : Command
    {
        public Guid ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public UpdateWarehouseCommand(Guid id, string code, string name)
        {
            ID = id;
            Code = code;
            Name = name;
        }

        public override bool IsValid()
        {
            ValidationResult = new DeleteWarehouseValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }

    public class DeleteWarehouseValidation : AbstractValidator<UpdateWarehouseCommand>
    {
        //VALIDAÇÃO DOS CAMPOS DO COMANDO PODE SER FEITA AQUI ATRAVÉS DO FLUENT VALIDATION
    }
}
