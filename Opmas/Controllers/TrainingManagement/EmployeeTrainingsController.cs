using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Opmas.Data.DataContext.DataContext.EmployeeDataContext;
using Opmas.Data.Objects.Entities.User;
using System;
using Opmas.Data.DataContext.DataContext.MappingDataContext;
using Opmas.Data.Objects.Mappings;
using Opmas.Data.Objects.Training;

namespace Opmas.Controllers.TrainingManagement
{
    public class EmployeeTrainingsController : Controller
    {
        private EmployeeTrainingDataContext db = new EmployeeTrainingDataContext();
        private EmployeeTrainingMappingDataContext dbc = new EmployeeTrainingMappingDataContext();

        // GET: EmployeeTrainings
        public ActionResult Index()
        {
            var employeeTrainings = db.EmployeeTrainings;
           
            return View(employeeTrainings.ToList());
        }

        // GET: AttendeeList
        public ActionResult AttendeeList(long id)
        {
            var user = Session["opmasloggedinuser"] as AppUser;
            var employeeTrainings = db.Employees.Where(n=>n.InstitutionId == user.InstitutionId);
            ViewBag.Id = id;
             
            return View(employeeTrainings.ToList());
        }
        // GET: AttendeeList
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitAttendeeList(int[] selected,FormCollection collectedValues)
        {
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            long trainingId = Convert.ToInt64(collectedValues["id"]);
            var length = selected.Length;
            for (var i = 0; i < length; i++)
            {
                var id =  selected[i];
                if (loggedinuser?.InstitutionId != null)
                {
                    var trainingMapping = new EmployeeTrainingMapping
                    {
                        EmployeeId = id,
                        EmployeeTrainingId = trainingId,
                        InstitutionId = (long) loggedinuser.InstitutionId,
                        DateCreated = DateTime.Now,
                        DateLastModified = DateTime.Now,
                        LastModifiedBy = loggedinuser.AppUserId,
                        CreatedBy = loggedinuser.AppUserId
                    };
                    dbc.EmployeeTrainingMappings.Add(trainingMapping);
                    dbc.SaveChanges();
                }
                
            }
            return RedirectToAction("AttendeeList");
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
            ViewBag.TrainingCategoryId = new SelectList(db.TrainingCategory, "TrainingCategoryId", "Name");
            return View();
        }

        // POST: EmployeeTrainings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeTrainingId,Title,Location,StartDate,EndDate,StartTime,EndTime,CoordinatorFullname,CoordinatorCompany,TrainingCategoryId")] EmployeeTraining employeeTraining)
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
        public ActionResult Edit([Bind(Include = "EmployeeTrainingId,TrainingCategoryId,InstitutionId,Title,Location,StartDate,EndDate,StartTime,EndTime,CoordinatorFullname,CoordinatorCompany,CreatedBy,DateCreated")] EmployeeTraining employeeTraining)
        {
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            if (ModelState.IsValid)
            {
                employeeTraining.DateLastModified = DateTime.Now;
                if (loggedinuser != null) employeeTraining.LastModifiedBy = loggedinuser.AppUserId;
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
