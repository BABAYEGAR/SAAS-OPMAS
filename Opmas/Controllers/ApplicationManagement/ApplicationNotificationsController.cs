using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Opmas.Data.DataContext.DataContext.SystemDataContext;
using Opmas.Data.Objects.Entities.SystemManagement;
using Opmas.Data.Objects.Entities.User;

namespace Opmas.Controllers.ApplicationManagement
{
    public class ApplicationNotificationsController : Controller
    {
        private ApplicationNotificationDataContext db = new ApplicationNotificationDataContext();

        // GET: ApplicationNotifications
        public ActionResult Index()
        {
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            var applicationNotifications = db.ApplicationNotifications.Where(n=>n.InstitutionId== loggedinuser.InstitutionId && n.AssignedTo == loggedinuser.EmployeeId).Include(a => a.Institution);
            return View(applicationNotifications.ToList());
        }
        // GET: ApplicationNotifications
        public ActionResult GetAllUserNotifications()
        {
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            var applicationNotifications = db.ApplicationNotifications.Where(n => n.InstitutionId == loggedinuser.InstitutionId && n.AssignedTo == loggedinuser.EmployeeId).Include(a => a.Institution);
            return View("Index",applicationNotifications.ToList());
        }

        // GET: ApplicationNotifications/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationNotification applicationNotification = db.ApplicationNotifications.Find(id);
            if (applicationNotification == null)
            {
                return HttpNotFound();
            }
            return View(applicationNotification);
        }

        // GET: ApplicationNotifications/Create
        public ActionResult Create()
        {
            ViewBag.InstitutionId = new SelectList(db.Institutions, "InstitutionId", "Name");
            return View();
        }

        // POST: ApplicationNotifications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ApplicationNotificationId,ItemId,Description,NotificationType,Read,DateCreated,CreatedBy,AssignedTo,InstitutionId")] ApplicationNotification applicationNotification)
        {
            if (ModelState.IsValid)
            {
                db.ApplicationNotifications.Add(applicationNotification);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.InstitutionId = new SelectList(db.Institutions, "InstitutionId", "Name", applicationNotification.InstitutionId);
            return View(applicationNotification);
        }

        // GET: ApplicationNotifications/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationNotification applicationNotification = db.ApplicationNotifications.Find(id);
            if (applicationNotification == null)
            {
                return HttpNotFound();
            }
            ViewBag.InstitutionId = new SelectList(db.Institutions, "InstitutionId", "Name", applicationNotification.InstitutionId);
            return View(applicationNotification);
        }

        // POST: ApplicationNotifications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ApplicationNotificationId,ItemId,Description,NotificationType,Read,DateCreated,CreatedBy,AssignedTo,InstitutionId")] ApplicationNotification applicationNotification)
        {
            if (ModelState.IsValid)
            {
                db.Entry(applicationNotification).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InstitutionId = new SelectList(db.Institutions, "InstitutionId", "Name", applicationNotification.InstitutionId);
            return View(applicationNotification);
        }

        // GET: ApplicationNotifications/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationNotification applicationNotification = db.ApplicationNotifications.Find(id);
            if (applicationNotification == null)
            {
                return HttpNotFound();
            }
            return View(applicationNotification);
        }

        // POST: ApplicationNotifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            ApplicationNotification applicationNotification = db.ApplicationNotifications.Find(id);
            db.ApplicationNotifications.Remove(applicationNotification);
            db.SaveChanges();
            return RedirectToAction("Index");
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
