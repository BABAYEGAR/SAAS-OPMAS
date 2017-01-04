using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opmas.Data.Service.Enums
{
    public enum SubscriptionDuration
    {
        [Display(Name = "One Month")]
        OneMonth,
        [Display(Name = "Six Months")]
        SixMonths,
        [Display(Name = "One Year")]
        OneYear,
        [Display(Name = "Two Years")]
        TwoYears,
        [Display(Name = "Three Years")]
        ThreeYears
    }
}
