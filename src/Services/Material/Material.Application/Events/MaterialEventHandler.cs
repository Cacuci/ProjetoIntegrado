using Core.Communication.Mediator;
using MediatR;

namespace Material.Application.Events
{
    public class MaterialEventHandler : INotificationHandler<UpdatedProductEvent>
    {
        private readonly IMediatorHandler _mediatorHandler;

        public MaterialEventHandler(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        public Task Handle(UpdatedProductEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
