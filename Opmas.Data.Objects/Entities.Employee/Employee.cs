using System.Collections.Generic;
using Opmas.Data.Objects.Entities.SystemManagement;
using Opmas.Data.Objects.Entities.User;

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
        public List<AppUser> AppUsers { get; set; }
    }
}