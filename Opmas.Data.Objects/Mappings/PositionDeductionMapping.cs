using System.ComponentModel.DataAnnotations.Schema;
using Opmas.Data.Objects.Entities.Employee;
using Opmas.Data.Objects.Entities.SystemManagement;

namespace Opmas.Data.Objects.Payment
{
    public class PositionDeductionMapping : Transport
    {
        public long PositionDeductionMappingId { get; set; }
        public long PaymentDeductionId { get; set; }
        [ForeignKey("PaymentDeductionId")]

        public PaymentDeduction PaymentDeduction { get; set; }
        public long EmploymentPositionId { get; set; }
        [ForeignKey("EmploymentPositionId")]

        public EmploymentPosition EmploymentPosition { get; set; }
        public long InstitutionId { get; set; }

        [ForeignKey("InstitutionId")]
        public virtual Institution Institution { get; set; }
    }
}
