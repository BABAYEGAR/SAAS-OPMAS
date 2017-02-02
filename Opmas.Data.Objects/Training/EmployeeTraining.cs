using Opmas.Data.Objects.Entities.SystemManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Opmas.Data.Objects.Entities.Employee
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
        public List<EmployeeTrainingMapping> EmployeeTrainingMappings { get; set; }
        public IEnumerable<TrainingCategory> TrainingCategories{ get; set; }

    }
}