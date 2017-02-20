using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Build.Framework;
using Opmas.Data.Objects.Entities.Employee;
using Opmas.Data.Objects.Entities.SystemManagement;

namespace Opmas.Data.Objects.LeaveManagement
{
    public class Leave: Transport
    {
        public long LeaveId { get; set; }
        [Required]
        public string Reason { get; set; }
        [Required]
        public long LeaveTypeId { get; set; }
        [ForeignKey("LeaveTypeId")]
        public LeaveType LeaveType { get; set; }
        public long EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
        public long InstitutionId { get; set; }

        [ForeignKey("InstitutionId")]
        public virtual Institution Institution { get; set; }
        public long DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; }
        public long FacultyId { get; set; }

        [ForeignKey("FacultyId")]
        public virtual Faculty Faculty { get; set; }
        public string Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Comment { get; set; }
    }
}
