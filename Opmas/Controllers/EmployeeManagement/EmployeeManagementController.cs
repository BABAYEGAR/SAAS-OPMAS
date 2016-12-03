using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using BhuInfo.Data.Service.Encryption;
using BhuInfo.Data.Service.TextFormatter;
using Opmas.Data.DataContext.DataContext.EmployeeDataContext;
using Opmas.Data.Factory.EmployeeManagement;
using Opmas.Data.Objects.Entities.Employee;
using Opmas.Data.Objects.Entities.User;
using Opmas.Data.Service.Enums;

namespace Opmas.Controllers.EmployeeManagement
{
    public class EmployeeManagementController : Controller
    {
        private readonly StateDataContext _db = new StateDataContext();
        private readonly EmployeeDataContext _dbEmployee = new EmployeeDataContext();
        private readonly BankDataContext _dbBanks = new BankDataContext();
        private Employee _employee = new Employee();
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
            _employee.EmployeePersonalData = new List<EmployeePersonalData> {personalData};
            Session["Employee"] = _employee;

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
            _employee = Session["Employee"] as Employee;
            //collect data from form using form collection
            if (_employee != null)
            {
                if (_employee.EmployeeEducationalQualifications == null)
                {
                    _employee.EmployeeEducationalQualifications = new List<EmployeeEducationalQualification>();
                }
                _employee.EmployeeEducationalQualifications.Add(new EmployeeEducationalQualification()
                {
                    ClassOfDegree = typeof(ClassOfDegreeEnum).GetEnumName(int.Parse(collectedValues["ClassOfDegree"])),
                    DegreeAttained = typeof(DegreeTypeEnum).GetEnumName(int.Parse(collectedValues["DegreeAttained"])),
                    InstitutionName = collectedValues["InstitutionName"],
                    Location = collectedValues["Location"],
                    StartDate = Convert.ToDateTime(collectedValues["StartDate"]),
                    EndDate = Convert.ToDateTime(collectedValues["EndDate"]),
                    FakeId = _employee.EmployeeEducationalQualifications.Count + 1
                });
                //store data in a session
                Session["Employee"] = _employee;
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
            _employee = Session["Employee"] as Employee;
            if (_employee != null)
            {
                if (_employee.EmployeePastWorkExperiences == null)
                {
                    _employee.EmployeePastWorkExperiences = new List<EmployeePastWorkExperience>();
                }
                _employee.EmployeePastWorkExperiences.Add(new EmployeePastWorkExperience()
                {
                    EmployerName = collectedValues["EmployerName"],
                    EmployerLocation = collectedValues["EmployerLocation"],
                    EmployerContact = collectedValues["EmployerContact"],
                    PositionHeld = collectedValues["PositionHeld"],
                    ReasonForLeaving = collectedValues["ReasonForLeaving"],
                    StartDate = Convert.ToDateTime(collectedValues["StartDate"]),
                    EndDate = Convert.ToDateTime(collectedValues["EndDate"]),
                    FakeId = _employee.EmployeePastWorkExperiences.Count + 1

                });
                //store data in a session
                Session["Employee"] = _employee;
            }
            return View("PastWorkExperience");
        }
        // GET: EmployeeManagement/BankData
        public ActionResult BankData()
        {
            ViewBag.Bank = new SelectList(_dbBanks.Banks, "BankId", "Name");
            return View();
        }
        // POST: EmployeeManagement/BankData
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BankData(FormCollection collectedValues)
        {
            _employee = Session["Employee"] as Employee;
            //append data to list
            if (_employee != null)
            {
                if (_employee.EmployeeBankDatas == null)
                {
                    _employee.EmployeeBankDatas = new List<EmployeeBankData>();
                }
                _employee.EmployeeBankDatas.Add(new EmployeeBankData()
                {
                    BankId = Convert.ToInt64(collectedValues["BankId"]),
                    AccountNumber = collectedValues["AccountNumber"],
                    AccountName = collectedValues["AccountName"],
                    AccountType = typeof(AccountTypeEnum).GetEnumName(int.Parse(collectedValues["AccountType"])),
                    FakeId = _employee.EmployeeBankDatas.Count + 1
                });
                //store data in a session
                Session["Employee"] = _employee;
            }
            ViewBag.Bank = new SelectList(_dbBanks.Banks, "BankId", "Name");
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
            _employee = Session["Employee"] as Employee;
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
            if (_employee != null)
            {
                _employee.EmployeeMedicalDatas = new List<EmployeeMedicalData> {medicalData};

                _employee.EmployeeWorkDatas = new List<EmployeeWorkData> {workData};
                Session["Employee"] = _employee;
            }
        
            return RedirectToAction("ReviewEmployeeData");
        }
        // GET: EmployeeManagement/ReviewEmployeeData
        public ActionResult ReviewEmployeeData()
        {  
            return View();
        }
        // POST: EmployeeManagement/ReviewEmployeeData
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReviewEmployeeDatas()
        {
            SavaEmployeeData();
            return RedirectToAction("Index","Home");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public void SavaEmployeeData()
        {
            var employeeData = Session["Employee"] as Employee;
            if (employeeData != null)
            {
                employeeData.DateCreated = DateTime.Now;
                _employee.DateLastModified = DateTime.Now;
                _dbEmployee.Employees.Add(_employee);
                

                if (employeeData.EmployeeBankDatas != null)
                {
                    foreach (var item in employeeData.EmployeeBankDatas)
                    {
                        item.EmployeeId = employeeData.EmployeeId;
                        item.FakeId = 0;
                    }
                    foreach (var employeeDataEmployeeBankData in employeeData.EmployeeBankDatas)
                    {
                        _dbEmployee.EmployeeBankDatas.Add(employeeDataEmployeeBankData);
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
                        _dbEmployee.EmployeeEducationalQualifications.Add(employeeDataEmployeeEducationalQualification);
                        //_dbd.SaveChanges();
                    }
                }

                var employeePersonalData = employeeData.EmployeePersonalData.FirstOrDefault();
                if (employeePersonalData != null)
                {
                    employeePersonalData.EmployeeId = employeeData.EmployeeId;
                    _dbEmployee.EmployeePersonalDatas.Add(employeeData.EmployeePersonalData.FirstOrDefault());
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
                        _dbEmployee.EmployeePastWorkExperiences.Add(employeeDataEmployeePastWorkExperience);
                        //_dbf.SaveChanges();
                    }
                }

                var employeeWorkData = employeeData.EmployeeWorkDatas.FirstOrDefault();
                if (employeeWorkData != null)
                {
                    employeeWorkData.EmployeeId = employeeData.EmployeeId;
                    _dbEmployee.EmployeeWorkDatas.Add(employeeData.EmployeeWorkDatas.FirstOrDefault());
                    //_dbg.SaveChanges();
                }

                var employeeMedicalData = employeeData.EmployeeMedicalDatas.FirstOrDefault();
                if (employeeMedicalData != null)
                {
                    employeeMedicalData.EmployeeId = employeeData.EmployeeId;
                    _dbEmployee.EmployeeMedicalDatas.Add(employeeData.EmployeeMedicalDatas.FirstOrDefault());
                   // _dbh.SaveChanges();
                }
                _dbEmployee.SaveChanges();

            }
        }

