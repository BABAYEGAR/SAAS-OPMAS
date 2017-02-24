using System;
using System.ComponentModel;

namespace Opmas.Data.Objects.Entities.SystemManagement
{
    public class ApplicationStatistic
    {
        public long ApplicationStatisticId { get; set; }
        public string Action { get; set; }
        [DisplayName("Date Occured")]
        public DateTime DateOccured { get; set; }
        [DisplayName("Institution")]
        public long? InstitutionId { get; set; }
        [DisplayName("Logged In User")]
        public long? LoggedInUserId { get; set; }
    }
}
