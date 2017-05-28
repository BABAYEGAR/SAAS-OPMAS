using System.Data.Entity;
using Opmas.Data.Objects.Entities.Employee;
using Opmas.Data.Objects.Entities.SystemManagement;

namespace Opmas.Data.DataContext.DataContext.SystemDataContext
{
    public class InstitutionQualificationDataContext : DbContext
    {
        public InstitutionQualificationDataContext()
            : base("name=Opmas")
        {
        }

        public virtual DbSet<Institution> Institutions { get; set; }
        public virtual DbSet<InstitutionQualification> InstitutionQualifications { get; set; }

        public virtual DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}