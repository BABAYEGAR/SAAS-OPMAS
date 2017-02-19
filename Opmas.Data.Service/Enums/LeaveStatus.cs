using System.ComponentModel.DataAnnotations;

namespace Opmas.Data.Service.Enums
{
    public enum LeaveStatus
    {
        Approved,
        [Display(Name = "  Approved By Department")]
        ApprovedByDepartment,
        Rejected,
        Cancelled
    }
}