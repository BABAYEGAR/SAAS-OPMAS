using System.Data.Entity;

namespace Opmas.Data.DataContext.DataContext.AccessDataContext
{
    public class SubscriptionDataContext : DbContext
    {
        public SubscriptionDataContext()
            : base("name=SAAS-OPMAS")
        {
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }

        public System.Data.Entity.DbSet<Opmas.Data.Objects.Entities.AccessManagement.Subscription> Subscriptions { get; set; }

        public System.Data.Entity.DbSet<Opmas.Data.Objects.Entities.AccessManagement.Package> Packages { get; set; }
    }
}