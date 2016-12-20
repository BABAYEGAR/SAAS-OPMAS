using System.ComponentModel.DataAnnotations;

namespace Opmas.Data.Service.Enums
{
    public enum  DegreeTypeEnum
    {
        [Display(Name = "Basic Education")]
        Basic,
        [Display(Name = "Junior High School")]
        JSCE,
        [Display(Name = "Senior High School")]
        SSCE,
        [Display(Name = "Bachelors")]
        BSc,
        [Display(Name = "Masters")]
        MSc,
        [Display(Name = "Doctorate")]
        Phd,

    }
}
