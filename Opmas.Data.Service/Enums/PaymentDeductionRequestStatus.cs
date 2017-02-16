using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opmas.Data.Service.Enums
{
    public enum PaymentDeductionRequestStatus
    {
        [Display(Name = "Request Granted")]
        Granted,
        [Display(Name = "Request Denied")]
        Denied,
        Pending,
        [Display(Name = "Canceled")]
        Cancel
    }
}
