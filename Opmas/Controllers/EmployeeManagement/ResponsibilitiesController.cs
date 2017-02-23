using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Opmas.Data.DataContext.DataContext.EmployeeDataContext;
using Opmas.Data.DataContext.DataContext.MappingDataContext;
using Opmas.Data.DataContext.DataContext.SystemDataContext;
using Opmas.Data.Objects.Entities.Employee;
using Opmas.Data.Objects.Entities.SystemManagement;
using Opmas.Data.Objects.Entities.User;
using Opmas.Data.Objects.Mappings;
using Opmas.Data.Service.Enums;

namespace Opmas.Controllers.EmployeeManagement
{
    public class ResponsibilitiesController : Controller
    {
        private readonly ResponsibilityDataContext db = new ResponsibilityDataContext();
        private readonly ResponsibilityDataContext _db = new ResponsibilityDataContext();
        private readonly EmployeeResponsibilityMappingDataContext _dbc = new EmployeeResponsibilityMappingDataContext();
        private readonly ApplicationNotificationDataContext dbc = new ApplicationNotificationDataContext();
        private readonly RoleDataContext _dbd = new RoleDataContext();

        // GET: Responsibilities
        public ActionResult Index()
        {
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            var responsibilities = db.Responsibilities.Where(n=>n.InstitutionId == loggedinuser.InstitutionId).Include(r => r.Institution);
            return View(responsibilities.ToList());
        }
        // GET: AppointeeList
        public ActionResult AppointeeList(long? id)
        {
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            var allEmployees = _db.Employees.Where(n => n.InstitutionId == loggedinuser.InstitutionId)
                .Include(n => n.Department);
            List<Employee> attendingmployees = new List<Employee>();
            foreach (var item in allEmployees.Include(n=>n.Department))
            {
                var role = _dbd.Roles.Find(item.RoleId);
                if ((role.Name != "Platform Administrator") || (role.Name != "Institution Administrator"))
                {
                    attendingmployees.Add(item);
                }

            }
            ViewBag.Id = id;
            return View(attendingmployees);
        }

        // POST: SubmitAppointeeList
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitAppointeeList(int[] table_records, FormCollection collectedValues)
        {
            var allMappings = _dbc.EmployeeResponsibilityMappings.ToList();
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            var responsibilityId = Convert.ToInt64(collectedValues["id"]);
            if (table_records != null)
            {
                var length = table_records.Length;
                for (var i = 0; i < length; i++)
                {
                    var id = table_records[i];
                    if (
                        allMappings.Any(
                            n =>
                                (n.EmployeeId == id) && (n.InstitutionId == loggedinuser?.InstitutionId) &&
                                (n.ResponsibilityId == responsibilityId)))
                    {
                    }
                    else
                    {
                        if (loggedinuser?.InstitutionId != null)
                        {
                            var responsibilityMapping = new EmployeeResponsibilityMapping
                            {
                                EmployeeId = id,
                                ResponsibilityId = responsibilityId,
                                InstitutionId = (long)loggedinuser.InstitutionId,
                                DateCreated = DateTime.Now,
                                DateLastModified = DateTime.Now,
                                LastModifiedBy = loggedinuser.AppUserId,
                                CreatedBy = loggedinuser.AppUserId,
                            };
                            _dbc.EmployeeResponsibilityMappings.Add(responsibilityMapping);
                            _dbc.SaveChanges();
                            ApplicationNotification notify = new ApplicationNotification
                            {
                                AssignedTo = responsibilityMapping.EmployeeId,
                                Description = "You have been appointed!",
                                CreatedBy = loggedinuser.AppUserId,
                                DateCreated = DateTime.Now,
                                InstitutionId = loggedinuser.InstitutionId,
                                NotificationType = ApplicationNotificationType.Appointment.ToString(),
                                ItemId = responsibilityMapping.ResponsibilityId,
                                Read = false
                            };
                            dbc.ApplicationNotifications.Add(notify);
                            dbc.SaveChanges();

                            TempData["responsibility"] = "you have succesfully appointed the employee(s)!";
                            TempData["notificationtype"] = NotificationTypeEnum.Success.ToString();
                        }
                        else
                        {
                            TempData["login"] = "Session has expired, Login and try again!";
                            TempData["notificationtype"] = NotificationTypeEnum.Success.ToString();
                            return RedirectToAction("Login", "Account");
                        }
                    }
                }
            }
            else
            {
                TempData["responsibility"] = "no employee has been selected!";
                TempData["notificationtype"] = NotificationTypeEnum.Error.ToString();
                return RedirectToAction("AppointeeList", new { id = responsibilityId });
            }
            return RedirectToAction("AppointeeList", new { id = responsibilityId });
        }

