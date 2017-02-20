using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opmas.Data.Objects.Entities.SystemManagement
{
    public class ApplicationNotification
    {
        public long ApplicationNotificationId { get; set; } 
        public long? ItemId { get; set; }
        public string Description { get; set; }
        public string NotificationType { get; set; }
        public bool Read { get; set; }
        public DateTime DateCreated { get; set; }
        public long? CreatedBy { get; set; }
        public long? AssignedTo { get; set; }
        public long? InstitutionId { get; set; }
        [ForeignKey("InstitutionId")]
        public Institution Institution { get; set; }
    }
}
