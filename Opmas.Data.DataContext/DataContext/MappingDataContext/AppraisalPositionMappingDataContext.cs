using System.Data.Entity;
using Opmas.Data.Objects.Entities.Employee;
using Opmas.Data.Objects.Entities.SystemManagement;
using Opmas.Data.Objects.Mappings;
using Opmas.Data.Objects.Payment;
using Opmas.Data.Objects.PerformanceManagement;
using Opmas.Data.Objects.Training;

namespace Opmas.Data.DataContext.DataContext.MappingDataContext
{
    public class AppraisalPositionMappingDataContext : DbContext
    {
        public AppraisalPositionMappingDataContext()
            : base("name=Opmas")
        {
        }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<AppraisalPositionMapping> AppraisalPositionMappings { get; set; }
        public virtual DbSet<AppraisalFactor> AppraisalFactors { get; set; }
        public virtual DbSet<AppraisalCategory> AppraisalCategories { get; set; }
        public virtual DbSet<EmploymentPosition> EmploymentPosition { get; set; }
        public virtual DbSet<Institution> Institutions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}