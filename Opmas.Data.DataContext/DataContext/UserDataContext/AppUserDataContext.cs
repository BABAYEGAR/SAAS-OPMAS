using System.Data.Entity;
using Opmas.Data.Objects.Entities.Employee;
using Opmas.Data.Objects.Entities.User;

namespace Opmas.Data.DataContext.DataContext.UserDataContext
{
    public class AppUserDataContext : DbContext
    {
        public AppUserDataContext()
            : base("name=SAAS-OPMAS")
        {
        }


        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<AppUser> AppUsers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}