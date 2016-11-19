using System;
using System.Collections.Generic;
using Opmas.Data.Objects.Entities.SystemManagement;

namespace Opmas.Data.Objects.Entities.AccessManagement
{
    public class Package : Transport
    {
        public long PackageId { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IEnumerable<Institution> Institutions { get; set; }
    }
}