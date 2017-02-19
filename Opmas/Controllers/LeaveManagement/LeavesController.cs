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
    public class LeavesController : Controller
    {
        private LeaveDataContext db = new LeaveDataContext();

        // GET: Leaves
        public ActionResult Index()
        {
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            var leaves = db.Leaves.Where(n=>n.InstitutionId == loggedinuser.InstitutionId).Include(l => l.Employee).Include(l => l.Institution).Include(l => l.LeaveType);
            return View(leaves.ToList());
        }
        // GET: EmployeeLeave
        public ActionResult EmployeeLeave()
        {
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            var leaves = db.Leaves.Where(n => n.InstitutionId == loggedinuser.InstitutionId && n.EmployeeId == loggedinuser.EmployeeId).Include(l => l.Employee).Include(l => l.Institution).Include(l => l.LeaveType);
            return View(leaves.ToList());
        }


        // GET: Leaves/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Leave leave = db.Leaves.Find(id);
            if (leave == null)
            {
                return HttpNotFound();
            }
            return View(leave);
        }

        // GET: Leaves/Create
        public ActionResult Create()
        {
            ViewBag.LeaveTypeId = new SelectList(db.LeaveTypes, "LeaveTypeId", "Name");
            return View();
        }

        // POST: Leaves/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LeaveId,Reason,LeaveTypeId,StartDate,EndDate,Comment")] Leave leave,FormCollection collectedValues)
        {

            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            if (ModelState.IsValid)
            {
                leave.DateCreated = DateTime.Now;
                leave.DateLastModified = DateTime.Now;
                if (loggedinuser != null)
                {
                    leave.CreatedBy = loggedinuser.AppUserId;
                    leave.LastModifiedBy = loggedinuser.AppUserId;
                    if (loggedinuser.InstitutionId != null)
                    {
                        leave.InstitutionId = (long) loggedinuser.InstitutionId;
                        if (loggedinuser.EmployeeId != null) leave.EmployeeId = (long) loggedinuser.EmployeeId;
                    }
                }
                db.Leaves.Add(leave);
                db.SaveChanges();
                TempData["leave"] = "you have succesfully applied for a leave!";
                TempData["notificationtype"] = NotificationTypeEnum.Success.ToString();
                return RedirectToAction("Index");
            }
            ViewBag.LeaveTypeId = new SelectList(db.LeaveTypes, "LeaveTypeId", "Name", leave.LeaveTypeId);
            return View(leave);
        }
        // POST: Leaves/ApproveLeave
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ApproveLeave(long? id)
        {

            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            if (ModelState.IsValid)
            {
                var leave = db.Leaves.Find(id);
                var leaveType = db.LeaveTypes.Find(leave.LeaveTypeId);
                leave.Status = LeaveStatus.Approved.ToString();
                    if (leaveType.DurationIn == DurationType.Day.ToString())
                    {
                        leave.StartDate = DateTime.Now;
                        leave.EndDate = leave.StartDate?.AddDays(leaveType.Duration);
                    }
                    if (leaveType.DurationIn == DurationType.Week.ToString())
                    {
                        leave.StartDate = DateTime.Now;
                        leave.EndDate = leave.StartDate?.AddDays(leaveType.Duration);
                    }
                    if (leaveType.DurationIn == DurationType.Year.ToString())
                    {
                        leave.StartDate = DateTime.Now;
                        leave.EndDate = leave.StartDate?.AddDays(leaveType.Duration);
                    }
                
                leave.DateLastModified = DateTime.Now;
                    if (loggedinuser != null)
                    {
                        leave.LastModifiedBy = loggedinuser.AppUserId;
                    }
                
                db.Entry(leave).State = EntityState.Modified;
                db.SaveChanges();
                TempData["leave"] = "you have approved the leave!";
                TempData["notificationtype"] = NotificationTypeEnum.Success.ToString();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        // POST: Leaves/RejectLeave
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RejectLeave(long? id)
        {

            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            if (ModelState.IsValid)
            {
                var leave = db.Leaves.Find(id);
                leave.Status = LeaveStatus.Rejected.ToString();
                leave.DateLastModified = DateTime.Now;
                if (loggedinuser != null)
                {
                    leave.LastModifiedBy = loggedinuser.AppUserId;
                }

                db.Entry(leave).State = EntityState.Modified;
                db.SaveChanges();
                TempData["leave"] = "you have rejected the leave!";
                TempData["notificationtype"] = NotificationTypeEnum.Success.ToString();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        // POST: Leaves/RejectLeave
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CancelLeave(long? id)
        {

            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            if (ModelState.IsValid)
            {
                var leave = db.Leaves.Find(id);
                leave.Status = LeaveStatus.Cancelled.ToString();
                leave.DateLastModified = DateTime.Now;
                if (loggedinuser != null)
                {
                    leave.LastModifiedBy = loggedinuser.AppUserId;
                }

                db.Entry(leave).State = EntityState.Modified;
                db.SaveChanges();
                TempData["leave"] = "you have cancelled your leave!";
                TempData["notificationtype"] = NotificationTypeEnum.Success.ToString();
                return RedirectToAction("EmployeeLeave");
            }
            return RedirectToAction("Index");
        }
        // POST: Leaves/ApprovedLeaveByDepartment
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ApprovedLeaveByDepartment(long? id)
        {

            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            if (ModelState.IsValid)
            {
                var leave = db.Leaves.Find(id);
                leave.Status = LeaveStatus.Approved.ToString();
                leave.DateLastModified = DateTime.Now;
                if (loggedinuser != null)
                {
                    leave.LastModifiedBy = loggedinuser.AppUserId;
                }

                db.Entry(leave).State = EntityState.Modified;
                db.SaveChanges();
                TempData["leave"] = "you have approved the leave!";
                TempData["notificationtype"] = NotificationTypeEnum.Success.ToString();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        // GET: Leaves/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Leave leave = db.Leaves.Find(id);
            if (leave == null)
            {
                return HttpNotFound();
            }
            ViewBag.LeaveTypeId = new SelectList(db.LeaveTypes, "LeaveTypeId", "Name", leave.LeaveTypeId);
            return View(leave);
        }

        // POST: Leaves/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LeaveId,Reason,LeaveTypeId,EmployeeId,InstitutionId,Status,CreatedBy,DateCreated,DateLastModified,LastModifiedBy")] Leave leave)
        {
            if (ModelState.IsValid)
            {
                db.Entry(leave).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LeaveTypeId = new SelectList(db.LeaveTypes, "LeaveTypeId", "Name", leave.LeaveTypeId);
            return View(leave);
        }

        // GET: Leaves/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Leave leave = db.Leaves.Find(id);
            if (leave == null)
            {
                return HttpNotFound();
            }
            return View(leave);
        }

        // POST: Leaves/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Leave leave = db.Leaves.Find(id);
            db.Leaves.Remove(leave);
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
