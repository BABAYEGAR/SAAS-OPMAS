using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Opmas.Data.DataContext.DataContext.EmployeeDataContext;
using Opmas.Data.Objects.Entities.Employee;
using Opmas.Data.Objects.Entities.User;
using Opmas.Data.Service.Enums;

namespace Opmas.Controllers.EmployeeManagement
{
    public class EmploymentPositionsController : Controller
    {
        private EmploymentPositionDataContext db = new EmploymentPositionDataContext();

        // GET: EmploymentPositions
        public ActionResult Index()
        {
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            var employmentPosition = db.EmploymentPosition.Where(n=>n.InstitutionId == loggedinuser.InstitutionId).Include(e => e.Institution);
            return View(employmentPosition.ToList());
        }

        // GET: EmploymentPositions/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmploymentPosition employmentPosition = db.EmploymentPosition.Find(id);
            if (employmentPosition == null)
            {
                return HttpNotFound();
            }
            return View(employmentPosition);
        }

        // GET: EmploymentPositions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmploymentPositions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmploymentPositionId,Name,Income,Description")] EmploymentPosition employmentPosition)
        {
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            if (ModelState.IsValid)
            {
                employmentPosition.DateCreated = DateTime.Now;
                employmentPosition.DateLastModified = DateTime.Now;
                if (loggedinuser != null)
                {
                    employmentPosition.CreatedBy = loggedinuser.AppUserId;
                    employmentPosition.LastModifiedBy = loggedinuser.AppUserId;
                    if (loggedinuser.InstitutionId != null)
                        employmentPosition.InstitutionId = (long)loggedinuser.InstitutionId;
                }
                db.EmploymentPosition.Add(employmentPosition);
                db.SaveChanges();
                TempData["message"] = "you have succesfully created a new Employement position for your organization!";
                TempData["notificationtype"] = NotificationTypeEnum.Success.ToString();
                return RedirectToAction("Index");
            }
            return View(employmentPosition);
        }
        // GET: EmploymentPositions/UpdateIncome/5
        public ActionResult UpdateIncome(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmploymentPosition employmentPosition = db.EmploymentPosition.Find(id);
            if (employmentPosition == null)
            {
                return HttpNotFound();
            }
            return View(employmentPosition);
        }
        // GET: EmploymentPositions/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmploymentPosition employmentPosition = db.EmploymentPosition.Find(id);
            if (employmentPosition == null)
            {
                return HttpNotFound();
            }
            return View(employmentPosition);
        }

        // POST: EmploymentPositions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmploymentPositionId,Name,InstitutionId,Income,CreatedBy,DateCreated,Description")] EmploymentPosition employmentPosition)
        {
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            if (ModelState.IsValid)
            {
                employmentPosition.DateLastModified = DateTime.Now;
                if (loggedinuser != null) employmentPosition.LastModifiedBy = loggedinuser.AppUserId;

                TempData["message"] = "you have succesfully modified the Employement Position!";
                TempData["notificationtype"] = NotificationTypeEnum.Success.ToString();
                db.Entry(employmentPosition).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employmentPosition);
        }

        // GET: EmploymentPositions/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmploymentPosition employmentPosition = db.EmploymentPosition.Find(id);
            if (employmentPosition == null)
            {
                return HttpNotFound();
            }
            return View(employmentPosition);
        }

        // POST: EmploymentPositions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            EmploymentPosition employmentPosition = db.EmploymentPosition.Find(id);
            db.EmploymentPosition.Remove(employmentPosition);
            db.SaveChanges();
            TempData["message"] = "you have succesfully deleted the Employement Position!";
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
