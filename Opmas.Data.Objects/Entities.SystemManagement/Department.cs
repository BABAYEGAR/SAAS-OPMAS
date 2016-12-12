using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Opmas.Data.Objects.Entities.SystemManagement
{
    public class Department
    {
        public long DepartmentId { get; set; }
        [DisplayName("Department Name")]
        public string Name { get; set; }
        [DisplayName("Faculty Name")]
        public long FacultyId { get; set; }

        [ForeignKey("FacultyId")]
        public virtual Faculty Faculty { get; set; }
        [DisplayName("Institution Name")]

        public long InstitutionId { get; set; }

        [ForeignKey("InstitutionId")]
        public virtual Institution Institution { get; set; }
    }
}