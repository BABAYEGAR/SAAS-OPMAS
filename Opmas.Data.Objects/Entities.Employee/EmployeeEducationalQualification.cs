using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Build.Framework;

namespace Opmas.Data.Objects.Entities.Employee
{
    public class EmployeeEducationalQualification
    {
        public long EmployeeEducationalQualificationId { get; set; }
        [Required]
        [DisplayName("Institution Name")]
        public string InstitutionName { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        [DisplayName("Start Date")]
        public DateTime StartDate { get; set; }
        [Required]
        [DisplayName("End Date")]
        public DateTime EndDate { get; set; }
        [Required]
        [DisplayName("Degree Attained")]
        public string DegreeAttained { get; set; }
        [Required]
        [DisplayName("Class Of Degree")]
        public string ClassOfDegree { get; set; }
        public long FakeId { get; set; }

        public long EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }
    }
}