using System.ComponentModel.DataAnnotations.Schema;
using Opmas.Data.Objects.Entities.Employee;
using Opmas.Data.Objects.Entities.SystemManagement;

namespace Opmas.Data.Objects.PerformanceManagement
{
    public class AppraisalScoreCard : Transport
    { 
        public long AppraisalScoreCardId { get; set; }
        public long? EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
        public long? AppraisalFactorId { get; set; }
        [ForeignKey("AppraisalFactorId")]
        public AppraisalFactor AppraisalFactor { get; set; }
        public long? Score { get; set; }
        public long InstitutionId { get; set; }
        [ForeignKey("InstitutionId")]
        public virtual Institution Institution { get; set; }
    }
}
