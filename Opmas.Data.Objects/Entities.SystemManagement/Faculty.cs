using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Opmas.Data.Objects.Entities.User;

namespace Opmas.Data.Objects.Entities.SystemManagement
{
    public class Faculty : Transport
    {

        public long FacultyId { get; set; }
        [DisplayName("Faculty Name")]
        public string Name { get; set; }
        [DisplayName("Institution Name")]
        public long InstitutionId { get; set; }

        [ForeignKey("InstitutionId")]
        public virtual Institution Institution { get; set; }
        public IEnumerable<Employee.Employee> Employees { get; set; }
        public IEnumerable<AppUser> AppUsers { get; set; }
    }
}