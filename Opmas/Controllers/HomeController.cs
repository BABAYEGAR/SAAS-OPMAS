using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Opmas.Data.DataContext.DataContext.SystemDataContext;
using Opmas.Data.Factory.ApplicationManagement;
using Opmas.Data.Service.Enums;

namespace Opmas.Controllers
{
    public class HomeController : Controller
    {
        private readonly InstitutionDataContext _db = new InstitutionDataContext();
        public ActionResult SelectInstitution()
        {
            Session["institution"] = null;
            ViewBag.Institutions = new SelectList(_db.Institutions, "InstitutionId", "Name");
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult SelectInstitution(FormCollection collectedValues)
        {
            var institutionId = Convert.ToInt64(collectedValues["InstitutionId"]);
            var accessCode = collectedValues["AccessCode"];
            var institution = _db.Institutions.Find(institutionId);
            if (institution.AccessCode == accessCode)
            {
                Session["institution"] = institution;
            }
            else
            {
                ViewBag.Institutions = new SelectList(_db.Institutions, "InstitutionId", "Name");
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
        public ActionResult CheckOut()
        {
            return View();
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