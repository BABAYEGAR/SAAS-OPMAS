using System.Data.Entity;
using Opmas.Data.Objects.Entities.SystemManagement;

namespace Opmas.Data.DataContext.DataContext.SystemDataContext
{
    public class UnitDataContext : DbContext
    {
        public UnitDataContext()
            : base("name=SAAS-OPMAS")
        {
        }

        public virtual DbSet<Unit> Units { get; set; }
        public virtual DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}