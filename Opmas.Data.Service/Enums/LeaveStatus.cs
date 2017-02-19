using System.ComponentModel.DataAnnotations;

namespace Opmas.Data.Service.Enums
{
    public enum LeaveStatus
    {
        Pending,
        Approved,
        [Display(Name = "  Approved By Department")]
        ApprovedByDepartment,
        [Display(Name = "  Approved By Faculty")]
        ApprovedByFaculty,
        Rejected,
        Cancelled
    }
}