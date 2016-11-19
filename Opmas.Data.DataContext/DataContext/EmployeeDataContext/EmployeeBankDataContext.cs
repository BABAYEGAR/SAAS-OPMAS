using System.Data.Entity;
using Opmas.Data.Objects.Entities.Employee;

namespace Opmas.Data.DataContext.DataContext.EmployeeDataContext
{
    public partial class EmployeeBankDataContext : DbContext
    {
        public EmployeeBankDataContext()
            : base("name=SAAS-OPMAS")
        {
        }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeBankData> EmployeeBankDatas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
