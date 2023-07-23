using MediatR;

namespace Core.Messages
{
    public abstract class Event : Message, INotification
    {
        public DateTime TimeStamp { get; }

        public Event()
        {
            TimeStamp = DateTime.Now;
        }
    }
}
