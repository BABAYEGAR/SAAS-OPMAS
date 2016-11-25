using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Opmas.Data.DataContext.DataContext.EmployeeDataContext;
using Opmas.Data.Objects.Entities.Employee;

namespace Opmas.Data.Factory.EmployeeManagement
{
    public class BankFactory
    {
        private readonly BankDataContext _db = new BankDataContext();
        /// <summary>
        /// This method retireves a single bank from the databse using the bank Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Bank GetBankById(long Id)
        {
            Bank bank = null;
             bank = _db.Banks.Find(Id);
                return bank;
        }
    }
}
