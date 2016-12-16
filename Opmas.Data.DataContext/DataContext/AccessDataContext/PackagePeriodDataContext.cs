using System.Data.Entity;

namespace Opmas.Data.DataContext.DataContext.AccessDataContext
{
    public class PackagePeriodDataContext : DbContext
    {
        public PackagePeriodDataContext()
            : base("name=Opmas")
        {
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }

        public System.Data.Entity.DbSet<Opmas.Data.Objects.Entities.AccessManagement.PackagePeriod> PackagePeriods { get; set; }
    }
}