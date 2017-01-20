using System;
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
        [Required]
        public string EmploymentType { get; set; }
        [Required]
        public string EmploymentCategory { get; set; }
        [Required]
        public string EmploymentPosition { get; set; }
        [Required]
        public DateTime? EmploymentDate { get; set; }
        public long EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }
        [Required]
        public long? RoleId { get; set; }
        [Required]
        public long? DepartmentId { get; set; }
        [Required]
        public long? FacultyId { get; set; }
        [Required]
        public long? UnitId { get; set; }


    }
}