using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Opmas.Data.Objects.Entities.SystemManagement
{
    public class Unit
    {
        public long UnitId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }

        public IEnumerable<Employee.Employee> Employees { get; set; }
    }
}