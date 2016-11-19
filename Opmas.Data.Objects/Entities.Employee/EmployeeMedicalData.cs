using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Opmas.Data.Objects.Entities.Employee
{
    public class EmployeeMedicalData
    {
        public long EmployeeMedicalDataId { get; set; }
        [Required]
        public string Genotype { get; set; }

        [DisplayName("Blood Group")]
        [Required]
        public string BloodGroup { get; set; }

        public long EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }
    }
}