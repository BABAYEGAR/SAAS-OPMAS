using System.Data.Entity;
using Opmas.Data.Objects.Entities.SystemManagement;

namespace Opmas.Data.DataContext.DataContext.SystemDataContext
{
    public class DepartmentDataContext : DbContext
    {
        public DepartmentDataContext()
            : base("name=Opmas")
        {
        }

        public virtual DbSet<Institution> Universities { get; set; }
        public virtual DbSet<Faculty> Faculties { get; set; }
        public virtual DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}