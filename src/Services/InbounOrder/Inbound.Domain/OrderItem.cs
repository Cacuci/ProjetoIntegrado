using Core.DomainObjects;

namespace Inbound.Domain
{
    public class OrderItem : Entity
    {
        public OrderItem(Guid documentId, string productCode, string typePackage, int quantityPackage, decimal quantity)
        {
            DocumentId = documentId;
            ProductCode = productCode;
            TypePackage = typePackage;
            QuantityPackage = quantityPackage;
            Quantity = quantity;
        }

        public Guid DocumentId { get; set; }
        public string ProductCode { get; private set; }
        public string TypePackage { get; private set; }
        public int QuantityPackage { get; private set; }
        public decimal Quantity { get; private set; }
    }
}
