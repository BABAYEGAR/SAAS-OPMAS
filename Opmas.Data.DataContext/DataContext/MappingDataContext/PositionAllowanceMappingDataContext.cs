using System.Data.Entity;
using Opmas.Data.Objects.Entities.Employee;
using Opmas.Data.Objects.Entities.SystemManagement;
using Opmas.Data.Objects.Mappings;
using Opmas.Data.Objects.Payment;
using Opmas.Data.Objects.Training;

namespace Opmas.Data.DataContext.DataContext.MappingDataContext
{
    public class PositionAllowanceMappingDataContext : DbContext
    {
        public PositionAllowanceMappingDataContext()
            : base("name=Opmas")
        {
        }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<PositionAllowanceMapping> PositionAllowanceMappings { get; set; }
        public virtual DbSet<PaymentAllowance> PaymentAllowances { get; set; }
        public virtual DbSet<EmploymentPosition> EmploymentPosition { get; set; }
        public virtual DbSet<Institution> Institutions { get; set; }
        public virtual DbSet<EmployeeTrainingMapping> EmployeeTrainingMappings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}