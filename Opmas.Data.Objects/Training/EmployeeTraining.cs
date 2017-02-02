using Opmas.Data.Objects.Entities.SystemManagement;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Opmas.Data.Objects.Entities.Employee
{
    public class EmployeeTraining : Transport
    {
        public long EmployeeTrainingId { get; set; }
        public string Name { get; set; }
        public long EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public virtual Employee Employees { get; set; }
        public IEnumerable<TrainingCategory> TrainingCategories{ get; set; }

    }
}