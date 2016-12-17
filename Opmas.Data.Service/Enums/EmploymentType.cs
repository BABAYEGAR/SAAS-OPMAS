using System.ComponentModel.DataAnnotations;

namespace Opmas.Data.Service.Enums
{
    public enum EmploymentType
    {
        [Display(Name = "Full-Time")]
        FullTime,
        [Display(Name = "Part-Time")]
        PartTime
    }
}
