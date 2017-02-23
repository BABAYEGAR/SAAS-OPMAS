using System.Data.Entity;
using Opmas.Data.Objects.Entities.AccessManagement;
using Opmas.Data.Objects.Entities.Employee;
using Opmas.Data.Objects.Entities.SystemManagement;
using Opmas.Data.Objects.Mappings;

namespace Opmas.Data.DataContext.DataContext.EmployeeDataContext
{
    public class PositionChangeDataContext : DbContext
    {
        public PositionChangeDataContext()
            : base("name=Opmas")
        {
        }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<PositionChange> PositionChanges { get; set; }
        public virtual DbSet<Institution> Institutions { get; set; }
        public virtual DbSet<EmploymentPosition> EmploymentPositions { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}