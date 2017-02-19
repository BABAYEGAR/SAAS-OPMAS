using System.ComponentModel.DataAnnotations;

namespace Opmas.Data.Service.Enums
{
    public enum LeaveStatus
    {
        Pending,
        Approved,
        [Display(Name = "  Approved By Department")]
        ApprovedByDepartment,
        Rejected,
        Cancelled
    }
}