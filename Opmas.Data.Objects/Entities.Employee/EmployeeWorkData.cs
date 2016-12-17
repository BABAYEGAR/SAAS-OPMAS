using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Opmas.Data.Objects.Entities.Employee
{
    public class EmployeeWorkData
    {
        public long EmployeeWorkDataId { get; set; }
        public string EmploymentType { get; set; }
        public DateTime EmploymentDate { get; set; }
        public string EmploymentStatus { get; set; }
        public string PositionHeld { get; set; }
        public string Category { get; set; }
        public long EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }
    }
}