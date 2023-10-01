using Core.Messages;
using FluentValidation;
using Inbound.Application.DTOs;

namespace Inbound.Application.Commands
{
    public class UpdateOrderCommand : Command
    {
        public Guid Id { get; private set; }
        public OrderRequestDTO Order { get; private set; }

        public UpdateOrderCommand(Guid id, OrderRequestDTO order)
        {
            Id = id;
            Order = order;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateOrderValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }

    public class UpdateOrderValidation : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderValidation()
        {
            RuleFor(order => order.Order.Documents)
                .NotEmpty()
                .WithMessage("Campo obrigatório vazio")
                .OverridePropertyName("Documents");

            RuleFor(order => order.Order.Documents.All(item => !item.Items.Any()))
                .NotEqual(true)
                .WithMessage("Campo obrigatório vazio")
                .OverridePropertyName("Items");
        }
    }
}
