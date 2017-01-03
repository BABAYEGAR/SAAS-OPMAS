using System.Data.Entity;
using Opmas.Data.Objects.Entities.SystemManagement;

namespace Opmas.Data.DataContext.DataContext.AccessDataContext
{
    public class PackageDataContext : DbContext
    {
        public PackageDataContext()
            : base("name=Opmas")
        {
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }

        public System.Data.Entity.DbSet<Opmas.Data.Objects.Entities.AccessManagement.Package> Packages { get; set; }
        public virtual DbSet<Institution> Institutions { get; set; }
    }
}