using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Opmas.Data.Objects.Entities.Employee
{
    public class Lga
    {
        [Key]
        public int LgaId { get; set; }
        public string Name { get; set; }
        public int StateId { get; set; }

        [ForeignKey("StateId")]
        public virtual State State { get; set; }
    }
}