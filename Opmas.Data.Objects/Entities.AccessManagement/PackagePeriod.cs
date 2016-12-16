using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Build.Framework;
using Opmas.Data.Objects.Entities.SystemManagement;

namespace Opmas.Data.Objects.Entities.AccessManagement
{
    public class PackagePeriod
    {
        public long PackagePeriodId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public IEnumerable<Institution> Institutions { get; set; }
    }
}