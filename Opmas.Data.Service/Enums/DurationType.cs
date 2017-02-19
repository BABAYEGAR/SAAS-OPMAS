using System.ComponentModel.DataAnnotations;

namespace Opmas.Data.Service.Enums
{
    public enum DurationType
    {
        [Display(Name = "Day(s)")]
        Day,
        [Display(Name = "Week(s)")]
        Week,
        [Display(Name = "Year(s)")]
        Year

    }
}
