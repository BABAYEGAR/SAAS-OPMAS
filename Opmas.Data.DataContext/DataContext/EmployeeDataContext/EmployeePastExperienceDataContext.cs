using System.Data.Entity;
using Opmas.Data.Objects.Entities.Employee;

namespace Opmas.Data.DataContext.DataContext.EmployeeDataContext
{
    public class EmployeePastExperienceDataContext : DbContext
    {
        public EmployeePastExperienceDataContext()
            : base("name=SAAS-OPMAS")
        {
        }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeePastWorkExperience> EmployeePastWorkExperiences { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}