using Opmas.Data.Objects.Entities.SystemManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Opmas.Data.Objects.Entities.Employee
{
    public class EmploymentPosition : Transport
    {
        public long EmploymentPositionId { get; set; }
        public string Name { get; set; }
        public long InstitutionId { get; set; }

        [ForeignKey("InstitutionId")]
        public virtual Institution Institution { get; set; }
        public IEnumerable<EmployeeWorkData> EmployeeWorkDatas { get; set; }

    }
}