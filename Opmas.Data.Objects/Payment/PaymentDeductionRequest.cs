using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Build.Framework;
using Opmas.Data.Objects.Entities.Employee;
using Opmas.Data.Objects.Entities.SystemManagement;

namespace Opmas.Data.Objects.Payment
{
    public class PaymentDeductionRequest : Transport
    {
        public long PaymentDeductionRequestId { get; set; }
        [DisplayName("Request Title")]
        [Required]
        public string RequestTitle { get; set; }
        [Required]
        public string Reason { get; set; }
        public string Status { get; set; }
        [Required]
        public long Amount { get; set; }
        public string Comment { get; set; }
        public long? EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }
        public long InstitutionId { get; set; }

        [ForeignKey("InstitutionId")]
        public virtual Institution Institution { get; set; }

    }
}
