using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Build.Framework;
using Opmas.Data.Objects.Entities.SystemManagement;

namespace Opmas.Data.Objects.LeaveManagement
{
    public class LeaveType : Transport
    {
        public long LeaveTypeId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [DisplayName("Duration Category")]
        public string DurationIn { get; set; }
        [Required]
        public int Duration { get; set; }
        public long InstitutionId { get; set; }

        [ForeignKey("InstitutionId")]
        public virtual Institution Institution { get; set; }
        public IEnumerable<Leave> Leaves { get; set; }
    }
}
