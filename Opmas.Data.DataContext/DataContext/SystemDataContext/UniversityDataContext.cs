using System.Data.Entity;
using Opmas.Data.Objects.Entities.SystemManagement;

namespace Opmas.Data.DataContext.DataContext.SystemDataContext
{
    public class UniversityDataContext : DbContext
    {
        public UniversityDataContext()
            : base("name=SAAS-OPMAS")
        {
        }

        public virtual DbSet<University> Universities { get; set; }
        public virtual DbSet<Faculty> Faculties { get; set; }
        public virtual DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}