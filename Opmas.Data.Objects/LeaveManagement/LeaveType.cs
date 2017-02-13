using System.ComponentModel.DataAnnotations.Schema;
using Opmas.Data.Objects.Entities.SystemManagement;

namespace Opmas.Data.Objects.LeaveManagement
{
    public class LeaveType : Transport
    {
        public long LeaveTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long InstitutionId { get; set; }

        [ForeignKey("InstitutionId")]
        public virtual Institution Institution { get; set; }
    }
}
