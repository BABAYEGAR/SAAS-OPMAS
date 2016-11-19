using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Opmas.Data.Objects.Entities.AccessManagement;

namespace Opmas.Data.Objects.Entities.SystemManagement
{
    public class University
    {
        public long UniversityId { get; set; }
        public string Name { get; set; }
        public string Motto { get; set; }
        public string Logo { get; set; }
        public string Location { get; set; }
        public string ContactNumber { get; set; }
        public string ContactEmail { get; set; }
        public IEnumerable<Faculty> Faculties { get; set; }
        public IEnumerable<Department> Departments { get; set; }
        public long SubscriptionId { get; set; }

        [ForeignKey("SubscriptionId")]
        public virtual Subscription Subscription { get; set; }
    }
}