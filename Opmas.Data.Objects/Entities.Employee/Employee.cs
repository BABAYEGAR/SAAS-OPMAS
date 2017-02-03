using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Opmas.Data.Objects.Entities.SystemManagement;
using Opmas.Data.Objects.Entities.User;
using Opmas.Data.Objects.Mappings;

namespace Opmas.Data.Objects.Entities.Employee
{
    public class Employee : Transport
    {
        public long EmployeeId { get; set; }
        public List<EmployeeBankData> EmployeeBankDatas { get; set; }
        public List<EmployeePersonalData> EmployeePersonalData { get; set; }
        public List<EmployeeEducationalQualification> EmployeeEducationalQualifications { get; set; }
        public List<EmployeePastWorkExperience> EmployeePastWorkExperiences { get; set; }
        public List<EmployeeMedicalData> EmployeeMedicalDatas { get; set; }
        public List<EmployeeWorkData> EmployeeWorkDatas { get; set; }
        public IEnumerable<AppUser> AppUsers { get; set; }
        public List<EmployeeFamilyData> EmployeeFamilyDatas { get; set; }
        public long InstitutionId { get; set; }

        [ForeignKey("InstitutionId")]
        public virtual Institution Institution { get; set; }
        [DisplayName("Assigned Role")]
        public long? RoleId { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
        [DisplayName("Assigned Unit")]
        public long? UnitId { get; set; }

        [ForeignKey("UnitId")]
        public virtual Unit Unit { get; set; }
        [DisplayName("Assigned Department")]
        public long? DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; }
        [DisplayName("Assigned Faculty")]
        public long? FacultyId { get; set; }
        [ForeignKey("FacultyId")]
        public virtual Faculty Faculty { get; set; }
        public IEnumerable<Role> Roles { get; set; }
        public IEnumerable<EmployeeTrainingMapping> EmployeeTrainingMappings { get; set; }

    }
}