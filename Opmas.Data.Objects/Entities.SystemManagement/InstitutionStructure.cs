using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Opmas.Data.Objects.Entities.SystemManagement
{
    public class InstitutionStructure : Transport
    {
        public long InstitutionStructureId { get; set; }
        [Display(Name = "Does your institution have faculties as part of its structure?")]
        public bool Faculty { get; set; }
        public long InstitutionId { get; set; }

        [ForeignKey("InstitutionId")]
        public virtual Institution Institution { get; set; }
    }
}
