using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Opmas.Data.Objects.Entities.Employee
{
    public class EmployeePastWorkExperience
    {
        public long EmployeePastWorkExperienceId { get; set; }

        [DisplayName("Employer Name")]
        [ Required]
        public string EmployerName { get; set; }
        [ Required]
        [DisplayName("Employer Location")]
        public string EmployerLocation { get; set; }
        [DisplayName("Employer Contact Number")]
        [ Required]
        public string EmployerContact { get; set; }

        [DisplayName("Start Date")]
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy HH:mm}")]
        public DateTime StartDate { get; set; }

        [DisplayName("End Date")]
        [ Required]
        public DateTime EndDate { get; set; }

        [DisplayName("Reason for leaving")]
        [ Required]
        public string ReasonForLeaving { get; set; }

        [DisplayName("Position Held")]
        [ Required]
        public string PositionHeld { get; set; }
        public long FakeId { get; set; }

        public long EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }
    }
}