﻿using Core.DomainObjects;

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
        public DateTime DateRegister { get; private set; }

        public void Activate(bool active)
        {
            Active = active;
        }

        public static Barcode BarcodeFactory(Guid packageId, string code)
        {
            return new Barcode(packageId, code);
        }
    }
}
