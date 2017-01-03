using System.Data.Entity;
using Opmas.Data.Objects.Entities.AccessManagement;
using Opmas.Data.Objects.Entities.Employee;
using Opmas.Data.Objects.Entities.SystemManagement;

namespace Opmas.Data.DataContext.DataContext.EmployeeDataContext
{
    public class LgaDataContext : DbContext
    {
        public LgaDataContext()
            : base("name=Opmas")
        {
        }

        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<Lga> Lgas { get; set; }
        public virtual DbSet<Institution> Institutions { get; set; }
        public virtual DbSet<Package> Packages { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Faculty> Faculties { get; set; }
        public virtual DbSet<Unit> Units { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}