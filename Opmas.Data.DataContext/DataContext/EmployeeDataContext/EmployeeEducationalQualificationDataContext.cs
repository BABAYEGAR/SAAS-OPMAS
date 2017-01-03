using System.Data.Entity;
using Opmas.Data.Objects.Entities.AccessManagement;
using Opmas.Data.Objects.Entities.Employee;
using Opmas.Data.Objects.Entities.SystemManagement;

namespace Opmas.Data.DataContext.DataContext.EmployeeDataContext
{
    public class EmployeeEducationalQualificationDataContext : DbContext
    {
        public EmployeeEducationalQualificationDataContext()
            : base("name=Opmas")
        {
        }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeEducationalQualification> EmployeeEducationalQualifications { get; set; }
        public virtual DbSet<Institution> Institutions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}