namespace Inbound.Application.Queries.DTOs
{
    public class OrderDocumentResponseDTO
    {
        public OrderDocumentResponseDTO(Guid id, string number, IEnumerable<OrderItemResponseDTO> items)
        {
            Id = id;
            Number = number;
            Items = items;
        }

        public Guid Id { get; private set; }
        public string Number { get; private set; }
        public IEnumerable<OrderItemResponseDTO> Items { get; set; }
    }
}
