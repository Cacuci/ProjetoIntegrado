using Core.DomainObjects;
using Inbound.Domain.Comparer;

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

        public IEnumerable<Barcode> GetAllNewBarcode(IEnumerable<Barcode> barcodes)
        {
            var news = barcodes.Select(barcode => Barcode.BarcodeFactory(Id, barcode.Code))
                               .ExceptBy(_barcodes.Select(c => new { c.PackageId, c.Code }), e => new { e.PackageId, e.Code });

            return news ?? Enumerable.Empty<Barcode>();
        }

        public IEnumerable<Barcode> GetAllExistingBarcode(IEnumerable<Barcode> barcodes)
        {
            var news = barcodes.Select(barcode => Barcode.BarcodeFactory(Id, barcode.Code))
                               .IntersectBy(_barcodes.Select(c => new { c.PackageId, c.Code }), e => new { e.PackageId, e.Code });

            return news ?? Enumerable.Empty<Barcode>();
        }

        public IEnumerable<Barcode> GetBarcodesToDesactive(IEnumerable<Barcode> barcodes)
        {
            var olds = _barcodes.Select(barcode => Barcode.BarcodeFactory(Id, barcode.Code))
                                .Except(barcodes, new BarcodeComparer());

            return olds ?? Enumerable.Empty<Barcode>();
        }

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

        public void DisableAllBarcode()
        {
            foreach (var barcode in _barcodes)
            {
                barcode.Activate(false);
            }
        }

        public void ActivateBarcodeRange(IEnumerable<Barcode> barcodes)
        {
            foreach (var barcode in _barcodes)
            {
                if (barcodes.Any(c => c.PackageId == barcode.PackageId &&
                                      c.Code == barcode.Code))
                {
                    barcode.Activate(true);
                }
            }
        }

        public static Package PackageFactory(Guid productId, string type, int capacity)
        {
            return new Package(productId, type, capacity);
        }
    }
}
