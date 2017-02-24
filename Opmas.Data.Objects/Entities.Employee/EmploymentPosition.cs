using Opmas.Data.Objects.Entities.SystemManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Build.Framework;
using Opmas.Data.Objects.Mappings;
using Opmas.Data.Objects.Payment;
using Opmas.Data.Objects.PerformanceManagement;

namespace Opmas.Data.Objects.Entities.Employee
{
    public class EmploymentPosition : Transport
    {
        public long EmploymentPositionId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public long Income { get; set; }
        [DisplayName("Is this position for senior members of your institution?")]
        public bool SeniorMember { get; set; }
        public long InstitutionId { get; set; }

        [ForeignKey("InstitutionId")]
        public virtual Institution Institution { get; set; }
        public IEnumerable<EmployeeWorkData> EmployeeWorkDatas { get; set; }
        public IEnumerable<PositionDeductionMapping> AssignPaymentDeductions { get; set; }
        public IEnumerable<PositionAllowanceMapping> AssignPaymentAllowances { get; set; }
        public IEnumerable<AppraisalPositionMapping> AppraisalPositionMappings { get; set; }
        public IEnumerable<PositionChange> PositionChanges { get; set; }

    }
}