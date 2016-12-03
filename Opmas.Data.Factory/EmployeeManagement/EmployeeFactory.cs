﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Opmas.Data.DataContext.DataContext.EmployeeDataContext;
using Opmas.Data.Objects.Entities.Employee;

namespace Opmas.Data.Factory.EmployeeManagement
{
    public class EmployeeFactory
    {
        private readonly EmployeeDataContext _employee = new EmployeeDataContext();
        /// <summary>
        /// This method rerieves an employees personal data
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public EmployeePersonalData GetEmployeePersonalData(long employeeId)
        {
            var employeePersonalData =
                _employee.EmployeePersonalDatas.SingleOrDefault(n => n.EmployeeId == employeeId);
            return employeePersonalData;
        }
        /// <summary>
        /// This method retrieves an employees medical data
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public EmployeeMedicalData GetEmployeeMedicalData(long employeeId)
        {
            var employeeMedicalData =
                _employee.EmployeeMedicalDatas.SingleOrDefault(n => n.EmployeeId == employeeId);
            return employeeMedicalData;
        }
        /// <summary>
        /// This method retrieves an employee work data
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public EmployeeWorkData GetEmployeeWorkData(long employeeId)
        {
            var employeeWorkData =
                _employee.EmployeeWorkDatas.SingleOrDefault(n => n.EmployeeId == employeeId);
            return employeeWorkData;
        }
        /// <summary>
        /// This method retrives all the list of employee bank data and appends to a list
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public IEnumerable<EmployeeBankData> GetEmployeeBankData(long employeeId)
        {
            var employeeBankData =
                _employee.EmployeeBankDatas.Where(n => n.EmployeeId == employeeId);
            return employeeBankData.ToList();
        }
        /// <summary>
        /// This method retrieves all the employees past work experience and appends to a list
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public IEnumerable<EmployeePastWorkExperience> GetEmployeePastWorkData(long employeeId)
        {
            var employeePastWork =
                _employee.EmployeePastWorkExperiences.Where(n => n.EmployeeId == employeeId);
            return employeePastWork.ToList();
        }
        /// <summary>
        /// This method retrieves all the employees educational qualification and appends to a list
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public IEnumerable<EmployeeEducationalQualification> GetEmployeeEducationalData(long employeeId)
        {
            var employeeEducation =
                _employee.EmployeeEducationalQualifications.Where(n => n.EmployeeId == employeeId);
            return employeeEducation;
        }
    }
}
