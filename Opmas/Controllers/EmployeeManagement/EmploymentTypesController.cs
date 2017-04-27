using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Opmas.Data.DataContext.DataContext.EmployeeDataContext;
using Opmas.Data.Objects.Entities.User;
using Opmas.Data.Service.Enums;
using EmploymentType = Opmas.Data.Objects.Entities.Employee.EmploymentType;

namespace Opmas.Controllers.EmployeeManagement
{
    public class EmploymentTypesController : Controller
    {
        private EmploymentTypeDataContext db = new EmploymentTypeDataContext();

        // GET: EmploymentTypes
        public ActionResult Index()
        {
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            var employmentTypes = db.EmploymentTypes.Where(n=>n.InstitutionId == loggedinuser.InstitutionId).Include(e => e.Institution);
            return View(employmentTypes.ToList());
        }

        // GET: EmploymentTypes/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmploymentType employmentType = db.EmploymentTypes.Find(id);
            if (employmentType == null)
            {
                return HttpNotFound();
            }
            return View(employmentType);
        }

        // GET: EmploymentTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmploymentTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmploymentTypeId,Name,Description")] EmploymentType employmentType)
        {

            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            if (ModelState.IsValid)
            {
                employmentType.DateCreated = DateTime.Now;
                employmentType.DateLastModified = DateTime.Now;
                if (loggedinuser != null)
                {
                    employmentType.CreatedBy = loggedinuser.AppUserId;
                    employmentType.LastModifiedBy = loggedinuser.AppUserId;
                    if (loggedinuser.InstitutionId != null)
                        employmentType.InstitutionId = (long)loggedinuser.InstitutionId;
                }
                db.EmploymentTypes.Add(employmentType);
                db.SaveChanges();
                TempData["message"] = "you have succesfully created a new Employement Type!";
                TempData["notificationtype"] = NotificationTypeEnum.Success.ToString();
                return RedirectToAction("Index");
            }
            return View(employmentType);
        }

        // GET: EmploymentTypes/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmploymentType employmentType = db.EmploymentTypes.Find(id);
            if (employmentType == null)
            {
                return HttpNotFound();
            }
            return View(employmentType);
        }

        // POST: EmploymentTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmploymentTypeId,Name,InstitutionId,CreatedBy,DateCreated,Description")] EmploymentType employmentType)
        {
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            if (ModelState.IsValid)
            {
                employmentType.DateLastModified = DateTime.Now;
                if (loggedinuser != null) employmentType.LastModifiedBy = loggedinuser.AppUserId;
                db.Entry(employmentType).State = EntityState.Modified;
                db.SaveChanges();
                TempData["message"] = "you have succesfully modified the Employement Type!";
                TempData["notificationtype"] = NotificationTypeEnum.Success.ToString();
                return RedirectToAction("Index");
            }
            return View(employmentType);
        }

        // GET: EmploymentTypes/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmploymentType employmentType = db.EmploymentTypes.Find(id);
            if (employmentType == null)
            {
                return HttpNotFound();
            }
            return View(employmentType);
        }

        // POST: EmploymentTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            EmploymentType employmentType = db.EmploymentTypes.Find(id);
            db.EmploymentTypes.Remove(employmentType);
            db.SaveChanges();
            TempData["message"] = "you have succesfully deleted the Employement Type!";
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
