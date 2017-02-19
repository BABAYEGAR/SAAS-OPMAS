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
        public string Description { get; set; }
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
        [DisplayName("Manage Admin Users")]
        public bool ManageAdminAppUsers { get; set; }
        [DisplayName("Manage All Institutions")]
        public bool ManageAllInstitutions { get; set; }
        [DisplayName("Manage Priviledges")]
        public bool ManageRolePriviledges { get; set; }
        [DisplayName("Manage Training")]
        public bool ManageTraining { get; set; }
        [DisplayName("Manage Training  Types")]
        public bool ManageTrainingTypes { get; set; }
        [DisplayName("Role Category")]
        public string RoleType { get; set; }
        [DisplayName("Manage Employment Type")]
        public bool ManageEmploymentTypes { get; set; }
        [DisplayName("Manage Payment")]
        public bool ManagePayment { get; set; }
        [DisplayName("Manage Leave Types")]
        public bool ManageLeaveTypes { get; set; }
        [DisplayName("Manage Leave")]
        public bool ManageLeave { get; set; }
        [DisplayName("Manage Senior Staff Leave")]
        public bool ManageSeniorStaffLeave { get; set; }
        public IEnumerable<AppUser> AppUsers { get; set; }
        public IEnumerable<Employee> Employees { get; set; }
        public long? InstitutionId { get; set; }
        [ForeignKey("InstitutionId")]
        public virtual Institution Institution { get; set; }
    }
}
