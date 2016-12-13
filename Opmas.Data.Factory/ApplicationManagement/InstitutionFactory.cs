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
        /// <summary>
        /// This method retrieves the list of all institutions
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Institution> GetListOfInstitutions()
        {
            var allInstitutions = _db.Institutions.ToList();
            var institutions = allInstitutions;
            return institutions;
        }

    }
}
