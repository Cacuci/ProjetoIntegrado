namespace Inbound.Domain.Comparer
{
    public class BarcodeComparer : IEqualityComparer<Barcode>
    {
        public bool Equals(Barcode? x, Barcode? y)
        {
            if (ReferenceEquals(x, y))
                return true;

            if (x is null || y is null)
                return false;

            return x.PackageId == y.PackageId &&
                   x.Code == y.Code;
        }

        public int GetHashCode(Barcode barcode) => barcode.GetHashCode();
    }
}
