using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Opmas.Data.Objects.Entities.Employee;
using Opmas.Data.Objects.Entities.SystemManagement;

namespace Opmas.Data.Objects.Payment
{
    public class PaymentAllowance : Transport
    {
        public long PaymentAllowanceId { get; set; }
        public string Name { get; set; }
        public long Amount { get; set; }
        public long InstitutionId { get; set; }

        [ForeignKey("InstitutionId")]
        public virtual Institution Institution { get; set; }
        public  IEnumerable<PositionAllowanceMapping> PositionAllowanceMappings { get; set; }
    }
}
