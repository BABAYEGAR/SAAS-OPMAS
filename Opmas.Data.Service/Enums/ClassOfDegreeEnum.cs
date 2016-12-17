using System.ComponentModel.DataAnnotations;

namespace Opmas.Data.Service.Enums
{
    public enum  ClassOfDegreeEnum
    {
        [Display(Name = "First Class")]
        FirstClass,
        [Display(Name = "Second Class Upper")]
        SecondClassUpper,
        [Display(Name = "Second Class Lower")]
        SecondClassLower,
        [Display(Name = "Third Class")]
        ThirdClass
    }
}
