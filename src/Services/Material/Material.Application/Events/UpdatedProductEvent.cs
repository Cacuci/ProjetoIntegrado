using Core.Messages;

namespace Material.Application.Events
{
    public class UpdatedProductEvent : Event
    {
        public Guid ProductId { get; private set; }
        public string Code { get; private set; }
        public string Name { get; private set; }
        public bool Active { get; private set; }

        public UpdatedProductEvent(Guid productId, string code, string name, bool active)
        {
            AggregateId = productId;
            ProductId = productId;
            Code = code;
            Name = name;
            Active = active;
        }
    }
}
