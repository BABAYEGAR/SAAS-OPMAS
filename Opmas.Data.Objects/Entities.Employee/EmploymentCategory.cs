using Opmas.Data.Objects.Entities.SystemManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Build.Framework;

namespace Opmas.Data.Objects.Entities.Employee
{
    public class EmploymentCategory : Transport
    {
        public long EmploymentCategoryId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public long InstitutionId { get; set; }

        [ForeignKey("InstitutionId")]
        public virtual Institution Institution { get; set; }
        public IEnumerable<EmployeeWorkData> EmployeeWorkDatas { get; set; }

    }
}