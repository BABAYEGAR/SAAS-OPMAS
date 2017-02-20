using System.Data.Entity;
using Opmas.Data.Objects.Entities.Employee;
using Opmas.Data.Objects.Entities.SystemManagement;

namespace Opmas.Data.DataContext.DataContext.SystemDataContext
{
    public class ApplicationNotificationDataContext : DbContext
    {
        public ApplicationNotificationDataContext()
            : base("name=Opmas")
        {
        }

        public virtual DbSet<ApplicationNotification> ApplicationNotifications { get; set; }
        public virtual DbSet<Institution> Institutions { get; set; }
        public virtual DbSet<Faculty> Faculties { get; set; }
        public virtual DbSet<Department> Departments { get; set; }

        public virtual DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}