namespace Material.Domain.Comparer
{
    public class ProductComparer : IEqualityComparer<Product>
    {
        public bool Equals(Product? x, Product? y)
        {
            if (ReferenceEquals(x, y))
                return true;

            if (x is null || y is null)
                return false;

            return x.Code == y.Code;
        }

        public int GetHashCode(Product product) => product.GetHashCode();
    }
}
