using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Opmas.Data.Objects.Entities.SystemManagement
{
    public class Faculty : Transport
    {

        public long FacultyId { get; set; }
        [DisplayName("Faculty Name")]
        public string Name { get; set; }
        [DisplayName("Institution Name")]
        public long InstitutionId { get; set; }

        [ForeignKey("InstitutionId")]
        public virtual Institution Institution { get; set; }
    }
}