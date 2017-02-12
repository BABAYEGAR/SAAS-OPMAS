using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Build.Framework;
using Opmas.Data.Objects.Entities.SystemManagement;

namespace Opmas.Data.Objects.Payment
{
    public class PaymentAllowance : Transport
    {
        public long PaymentAllowanceId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public long Rate { get; set; }
        public long InstitutionId { get; set; }

        [ForeignKey("InstitutionId")]
        public virtual Institution Institution { get; set; }
        public  IEnumerable<PositionAllowanceMapping> PositionAllowanceMappings { get; set; }
    }
}
