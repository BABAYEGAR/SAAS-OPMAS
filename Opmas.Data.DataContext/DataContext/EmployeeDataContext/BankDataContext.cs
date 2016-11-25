using System.Data.Entity;
using Opmas.Data.Objects.Entities.Employee;

namespace Opmas.Data.DataContext.DataContext.EmployeeDataContext
{
    public class BankDataContext : DbContext
    {
        public BankDataContext()
            : base("name=SAAS-OPMAS")
        {
        }

        public virtual DbSet<Bank> Banks { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}