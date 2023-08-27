namespace Inbound.Application.Queries.DTOs
{
    public class OrderResponseDTO
    {
        public OrderResponseDTO(Guid orderId,
                                string number,
                                string warehouseCode,
                                DateTime dateCreated,
                                IEnumerable<OrderDocumentResponseDTO> documents)
        {
            Id = orderId;
            Number = number;
            WarehouseCode = warehouseCode;
            DateCreated = dateCreated;
            Documents = documents;
        }

        public Guid Id { get; set; }
        public string Number { get; private set; }
        public string WarehouseCode { get; private set; }
        public DateTime DateCreated { get; private set; }
        public IEnumerable<OrderDocumentResponseDTO> Documents { get; set; }
    }
}
