using System.Data.Entity;
using Opmas.Data.Objects.Entities.AccessManagement;
using Opmas.Data.Objects.Entities.SystemManagement;

namespace Opmas.Data.DataContext.DataContext.SystemDataContext
{
    public class InstitutionDataContext : DbContext
    {
        public InstitutionDataContext()
            : base("name=SAAS-OPMAS")
        {
        }

        public virtual DbSet<Institution> Institutions { get; set; }
        public virtual DbSet<Faculty> Faculties { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Package> Packages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}