namespace Inbound.Application.Queries.DTOs
{
    public class OrderItemResponseDTO
    {
        public OrderItemResponseDTO(Guid id, string productCode, string typepackage, decimal quantityPackage, decimal quantity)
        {
            Id = id;
            ProductCode = productCode;
            TypePackage = typepackage;
            QuantityPackage = quantityPackage;
            Quantity = quantity;
        }

        public Guid Id { get; private set; }
        public string ProductCode { get; private set; }
        public string TypePackage { get; private set; }
        public decimal QuantityPackage { get; private set; }
        public decimal Quantity { get; private set; }
    }
}
