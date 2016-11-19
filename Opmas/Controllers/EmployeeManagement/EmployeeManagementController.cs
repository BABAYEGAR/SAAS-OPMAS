using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Opmas.Data.DataContext.DataContext.EmployeeDataContext;
using Opmas.Data.Factory.EmployeeManagement;
using Opmas.Data.Objects.Entities.Employee;
using Opmas.Data.Service.Enums;

namespace Opmas.Controllers.EmployeeManagement
{
    public class EmployeeManagementController : Controller
    {
        private readonly StateDataContext _db = new StateDataContext();
        private readonly EmployeeBankDataContext _dbc = new EmployeeBankDataContext();
        private readonly EmployeeEducationalQualificationDataContext _dbd = new EmployeeEducationalQualificationDataContext();
        private readonly EmployeePersonalDataContext _dbe = new EmployeePersonalDataContext();
        private readonly EmployeePastExperienceDataContext _dbf = new EmployeePastExperienceDataContext();
        private readonly EmployeeWorkDataContext _dbg = new EmployeeWorkDataContext();
        private readonly EmployeeMedicalDataContext _dbh = new EmployeeMedicalDataContext();
        private readonly EmployeeDataContext _dbi = new EmployeeDataContext();
        private Employee employee = new Employee();
        /// <summary>
        /// Sends Json responds object to view with lga of the state requested via an Ajax call
        /// </summary>
        /// <param name="id"> state id</param>
        /// <returns>lgas</returns>Microsoft.CodeDom.Providers.DotNetCompilerPlatform
        public JsonResult GetLgaForState(int id)
        {
            var lgas = new StateFactory().GetLgaForState(id);
            return Json(lgas, JsonRequestBehavior.AllowGet);
        }

        // GET: EmployeeManagement/PersonalData
        public ActionResult PersonalData()
        {
            ViewBag.State = new SelectList(_db.States,"StateId", "Name");
            return View();
        }
        // Post: EmployeeManagement/PersonalData
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PersonalData(EmployeePersonalData personalData, FormCollection collectedValues)
        {
            //collect data from form using form collection
            personalData.Firstname = collectedValues["Firstname"];
            personalData.Lastname = collectedValues["Lastname"];
            personalData.Middlename = collectedValues["Middlename"];
            personalData.Gender = collectedValues["Gender"];
            personalData.Email = collectedValues["Email"];
            personalData.PrimaryAddress = collectedValues["PrimaryAddress"];
            personalData.SecondaryAddress = collectedValues["SecondaryAddress"];
            personalData.DateOfBirth = Convert.ToDateTime(collectedValues["DateOfBirth"]);
            personalData.PlaceOfBirth = collectedValues["PlaceOfBirth"];
            personalData.HomePhone = collectedValues["HomePhone"];
            personalData.WorkPhone = collectedValues["WorkPhone"];
            personalData.MobilePhone = collectedValues["MobilePhone"];
            personalData.MaritalStatus = collectedValues["MaritalStatus"];
            personalData.PostalCode = collectedValues["PostalCode"];
            personalData.LgaId = Convert.ToInt32(collectedValues["LgaId"]);
            personalData.Gender = collectedValues["Gender"];
            personalData.StateId = Convert.ToInt32(collectedValues["StateId"]);

            //store data in a session
            //Session["EmployeePersonalData"] = personalData;
            employee.EmployeePersonalData = new List<EmployeePersonalData>();
            employee.EmployeePersonalData.Add(personalData);
            Session["Employee"] = employee;

            //return next view
            return View("EducationalQualification");
        }
        // GET: EmployeeManagement/EducationalQualification
        public ActionResult EducationalQualification()
        {
            return View();
        }
        // POST: EmployeeManagement/EducationalQualification
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EducationalQualification(FormCollection collectedValues)
        {
            employee = Session["Employee"] as Employee;
            //collect data from form using form collection
            if (employee != null)
            {
                employee.EmployeeEducationalQualifications = new List<EmployeeEducationalQualification>();
                employee.EmployeeEducationalQualifications.Add(new EmployeeEducationalQualification()
                {
                    ClassOfDegree = typeof(ClassOfDegreeEnum).GetEnumName(int.Parse(collectedValues["ClassOfDegree"])),
                    DegreeAttained = typeof(DegreeTypeEnum).GetEnumName(int.Parse(collectedValues["DegreeAttained"])),
                    InstitutionName = collectedValues["InstitutionName"],
                    Location = collectedValues["Location"],
                    StartDate = Convert.ToDateTime(collectedValues["StartDate"]),
                    EndDate = Convert.ToDateTime(collectedValues["EndDate"]),
                    FakeId = employee.EmployeeEducationalQualifications.Count + 1
                });
                //store data in a session
                Session["Employee"] = employee;
            }
            return View("EducationalQualification");
        }
        // GET: EmployeeManagement/PastWorkExperience
        public ActionResult PastWorkExperience()
        {
            return View();
        }
        // POST: EmployeeManagement/PastWorkExperience
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PastWorkExperience(FormCollection collectedValues)
        {
            employee = Session["Employee"] as Employee;
            if (employee != null)
            {
                employee.EmployeePastWorkExperiences = new List<EmployeePastWorkExperience>();
                employee.EmployeePastWorkExperiences.Add(new EmployeePastWorkExperience()
                {
                    EmployerName = collectedValues["EmployerName"],
                    EmployerLocation = collectedValues["EmployerLocation"],
                    EmployerContact = collectedValues["EmployerContact"],
                    PositionHeld = collectedValues["PositionHeld"],
                    ReasonForLeaving = collectedValues["ReasonForLeaving"],
                    StartDate = Convert.ToDateTime(collectedValues["StartDate"]),
                    EndDate = Convert.ToDateTime(collectedValues["EndDate"]),
                    FakeId = employee.EmployeePastWorkExperiences.Count + 1

                });
                //store data in a session
                Session["Employee"] = employee;
            }
            return View("PastWorkExperience");
        }
        // GET: EmployeeManagement/BankData
        public ActionResult BankData()
        {
            return View();
        }
        // POST: EmployeeManagement/BankData
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BankData(FormCollection collectedValues)
        {
            employee = Session["Employee"] as Employee;
            //append data to list
            if (employee != null)
            {
                employee.EmployeeBankDatas = new List<EmployeeBankData>();
                employee.EmployeeBankDatas.Add(new EmployeeBankData()
                {
                    BankName = typeof(BanksEnum).GetEnumName(int.Parse(collectedValues["BankName"])),
                    AccountNumber = collectedValues["AccountNumber"],
                    AccountName = collectedValues["AccountName"],
                    AccountType = typeof(AccountTypeEnum).GetEnumName(int.Parse(collectedValues["AccountType"])),
                    FakeId = employee.EmployeeBankDatas.Count + 1
                });
                //store data in a session
                Session["Employee"] = employee;
            }
            return View("BankData");
        }

