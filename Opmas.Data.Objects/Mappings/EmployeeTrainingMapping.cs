using Opmas.Data.Objects.Entities.SystemManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Opmas.Data.Objects.Entities.Employee
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

    }
}