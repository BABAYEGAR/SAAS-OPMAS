using System.Data.Entity;
using Opmas.Data.Objects.Entities.Employee;
using Opmas.Data.Objects.Entities.SystemManagement;
using Opmas.Data.Objects.Mappings;
using Opmas.Data.Objects.Training;

namespace Opmas.Data.DataContext.DataContext.MappingDataContext
{
    public class EmployeeResponsibilityMappingDataContext : DbContext
    {
        public EmployeeResponsibilityMappingDataContext()
            : base("name=Opmas")
        {
        }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeResponsibilityMapping> EmployeeResponsibilityMappings { get; set; }
        public virtual DbSet<Institution> Institutions { get; set; }
        public virtual DbSet<Responsibility> Responsibilities { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}