using Core.Messages;
using FluentValidation;
using Inbound.Application.Queries.DTOs;

namespace Inbound.Application.Commands
{
    public class CreateOrderCommand : Command
    {
        public OrderRequestDTO Order { get; set; }

        public CreateOrderCommand(OrderRequestDTO order)
        {
            Order = order;
        }

        public override bool IsValid()
        {
            ValidationResult = new CreateOrderValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }

    public class CreateOrderValidation : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderValidation()
        {
            RuleFor(order => order.Order.Documents)
                .NotEmpty()
                .WithMessage("Campo obrigatório vazio")
                .OverridePropertyName("Documents");

            RuleFor(order => order.Order.Documents.All(item => !item.Items.Any()))
                .NotEqual(true)
                .WithMessage("Campo obrigatório vazio")
                .OverridePropertyName("Items");

            RuleFor(order => order.Order.Documents.All(item => item.Items.All(package => !package.Barcodes.Any())))
                .NotEqual(true)
                .WithMessage("Campo obrigatório vazio")
                .OverridePropertyName("Barcodes");
        }
    }
}
