using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Opmas.Data.Objects.Entities.SystemManagement;

namespace Opmas.Data.Objects.Entities.Employee
{
   public class PositionChange  : Transport
    {
        public long PositionChangeId { get; set; }
        public string Action { get; set; }
        [DisplayName("Previous Position")]
        public long PreviousPositionId { get; set; }
        [DisplayName("Current Position")]
        public long EmploymentPositionId { get; set; }
        [ForeignKey("EmploymentPositionId")]
        public virtual EmploymentPosition EmploymentPosition { get; set; }
        public long EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }
        public long InstitutionId { get; set; }

        [ForeignKey("InstitutionId")]
        public virtual Institution Institution { get; set; }
    }
}
