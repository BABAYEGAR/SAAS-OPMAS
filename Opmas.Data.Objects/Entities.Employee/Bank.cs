using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opmas.Data.Objects.Entities.Employee
{
    public class Bank
    {
        public long BankId { get; set; }
        public string Name { get; set; }
        public IEnumerable<EmployeeBankData> EmployeeBankDatas { get; set; }
    }
}
