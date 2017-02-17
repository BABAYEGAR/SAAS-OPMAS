using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Opmas.Data.Objects.Entities.AccessManagement;
using Opmas.Data.Objects.Entities.User;
using Opmas.Data.Objects.Entities.Employee;
using Opmas.Data.Objects.LeaveManagement;
using Opmas.Data.Objects.Mappings;
using Opmas.Data.Objects.Payment;
using Opmas.Data.Objects.Training;

namespace Opmas.Data.Objects.Entities.SystemManagement
{
    public class Institution
    {
        [Required]   
        public long InstitutionId { get; set; }

        [Required]
        [DisplayName("Institution Name")]
        public string Name { get; set; }

        public string Motto { get; set; }
        public string Logo { get; set; }
        public string Location { get; set; }
        public DateTime SubscriprionStartDate { get; set; }
        public DateTime SubscriptonEndDate { get; set; }
        [DisplayName("Subscription Duration")]
        public string SubscriptionDuration { get; set; }
        public string AccessCode { get; set; }
        [DisplayName("Registration Number")]
        public string RegistrationNumber { get; set; }
        public string SetUpStatus { get; set; }

        [Required]
        [DisplayName("Contact Number")]
        public string ContactNumber { get; set; }

        [Required]
        [EmailAddress]
        [DisplayName("Contact Email")]
        public string ContactEmail { get; set; }

        public IEnumerable<Faculty> Faculties { get; set; }
        public IEnumerable<Department> Departments { get; set; }
        public IEnumerable<EmploymentType> EmploymentTypes { get; set; }
        public IEnumerable<AppUser> AppUsers { get; set; }
        public IEnumerable<EmployeeTraining> EmployeeTrainings { get; set; }
        public IEnumerable<TrainingCategory> TrainingCategories { get; set; }
        public IEnumerable<EmployeeTrainingMapping> EmployeeTrainingMapping { get; set; }
        public IEnumerable<EmploymentCategory> EmploymentCategory { get; set; }
        public IEnumerable<PositionDeductionMapping> PositionDeductionMappings { get; set; }
        public IEnumerable<PositionAllowanceMapping> PositionAllowanceMappings { get; set; }
        public IEnumerable<PaymentDeduction> PaymentDeductions { get; set; }
        public IEnumerable<PaymentAllowance> PaymentAllowances { get; set; }
        public IEnumerable<InstitutionStructure> InstitutionStructure { get; set; }
        public IEnumerable<Employee.Employee> Employees { get; set; }
        public IEnumerable<PaymentDeductionRequest> PaymentDeductionRequests { get; set; }
        public IEnumerable<LeaveType> LeaveTypes { get; set; }

        [DisplayName("Package")]
        public long PackageId { get; set; }

        [ForeignKey("PackageId")]
        public virtual Package Packages { get; set; }

    }
}