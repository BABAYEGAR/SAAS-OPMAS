using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Opmas.Data.Objects.Entities.Employee;
using Opmas.Data.Objects.Entities.SystemManagement;

namespace Opmas.Data.Objects.Mappings
{
    public class EmployeeResponsibilityMapping : Transport
    {
        public long EmployeeResponsibilityMappingId { get; set; }
        public long ResponsibilityId { get; set; }
        [ForeignKey("ResponsibilityId")]
        public Responsibility Responsibility { get; set; }
        public long EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
        public long InstitutionId { get; set; }

        [ForeignKey("InstitutionId")]
        public virtual Institution Institution { get; set; }
    }
}