        public ActionResult RemoveEducationalQualification(long fakeId)
        {
            var employeeData = Session["Employee"] as Employee;
            if (employeeData != null)
            {
                employeeData.EmployeeEducationalQualifications.RemoveAll(n=>n.FakeId == fakeId);
                
            }
            return View("EducationalQualification");
        }
        public ActionResult RemoveBankData(long fakeId)
        {
            var employeeData = Session["Employee"] as Employee;
            if (employeeData != null)
            {
                employeeData.EmployeeBankDatas.RemoveAll(n => n.FakeId == fakeId);

            }
            ViewBag.Bank = new SelectList(_dbBanks.Banks, "BankId", "Name");
            return View("BankData");
        }
        public ActionResult RemovePastWorkExperience(long fakeId)
        {
            var employeeData = Session["Employee"] as Employee;
            if (employeeData != null)
            {
                employeeData.EmployeePastWorkExperiences.RemoveAll(n => n.FakeId == fakeId);

            }
            return View("PastWorkExperience");
        }
        public ActionResult ConvertEmployeeToApplicationUser(long employeeId)
        {
            var employee = _dbEmployee.EmployeePersonalDatas.Find(employeeId);
            AppUser appUser = new AppUser();
            appUser.Firstname = employee.Firstname;
            appUser.Lastname = employee.Lastname;
            appUser.Middlename = employee.Middlename;
            appUser.Email = employee.Email;
            appUser.CreatedById = 1;
            appUser.LastModifiedById = 1;
            appUser.DateCreated  = DateTime.Now;
            appUser.DateLastModified = DateTime.Now;
            appUser.Role = UserType.Employee.ToString();
            appUser.EmployeeId = employeeId;
            appUser.Mobile = employee.MobilePhone;

            //generate password and convert to md5 hash
            var password = Membership.GeneratePassword(8, 1);
            var hashPassword = new Md5Ecryption().ConvertStringToMd5Hash(password.Trim());
            appUser.Password = new RemoveCharacters().RemoveSpecialCharacters(hashPassword);

            
            return View("PastWorkExperience");
        }

    }
}