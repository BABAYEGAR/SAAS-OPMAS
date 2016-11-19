using System.Collections.Generic;
using System.Linq;
using Opmas.Data.DataContext.DataContext.EmployeeDataContext;
using Opmas.Data.Objects.Entities.Employee;

namespace Opmas.Data.Factory.EmployeeManagement
{
    public class StateFactory
    {
        private readonly LgaDataContext _db = new LgaDataContext();
        public IEnumerable<Lga> GetLgaForState(long stateId)
        {
            return _db.Lgas.Where(n => n.StateId == stateId);
        }
    }
}
