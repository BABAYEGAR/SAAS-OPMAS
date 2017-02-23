using System.Data.Entity;
using Opmas.Data.Objects.Entities.AccessManagement;
using Opmas.Data.Objects.Entities.Employee;
using Opmas.Data.Objects.Entities.SystemManagement;
using Opmas.Data.Objects.Mappings;

namespace Opmas.Data.DataContext.DataContext.EmployeeDataContext
{
    public class ResponsibilityDataContext : DbContext
    {
        public ResponsibilityDataContext()
            : base("name=Opmas")
        {
        }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Responsibility> Responsibilities { get; set; }
        public virtual DbSet<Institution> Institutions { get; set; }
        public virtual DbSet<EmployeeResponsibilityMapping> EmployeeResponsibilityMappings { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}