using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Opmas.Data.Objects.Entities.Employee;
using Opmas.Data.Objects.Entities.SystemManagement;
using Opmas.Data.Objects.Mappings;

namespace Opmas.Data.Objects.Training
{
    public class EmployeeTraining : Transport
    {
        public long EmployeeTrainingId { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string CoordinatorFullname { get; set; }
        public string CoordinatorCompany { get; set; }
        public long InstitutionId { get; set; }

        [ForeignKey("InstitutionId")]
        public virtual Institution Institution { get; set; }
        public long TrainingCategoryId { get; set; }

        [ForeignKey("TrainingCategoryId")]
        public virtual TrainingCategory TrainingCategories { get; set; }
        public List<EmployeeTrainingMapping> EmployeeTrainingMappings { get; set; }

    }
}