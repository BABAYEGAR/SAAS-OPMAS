using System.Data.Entity;
using Opmas.Data.Objects.Entities.Employee;
using Opmas.Data.Objects.Entities.SystemManagement;
using Opmas.Data.Objects.Mappings;
using Opmas.Data.Objects.PerformanceManagement;

namespace Opmas.Data.DataContext.DataContext.PerformanceDataContext
{
    public class AppraisalCategoryDataContext : DbContext
    {
        public AppraisalCategoryDataContext()
            : base("name=Opmas")
        {
        }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<AppraisalFactor> AppraisalFactors { get; set; }
        public virtual DbSet<AppraisalCategory> AppraisalCategories { get; set; }
        public virtual DbSet<Institution> Institutions { get; set; }
        public virtual DbSet<EmploymentPosition> EmploymentPositions { get; set; }
        public virtual DbSet<AppraisalPositionMapping> AppraisalPositionMappings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}