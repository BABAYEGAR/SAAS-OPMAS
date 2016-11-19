using System.Data.Entity;
using Opmas.Data.Objects.Entities.Employee;

namespace Opmas.Data.DataContext.DataContext.EmployeeDataContext
{
    public partial class EmployeeDataContext : DbContext
    {
        public EmployeeDataContext()
            : base("name=SAAS-OPMAS")
        {
        }


        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeePersonalData> EmployeePersonalDatas { get; set; }
        public virtual DbSet<EmployeeBankData> EmployeeBankDatas { get; set; }
        public virtual DbSet<EmployeeEducationalQualification> EmployeeEducationalQualifications { get; set; }
        public virtual DbSet<EmployeeMedicalData> EmployeeMedicalDatas { get; set; }
        public virtual DbSet<EmployeePastWorkExperience> EmployeePastWorkExperiences { get; set; }
        public virtual DbSet<EmployeeWorkData> EmployeeWorkDatas { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
