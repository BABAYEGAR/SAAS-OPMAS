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
    }
}