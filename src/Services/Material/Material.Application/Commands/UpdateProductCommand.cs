using Core.Messages;
using FluentValidation;
using Material.Application.DTOs;

namespace Material.Application.Commands
{
    public class UpdateProductCommand : Command
    {
        public IEnumerable<ProductRequestDTO> Products { get; set; }

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

    public class UpdateProductValidation : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductValidation()
        {
            RuleFor(product => product.Products)
                .NotEmpty()
                .WithMessage("Campo obrigatório vazio")
                .OverridePropertyName("Products");
        }
    }
}
