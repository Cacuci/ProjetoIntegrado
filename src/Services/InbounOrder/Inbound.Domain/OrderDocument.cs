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

        private readonly List<OrderItem> _items;
        public IEnumerable<OrderItem> Items => _items;

        public void AddItem(OrderItem item)
        {
            if (!_items.Any(c => c.ProductCode == item.ProductCode))
            {
                _items.Add(item);
            }
        }

        public void AddItemRange(IEnumerable<OrderItem> items)
        {
            foreach (var item in items)
            {
                if (!_items.Any(c => c.ProductCode == item.ProductCode))
                {
                    _items.Add(item);
                }
            }
        }

        public void RemoveItem(OrderItem item)
        {
            var result = _items.FirstOrDefault(c => c.ProductCode == item.ProductCode);

            if (result is not null)
            {
                _items.Remove(result);
            }
        }

        public void UpdateItem(IEnumerable<OrderItem> items)
        {
            _items.Clear();

            _items.AddRange(items);
        }
    }
}
