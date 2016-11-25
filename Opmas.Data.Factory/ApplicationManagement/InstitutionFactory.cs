using Opmas.Data.DataContext.DataContext.EmployeeDataContext;
using Opmas.Data.DataContext.DataContext.SystemDataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Opmas.Data.Objects.Entities.SystemManagement;

namespace Opmas.Data.Factory.ApplicationManagement
{
    public class InstitutionFactory
    {
        private readonly InstitutionDataContext _db = new InstitutionDataContext();

        public IEnumerable<Institution> GetListOfInstitutionsByCategory(string category)
        {
            var allInstitutions = _db.Institutions.ToList();
            var institutions = allInstitutions.Where(n => n.InstitutionCategory == category);
            return institutions;
        }
    }
}
