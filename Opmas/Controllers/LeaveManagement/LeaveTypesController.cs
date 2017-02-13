using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Opmas.Data.DataContext.DataContext.LeaveDataContext;
using Opmas.Data.Objects.Entities.User;
using Opmas.Data.Objects.LeaveManagement;
using Opmas.Data.Service.Enums;

namespace Opmas.Controllers.LeaveManagement
{
    public class LeaveTypesController : Controller
    {
        private LeaveTypeDataContext db = new LeaveTypeDataContext();

        // GET: LeaveTypes
        public ActionResult Index()
        {
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            var leaveTypes = db.LeaveTypes.Where(n=>n.InstitutionId == loggedinuser.InstitutionId).Include(l => l.Institution);
            return View(leaveTypes.ToList());
        }

        // GET: LeaveTypes/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LeaveType leaveType = db.LeaveTypes.Find(id);
            if (leaveType == null)
            {
                return HttpNotFound();
            }
            return View(leaveType);
        }

        // GET: LeaveTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LeaveTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LeaveTypeId,Name,Description")] LeaveType leaveType)
        {
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            if (ModelState.IsValid)
            {
                leaveType.DateCreated = DateTime.Now;
                leaveType.DateLastModified = DateTime.Now;
                if (loggedinuser != null)
                {
                    leaveType.CreatedBy = loggedinuser.AppUserId;
                    leaveType.LastModifiedBy = loggedinuser.AppUserId;
                    if (loggedinuser.InstitutionId != null)
                        leaveType.InstitutionId = (long)loggedinuser.InstitutionId;
                }
                db.LeaveTypes.Add(leaveType);
                db.SaveChanges();
                TempData["leavetype"] = "you have succesfully added a new leave type!";
                TempData["notificationtype"] = NotificationTypeEnum.Success.ToString();
                return RedirectToAction("Index");
            }

            ViewBag.InstitutionId = new SelectList(db.Institutions, "InstitutionId", "Name", leaveType.InstitutionId);
            return View(leaveType);
        }

        // GET: LeaveTypes/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LeaveType leaveType = db.LeaveTypes.Find(id);
            if (leaveType == null)
            {
                return HttpNotFound();
            }
            return View(leaveType);
        }

        // POST: LeaveTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LeaveTypeId,Name,InstitutionId,CreatedBy,DateCreated,Description")] LeaveType leaveType)
        {
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            if (ModelState.IsValid)
            {
                leaveType.DateLastModified = DateTime.Now;
                if (loggedinuser != null) leaveType.LastModifiedBy = loggedinuser.AppUserId;
                db.Entry(leaveType).State = EntityState.Modified;
                db.SaveChanges();
                TempData["leavetype"] = "you have succesfully modified the leave type!";
                TempData["notificationtype"] = NotificationTypeEnum.Success.ToString();
                return RedirectToAction("Index");
            }
            return View(leaveType);
        }

        // GET: LeaveTypes/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LeaveType leaveType = db.LeaveTypes.Find(id);
            if (leaveType == null)
            {
                return HttpNotFound();
            }
            return View(leaveType);
        }

        // POST: LeaveTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            LeaveType leaveType = db.LeaveTypes.Find(id);
            db.LeaveTypes.Remove(leaveType);
            db.SaveChanges();
            TempData["leavetype"] = "you have succesfully deleted the leave type!";
            TempData["notificationtype"] = NotificationTypeEnum.Success.ToString();
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
