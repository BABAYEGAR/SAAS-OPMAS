using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Opmas.Data.Objects.Entities.SystemManagement;
using Opmas.Data.Objects.Mappings;

namespace Opmas.Data.Objects.Entities.Employee
{
    public class Responsibility : Transport
    {
        public long ResponsibilityId { get; set; }
        public string Name { get; set; }
        [DisplayName("Institution")]
        public long InstitutionId { get; set; }

        [ForeignKey("InstitutionId")]
        public virtual Institution Institution { get; set; }
        public IEnumerable<EmployeeResponsibilityMapping> ResponsibilityMappings { get; set; }
    }
}
