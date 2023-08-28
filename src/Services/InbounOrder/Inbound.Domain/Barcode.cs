using Core.DomainObjects;

namespace Inbound.Domain
{
    public class Barcode : Entity
    {
        public Barcode(Guid packageId, string code)
        {
            PackageId = packageId;
            Code = code;
        }

        public Guid PackageId { get; set; }
        public string Code { get; set; }
        public bool Active { get; set; } = true;

        public void Activate(bool active)
        {
            Active = active;
        }
    }
}
