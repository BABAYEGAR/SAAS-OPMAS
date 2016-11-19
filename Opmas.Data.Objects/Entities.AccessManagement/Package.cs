using System.Collections.Generic;
using Opmas.Data.Objects.Entities.SystemManagement;

namespace Opmas.Data.Objects.Entities.AccessManagement
{
    public class Package : Transport
    {
        public long PackageId { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
        public IEnumerable<Subscription> Subscriptions { get; set; }
    }
}