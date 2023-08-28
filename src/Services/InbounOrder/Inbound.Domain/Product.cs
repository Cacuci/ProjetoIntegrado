using Core.DomainObjects;

namespace Inbound.Domain
{
    public class Product : Entity, IAggregateRoot
    {
        public Product(string code, string name)
        {
            Code = code;
            Name = name;
            _packages = new List<Package>();
        }

        public string Code { get; private set; }
        public string Name { get; private set; }
        public bool Active { get; private set; } = true;

        private readonly List<Package> _packages;
        public IReadOnlyCollection<Package> Packages => _packages;

        public bool PackageExists(Package package)
        {
            bool found = _packages.Exists(c => c.ProductId == package.ProductId &&
                                                c.Type == package.Type &&
                                                c.Capacity == package.Capacity);

            return found;
        }

        public void AddPackage(Package package)
        {
            if (!PackageExists(package))
            {
                _packages.Add(package);
            }
        }

        public void RemovePackage(Package package)
        {
            var result = _packages.FirstOrDefault(c => c.Type == package.Type && c.Capacity == package.Capacity);

            if (result != null)
            {
                _packages.Remove(result);
            }
        }

        public void Activate(bool active)
        {
            Active = active;
        }
    }
}
