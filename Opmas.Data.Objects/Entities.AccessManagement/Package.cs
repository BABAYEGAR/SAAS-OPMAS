using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Build.Framework;
using Opmas.Data.Objects.Entities.SystemManagement;

namespace Opmas.Data.Objects.Entities.AccessManagement
{
    public class Package : Transport
    {
        public long PackageId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Amount { get; set; }
        [DisplayName("Start Date")]
        [Required]
        public DateTime StartDate { get; set; }
        [DisplayName("End Date")]
        [Required]
        public DateTime EndDate { get; set; }
        public IEnumerable<Institution> Institutions { get; set; }
    }
}