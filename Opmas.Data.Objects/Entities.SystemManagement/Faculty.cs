using System.ComponentModel.DataAnnotations.Schema;

namespace Opmas.Data.Objects.Entities.SystemManagement
{
    public class Faculty : Transport
    {
        public long FacultyId { get; set; }
        public string Name { get; set; }
        public long UniversityId { get; set; }

        [ForeignKey("UniversityId")]
        public virtual University University { get; set; }
    }
}