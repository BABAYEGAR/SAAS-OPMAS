using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Security;
using BhuInfo.Data.Service.Encryption;
using BhuInfo.Data.Service.TextFormatter;
using Opmas.Data.DataContext.DataContext.EmployeeDataContext;
using Opmas.Data.DataContext.DataContext.UserDataContext;
using Opmas.Data.Factory.EmployeeManagement;
using Opmas.Data.Objects.Entities.Employee;
using Opmas.Data.Objects.Entities.SystemManagement;
using Opmas.Data.Objects.Entities.User;
using Opmas.Data.Service.Enums;
using Opmas.Data.Service.FileUploader;

namespace Opmas.Controllers.EmployeeManagement
{
    public class EmployeeManagementController : Controller
    {
        private readonly StateDataContext _db = new StateDataContext();
        private readonly AppUserDataContext _dbAppUser = new AppUserDataContext();
        private readonly BankDataContext _dbBanks = new BankDataContext();
        private readonly EmployeeDataContext _dbEmployee = new EmployeeDataContext();
        private Employee _employee = new Employee();

        #region Fetch data

        /// <summary>
        ///     Sends Json responds object to view with lga of the state requested via an Ajax call
        /// </summary>
        /// <param name="id"> state id</param>
        /// <returns>lgas</returns>
        /// Microsoft.CodeDom.Providers.DotNetCompilerPlatform
        public JsonResult GetLgaForState(int id)
        {
            var lgas = new StateFactory().GetLgaForState(id);
            return Json(lgas, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///     Sends Json responds object to view with departments of the state requested via an Ajax call
        /// </summary>
        /// <param name="id"> faculty id</param>
        /// <returns>lgas</returns>
        /// Microsoft.CodeDom.Providers.DotNetCompilerPlatform
        public JsonResult GetDepartmentForFaculty(int id)
        {
            var departments = _dbEmployee.Departments.Where(n => n.FacultyId == id);
            return Json(departments, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        ///     Sends Json responds object to view with Units of the state requested via an Ajax call
        /// </summary>
        /// <param name="id"> faculty id</param>
        /// <returns>lgas</returns>
        /// Microsoft.CodeDom.Providers.DotNetCompilerPlatform
        public JsonResult GetUnitsForDepartment(int id)
        {
            var units = _dbEmployee.Units.Where(n => n.DepartmentId == id);
            return Json(units, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///     Sends Json responds object to view with email of the state requested via an Ajax call
        /// </summary>
        /// <param name="email">email</param>
        /// <returns>lgas</returns>
        /// Microsoft.CodeDom.Providers.DotNetCompilerPlatform
        public JsonResult CheckIfEmailExist(string email)
        {
            var emailCheck = _dbEmployee.EmployeePersonalDatas.Where(n => n.Email == email);
            return Json(emailCheck, JsonRequestBehavior.AllowGet);
        }

        // GET: EmployeeManagement/ListOfEmployeesByStatus
        public ActionResult ListOfEmployeesByStatus(string status, long? id)
        {
            var employees = new EmployeeFactory().GetAllEmployeesByStatus(status, id);
            return View(employees.ToList());
        }

        // GET: EmployeeManagement/ListOfEmployeesInactive
        public ActionResult ListOfEmployeesInactive(string status)
        {
            var employees = new EmployeeFactory().GetAllInactiveEmployees(status);
            return View("ListOfEmployeesByStatus", employees);
        }

        // GET: EmployeeManagement/ListOfEmployees
        public ActionResult ListOfEmployees()
        {
            var institution = Session["institution"] as Institution;
            return
                View(
                    _dbEmployee.Employees.ToList()
                        .Where(n => (institution != null) && (n.InstitutionId == institution.InstitutionId)));
        }

        #endregion

        #region Employee Process  

        // GET: EmployeeManagement/PersonalData
        public ActionResult PersonalData(bool? returnUrl)
        {
            _employee = Session["Employee"] as Employee;
            ViewBag.State = new SelectList(_db.States, "StateId", "Name");
            if ((returnUrl != null) && returnUrl.Value)
            {
                ViewBag.returnUrl = true;
                _employee = Session["Employee"] as Employee;
                if (_employee != null)
                    return View(_employee.EmployeePersonalData.SingleOrDefault());
            }
            return View(_employee?.EmployeePersonalData.SingleOrDefault());
        }

        // Post: EmployeeManagement/PersonalData
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PersonalData(EmployeePersonalData personalData, FormCollection collectedValues)
        {
            _employee = Session["Employee"] as Employee;
            if (_employee != null)
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
                personalData.Title = typeof(NameTitle).GetEnumName(int.Parse(collectedValues["Title"]));
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
            }
            else
            {
                var employeePersonalData = new Employee();
                //collect data from form using form collection
                personalData.Firstname = collectedValues["Firstname"];
                personalData.Lastname = collectedValues["Lastname"];
                personalData.Middlename = collectedValues["Middlename"];
                personalData.Gender = collectedValues["Gender"];
                personalData.Email = collectedValues["Email"];
                personalData.PrimaryAddress = collectedValues["PrimaryAddress"];
                personalData.Title = typeof(NameTitle).GetEnumName(int.Parse(collectedValues["Title"]));
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
                employeePersonalData.EmployeePersonalData = new List<EmployeePersonalData> {personalData};
                Session["Employee"] = employeePersonalData;
            }
            var returnUrl = Convert.ToBoolean(collectedValues["returnUrl"]);
            //if it is edit from review page return to the review page
            if (returnUrl)
                return View("ReviewEmployeeData");
            //return next view
            return View("EducationalQualification");
        }

        // GET: EmployeeManagement/EducationalQualification
        public ActionResult EducationalQualification(bool? returnUrl)
        {
            _employee = Session["Employee"] as Employee;
            if ((returnUrl != null) && returnUrl.Value)
            {
                ViewBag.returnUrl = true;
                _employee = Session["Employee"] as Employee;
                if (_employee != null)
                    return View();
            }
            return View();
        }

        // POST: EmployeeManagement/EducationalQualification
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EducationalQualification(FormCollection collectedValues)
        {
            _employee = Session["Employee"] as Employee;
            //collect data from form using form collection
            var returnUrl = Convert.ToBoolean(collectedValues["returnUrl"]);

            var file = Request.Files["file"];
            if (_employee != null)
            {
                var degree = typeof(DegreeTypeEnum).GetEnumName(int.Parse(collectedValues["DegreeAttained"]));
                //var startDate = Convert.ToDateTime(collectedValues["StartDate"]);
                var endDate = Convert.ToDateTime(collectedValues["EndDate"]);
                if (_employee.EmployeeEducationalQualifications != null)
                {
                    var checkMasters =
                        _employee?.EmployeeEducationalQualifications.Where(
                            n =>
                                (n.DegreeAttained == DegreeTypeEnum.Basic.ToString()) ||
                                (n.DegreeAttained == DegreeTypeEnum.JSCE.ToString()) ||
                                (n.DegreeAttained == DegreeTypeEnum.SSCE.ToString()) ||
                                (n.DegreeAttained == DegreeTypeEnum.BSc.ToString()));
                    var checkPhd =
                        _employee?.EmployeeEducationalQualifications.Where(
                            n =>
                                (n.DegreeAttained == DegreeTypeEnum.Basic.ToString()) ||
                                (n.DegreeAttained == DegreeTypeEnum.JSCE.ToString()) ||
                                (n.DegreeAttained == DegreeTypeEnum.SSCE.ToString()) ||
                                (n.DegreeAttained == DegreeTypeEnum.BSc.ToString()) ||
                                (n.DegreeAttained == DegreeTypeEnum.MSc.ToString()));

                    if (degree == DegreeTypeEnum.MSc.ToString())
                        if (checkMasters.Any(item => endDate < item.StartDate))
                        {
                            TempData["education"] =
                                "You cannot offer a masters degree before basic and college education!";
                            TempData["notificationType"] = NotificationTypeEnum.Error.ToString();
                            return View();
                        }
                    if (degree == DegreeTypeEnum.Phd.ToString())
                        if (checkPhd.Any(item => endDate < item.StartDate))
                        {
                            TempData["education"] =
                                "You cannot offer a doctorate degree before basic,college and masters education!";
                            TempData["notificationType"] = NotificationTypeEnum.Error.ToString();
                            return View();
                        }
                }
                if (_employee.EmployeeEducationalQualifications == null)
                    _employee.EmployeeEducationalQualifications = new List<EmployeeEducationalQualification>();
                _employee.EmployeeEducationalQualifications.Add(new EmployeeEducationalQualification
                {
                    ClassOfDegree = typeof(ClassOfDegreeEnum).GetEnumName(int.Parse(collectedValues["ClassOfDegree"])),
                    DegreeAttained = typeof(DegreeTypeEnum).GetEnumName(int.Parse(collectedValues["DegreeAttained"])),
                    InstitutionName = collectedValues["InstitutionName"],
                    Location = collectedValues["Location"],
                    StartDate = Convert.ToDateTime(collectedValues["StartDate"]),
                    EndDate = Convert.ToDateTime(collectedValues["EndDate"]),
                    FakeId = _employee.EmployeeEducationalQualifications.Count + 1,
                    FileUpload =
                        (file != null) && (file.FileName != "")
                            ? new FileUploader().UploadFile(file, UploadType.Education)
                            : null
                });
                TempData["education"] = "You ave successfully added a " + degree + " qualification!";
                TempData["notificationType"] = NotificationTypeEnum.Success.ToString();
                //store data in a session
                Session["Employee"] = _employee;
            }

            //if it is edit from review page return to the review page
            if (returnUrl)
                return RedirectToAction("EducationalQualification", new {returnUrl = true});
            return View("EducationalQualification");
        }

        // GET: EmployeeManagement/PastWorkExperience
        public ActionResult PastWorkExperience(bool? returnUrl)
        {
            _employee = Session["Employee"] as Employee;
            if ((returnUrl != null) && returnUrl.Value)
            {
                ViewBag.returnUrl = true;
                _employee = Session["Employee"] as Employee;
                if (_employee != null)
                    return View();
            }
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
                    _employee.EmployeePastWorkExperiences = new List<EmployeePastWorkExperience>();
                _employee.EmployeePastWorkExperiences.Add(new EmployeePastWorkExperience
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
                TempData["work"] =
                    "You have successfully added a work experience!";
                TempData["notificationType"] = NotificationTypeEnum.Success.ToString();
            }
            var returnUrl = Convert.ToBoolean(collectedValues["returnUrl"]);
            //if it is edit from review page return to the review page
            if (returnUrl)
                return RedirectToAction("PastWorkExperience", new {returnUrl = true});
            return View("PastWorkExperience");
        }

        // GET: EmployeeManagement/EmployeeFamilyData
        public ActionResult EmployeeFamilyData(bool? returnUrl)
        {
            _employee = Session["Employee"] as Employee;
            if ((returnUrl != null) && returnUrl.Value)
            {
                ViewBag.returnUrl = true;
                _employee = Session["Employee"] as Employee;
                if (_employee?.EmployeeFamilyDatas != null)
                    return View(_employee.EmployeeFamilyDatas.SingleOrDefault());
            }
            return View();
        }

        // POST: EmployeeManagement/EmployeeFamilyData
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EmployeeFamilyData(EmployeeFamilyData familyData, FormCollection collectedValues)
        {
            _employee = Session["Employee"] as Employee;
            if (_employee != null)
            {
                familyData.FullName = collectedValues["FullName"];
                familyData.ContactNumber = collectedValues["ContactNumber"];
                familyData.Address = collectedValues["Address"];
                familyData.Email = collectedValues["Email"];
                familyData.Relationship = typeof(FamilyEnum).GetEnumName(int.Parse(collectedValues["Relationship"]));
                familyData.DateOfBirth = Convert.ToDateTime(collectedValues["DateOfBirth"]);
                //store data in a session
                _employee.EmployeeFamilyDatas = new List<EmployeeFamilyData> {familyData};
                Session["Employee"] = _employee;
            }
            else
            {
                var employeeFamilyData = new Employee();
                familyData.FullName = collectedValues["FullName"];
                familyData.ContactNumber = collectedValues["ContactNumber"];
                familyData.Address = collectedValues["Address"];
                familyData.Email = collectedValues["Email"];
                familyData.Relationship = typeof(FamilyEnum).GetEnumName(int.Parse(collectedValues["Relationship"]));
                familyData.DateOfBirth = Convert.ToDateTime(collectedValues["DateOfBirth"]);
                //store data in a session
                employeeFamilyData.EmployeeFamilyDatas = new List<EmployeeFamilyData> {familyData};
                Session["Employee"] = employeeFamilyData;
            }
            var returnUrl = Convert.ToBoolean(collectedValues["returnUrl"]);
            //if it is edit from review page return to the review page
            if (returnUrl)
                return View("ReviewEmployeeData");
            //return next view

            return RedirectToAction("BankData");
        }

        // GET: EmployeeManagement/BankData
        public ActionResult BankData(bool? returnUrl)
        {
            _employee = Session["Employee"] as Employee;
            ViewBag.Bank = new SelectList(_dbBanks.Banks, "BankId", "Name");
            if ((returnUrl != null) && returnUrl.Value)
            {
                ViewBag.returnUrl = true;
                _employee = Session["Employee"] as Employee;
                if (_employee != null)
                    return View();
            }
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
                    _employee.EmployeeBankDatas = new List<EmployeeBankData>();
                _employee.EmployeeBankDatas.Add(new EmployeeBankData
                {
                    BankId = Convert.ToInt64(collectedValues["BankId"]),
                    AccountNumber = collectedValues["AccountNumber"],
                    AccountFirstName = collectedValues["AccountFirstName"],
                    AccountMiddleName = collectedValues["AccountMiddleName"],
                    AccountLastName = collectedValues["AccountLastName"],
                    AccountType = typeof(AccountTypeEnum).GetEnumName(int.Parse(collectedValues["AccountType"])),
                    FakeId = _employee.EmployeeBankDatas.Count + 1
                });
                //store data in a session
                Session["Employee"] = _employee;
                TempData["bank"] =
                    "You have successfully added a bank data!";
                TempData["notificationType"] = NotificationTypeEnum.Success.ToString();
            }
            var returnUrl = Convert.ToBoolean(collectedValues["returnUrl"]);
            //if it is edit from review page return to the review page
            if (returnUrl)
                return RedirectToAction("BankData", new {returnUrl = true});
            ViewBag.Bank = new SelectList(_dbBanks.Banks, "BankId", "Name");
            return View("BankData");
        }

        // GET: EmployeeManagement/MedicalData
        public ActionResult MedicalData()
        {
            _employee = Session["Employee"] as Employee;
            if (_employee?.EmployeeMedicalDatas != null)
                return View(_employee.EmployeeMedicalDatas.SingleOrDefault());
            return View();
        }

        // POST: EmployeeManagement/MedicalData
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MedicalData(EmployeeMedicalData medicalData, EmployeeWorkData workData,
            FormCollection collectedValues)
        {
            _employee = Session["Employee"] as Employee;
            //collect data from form
            //medical data
            medicalData.BloodGroup = typeof(BloodGroup).GetEnumName(int.Parse(collectedValues["BloodGroup"]));
            medicalData.Genotype = typeof(Genotype).GetEnumName(int.Parse(collectedValues["Genotype"]));
            //work data
            workData.EmploymentType = typeof(EmploymentType).GetEnumName(int.Parse(collectedValues["EmploymentType"]));
            workData.Category = typeof(EmployementCategory).GetEnumName(int.Parse(collectedValues["EmploymentCategory"]));
            workData.PositionHeld = collectedValues["EmploymentPosition"];
            workData.EmploymentDate = Convert.ToDateTime(collectedValues["EmploymentDate"]);
            workData.EmploymentStatus = EmploymentStatus.Active.ToString();
            if (_employee != null)
            {
                
                _employee.DepartmentId = Convert.ToInt64(collectedValues["DepartmentId"]);
                _employee.FacultyId = Convert.ToInt64(collectedValues["FacultyId"]);
                _employee.UnitId = Convert.ToInt64(collectedValues["UnitId"]);
                _employee.RoleId = Convert.ToInt64(collectedValues["RoleId"]);

                Session["Employee"] = _employee;

                var role = _dbEmployee.Roles.Find(_employee.RoleId);
                var allEmployees =
                    _dbEmployee.Employees.Where(
                        n => (n.RoleId == role.RoleId) && (n.DepartmentId == _employee.DepartmentId));
                if ((role.RoleType == RoleType.Single.ToString()) && (allEmployees.ToList().Count > 0))
                {
                    TempData["medical"] =
                        "This role has been assigned to an employee cannot be assigned to more than one employee!";
                    TempData["notificationType"] = NotificationTypeEnum.Error.ToString();
                    _employee.RoleId = null;
                    return View(_employee.EmployeeMedicalDatas.FirstOrDefault());
                }
                

                //store data in a session
                if (_employee != null)
                {
                    _employee.EmployeeMedicalDatas = new List<EmployeeMedicalData> {medicalData};

                    _employee.EmployeeWorkDatas = new List<EmployeeWorkData> {workData};
                    Session["Employee"] = _employee;
                }
            }

            return RedirectToAction("ReviewEmployeeData");
        }

        // GET: EmployeeManagement/ReviewEmployeeData
        public ActionResult ReviewEmployeeData()
        {
            var employeeData = Session["Employee"] as Employee;
            return View(employeeData);
        }

        // POST: EmployeeManagement/ReviewEmployeeData
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReviewEmployeeDatas()
        {
            var institution = Session["institution"] as Institution;
            if (institution != null)
            {
                var employeeData = Session["Employee"] as Employee;
                SavaEmployeeData(employeeData);
                Session["Employee"] = null;
                Session["institution"] = null;
                TempData["reg"] = "You have successfully registered!";
                TempData["notificationType"] = NotificationTypeEnum.Success.ToString();
            }
            else
            {
                TempData["regerror"] = "Make sure you select your institution and try again!";
                TempData["notificationType"] = NotificationTypeEnum.Error.ToString();
                return RedirectToAction("Login", "Account");
            }

            return RedirectToAction("Login", "Account");
        }

        public void SavaEmployeeData(Employee employeeData)
        {
            var institution = Session["institution"] as Institution;
            if (institution != null)
                if (employeeData != null)
                {
                    _employee.DateCreated = DateTime.Now;
                    _employee.DateLastModified = DateTime.Now;
                    _employee.LastModifiedBy = 1;
                    _employee.CreatedBy = 1;
                    _employee.InstitutionId = institution.InstitutionId;

                    _dbEmployee.Employees.Add(_employee);


                    if (employeeData.EmployeeBankDatas != null)
                    {
                        foreach (var item in employeeData.EmployeeBankDatas)
                        {
                            item.EmployeeId = employeeData.EmployeeId;
                            item.FakeId = 0;
                        }
                        foreach (var employeeDataEmployeeBankData in employeeData.EmployeeBankDatas)
                            _dbEmployee.EmployeeBankDatas.Add(employeeDataEmployeeBankData);
                    }

                    if (employeeData.EmployeeEducationalQualifications != null)
                    {
                        foreach (var item in employeeData.EmployeeEducationalQualifications)
                        {
                            item.EmployeeId = employeeData.EmployeeId;
                            item.FakeId = 0;
                        }
                        foreach (
                            var employeeDataEmployeeEducationalQualification in
                            employeeData.EmployeeEducationalQualifications)
                            _dbEmployee.EmployeeEducationalQualifications.Add(
                                employeeDataEmployeeEducationalQualification);
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
                            _dbEmployee.EmployeePastWorkExperiences.Add(employeeDataEmployeePastWorkExperience);
                    }
                    var employeeFamilyData = employeeData.EmployeeFamilyDatas.FirstOrDefault();
                    if (employeeFamilyData != null)
                    {
                        employeeFamilyData.EmployeeId = employeeData.EmployeeId;
                        _dbEmployee.EmployeeFamilyDatas.Add(employeeData.EmployeeFamilyDatas.FirstOrDefault());
                        //_dbe.SaveChanges();
                    }


                    var employeeWorkData = employeeData.EmployeeWorkDatas.FirstOrDefault();
                    if (employeeWorkData != null)
                    {
                        employeeWorkData.EmployeeId = employeeData.EmployeeId;
                        _dbEmployee.EmployeeWorkDatas.Add(employeeData.EmployeeWorkDatas.FirstOrDefault());
                    }

                    var employeeMedicalData = employeeData.EmployeeMedicalDatas.FirstOrDefault();
                    if (employeeMedicalData != null)
                    {
                        employeeMedicalData.EmployeeId = employeeData.EmployeeId;
                        _dbEmployee.EmployeeMedicalDatas.Add(employeeData.EmployeeMedicalDatas.FirstOrDefault());
                    }
                    _dbEmployee.SaveChanges();
                }
        }

        #endregion

        #region single employee data  

        // GET: EmployeeManagement/CreateSingleEducationalQualification
        public ActionResult CreateSingleEducationalQualification()
        {
            var educationalQualification = _dbEmployee.EmployeeEducationalQualifications.SingleOrDefault();
            return View(educationalQualification);
        }

        // POST: EmployeeManagement/CreateSingleEducationalQualification
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSingleEducationalQualification([Bind(
                                                                      Include =
                                                                          "EmployeeEducationalQualificationId,InstitutionName,Location"
                                                                  )] FormCollection collectedValues,
            EmployeeEducationalQualification educationalQualification)
        {
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            if (loggedinuser != null)
            {
                var degree = typeof(DegreeTypeEnum).GetEnumName(int.Parse(collectedValues["DegreeAttained"]));
                //var startDate = Convert.ToDateTime(collectedValues["StartDate"]);
                var endDate = Convert.ToDateTime(collectedValues["EndDate"]);
                if (_dbEmployee.EmployeeEducationalQualifications != null)
                {
                    var checkMasters =
                        _dbEmployee?.EmployeeEducationalQualifications.Where(
                            n =>
                                (n.DegreeAttained == DegreeTypeEnum.Basic.ToString()) ||
                                (n.DegreeAttained == DegreeTypeEnum.JSCE.ToString()) ||
                                (n.DegreeAttained == DegreeTypeEnum.SSCE.ToString()) ||
                                (n.DegreeAttained == DegreeTypeEnum.BSc.ToString())).ToList();
                    var checkPhd =
                        _dbEmployee?.EmployeeEducationalQualifications.Where(
                            n =>
                                (n.DegreeAttained == DegreeTypeEnum.Basic.ToString()) ||
                                (n.DegreeAttained == DegreeTypeEnum.JSCE.ToString()) ||
                                (n.DegreeAttained == DegreeTypeEnum.SSCE.ToString()) ||
                                (n.DegreeAttained == DegreeTypeEnum.BSc.ToString()) ||
                                (n.DegreeAttained == DegreeTypeEnum.MSc.ToString())).ToList();

                    if (degree == DegreeTypeEnum.MSc.ToString())
                        if (checkMasters.Any(item => endDate < item.StartDate))
                        {
                            TempData["education"] =
                                "You cannot offer a masters degree before basic and college education!";
                            TempData["notificationType"] = NotificationTypeEnum.Error.ToString();
                            return RedirectToAction("ListOfEducationalQualification", "EmployeeManagement",
                                new {id = loggedinuser.EmployeeId});
                        }
                    if (degree == DegreeTypeEnum.Phd.ToString())
                        if (checkPhd.Any(item => endDate < item.StartDate))
                        {
                            TempData["education"] =
                                "You cannot offer a doctorate degree before basic,college and masters education!";
                            TempData["notificationType"] = NotificationTypeEnum.Error.ToString();
                            return View();
                        }
                }

                educationalQualification.ClassOfDegree =
                    typeof(ClassOfDegreeEnum).GetEnumName(int.Parse(collectedValues["ClassOfDegree"]));
                educationalQualification.DegreeAttained =
                    typeof(DegreeTypeEnum).GetEnumName(int.Parse(collectedValues["DegreeAttained"]));
                educationalQualification.InstitutionName = collectedValues["InstitutionName"];
                educationalQualification.StartDate = Convert.ToDateTime(collectedValues["StartDate"]);
                educationalQualification.EndDate = Convert.ToDateTime(collectedValues["EndDate"]);
                educationalQualification.FakeId = 0;

                if (loggedinuser.EmployeeId != null)
                    educationalQualification.EmployeeId = (long) loggedinuser.EmployeeId;


                _dbEmployee.EmployeeEducationalQualifications?.Add(educationalQualification);
                _dbEmployee.SaveChanges();
                TempData["education"] =
                    "You have successfully added a " + degree + " qualification!";
                TempData["notificationType"] = NotificationTypeEnum.Success.ToString();
            }

            return RedirectToAction("ListOfEducationalQualification", "EmployeeManagement",
                new {id = educationalQualification.EmployeeId});
        }

        // POST: EmployeeManagement/CreateSingleBankData
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSingleBankData([Bind(
                                                      Include =
                                                          "EmployeeBankDataId,BankId,AccountFirstName,AccountMiddleName,AccountLastName,AccountNumber"
                                                  )] FormCollection collectedValues, EmployeeBankData employeeBankData)
        {
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            if (loggedinuser != null)
            {
                employeeBankData.AccountType =
                    typeof(AccountTypeEnum).GetEnumName(int.Parse(collectedValues["AccountType"]));
                employeeBankData.FakeId = 0;
                if (loggedinuser.EmployeeId != null) employeeBankData.EmployeeId = (long) loggedinuser.EmployeeId;
            }
            _dbEmployee.EmployeeBankDatas.Add(employeeBankData);
            _dbEmployee.SaveChanges();
            TempData["bank"] =
                "You have successfully added a new bank data!";
            TempData["notificationType"] = NotificationTypeEnum.Success.ToString();

            return RedirectToAction("ListOfBankData", "EmployeeManagement", new {id = employeeBankData.EmployeeId});
        }

        // POST: EmployeeManagement/PastWorkExperience
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSinglePastWorkExperience([Bind(
                                                                Include =
                                                                    "EmployeePastWorkExperienceId,EmployerName,EmployerLocation,EmployerContact,PositionHeld,ReasonForLeaving,StartDate,EndDate"
                                                            )] FormCollection collectedValues,
            EmployeePastWorkExperience pastWorkExperience)
        {
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            if (loggedinuser != null)
            {
                pastWorkExperience.FakeId = 0;
                if (loggedinuser.EmployeeId != null) pastWorkExperience.EmployeeId = (long) loggedinuser.EmployeeId;
            }
            _dbEmployee.EmployeePastWorkExperiences.Add(pastWorkExperience);
            _dbEmployee.SaveChanges();
            TempData["work"] =
                "You have successfully added a new past work experience data!";
            TempData["notificationType"] = NotificationTypeEnum.Success.ToString();
            return RedirectToAction("ListOfPastWorkExperience", "EmployeeManagement",
                new {id = pastWorkExperience.EmployeeId});
        }

        // POST: EmployeeManagement/PastWorkExperience
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSingleEmployeeFamilyData([Bind(
                                                                Include =
                                                                    "EmployeeFamilyDataId,FullName,Address,ContactNumber,Email,Relationship"
                                                            )] FormCollection collectedValues,
            EmployeeFamilyData familyData)
        {
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            if (loggedinuser != null)
            {
                familyData.DateOfBirth = Convert.ToDateTime(collectedValues["DateOfBirth"]);
                if (loggedinuser.EmployeeId != null) familyData.EmployeeId = (long) loggedinuser.EmployeeId;
            }
            _dbEmployee.EmployeeFamilyDatas.Add(familyData);
            _dbEmployee.SaveChanges();
            TempData["family"] =
                "You have successfully added a new family data!";
            TempData["notificationType"] = NotificationTypeEnum.Success.ToString();
            return RedirectToAction("ListOfEmployeeFamily", "EmployeeManagement",
                new {id = familyData.EmployeeId});
        }

        #endregion

        #region Remove employee data by Id

        /// <summary>
        ///     This method remove an educational qualification by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult RemoveEducationalQualificationById(long id)
        {
            var educaionalQualification = _dbEmployee.EmployeeEducationalQualifications.Find(id);
            _dbEmployee.EmployeeEducationalQualifications.Remove(educaionalQualification);
            _dbEmployee.SaveChanges();
            return RedirectToAction("ListOfEducationalQualification", new {id = educaionalQualification.EmployeeId});
        }

        /// <summary>
        ///     This method remove a past work experience by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult RemovePastWorkExperienceById(long id)
        {
            var pastWorkExperience = _dbEmployee.EmployeePastWorkExperiences.Find(id);
            _dbEmployee.EmployeePastWorkExperiences.Remove(pastWorkExperience);
            _dbEmployee.SaveChanges();
            return RedirectToAction("ListOfPastWorkExperience", new {id = pastWorkExperience.EmployeeId});
        }

        /// <summary>
        ///     This method remove a family data by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult RemoveEmloyeeFamilyDataById(long id)
        {
            var familyData = _dbEmployee.EmployeeFamilyDatas.Find(id);
            _dbEmployee.EmployeeFamilyDatas.Remove(familyData);
            _dbEmployee.SaveChanges();
            return RedirectToAction("ListOfEmployeeFamily", new {id = familyData.EmployeeId});
        }

        /// <summary>
        ///     This method remove a bank data by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult RemoveBankDataById(long id)
        {
            var bankData = _dbEmployee.EmployeeBankDatas.Find(id);
            _dbEmployee.EmployeeBankDatas.Remove(bankData);
            _dbEmployee.SaveChanges();
            return RedirectToAction("ListOfBankData", new {id = bankData.EmployeeId});
        }

        #endregion

        #region Remove employee data from session

        /// <summary>
        ///     This method remove an educational qualification from a session list
        /// </summary>
        /// <param name="fakeId"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public ActionResult RemoveEducationalQualification(long fakeId, bool? returnUrl)
        {
            var employeeData = Session["Employee"] as Employee;
            employeeData?.EmployeeEducationalQualifications.RemoveAll(n => n.FakeId == fakeId);
            if ((returnUrl != null) && (returnUrl == true))
                return RedirectToAction("EducationalQualification", new {returnUrl = true});
            return RedirectToAction("EducationalQualification");
        }

        /// <summary>
        ///     This method removes an appended list item from the session
        /// </summary>
        /// <param name="fakeId"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public ActionResult RemoveBankData(long fakeId, bool? returnUrl)
        {
            var employeeData = Session["Employee"] as Employee;
            employeeData?.EmployeeBankDatas.RemoveAll(n => n.FakeId == fakeId);
            ViewBag.Bank = new SelectList(_dbBanks.Banks, "BankId", "Name");
            if ((returnUrl != null) && (returnUrl == true))
                return RedirectToAction("BankData", new {returnUrl = true});
            return RedirectToAction("BankData");
        }

        /// <summary>
        ///     This method removes an appended list item from the session
        /// </summary>
        /// <param name="fakeId"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public ActionResult RemovePastWorkExperience(long fakeId, bool? returnUrl)
        {
            var employeeData = Session["Employee"] as Employee;
            employeeData?.EmployeePastWorkExperiences.RemoveAll(n => n.FakeId == fakeId);
            if ((returnUrl != null) && (returnUrl == true))
                return RedirectToAction("PastWorkExperience", new {returnUrl = true});
            return RedirectToAction("PastWorkExperience");
        }

        #endregion

        #region Convert employee to application user

        // GET: EmployeeManagement/ConvertEmployeeToAppUser
        public ActionResult ConvertEmployeeToAppUser(long employeeId)
        {
            var employee = _dbEmployee.Employees.Find(employeeId);
            ViewBag.employeeId = employeeId;
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConvertEmployeeToApplicationUser(FormCollection collectedValue)
        {
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            var institution = Session["institution"] as Institution;
            var employeeId = Convert.ToInt64(collectedValue["EmployeeId"]);
            var employeePersonalData = _dbEmployee.EmployeePersonalDatas.SingleOrDefault(n => n.EmployeeId == employeeId);
            var employees =
                _dbEmployee.Employees.ToList()
                    .Where(n => (institution != null) && (n.InstitutionId == institution.InstitutionId));
            var employee = _dbEmployee.Employees.Find(employeeId);
            var appUser = new AppUser();
            if (employeePersonalData != null)
            {
                appUser.Firstname = employeePersonalData.Firstname;
                appUser.Lastname = employeePersonalData.Lastname;
                appUser.Middlename = employeePersonalData.Middlename;
                appUser.Email = employeePersonalData.Email;
                appUser.RoleId = employee.RoleId;
                appUser.DepartmentId = employee.DepartmentId;
                appUser.FacultyId = employee.FacultyId;
                if (loggedinuser != null)
                {
                    appUser.CreatedBy = loggedinuser.AppUserId;
                    appUser.LastModifiedBy = loggedinuser.AppUserId;
                }
                appUser.DateCreated = DateTime.Now;
                appUser.DateLastModified = DateTime.Now;
                appUser.EmployeeId = employeeId;
                appUser.Mobile = employeePersonalData.MobilePhone;
            }
            if (institution != null) appUser.InstitutionId = institution.InstitutionId;
            //generate password and convert to md5 hash
            var password = Membership.GeneratePassword(8, 1);
            var hashPassword = new Md5Ecryption().ConvertStringToMd5Hash(password.Trim());
            appUser.Password = new RemoveCharacters().RemoveSpecialCharacters(hashPassword);

            //add and save user
            _dbAppUser.AppUsers.Add(appUser);
            _dbAppUser.SaveChanges();

            return View("ListOfEmployees", employees);
        }

        #endregion

        #region Edit Employee data

        // GET: EmployeeManagement/EditPersonalData/5
        public ActionResult EditPersonalData(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var employeePersonalData = _dbEmployee.EmployeePersonalDatas.SingleOrDefault(n => n.EmployeeId == id);
            if (employeePersonalData == null)
                return HttpNotFound();
            ViewBag.State = new SelectList(_db.States, "StateId", "Name");
            return View(employeePersonalData);
        }

        // POST: EmployeeManagement/EditPersonalData/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPersonalData(
            [Bind(
                 Include =
                     "EmployeePersonalDataId,Firstname,Middlename,Lastname,PlaceOfBirth,PrimaryAddress,SecondaryAddress,Gender,StateId,LgaId,PostalCode,HomePhone,MobilePhone,WorkPhone,Email,MaritalStatus,EmployeeImage,EmployeeId"
             )] EmployeePersonalData employeePersonalData, FormCollection collectedValues)
        {
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            if (ModelState.IsValid)
            {
                employeePersonalData.DateOfBirth = Convert.ToDateTime(collectedValues["DateOfBirth"]);
                var employeeId = Convert.ToInt64(collectedValues["EmployeeId"]);
                var employee = _dbEmployee.Employees.Find(employeeId);
                employee.DateLastModified = DateTime.Now;
                if (loggedinuser != null) employee.LastModifiedBy = loggedinuser.AppUserId;
                _dbEmployee.Entry(employeePersonalData).State = EntityState.Modified;
                _dbEmployee.Entry(employee).State = EntityState.Modified;
                _dbEmployee.SaveChanges();
                return RedirectToAction("Dashboard", "Home");
            }
            return View(employeePersonalData);
        }

        // GET: EmployeeManagement/EditMedicalData
        public ActionResult EditMedicalData(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var employeeMedicalData = _dbEmployee.EmployeeMedicalDatas.SingleOrDefault(n => n.EmployeeId == id);
            if (employeeMedicalData == null)
                return HttpNotFound();
            return View(employeeMedicalData);
        }

        // POST: EmployeeManagement/EditMedicalData
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditMedicalData([Bind(
                                                 Include =
                                                     "EmployeeMedicalDataId")] EmployeeMedicalData medicalData,
            FormCollection collectedValues)
        {
            var employeeId = collectedValues["EmployeeId"];
            //medical data
            medicalData.BloodGroup = typeof(BloodGroup).GetEnumName(int.Parse(collectedValues["BloodGroup"]));
            medicalData.Genotype = typeof(Genotype).GetEnumName(int.Parse(collectedValues["Genotype"]));
            medicalData.EmployeeId = Convert.ToInt64(employeeId);

            //update data
            _dbEmployee.Entry(medicalData).State = EntityState.Modified;
            _dbEmployee.SaveChanges();

            return RedirectToAction("Dashboard", "Home");
        }

        // GET: EmployeeManagement/EditWorkData
        public ActionResult EditWorkData(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var employeeWorkData = _dbEmployee.EmployeeWorkDatas.SingleOrDefault(n => n.EmployeeId == id);
            if (employeeWorkData == null)
                return HttpNotFound();
            return View(employeeWorkData);
        }

        // POST: EmployeeManagement/EditWorkData
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditWorkData([Bind(
                                              Include =
                                                  "EmployeeWorkDataId,EmploymentDate,EmployeeId,PositionHeld")] EmployeeWorkData workData,
            FormCollection collectedValues)
        {
            //medical data
            workData.EmploymentStatus =
                typeof(EmploymentStatus).GetEnumName(int.Parse(collectedValues["EmploymentStatus"]));
            workData.EmploymentType = typeof(EmploymentType).GetEnumName(int.Parse(collectedValues["EmploymentType"]));
            workData.Category = typeof(EmploymentType).GetEnumName(int.Parse(collectedValues["Category"]));

            //update data
            _dbEmployee.Entry(workData).State = EntityState.Modified;
            _dbEmployee.SaveChanges();

            return RedirectToAction("Dashboard", "Home");
        }

        #endregion

        #region List of employee data

        // GET: EmployeeManagement/ListOfEducationalQualification
        public ActionResult ListOfEducationalQualification(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var educationalQualification = _dbEmployee.EmployeeEducationalQualifications.Where(n => n.EmployeeId == id);
            return View(educationalQualification);
        }

        // GET: EmployeeManagement/ListOfPastWorkExperience
        public ActionResult ListOfPastWorkExperience(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var pastWorkExperience = _dbEmployee.EmployeePastWorkExperiences.Where(n => n.EmployeeId == id);
            return View(pastWorkExperience);
        }

        // GET: EmployeeManagement/ListOfEmployeeFamily
        public ActionResult ListOfEmployeeFamily(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var employeeFamily = _dbEmployee.EmployeeFamilyDatas.Where(n => n.EmployeeId == id);
            return View(employeeFamily);
        }

        // GET: EmployeeManagement/ListOfBankData
        public ActionResult ListOfBankData(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var bankData = _dbEmployee.EmployeeBankDatas.Where(n => n.EmployeeId == id);
            ViewBag.Banks = new SelectList(_dbBanks.Banks, "BankId", "Name");
            return View(bankData);
        }

        #endregion
    }
}