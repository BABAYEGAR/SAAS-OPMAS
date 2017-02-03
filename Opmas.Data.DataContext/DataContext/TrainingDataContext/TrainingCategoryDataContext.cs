using System.Data.Entity;
using Opmas.Data.Objects.Entities.Employee;
using Opmas.Data.Objects.Entities.SystemManagement;
using Opmas.Data.Objects.Training;

namespace Opmas.Data.DataContext.DataContext.TrainingDataContext
{
    public class TrainingCategoryDataContext : DbContext
    {
        public TrainingCategoryDataContext()
            : base("name=Opmas")
        {
        }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeTraining> EmployeeTrainings { get; set; }
        public virtual DbSet<Institution> Institutions { get; set; }
        public virtual DbSet<TrainingCategory> TrainingCategory { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}