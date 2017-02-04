using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Opmas.Data.DataContext.DataContext.EmployeeDataContext;
using Opmas.Data.DataContext.DataContext.MappingDataContext;
using Opmas.Data.Objects.Entities.User;
using Opmas.Data.Objects.Mappings;
using Opmas.Data.Objects.Training;
using Opmas.Data.Service.Enums;

namespace Opmas.Controllers.TrainingManagement
{
    public class EmployeeTrainingsController : Controller
    {
        private readonly EmployeeTrainingDataContext _db = new EmployeeTrainingDataContext();
        private readonly EmployeeTrainingMappingDataContext _dbc = new EmployeeTrainingMappingDataContext();

        // GET: EmployeeTrainings
        public ActionResult Index()
        {
            var employeeTrainings = _db.EmployeeTrainings;

            return View(employeeTrainings.ToList());
        }

        // GET: AttendeeList
        public ActionResult AttendeeList(long id)
        {
            var user = Session["opmasloggedinuser"] as AppUser;
            var employeeTrainings = _db.Employees.Where(n => n.InstitutionId == user.InstitutionId);
            ViewBag.Id = id;

            return View(employeeTrainings.ToList());
        }

        // GET: AttendeeList
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitAttendeeList(int[] table_records, FormCollection collectedValues)
        {
            var allMappings = _dbc.EmployeeTrainingMappings.ToList();
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            var trainingId = Convert.ToInt64(collectedValues["id"]);
            if (table_records != null)
            {
                var length = table_records.Length;
                for (var i = 0; i < length; i++)
                {
                    var id = table_records[i];
                    if (allMappings.Any(n => (n.EmployeeId == id) && (n.InstitutionId == loggedinuser?.InstitutionId) && n.EmployeeTrainingId == trainingId))
                    {

                    }
                    else
                    {
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
                            _dbc.EmployeeTrainingMappings.Add(trainingMapping);
                            _dbc.SaveChanges();
                            TempData["training"] = "you have succesfully added the employee(s) to the training event!";
                            TempData["notificationtype"] = NotificationTypeEnum.Success.ToString();

                        }
                        else
                        {
                            TempData["login"] = "Session has expired, Login and try again!";
                            TempData["notificationtype"] = NotificationTypeEnum.Success.ToString();
                            return RedirectToAction("Login","Account");
                        }
                    }
                }
            }
            else
            {
                TempData["training"] = "no employee has ben selected!";
                TempData["notificationtype"] = NotificationTypeEnum.Error.ToString();
                return RedirectToAction("AttendeeList", new { id = trainingId });
            }
            return RedirectToAction("AttendeeList",new {id = trainingId});

        }

        // GET: EmployeeTrainings/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var employeeTraining = _db.EmployeeTrainings.Find(id);
            if (employeeTraining == null)
                return HttpNotFound();
            return View(employeeTraining);
        }

        // GET: EmployeeTrainings/Create
        public ActionResult Create()
        {
            ViewBag.TrainingCategoryId = new SelectList(_db.TrainingCategory, "TrainingCategoryId", "Name");
            return View();
        }

        // POST: EmployeeTrainings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(
                 Include =
                     "EmployeeTrainingId,Title,Location,StartDate,EndDate,StartTime,EndTime,CoordinatorFullname,CoordinatorCompany,TrainingCategoryId"
             )] EmployeeTraining employeeTraining)
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
                _db.EmployeeTrainings.Add(employeeTraining);
                _db.SaveChanges();
                TempData["training"] = "you have succesfully created a new training event!";
                TempData["notificationtype"] = NotificationTypeEnum.Success.ToString();
                return RedirectToAction("Index");
            }

            return View(employeeTraining);
        }

        // GET: EmployeeTrainings/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var employeeTraining = _db.EmployeeTrainings.Find(id);
            if (employeeTraining == null)
                return HttpNotFound();
            return View(employeeTraining);
        }

        // POST: EmployeeTrainings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(
                 Include =
                     "EmployeeTrainingId,TrainingCategoryId,InstitutionId,Title,Location,StartDate,EndDate,StartTime,EndTime,CoordinatorFullname,CoordinatorCompany,CreatedBy,DateCreated"
             )] EmployeeTraining employeeTraining)
        {
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            if (ModelState.IsValid)
            {
                employeeTraining.DateLastModified = DateTime.Now;
                if (loggedinuser != null) employeeTraining.LastModifiedBy = loggedinuser.AppUserId;
                _db.Entry(employeeTraining).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employeeTraining);
        }

        // GET: EmployeeTrainings/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var employeeTraining = _db.EmployeeTrainings.Find(id);
            if (employeeTraining == null)
                return HttpNotFound();
            return View(employeeTraining);
        }

        // POST: EmployeeTrainings/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            var employeeTraining = _db.EmployeeTrainings.Find(id);
            _db.EmployeeTrainings.Remove(employeeTraining);
            _db.SaveChanges();
            TempData["training"] = "you have succesfully deleted a training event!";
            TempData["notificationtype"] = NotificationTypeEnum.Success.ToString();
            return RedirectToAction("Index");
        }
        // GET: EmployeeTrainings/RemoveMapping/5
        public ActionResult RemoveMapping(long id)
        {
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            var employeeTraining = _dbc.EmployeeTrainingMappings.SingleOrDefault(n=>n.EmployeeId == id && n.InstitutionId == loggedinuser.InstitutionId);
            long trainingId = 0;
            if (employeeTraining != null)
            {
                trainingId = employeeTraining.EmployeeTrainingId;
            }
            _dbc.EmployeeTrainingMappings.Remove(employeeTraining);
            _dbc.SaveChanges();
            return RedirectToAction("AttendeeList",new {id = trainingId});
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _db.Dispose();
            base.Dispose(disposing);
        }
    }
}