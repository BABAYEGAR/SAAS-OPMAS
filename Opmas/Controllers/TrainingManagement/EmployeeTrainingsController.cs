using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Opmas.Data.DataContext.DataContext.EmployeeDataContext;
using Opmas.Data.Objects.Entities.Employee;
using Opmas.Data.Objects.Entities.User;
using System;
using Opmas.Data.Objects.Training;

namespace Opmas.Controllers.TrainingManagement
{
    public class EmployeeTrainingsController : Controller
    {
        private EmployeeTrainingDataContext db = new EmployeeTrainingDataContext();

        // GET: EmployeeTrainings
        public ActionResult Index()
        {
            var employeeTrainings = db.EmployeeTrainings;
            return View(employeeTrainings.ToList());
        }

        // GET: AttendeeList
        public ActionResult AttendeeList()
        {
            var user = Session["opmasloggedinuser"] as AppUser;
            var employeeTrainings = db.Employees.Where(n=>n.InstitutionId == user.InstitutionId); ;
            return View(employeeTrainings.ToList());
        }
        // GET: AttendeeList
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AttendeeList(FormCollection collectedValues)
        {
            var user = Session["opmasloggedinuser"] as AppUser;
            var employeeTrainings = db.EmployeeTrainings; ;
            return View(employeeTrainings.ToList());
        }
        // GET: EmployeeTrainings/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeTraining employeeTraining = db.EmployeeTrainings.Find(id);
            if (employeeTraining == null)
            {
                return HttpNotFound();
            }
            return View(employeeTraining);
        }

        // GET: EmployeeTrainings/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeId");
            return View();
        }

        // POST: EmployeeTrainings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeTrainingId,Title,Location,StartDate,EndDate,StartTime,EndTime,CoordinatorFullname,CoordinatorCompany")] EmployeeTraining employeeTraining)
        {
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            if (ModelState.IsValid)
            {
                employeeTraining.DateCreated = DateTime.Now;
                employeeTraining.DateLastModified = DateTime.Now;
                if (loggedinuser != null)
                {
                    employeeTraining.CreatedBy = loggedinuser.AppUserId;
                    employeeTraining.LastModifiedBy = loggedinuser.AppUserId;
                    if (loggedinuser.InstitutionId != null)
                        employeeTraining.InstitutionId = (long) loggedinuser.InstitutionId;
                }
                db.EmployeeTrainings.Add(employeeTraining);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employeeTraining);
        }

        // GET: EmployeeTrainings/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeTraining employeeTraining = db.EmployeeTrainings.Find(id);
            if (employeeTraining == null)
            {
                return HttpNotFound();
            }
            return View(employeeTraining);
        }

        // POST: EmployeeTrainings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeTrainingId,InstitutionId,Title,Location,StartDate,EndDate,StartTime,EndTime,CoordinatorFullname,CoordinatorCompany,CreatedBy,DateCreated")] EmployeeTraining employeeTraining)
        {
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            if (ModelState.IsValid)
            {
                employeeTraining.DateLastModified = DateTime.Now;
                employeeTraining.LastModifiedBy = loggedinuser.AppUserId;
                db.Entry(employeeTraining).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employeeTraining);
        }

        // GET: EmployeeTrainings/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeTraining employeeTraining = db.EmployeeTrainings.Find(id);
            if (employeeTraining == null)
            {
                return HttpNotFound();
            }
            return View(employeeTraining);
        }

        // POST: EmployeeTrainings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            EmployeeTraining employeeTraining = db.EmployeeTrainings.Find(id);
            db.EmployeeTrainings.Remove(employeeTraining);
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
