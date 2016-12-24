using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Opmas.Data.DataContext.DataContext.AccessDataContext;
using Opmas.Data.DataContext.DataContext.SystemDataContext;
using Opmas.Data.DataContext.DataContext.UserDataContext;
using Opmas.Data.Factory.ApplicationManagement;
using Opmas.Data.Objects.Entities.SystemManagement;
using Opmas.Data.Objects.Entities.User;
using Opmas.Data.Service.Enums;

namespace Opmas.Controllers
{
    public class HomeController : Controller
    {
        private readonly InstitutionDataContext _dbInstitution = new InstitutionDataContext();
        private readonly PackageDataContext _dbPackage = new PackageDataContext();
        private readonly AppUserDataContext _dbAppUser = new AppUserDataContext();
        public ActionResult SelectInstitution()
        {
            Session["institution"] = null;
            ViewBag.Institutions = new SelectList(_dbInstitution.Institutions, "InstitutionId", "Name");
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult SelectInstitution(FormCollection collectedValues)
        {
            var institutionId = Convert.ToInt64(collectedValues["InstitutionId"]);
            var accessCode = collectedValues["AccessCode"];
            var institution = _dbInstitution.Institutions.Find(institutionId);
            if (institution.AccessCode == accessCode)
            {
                Session["institution"] = institution;
            }
            else
            {
                ViewBag.Institutions = new SelectList(_dbInstitution.Institutions, "InstitutionId", "Name");
                TempData["access"] = "Access code doesn't match institution!Try Again";
                TempData["notificationType"] = NotificationTypeEnum.Error.ToString();
                return View();
            }
          
            return RedirectToAction("Login","Account");
        }
        public ActionResult SystemAdminIndex()
        {
            return View();
        }
        public ActionResult InstitutionAdminIndex()
        {
            return View();
        }
        // GET: Index
        public ActionResult EmployeeIndex()
        {
            return View();
        }
        // GET: CheckOut
        public ActionResult CheckOut(long? id)
        {
            var package = _dbPackage.Packages.Find(id);
            Session["package"] = package;
            return View();
        }
        // GET: CheckOut
        public ActionResult CheckOut()
        {
            var institution = Session["institution"] as Institution;
            AppUser appUser = new AppUser();
            if (institution != null)
            {
                appUser.Firstname = institution.Name;
                appUser.Lastname = institution.Name;
                appUser.Email = institution.ContactEmail;
                appUser.InstitutionId = institution.InstitutionId;
                appUser.Mobile = institution.ContactNumber;
                appUser.EmployeeId = null;
                appUser.Role = AdminUserType.InstitutionAdministrator.ToString();
            }
            appUser.Password = Membership.GeneratePassword(6,0);
            _dbAppUser.AppUsers.Add(appUser);
            _dbAppUser.SaveChanges();
            return RedirectToAction("Login","Account");
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}