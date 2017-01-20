using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Opmas.Data.Objects.Entities.Employee
{
    public class EmployeePersonalData
    {
        public long EmployeePersonalDataId { get; set; }
        [Required]
        [DisplayName("Employee Title")]
        public string Title { get; set; }
        [Required]
        public string Firstname { get; set; }

        public string Middlename { get; set; }
        [Required]
        public string Lastname { get; set; }
        [DisplayName("Place Of Birth")]
        [Required]
        public string PlaceOfBirth { get; set; }
        [DisplayName("Date Of Birth")]
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy HH:mm}")]
        public DateTime DateOfBirth { get; set; }

        [DisplayName("Primary Address")]
        [Required]
        public string PrimaryAddress { get; set; }

        [DisplayName("Secondary Address")]
        public string SecondaryAddress { get; set; }
        [Required]
        public string Gender { get; set; }
        [DisplayName("State")]
        public int StateId { get; set; }

        [ForeignKey("StateId")]
        public virtual State State { get; set; }
        [DisplayName("Lga")]
        public int LgaId { get; set; }

        [ForeignKey("LgaId")]
        public virtual Lga Lga { get; set; }

        [DisplayName("Postal Code")]
        [Required]
        public string PostalCode { get; set; }

        [DisplayName("Home Phone")]
        [Required]
        public string HomePhone { get; set; }

        [DisplayName("Mobile Number")]
        public string MobilePhone { get; set; }

        [DisplayName("Work Phone")]
        [Required]
        public string WorkPhone { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [DisplayName("Marital Status")]
        [Required]
        public string MaritalStatus { get; set; }

        [DisplayName("Image")]
        public string EmployeeImage { get; set; }

        public long EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }
    }
}