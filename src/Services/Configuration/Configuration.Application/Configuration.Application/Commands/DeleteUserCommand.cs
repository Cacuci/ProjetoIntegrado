using Core.Messages;
using FluentValidation;

namespace Configuration.Application.Commands
{
    public class DeleteUserCommand : Command
    {
        public string ID { get; private set; }

        public DeleteUserCommand(string iD)
        {
            ID = iD;
        }

        public override bool IsValid()
        {
            ValidationResult = new DeleteUserValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }

    internal class DeleteUserValidation : AbstractValidator<DeleteUserCommand>
    {
        //VALIDAÇÃO DOS CAMPOS DO COMANDO PODE SER FEITA AQUI ATRAVÉS DO FLUENT VALIDATION
    }
}
