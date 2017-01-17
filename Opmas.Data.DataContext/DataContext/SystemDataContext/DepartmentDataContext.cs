using System.Data.Entity;
using Opmas.Data.Objects.Entities.Employee;
using Opmas.Data.Objects.Entities.SystemManagement;
using Opmas.Data.Objects.Entities.User;

namespace Opmas.Data.DataContext.DataContext.SystemDataContext
{
    public class DepartmentDataContext : DbContext
    {
        public DepartmentDataContext()
            : base("name=Opmas")
        {
        }

        public virtual DbSet<Institution> Universities { get; set; }
        public virtual DbSet<Faculty> Faculties { get; set; }
        public virtual DbSet<Department> Departments { get; set; }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<AppUser> AppUsers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}