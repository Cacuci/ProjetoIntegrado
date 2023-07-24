using MediatR;

namespace Core.Messages.CommonMessages.Notifications
{
    public class DomainNotificationHandler : INotificationHandler<DomainNotification>
    {
        private readonly List<DomainNotification> _notificationList;

        public DomainNotificationHandler()
        {
            _notificationList = new List<DomainNotification>();
        }

        public Task Handle(DomainNotification notification, CancellationToken cancellationToken)
        {
            _notificationList.Add(notification);

            return Task.CompletedTask;
        }
    }
}
