using Core.DomainObjects;

namespace Configuration.Domain
{
    public class Warehouse : Entity, IAggregateRoot
    {
        public string Code { get; private set; }
        public string Name { get; private set; }
        public DateTime DateRegister { get; private set; }
        public bool Active { get; private set; } = true;

        public Warehouse(string code, string name)
        {
            Code = code;
            Name = name;
        }

        public void Activate() => Active = true;

        public void Deactivate() => Active = false;

        public void UpdateName(string name) => Name = name;

        public void UpdateCode(string code) => Code = code;
    }
}
