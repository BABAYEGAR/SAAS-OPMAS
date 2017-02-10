using System.Data.Entity;
using Opmas.Data.Objects.Entities.Employee;
using Opmas.Data.Objects.Entities.SystemManagement;
using Opmas.Data.Objects.Mappings;
using Opmas.Data.Objects.Payment;

namespace Opmas.Data.DataContext.DataContext.PaymentDataContext
{
    public class PaymentDeductionDataContext : DbContext
    {
        public PaymentDeductionDataContext()
            : base("name=Opmas")
        {
        }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<PositionDeductionMapping> PositionDeductionMappings { get; set; }
        public virtual DbSet<PaymentDeduction> PaymentDeduction { get; set; }
        public virtual DbSet<EmploymentPosition> EmploymentPosition { get; set; }
        public virtual DbSet<Institution> Institutions { get; set; }
        public virtual DbSet<EmployeeTrainingMapping> EmployeeTrainingMappings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}