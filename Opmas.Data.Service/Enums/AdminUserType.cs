using System.ComponentModel.DataAnnotations;

namespace Opmas.Data.Service.Enums
{
    public enum AdminUserType
    {
        [Display(Name = "System Administrator")]
        SystemAdministrator,
        [Display(Name = "Institution Administrator")]
        InstitutionAdministrator
    }
}