        // GET: EmployeeManagement/MedicalData
        public ActionResult MedicalData()
        {
            return View();
        }
        // POST: EmployeeManagement/MedicalData
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MedicalData(EmployeeMedicalData medicalData,EmployeeWorkData workData,FormCollection collectedValues)
        {
            employee = Session["Employee"] as Employee;
            //collect data from form
            //medical data
            medicalData.BloodGroup = typeof(BloodGroup).GetEnumName(int.Parse(collectedValues["BloodGroup"]));
            medicalData.Genotype = typeof(Genotype).GetEnumName(int.Parse(collectedValues["Genotype"]));
            //work data
            workData.EmploymentType = typeof(EmploymentType).GetEnumName(int.Parse(collectedValues["EmploymentType"]));
            workData.PositionHeld = collectedValues["EmploymentPosition"];
            workData.EmploymentDate = Convert.ToDateTime(collectedValues["EmploymentDate"]);
            workData.EmploymentStatus = EmploymentStatus.Active.ToString();
            //store data in a session
            if (employee != null)
            {
                employee.EmployeeMedicalDatas = new List<EmployeeMedicalData>();
                employee.EmployeeMedicalDatas.Add(medicalData);

                employee.EmployeeWorkDatas = new List<EmployeeWorkData>();
                employee.EmployeeWorkDatas.Add(workData);
                Session["Employee"] = employee;
            }
            SavaEmployeeData();
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public void SavaEmployeeData()
        {
            var employeeData = Session["Employee"] as Employee;
            if (employeeData != null)
            {
                employeeData.DateCreated = DateTime.Now;
                employee.DateLastModified = DateTime.Now;
                _dbi.Employees.Add(employee);
                

                if (employeeData.EmployeeBankDatas != null)
                {
                    foreach (var item in employeeData.EmployeeBankDatas)
                    {
                        item.EmployeeId = employeeData.EmployeeId;
                        item.FakeId = 0;
                    }
                    foreach (var employeeDataEmployeeBankData in employeeData.EmployeeBankDatas)
                    {
                        _dbc.EmployeeBankDatas.Add(employeeDataEmployeeBankData);
                       // _dbc.SaveChanges();
                    }
                }

                if (employeeData.EmployeeEducationalQualifications != null)
                {
                    foreach (var item in employeeData.EmployeeEducationalQualifications)
                    {
                        item.EmployeeId = employeeData.EmployeeId;
                        item.FakeId = 0;
                    }
                    foreach (
                        var employeeDataEmployeeEducationalQualification in employeeData.EmployeeEducationalQualifications)
                    {
                        _dbd.EmployeeEducationalQualifications.Add(employeeDataEmployeeEducationalQualification);
                        //_dbd.SaveChanges();
                    }
                }

                var employeePersonalData = employeeData.EmployeePersonalData.FirstOrDefault();
                if (employeePersonalData != null)
                {
                    employeePersonalData.EmployeeId = employeeData.EmployeeId;
                    _dbe.EmployeePersonalDatas.Add(employeeData.EmployeePersonalData.FirstOrDefault());
                    //_dbe.SaveChanges();
                }

                if (employeeData.EmployeePastWorkExperiences != null)
                {
                    foreach (var item in employeeData.EmployeePastWorkExperiences)
                    {
                        item.EmployeeId = employeeData.EmployeeId;
                        item.FakeId = 0;
                    }
                    foreach (var employeeDataEmployeePastWorkExperience in employeeData.EmployeePastWorkExperiences)
                    {
                        _dbf.EmployeePastWorkExperiences.Add(employeeDataEmployeePastWorkExperience);
                        //_dbf.SaveChanges();
                    }
                }

                var employeeWorkData = employeeData.EmployeeWorkDatas.FirstOrDefault();
                if (employeeWorkData != null)
                {
                    employeeWorkData.EmployeeId = employeeData.EmployeeId;
                    _dbg.EmployeeWorkData.Add(employeeData.EmployeeWorkDatas.FirstOrDefault());
                    //_dbg.SaveChanges();
                }

                var employeeMedicalData = employeeData.EmployeeMedicalDatas.FirstOrDefault();
                if (employeeMedicalData != null)
                {
                    employeeMedicalData.EmployeeId = employeeData.EmployeeId;
                    _dbh.EmployeeMedicalDatas.Add(employeeData.EmployeeMedicalDatas.FirstOrDefault());
                   // _dbh.SaveChanges();
                }
                _dbi.SaveChanges();

            }
        }
    }
}