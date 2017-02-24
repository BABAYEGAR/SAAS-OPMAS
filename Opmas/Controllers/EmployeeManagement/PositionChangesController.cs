using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Opmas.Data.DataContext.DataContext.EmployeeDataContext;
using Opmas.Data.Objects.Entities.Employee;
using Opmas.Data.Objects.Entities.SystemManagement;
using Opmas.Data.Objects.Entities.User;
using Opmas.Data.Service.Enums;

namespace Opmas.Controllers.EmployeeManagement
{
    public class PositionChangesController : Controller
    {
        private PositionChangeDataContext db = new PositionChangeDataContext();
        private EmployeeDataContext dbc = new EmployeeDataContext();

        // GET: PositionChanges
        public ActionResult Index()
        {
            var positionChanges = db.PositionChanges.Include(p => p.Employee).Include(p => p.EmploymentPosition).Include(p => p.Institution);
            return View(positionChanges.ToList());
        }
        // GET: EmployeePositionChanges
        public ActionResult EmployeePositionChanges(long id)
        {
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            var positionChanges = db.PositionChanges.Where(n=>n.EmployeeId == id && n.InstitutionId == loggedinuser.InstitutionId).Include(p => p.Employee).Include(p => p.EmploymentPosition).Include(p => p.Institution);
            return View("Index",positionChanges.ToList());
        }

        // GET: PositionChanges/Details/5
        public ActionResult Details(long? id,long? readId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PositionChange positionChange = db.PositionChanges.Find(id);
            if (positionChange == null)
            {
                return HttpNotFound();
            }
            ViewBag.ReadId = readId;
            return View(positionChange);
        }

        // GET: PositionChanges/Create
        public ActionResult Create(long id,string promote,string demote)
        {
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            var employeeWorkData = dbc.EmployeeWorkDatas.SingleOrDefault(n => n.EmployeeId == id);
            var employmentPosition =
                dbc.EmploymentPositions.SingleOrDefault(
                    n => n.EmploymentPositionId == employeeWorkData.EmploymentPositionId);
            ViewBag.EmploymentPositionId =
                new SelectList(
                    db.EmploymentPositions.Where(
                        n => n.EmploymentPositionId != employmentPosition.EmploymentPositionId && n.InstitutionId == loggedinuser.InstitutionId), "EmploymentPositionId",
                    "Name");

            if (promote != null)
                ViewBag.action = promote;

            if (demote != null)
                ViewBag.action = demote;


                ViewBag.id = id;
            return View();
        }

