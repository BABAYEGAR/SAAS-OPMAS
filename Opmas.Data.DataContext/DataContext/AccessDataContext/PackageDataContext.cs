using System.Data.Entity;

namespace Opmas.Data.DataContext.DataContext.AccessDataContext
{
    public class PackageDataContext : DbContext
    {
        public PackageDataContext()
            : base("name=SAAS-OPMAS")
        {
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }

        public System.Data.Entity.DbSet<Opmas.Data.Objects.Entities.AccessManagement.Package> Packages { get; set; }
    }
}