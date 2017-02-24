using System.Data.Entity;
using Opmas.Data.Objects.Entities.Employee;
using Opmas.Data.Objects.Entities.SystemManagement;

namespace Opmas.Data.DataContext.DataContext.SystemDataContext
{
    public class ApplicationStatisticDataContext : DbContext
    {
        public ApplicationStatisticDataContext()
            : base("name=Opmas")
        {
        }

        public virtual DbSet<ApplicationStatistic> ApplicationStatistics { get; set; }
        public virtual DbSet<Institution> Institutions { get; set; }

        public virtual DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}