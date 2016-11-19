using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Opmas.Data.Objects.Entities.Employee
{
    public class State
    {
        [Key]
        public int StateId { get; set; }
        public string Name { get; set; }
        public IEnumerable<Lga> Lgas { get; set; }
    }
}