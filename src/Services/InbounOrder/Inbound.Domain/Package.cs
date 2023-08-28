using Core.DomainObjects;

namespace Inbound.Domain
{
    public class Package : Entity
    {
        public Package(Guid productId, string type, int capacity)
        {
            ProductId = productId;
            Type = type;
            Capacity = capacity;
            _barcodes = new List<Barcode>();
        }

        public Guid ProductId { get; set; }
        public string Type { get; private set; }
        public int Capacity { get; private set; }
        public bool Active { get; private set; } = true;

        private readonly List<Barcode> _barcodes;
        public IEnumerable<Barcode> Barcodes => _barcodes;

        public bool BarcodeExists(Barcode barcode)
        {
            bool found = _barcodes.Exists(c => c.Code == barcode.Code);

            return found;
        }

        public void AddBarcodeRange(IEnumerable<Barcode> barcodes)
        {
            foreach (var barcode in barcodes)
            {
                if (!BarcodeExists(barcode))
                {
                    _barcodes.Add(barcode);
                }
            }
        }

        public void AddBarcode(Barcode barcode)
        {
            if (!_barcodes.Exists(c => c.Code == barcode.Code))
            {
                _barcodes.Add(barcode);
            }
        }

        public void RemoveBarcode(Barcode barcode)
        {
            var result = _barcodes.FirstOrDefault(c => c.Code == barcode.Code);

            if (result != null)
            {
                _barcodes.Remove(barcode);
            }
        }

        public void Activate(bool active)
        {
            Active = active;
        }
    }
}
