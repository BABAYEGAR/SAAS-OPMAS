using System.ComponentModel.DataAnnotations;

namespace Opmas.Data.Service.Enums
{
    public enum BloodGroup
    {
        [Display(Name = "O+")]
        Opositive,
        [Display(Name = "O-")]
        Onegative,
        [Display(Name = "A+")]
        Apositive,
        [Display(Name = "A-")]
        Anegative,
        [Display(Name = "B+")]
        Bpositive,
        [Display(Name = "B-")]
        Bnegative,
        [Display(Name = "AB+")]
        ABpositive,
        [Display(Name = "AB-")]
        ABnegative

    }
}
