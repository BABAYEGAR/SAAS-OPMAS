using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Opmas.Data.Objects.Entities.SystemManagement;
using Opmas.Data.Objects.Mappings;

namespace Opmas.Data.Objects.PerformanceManagement
{
    public class AppraisalCategory :  Transport
    {
        public long AppraisalCategoryId { get; set; }
        [DisplayName("Category Name")]
        public string Name { get; set; }
        public string Description { get; set; }
        [DisplayName("Maximum Score")]
        public long? MaximumScore { get; set; }
        [DisplayName("Current Score")]
        public long? CurrentScore { get; set; }
        [DisplayName("Institution")]
        public long InstitutionId { get; set; }
        [ForeignKey("InstitutionId")]
        public virtual Institution Institution { get; set; }
        public IEnumerable<AppraisalFactor> AppraisalFactors { get; set; }
        public IEnumerable<AppraisalPositionMapping> AppraisalPositionMappings { get; set; }
    }
}
