using System.ComponentModel.DataAnnotations.Schema;

namespace Opmas.Data.Objects.Entities.SystemManagement
{
    public class Department
    {
        public long DepartmentId { get; set; }
        public string Name { get; set; }
        public long FacultyId { get; set; }

        [ForeignKey("FacultyId")]
        public virtual Faculty Faculty { get; set; }

        public long UniversityId { get; set; }

        [ForeignKey("UniversityId")]
        public virtual University University { get; set; }
    }
}