using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Opmas.Data.Objects.Entities.SystemManagement;

namespace Opmas.Data.Objects.Entities.AccessManagement
{
    public class Subscription
    {
        public long SubscriptionId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public long PackageId { get; set; }
        
        [ForeignKey("PackageId")]
        public virtual Package Package { get; set; }
        public IEnumerable<Institution> Institutions { get; set; }
    }
}