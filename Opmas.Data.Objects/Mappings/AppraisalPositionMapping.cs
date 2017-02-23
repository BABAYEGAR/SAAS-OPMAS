using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Opmas.Data.Objects.Entities.Employee;
using Opmas.Data.Objects.Entities.SystemManagement;
using Opmas.Data.Objects.PerformanceManagement;

namespace Opmas.Data.Objects.Mappings
{
    public class AppraisalPositionMapping : Transport
    {
        public long AppraisalPositionMappingId { get; set; }
        public long AppraisalCategoryId { get; set; }
        [ForeignKey("AppraisalCategoryId")]
        public AppraisalCategory AppraisalCategory { get; set; }
        public long EmploymentPositionId { get; set; }
        [ForeignKey("EmploymentPositionId")]
        public EmploymentPosition EmploymentPosition { get; set; }
        [DisplayName("Institution")]
        public long InstitutionId { get; set; }
        [ForeignKey("InstitutionId")]
        public virtual Institution Institution { get; set; }
    }
}
