using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Opmas.Data.DataContext.DataContext.SystemDataContext;
using Opmas.Data.Objects.Entities.SystemManagement;
using Opmas.Data.Objects.Entities.User;
using Opmas.Data.Service.Enums;

namespace Opmas.Controllers.ApplicationManagement
{
    public class InstitutionStructuresController : Controller
    {
        private InstitutionStructureDataContext _db = new InstitutionStructureDataContext();

        // GET: InstitutionStructures
        public ActionResult Index()
        {
            var institution = Session["institution"] as Institution;
            var institutionStructures =
                _db.InstitutionStructures.SingleOrDefault(n => n.InstitutionId == institution.InstitutionId);
            return View(institutionStructures);
        }

        // GET: InstitutionStructures/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InstitutionStructure institutionStructure = _db.InstitutionStructures.Find(id);
            if (institutionStructure == null)
            {
                return HttpNotFound();
            }
            return View(institutionStructure);
        }

        // GET: InstitutionStructures/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InstitutionStructures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InstitutionStructureId,Faculty,InstitutionId")] InstitutionStructure institutionStructure)
        {
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            Institution institution = null;
            if (ModelState.IsValid)
            {
                institutionStructure.DateCreated = DateTime.Now;
                institutionStructure.DateLastModified = DateTime.Now;
                if (loggedinuser != null)
                {
                    institutionStructure.CreatedBy = loggedinuser.AppUserId;
                    institutionStructure.LastModifiedBy = loggedinuser.AppUserId;
                    if (loggedinuser.InstitutionId != null)
                        institutionStructure.InstitutionId = (long)loggedinuser.InstitutionId;
                        institution = _db.Institutions.Find(loggedinuser.InstitutionId);
                    institution.SetUpStatus = SetUpStatus.Completed.ToString();
                }
                _db.Entry(institution).State = EntityState.Modified;
                _db.InstitutionStructures.Add(institutionStructure);
                _db.SaveChanges();
                Session["institutionstructure"] = institutionStructure;
                Session["institution"] = institution;
                TempData["message"] = "You have successfully defined your institutions structure";
                TempData["notificationType"] = NotificationTypeEnum.Success.ToString();
                return RedirectToAction("Dashboard","Home");
            }
            return View(institutionStructure);
        }

        // GET: InstitutionStructures/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InstitutionStructure institutionStructure = _db.InstitutionStructures.Find(id);
            if (institutionStructure == null)
            {
                return HttpNotFound();
            }
            return View(institutionStructure);
        }

        // POST: InstitutionStructures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InstitutionStructureId,Faculty,CreatedBy,DateCreated,InstitutionId")] InstitutionStructure institutionStructure)
        {
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            if (ModelState.IsValid)
            {
                institutionStructure.DateLastModified = DateTime.Now;
                if (loggedinuser != null) institutionStructure.LastModifiedBy = loggedinuser.AppUserId;
                _db.Entry(institutionStructure).State = EntityState.Modified;
                _db.SaveChanges();
                Session["institutionstructure"] = institutionStructure;
                TempData["message"] = "You have successfully modified your institutions structure";
                TempData["notificationType"] = NotificationTypeEnum.Success.ToString();
                return RedirectToAction("Dashboard","Home");
            }
            return View(institutionStructure);
        }

        // GET: InstitutionStructures/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InstitutionStructure institutionStructure = _db.InstitutionStructures.Find(id);
            if (institutionStructure == null)
            {
                return HttpNotFound();
            }
            return View(institutionStructure);
        }

        // POST: InstitutionStructures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            InstitutionStructure institutionStructure = _db.InstitutionStructures.Find(id);
            _db.InstitutionStructures.Remove(institutionStructure);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
