using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Opmas.Data.Objects.Entities.Employee;
using Opmas.Data.Objects.Entities.SystemManagement;

namespace Opmas.Data.Objects.Training
{
    public class TrainingCategory : Transport
    {
        public long TrainingCategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long InstitutionId { get; set; }

        [ForeignKey("InstitutionId")]
        public virtual Institution Institution { get; set; }
        public IEnumerable<EmployeeTraining> EmployeeTrainings { get; set; }

    }
}