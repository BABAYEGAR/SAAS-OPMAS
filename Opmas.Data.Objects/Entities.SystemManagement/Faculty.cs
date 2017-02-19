using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Opmas.Data.Objects.Entities.User;
using Opmas.Data.Objects.LeaveManagement;

namespace Opmas.Data.Objects.Entities.SystemManagement
{
    public class Faculty : Transport
    {
        [Key,ForeignKey("Employee")]
        public long FacultyId { get; set; }
        [DisplayName("Faculty Name")]
        public string Name { get; set; }
        [DisplayName("Institution Name")]
        public long InstitutionId { get; set; }

        [ForeignKey("InstitutionId")]
        public virtual Institution Institution { get; set; }
        public long? EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public virtual Employee.Employee Employee { get; set; }
        public IEnumerable<Employee.Employee> Employees { get; set; }
        public IEnumerable<AppUser> AppUsers { get; set; }
        public IEnumerable<Leave> Leaves { get; set; }
    }
}