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
        public string BankName { get; set; }
        [Required]
        [DisplayName("Account Name")]
        public string AccountName { get; set; }
        [Required]
        [DisplayName("Account Number")]
        public string AccountNumber { get; set; }
        [Required]
        [DisplayName("Account Type")]
        public string AccountType { get; set; }
        public long FakeId { get; set; }

        public long EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }
    }
}