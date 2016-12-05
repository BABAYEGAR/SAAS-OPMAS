using System.Data.Entity;
using Opmas.Data.Objects.Entities.Employee;

namespace Opmas.Data.DataContext.DataContext.EmployeeDataContext
{
    public class EmployeePersonalDataContext : DbContext
    {
        public EmployeePersonalDataContext()
            : base("name=Opmas")
        {
        }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeePersonalData> EmployeePersonalDatas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}