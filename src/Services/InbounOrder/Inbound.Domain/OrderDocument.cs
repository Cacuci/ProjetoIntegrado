using Core.DomainObjects;

namespace Inbound.Domain
{
    public class OrderDocument : Entity
    {
        public OrderDocument(Guid orderId, string number)
        {
            OrderId = orderId;
            Number = number;
            _items = new List<OrderItem>();
        }

        public Guid OrderId { get; private set; }
        public string Number { get; private set; }
        public DateTime DateRegister { get; private set; }

        private readonly List<OrderItem> _items;
        public IEnumerable<OrderItem> Items => _items;

        public bool ItemExists(OrderItem item)
        {
            bool found = _items.Exists(c => c.ProductId == item.ProductId);

            return found;
        }

        public void AddItem(OrderItem item)
        {
            if (!ItemExists(item))
            {
                _items.Add(item);
            }
        }

        public void AddItemRange(IEnumerable<OrderItem> items)
        {
            foreach (var item in items)
            {
                if (!ItemExists(item))
                {
                    _items.Add(item);
                }
            }
        }

        public void RemoveItem(OrderItem item)
        {
            var result = _items.FirstOrDefault(c => c.ProductId == item.ProductId);

            if (result is not null)
            {
                _items.Remove(result);
            }
        }

        public static OrderDocument OrderDocumentFactory(Guid orderId, string number)
        {
            return new OrderDocument(orderId, number);
        }
    }
}
