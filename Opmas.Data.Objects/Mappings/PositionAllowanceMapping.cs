using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Opmas.Data.Objects.Entities.Employee;
using Opmas.Data.Objects.Entities.SystemManagement;

namespace Opmas.Data.Objects.Payment
{
    public class PositionAllowanceMapping : Transport
    {
        public long PositionAllowanceMappingId { get; set; }
        public long PaymentAllowanceId { get; set; }
        [ForeignKey("PaymentAllowanceId")]

        public PaymentAllowance PaymentAllowance { get; set; }
        public long EmploymentPositionId { get; set; }
        [ForeignKey("EmploymentPositionId")]

        public EmploymentPosition EmploymentPosition { get; set; }
        public long InstitutionId { get; set; }

        [ForeignKey("InstitutionId")]
        public virtual Institution Institution { get; set; }
    }
}
