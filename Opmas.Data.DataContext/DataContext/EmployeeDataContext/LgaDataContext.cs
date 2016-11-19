using System.Data.Entity;
using Opmas.Data.Objects.Entities.Employee;

namespace Opmas.Data.DataContext.DataContext.EmployeeDataContext
{
    public class LgaDataContext : DbContext
    {
        public LgaDataContext()
            : base("name=SAAS-OPMAS")
        {
        }

        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<Lga> Lgas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}