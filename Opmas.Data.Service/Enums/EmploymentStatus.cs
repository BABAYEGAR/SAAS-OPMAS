using System.ComponentModel.DataAnnotations;

namespace Opmas.Data.Service.Enums
{
    public enum EmploymentStatus
    {
        Active,
        Retired,
        Suspended,
        [Display(Name = "On Leave")]
        OnLeave,
    }
}
