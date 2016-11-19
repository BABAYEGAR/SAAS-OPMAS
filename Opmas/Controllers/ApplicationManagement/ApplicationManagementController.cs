using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Opmas.Controllers.ApplicationManagement
{
    public class ApplicationManagementController : Controller
    {
        // GET: Admin
        public ActionResult AdminDashboard()
        {
            return View();
        }
    }
}