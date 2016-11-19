using System.Collections.Generic;

namespace Opmas.Data.Objects.Entities.AccessManagement
{
    public class Package
    {
        public long PackageId { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
        public IEnumerable<Subscription> Subscriptions { get; set; }
    }
}