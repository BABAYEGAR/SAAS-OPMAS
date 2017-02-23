using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Opmas.Data.Objects.Entities.SystemManagement;
using Opmas.Data.Objects.Mappings;

namespace Opmas.Data.Objects.PerformanceManagement
{
    public class AppraisalFactor :  Transport
    {
        public long AppraisalFactorId { get; set; }
        public string Factor { get; set; }
        public string Description { get; set; }
        [DisplayName("Factor Score")]
        public long FactorScore { get; set; }
        public long AppraisalCategoryId { get; set; }
        [ForeignKey("AppraisalCategoryId")]
        public virtual AppraisalCategory AppraisalCategory { get; set; }
        public long InstitutionId { get; set; }
        [ForeignKey("InstitutionId")]
        public virtual Institution Institution { get; set; }
        public IEnumerable<AppraisalScoreCard> AppraisalScoreCards { get; set; }
     
    }
}
