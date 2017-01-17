using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Opmas.Data.Objects.Entities.SystemManagement;
using Opmas.Data.Objects.Entities.User;

namespace Opmas.Data.Objects.Entities.Employee
{
    public class Role
    {
        public long RoleId { get; set; }
        public string Name { get; set; }
        [DisplayName("Manage Packages")]
        public bool ManagePackages { get; set; }
        [DisplayName("Manage Institutions")]
        public bool ManageInstitutions { get; set; }
        [DisplayName("Manage Faculties")]
        public bool ManageFaculties { get; set; }
        [DisplayName("Manage Departments")]
        public bool ManageDepartments { get; set; }
        [DisplayName("Manage Units")]
        public bool ManageUnits { get; set; }
        [DisplayName("Manage Employees")]
        public bool ManageEmployees { get; set; }
        [DisplayName("Manage AppUsers")]
        public bool ManageAppUsers { get; set; }
        public bool ManageAdminAppUsers { get; set; }
        public bool ManageAllInstitutions { get; set; }
        public bool ManageRolePriviledges { get; set; }
        public IEnumerable<AppUser> AppUsers { get; set; }
        public long? InstitutionId { get; set; }
        [ForeignKey("InstitutionId")]
        public virtual Institution Institution { get; set; }
    }
}
