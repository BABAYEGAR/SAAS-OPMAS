using System.ComponentModel.DataAnnotations;

namespace Opmas.Data.Service.Enums
{
    public enum UserType
    {
        [Display(Name = "Vice Chancellor")]
        ViceChancellor,
        Registrar,
        Dean,
        HOD,
        Employee
    }
}