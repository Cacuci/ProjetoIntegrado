using Core.Communication.Mediator;
using Core.Messages.CommonMessages.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Material.API.Controllers
{
    public class ControllerBase : Controller
    {
        private readonly DomainNotificationHandler _notification;
        private readonly IMediatorHandler _mediatorHandler;

        public ControllerBase(INotificationHandler<DomainNotification> notification,
                              IMediatorHandler mediatorHandler)
        {
            _notification = (DomainNotificationHandler)notification;
            _mediatorHandler = mediatorHandler;
        }

        protected bool OperationValid()
        {
            return !_notification.HasNotification();
        }

        protected IEnumerable<string> GetMessageError()
        {
            return _notification.GetNotifications().Select(x => x.Value).ToList();
        }

        protected void NotificationError(string code, string message)
        {
            _mediatorHandler.PublishNotification(new DomainNotification(code, message));
        }
    }
}
