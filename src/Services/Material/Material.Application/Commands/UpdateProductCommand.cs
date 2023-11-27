using Core.Messages;
using FluentValidation;
using Material.Application.DTOs;

namespace Material.Application.Commands
{
    public class UpdateProductCommand : Command
    {
        public IEnumerable<ProductRequestDTO> Products { get; private set; }

        public UpdateProductCommand(IEnumerable<ProductRequestDTO> products)
        {
            Products = products;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateProductValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }

    internal class UpdateProductValidation : AbstractValidator<UpdateProductCommand>
    {
        internal UpdateProductValidation()
        {
            RuleFor(product => product.Products)
                .NotEmpty()
                .WithMessage("Campo obrigatório vazio")
                .OverridePropertyName("Products");
        }
    }
}
