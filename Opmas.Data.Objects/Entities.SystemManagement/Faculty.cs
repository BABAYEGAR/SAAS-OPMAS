using System.ComponentModel.DataAnnotations.Schema;

namespace Opmas.Data.Objects.Entities.SystemManagement
{
    public class Faculty : Transport
    {
        public long FacultyId { get; set; }
        public string Name { get; set; }
        public long UniversityId { get; set; }

        [ForeignKey("InstitutionId")]
        public virtual Institution Institution { get; set; }
    }
}