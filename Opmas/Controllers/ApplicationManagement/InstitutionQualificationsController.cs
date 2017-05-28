using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Opmas.Data.DataContext.DataContext.SystemDataContext;
using Opmas.Data.Objects.Entities.SystemManagement;
using Opmas.Data.Objects.Entities.User;
using Opmas.Data.Service.Enums;

namespace Opmas.Controllers.ApplicationManagement
{
    public class InstitutionQualificationsController : Controller
    {
        private InstitutionQualificationDataContext db = new InstitutionQualificationDataContext();

        // GET: InstitutionQualifications
        public ActionResult Index()
        {
            var institution = Session["institution"] as Institution;
            var institutionQualifications = db.InstitutionQualifications.Include(i => i.Institution).Where(n=>n.InstitutionId == institution.InstitutionId);
            return View(institutionQualifications.ToList());
        }

        // GET: InstitutionQualifications/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InstitutionQualification institutionQualification = db.InstitutionQualifications.Find(id);
            if (institutionQualification == null)
            {
                return HttpNotFound();
            }
            return View(institutionQualification);
        }

        // GET: InstitutionQualifications/Create
        public ActionResult Create()
        {
            ViewBag.InstitutionId = new SelectList(db.Institutions, "InstitutionId", "Name");
            return View();
        }

        // POST: InstitutionQualifications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InstitutionQualificationId,Name,Description,InstitutionId")] InstitutionQualification institutionQualification)
        {
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            var institution = Session["institution"] as Institution;
            if (institution != null)
            {
                if (loggedinuser != null)
                {
                    if (ModelState.IsValid)
                    {
                        institutionQualification.DateCreated = DateTime.Now;
                        institutionQualification.DateLastModified = DateTime.Now;
                        institutionQualification.LastModifiedBy = loggedinuser.AppUserId;
                        institutionQualification.CreatedBy = loggedinuser.AppUserId;
                        institutionQualification.InstitutionId = institution.InstitutionId;
                        db.InstitutionQualifications.Add(institutionQualification);
                        db.SaveChanges();
                        TempData["message"] = "You have successfully added an Educational Qualification";
                        TempData["notificationtype"] = NotificationTypeEnum.Success.ToString();
                        return RedirectToAction("Index");
                    }
                }
            }
            return View(institutionQualification);
        }

        // GET: InstitutionQualifications/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InstitutionQualification institutionQualification = db.InstitutionQualifications.Find(id);
            if (institutionQualification == null)
            {
                return HttpNotFound();
            }
            ViewBag.InstitutionId = new SelectList(db.Institutions, "InstitutionId", "Name", institutionQualification.InstitutionId);
            return View(institutionQualification);
        }

        // POST: InstitutionQualifications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InstitutionQualificationId,Name,Description,InstitutionId,CreatedBy,DateCreated")] InstitutionQualification institutionQualification)
        {
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            if (ModelState.IsValid)
            {
                institutionQualification.DateLastModified = DateTime.Now;
                if (loggedinuser != null) institutionQualification.LastModifiedBy = loggedinuser.AppUserId;
                db.Entry(institutionQualification).State = EntityState.Modified;
                db.SaveChanges();
                TempData["message"] = "You have successfully modified the Educational Qualification";
                TempData["notificationtype"] = NotificationTypeEnum.Success.ToString();
                return RedirectToAction("Index");
            }
            return View(institutionQualification);
        }

        // GET: InstitutionQualifications/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InstitutionQualification institutionQualification = db.InstitutionQualifications.Find(id);
            if (institutionQualification == null)
            {
                return HttpNotFound();
            }
            return View(institutionQualification);
        }

        // POST: InstitutionQualifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            InstitutionQualification institutionQualification = db.InstitutionQualifications.Find(id);
            db.InstitutionQualifications.Remove(institutionQualification);
            db.SaveChanges();
            TempData["message"] = "You have successfully deleted the Educational Qualification";
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
