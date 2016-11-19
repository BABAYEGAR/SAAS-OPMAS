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
    }
}