        // POST: PositionChanges/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PositionChangeId,EmployeeId,EmploymentPositionId")] PositionChange positionChange,FormCollection collectedValues)
        {
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            long employeeId = Convert.ToInt64(collectedValues["EmployeeId"]);
            if (ModelState.IsValid)
            {
                positionChange.DateCreated = DateTime.Now;
                positionChange.DateLastModified = DateTime.Now;
                var status = collectedValues["Action"];
                positionChange.EmployeeId = Convert.ToInt64(collectedValues["EmployeeId"]);
                
                if (status == PositionChangeEnum.Demotion.ToString())
                {
                    positionChange.Action = PositionChangeEnum.Demotion.ToString();
                }
                if (status == PositionChangeEnum.Promotion.ToString())
                {
                    positionChange.Action = PositionChangeEnum.Promotion.ToString();
                }
                if (loggedinuser != null)
                {
                    positionChange.CreatedBy = loggedinuser.AppUserId;
                    positionChange.LastModifiedBy = loggedinuser.AppUserId;
                    if (loggedinuser.InstitutionId != null)
                        positionChange.InstitutionId = (long)loggedinuser.InstitutionId;
                   
                }
                var workData = dbc.EmployeeWorkDatas.SingleOrDefault(n => n.EmployeeId == employeeId);
                var employee = dbc.Employees.Find(employeeId);
                
                employee.DateLastModified = DateTime.Now;
                if (loggedinuser != null) employee.LastModifiedBy = loggedinuser.AppUserId;
                if (workData != null)
                {
                    long? previousPosition = workData.EmploymentPositionId;
                    if (previousPosition != null) positionChange.PreviousPositionId = (long) previousPosition;
                    workData.EmploymentPositionId = positionChange.EmploymentPositionId;
                    dbc.Entry(workData).State = EntityState.Modified;
                    dbc.Entry(employee).State = EntityState.Modified;
                    dbc.SaveChanges();
                    db.PositionChanges.Add(positionChange);
                    db.SaveChanges();
                }
                if (loggedinuser != null)
                {
                    ApplicationNotification notify = new ApplicationNotification
                    {
                        AssignedTo = positionChange.EmployeeId,
                        CreatedBy = loggedinuser.AppUserId,
                        DateCreated = DateTime.Now,
                        InstitutionId = loggedinuser.InstitutionId,
                        NotificationType = ApplicationNotificationType.PositionChange.ToString(),
                        ItemId = positionChange.PositionChangeId,
                        Read = false
                    };
                    if (status == PositionChangeEnum.Promotion.ToString())
                    {
                        notify.Description = "You have been Promoted!";
                    }
                    if (status == PositionChangeEnum.Demotion.ToString())
                    {
                        notify.Description = "You have been Demoted!";
                    }
                    dbc.ApplicationNotifications.Add(notify);
                }
                dbc.SaveChanges();

                if (status == PositionChangeEnum.Promotion.ToString())
                {
                    TempData["employee"] = "you have succesfully promoted the employee!";
                    TempData["notificationtype"] = NotificationTypeEnum.Success.ToString();
                }
                if (status == PositionChangeEnum.Demotion.ToString())
                {
                    
                    TempData["employee"] = "you have succesfully demoted the employee!";
                    TempData["notificationtype"] = NotificationTypeEnum.Success.ToString();
                }
                return RedirectToAction("EmployeesPositionChange","EmployeeManagement");
            }
            var employeeWorkData = dbc.EmployeeWorkDatas.SingleOrDefault(n => n.EmployeeId == employeeId);
            var employmentPosition =
                dbc.EmploymentPositions.SingleOrDefault(
                    n => n.EmploymentPositionId == employeeWorkData.EmploymentPositionId);
            ViewBag.EmploymentPositionId =
              new SelectList(
                  db.EmploymentPositions.Where(
                      n => n.EmploymentPositionId != employmentPosition.EmploymentPositionId && n.InstitutionId == loggedinuser.InstitutionId), "EmploymentPositionId",
                  "Name");
            return View(positionChange);
        }

        // GET: PositionChanges/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PositionChange positionChange = db.PositionChanges.Find(id);
            if (positionChange == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeId", positionChange.EmployeeId);
            ViewBag.EmploymentPositionId = new SelectList(db.EmploymentPositions, "EmploymentPositionId", "Name", positionChange.EmploymentPositionId);
            ViewBag.InstitutionId = new SelectList(db.Institutions, "InstitutionId", "Name", positionChange.InstitutionId);
            return View(positionChange);
        }

        // POST: PositionChanges/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PositionChangeId,Action,EmploymentPositionId,EmployeeId,InstitutionId,CreatedBy,DateCreated,DateLastModified,LastModifiedBy")] PositionChange positionChange)
        {
            if (ModelState.IsValid)
            {
                db.Entry(positionChange).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeId", positionChange.EmployeeId);
            ViewBag.EmploymentPositionId = new SelectList(db.EmploymentPositions, "EmploymentPositionId", "Name", positionChange.EmploymentPositionId);
            ViewBag.InstitutionId = new SelectList(db.Institutions, "InstitutionId", "Name", positionChange.InstitutionId);
            return View(positionChange);
        }

        // GET: PositionChanges/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PositionChange positionChange = db.PositionChanges.Find(id);
            if (positionChange == null)
            {
                return HttpNotFound();
            }
            return View(positionChange);
        }

        // POST: PositionChanges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            PositionChange positionChange = db.PositionChanges.Find(id);
            db.PositionChanges.Remove(positionChange);
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
