using System.Data.Entity;
using Opmas.Data.Objects.Entities.AccessManagement;
using Opmas.Data.Objects.Entities.Employee;
using Opmas.Data.Objects.Entities.SystemManagement;

namespace Opmas.Data.DataContext.DataContext.EmployeeDataContext
{
    public class EmployeeWorkDataContext : DbContext
    {
        public EmployeeWorkDataContext()
            : base("name=Opmas")
        {
        }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeWorkData> EmployeeWorkData { get; set; }
        public virtual DbSet<Institution> Institutions { get; set; }
        public virtual DbSet<EmploymentType> EmploymentTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}