        // GET: Responsibilities/Details/5
        public ActionResult Details(long? id,long? readId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Responsibility responsibility = db.Responsibilities.Find(id);
            if (responsibility == null)
            {
                return HttpNotFound();
            }
            ViewBag.ReadId = readId;
            return View(responsibility);
        }

        // GET: Responsibilities/Create
        public ActionResult Create()
        {
            ViewBag.InstitutionId = new SelectList(db.Institutions, "InstitutionId", "Name");
            return View();
        }

        // POST: Responsibilities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ResponsibilityId,Name")] Responsibility responsibility)
        {
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            if (ModelState.IsValid)
            {
                responsibility.DateCreated = DateTime.Now;
                responsibility.DateLastModified = DateTime.Now;
                if (loggedinuser != null)
                {
                    responsibility.CreatedBy = loggedinuser.AppUserId;
                    responsibility.LastModifiedBy = loggedinuser.AppUserId;
                    if (loggedinuser.InstitutionId != null)
                        responsibility.InstitutionId = (long)loggedinuser.InstitutionId;
                }
                db.Responsibilities.Add(responsibility);
                db.SaveChanges();
                TempData["responsibility"] = "you have succesfully created a new appointment responsibility!";
                TempData["notificationtype"] = NotificationTypeEnum.Success.ToString();
                return RedirectToAction("Index");
            }
            return View(responsibility);
        }

        // GET: Responsibilities/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Responsibility responsibility = db.Responsibilities.Find(id);
            if (responsibility == null)
            {
                return HttpNotFound();
            }
            return View(responsibility);
        }

        // POST: Responsibilities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ResponsibilityId,Name,InstitutionId,CreatedBy,DateCreated")] Responsibility responsibility)
        {
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            if (ModelState.IsValid)
            {
                responsibility.DateLastModified = DateTime.Now;
                if (loggedinuser != null)
                {
                    
                    responsibility.LastModifiedBy = loggedinuser.AppUserId;
                }
                db.Entry(responsibility).State = EntityState.Modified;
                db.SaveChanges();
                TempData["responsibility"] = "you have succesfully modified the appointment responsibility!";
                TempData["notificationtype"] = NotificationTypeEnum.Success.ToString();
                return RedirectToAction("Index");
            }
            return View(responsibility);
        }

        // GET: Responsibilities/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Responsibility responsibility = db.Responsibilities.Find(id);
            if (responsibility == null)
            {
                return HttpNotFound();
            }
            return View(responsibility);
        }

        // POST: Responsibilities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Responsibility responsibility = db.Responsibilities.Find(id);
            db.Responsibilities.Remove(responsibility);
            db.SaveChanges();
            TempData["responsibility"] = "you have succesfully deleted the appointment responsibility!";
            TempData["notificationtype"] = NotificationTypeEnum.Success.ToString();
            return RedirectToAction("Index");
        }
        // GET: Responsibilities/RemoveMapping/5
        public ActionResult RemoveMapping(long id)
        {
            var responsibility = Session["responsibility"] as Responsibility;
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            var employeeResponsibility =
                _dbc.EmployeeResponsibilityMappings.SingleOrDefault(
                    n =>
                        (n.EmployeeId == id) && (n.InstitutionId == loggedinuser.InstitutionId) &&
                        (n.ResponsibilityId == responsibility.ResponsibilityId));
            long responsibilityId = 0;
            if (employeeResponsibility != null)
                if (responsibility != null) responsibilityId = responsibility.ResponsibilityId;
            _dbc.EmployeeResponsibilityMappings.Remove(employeeResponsibility);
            _dbc.SaveChanges();
            Session["responsibility"] = null;
            return RedirectToAction("AppointeeList", new { id = responsibilityId });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
