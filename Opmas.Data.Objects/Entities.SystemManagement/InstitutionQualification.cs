using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Opmas.Data.Objects.Entities.Employee;

namespace Opmas.Data.Objects.Entities.SystemManagement
{
    public class InstitutionQualification: Transport
    {
        public long InstitutionQualificationId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public long InstitutionId { get; set; }

        [ForeignKey("InstitutionId")]
        public virtual Institution Institution { get; set; }
        public IEnumerable<EmployeeEducationalQualification> EducationalQualifications { get; set; }
    }
}
