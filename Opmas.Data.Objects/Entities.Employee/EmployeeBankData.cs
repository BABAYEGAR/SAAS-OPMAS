using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Opmas.Data.Objects.Entities.Employee
{
    public class EmployeeBankData
    {
        public long EmployeeBankDataId { get; set; }
        [Required]
        [DisplayName("Bank Name")]
        public long BankId { get; set; }
        [ForeignKey("BankId")]
        public virtual Bank Bank { get; set; }
        [Required]
        [DisplayName("Firstname")]
        public string AccountFirstName { get; set; }
        [DisplayName("Middlename")]
        public string AccountMiddleName { get; set; }
        [Required]
        [DisplayName("Lastname")]
        public string AccountLastName { get; set; }
        [Required]
        [DisplayName("Account Number")]
        [StringLength(11)]
        public string AccountNumber { get; set; }
        [Required]
        [DisplayName("Account Type")]
        public string AccountType { get; set; }
        public long FakeId { get; set; }
        public string DisplayName
      => AccountFirstName + " " + AccountLastName;
        public long EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }
    }
}