using Core.DomainObjects;

namespace Inbound.Domain
{
    public class Order : Entity, IAggregateRoot
    {
        public Order(string number, string warehouseCode, DateTime dateCreated)
        {
            Number = number;
            WarehouseCode = warehouseCode;
            DateCreated = dateCreated;
            _documents = new List<OrderDocument>();
        }

        public string Number { get; private set; }
        public string WarehouseCode { get; private set; }
        public DateTime DateCreated { get; private set; }
        public DateTime DateRegister { get; private set; }

        private readonly List<OrderDocument> _documents;
        public IReadOnlyCollection<OrderDocument> Documents => _documents;

        public bool DocumentExists(OrderDocument document)
        {
            bool found = _documents.Exists(c => c.Number == document.Number);

            return found;
        }

        public void AddDocument(OrderDocument document)
        {
            if (!DocumentExists(document))
            {
                _documents.Add(document);
            }
        }

        public void AddDocumentRange(IEnumerable<OrderDocument> documents)
        {
            foreach (var document in documents)
            {
                if (!DocumentExists(document))
                {
                    _documents.Add(document);
                }
            }
        }

        public void ClearDocuments()
        {
            _documents.Clear();
        }

        public void RemoveDocument(OrderDocument document)
        {
            _documents.Remove(document);
        }

        public OrderDocument? GetDocumentByNumber(string number)
        {
            var document = _documents.FirstOrDefault(document => document.Number == number);

            return document;
        }

        public static Order OrderFactory(string number, string warehouseCode, DateTime dateCreated)
        {
            return new Order(number, warehouseCode, dateCreated);
        }
    }
}
