using Core.DomainObjects;

namespace Inbound.Domain
{
    public class OrderItem : Entity
    {
        public OrderItem(Guid documentId, Guid productId, Guid packageId, decimal quantity)
        {
            DocumentId = documentId;
            ProductId = productId;
            PackageId = packageId;
            Quantity = quantity;
        }

        public Guid DocumentId { get; private set; }
        public Guid ProductId { get; private set; }
        public Guid PackageId { get; private set; }
        public decimal Quantity { get; private set; }

        public void UpdateQuantity(decimal quantity)
        {
            Quantity = quantity;
        }

        public static OrderItem OrderItemFactory(Guid documentId, Guid productId, Guid packageId, decimal quantity)
        {
            return new OrderItem(documentId, productId, packageId, quantity);
        }
    }
}
