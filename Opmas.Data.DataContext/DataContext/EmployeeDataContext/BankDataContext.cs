using System.Data.Entity;
using Opmas.Data.Objects.Entities.AccessManagement;
using Opmas.Data.Objects.Entities.Employee;
using Opmas.Data.Objects.Entities.SystemManagement;

namespace Opmas.Data.DataContext.DataContext.EmployeeDataContext
{
    public class BankDataContext : DbContext
    {
        public BankDataContext()
            : base("name=Opmas")
        {
        }

        public virtual DbSet<Bank> Banks { get; set; }
        public virtual DbSet<Institution> Institutions { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}