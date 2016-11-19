using System.Data.Entity;
using Opmas.Data.Objects.Entities.Employee;

namespace Opmas.Data.DataContext.DataContext.EmployeeDataContext
{
    public class EmployeeEducationalQualificationDataContext : DbContext
    {
        public EmployeeEducationalQualificationDataContext()
            : base("name=SAAS-OPMAS")
        {
        }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeEducationalQualification> EmployeeEducationalQualifications { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}