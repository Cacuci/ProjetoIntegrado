using Core.Messages;

namespace Core.DomainObjects
{
    public abstract class Entity
    {
        public Guid Id { get; set; }

        private readonly List<Event>? _notifications;
        public IReadOnlyCollection<Event>? Notifications => _notifications?.AsReadOnly();

        public Entity()
        {
            _notifications ??= new List<Event>();

            Id = Guid.NewGuid();
        }

        public void AddEvent(Event @event) => _notifications?.Add(@event);

        public void RemoveEvent(Event @event) => _notifications?.Remove(@event);

        public void ClearEvent() => _notifications?.Clear();

        public override bool Equals(object? obj)
        {
            var compareTo = obj as Entity;

            if (ReferenceEquals(this, compareTo))
            {
                return true;
            }

            if (compareTo is null)
            {
                return false;
            }

            return Id.Equals(compareTo.Id);
        }

        public static bool operator ==(Entity entity, Entity other)
        {
            if (entity is null && other is null)
            {
                return true;
            }

            if (entity is null || other is null)
            {
                return false;
            }

            return entity.Equals(other);
        }

        public static bool operator !=(Entity entity, Entity other)
        {
            return !(entity == other);
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 907) + Id.GetHashCode();
        }
    }
}
