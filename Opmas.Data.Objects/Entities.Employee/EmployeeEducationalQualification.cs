using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Opmas.Data.Objects.Entities.SystemManagement;

namespace Opmas.Data.Objects.Entities.Employee
{
    public class EmployeeEducationalQualification
    {
        public long EmployeeEducationalQualificationId { get; set; }
        [ Required]
        [DisplayName("Institution Name")]
        public string InstitutionName { get; set; }
        [ Required]
        public string Location { get; set; }
        [ Required]
        [DisplayName("Start Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy HH:mm}")]
        public DateTime StartDate { get; set; }
        [ Required]
        [DisplayName("End Date")]

        public DateTime EndDate { get; set; }
        [DisplayName("Degree Attained")]
        public string DegreeAttained { get; set; }
        [DisplayName("Class Of Degree")]
        [DefaultValue("None")]
        public string ClassOfDegree { get; set; }
        [DisplayName("File Upload")]
        public string FileUpload { get; set; }
        public long FakeId { get; set; }

        public long? InstitutionQualificationId { get; set; }

        [ForeignKey("InstitutionQualificationId")]
        public virtual InstitutionQualification InstitutionQualification { get; set; }
        public long EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }
    }
}