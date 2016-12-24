using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Opmas.Data.Objects.Entities.AccessManagement;
using Opmas.Data.Objects.Entities.User;

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
        public string AccessCode { get; set; }

        [Required]
        [DisplayName("Contact Number")]
        public string ContactNumber { get; set; }

        [Required]
        [EmailAddress]
        [DisplayName("Contact Email")]
        public string ContactEmail { get; set; }

        public IEnumerable<Faculty> Faculties { get; set; }
        public IEnumerable<Department> Departments { get; set; }
        public IEnumerable<AppUser> AppUsers { get; set; }
        public IEnumerable<Employee.Employee> Employees { get; set; }

        [DisplayName("Package")]
        public long PackageId { get; set; }

        [ForeignKey("PackageId")]
        public virtual Package Packages { get; set; }

    }
}