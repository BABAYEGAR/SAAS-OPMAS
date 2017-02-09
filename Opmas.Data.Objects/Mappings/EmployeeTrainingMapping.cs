using System.ComponentModel.DataAnnotations.Schema;
using Opmas.Data.Objects.Entities.Employee;
using Opmas.Data.Objects.Entities.SystemManagement;
using Opmas.Data.Objects.Training;

namespace Opmas.Data.Objects.Mappings
{
    public class EmployeeTrainingMapping : Transport
    {
        public long EmployeeTrainingMappingId { get; set; }
        public long EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public virtual Employee Employees { get; set; }
        public long EmployeeTrainingId { get; set; }
        [ForeignKey("EmployeeTrainingId")]
        public virtual EmployeeTraining EmployeeTrainings { get; set; }
        public long InstitutionId { get; set; }

        [ForeignKey("InstitutionId")]
        public virtual Institution Institution { get; set; }
        public string CompletionStatus { get; set; }

    }
}