using MediatR;

namespace Core.Messages.CommonMessages.Notifications
{
    public class DomainNotification : INotification
    {
        public DateTime TimeStamp { get; }
        public Guid DomainNotificationId { get; }
        public string Key { get; }
        public string Value { get; }
        public int Version { get; }

        public DomainNotification(string key, string value)
        {
            TimeStamp = DateTime.Now;
            DomainNotificationId = Guid.NewGuid();
            Version = 1;
            Key = key;
            Value = value;
        }
    }
}
