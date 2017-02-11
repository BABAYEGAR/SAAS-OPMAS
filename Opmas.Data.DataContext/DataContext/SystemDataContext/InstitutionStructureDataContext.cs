using System.Data.Entity;
using Opmas.Data.Objects.Entities.AccessManagement;
using Opmas.Data.Objects.Entities.Employee;
using Opmas.Data.Objects.Entities.SystemManagement;

namespace Opmas.Data.DataContext.DataContext.SystemDataContext
{
    public class InstitutionStructureDataContext : DbContext
    {
        public InstitutionStructureDataContext()
            : base("name=Opmas")
        {
        }

        public virtual DbSet<Institution> Institutions { get; set; }
        public virtual DbSet<InstitutionStructure> InstitutionStructures { get; set; }
        public virtual DbSet<Faculty> Faculties { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Package> Packages { get; set; }

        public virtual DbSet<Employee> Employees { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }

        
    }
}