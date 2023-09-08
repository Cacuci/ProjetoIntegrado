using Core.DomainObjects;

namespace Material.Domain
{
    public class Product : Entity, IAggregateRoot
    {
        public Product(string code, string name, bool active = true)
        {
            Code = code;
            Name = name;
            Active = active;
        }

        public string Code { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public DateTime DateCreated { get; private set; } = DateTime.UtcNow;
        public DateTime DateRegister { get; private set; }

        public void Activate(bool active)
        {
            Active = active;
        }

        public void Update(string name, bool active)
        {
            Name = name;
            Activate(active);
        }

        public static Product ProductFactory(string code, string name, bool active = true)
        {
            return new Product(code, name, active);
        }
    }
}
