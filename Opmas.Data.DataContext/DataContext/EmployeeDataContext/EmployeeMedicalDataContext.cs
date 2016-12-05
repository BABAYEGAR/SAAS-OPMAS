using System.Data.Entity;
using Opmas.Data.Objects.Entities.Employee;

namespace Opmas.Data.DataContext.DataContext.EmployeeDataContext
{
    public class EmployeeMedicalDataContext : DbContext
    {
        public EmployeeMedicalDataContext()
            : base("name=Opmas")
        {
        }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeMedicalData> EmployeeMedicalDatas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}