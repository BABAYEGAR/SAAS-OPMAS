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
    public class FacultiesController : Controller
    {
        private readonly FacultyDataContext db = new FacultyDataContext();

        // GET: Faculties
        public ActionResult Index()
        {
            var institution = Session["institution"] as Institution;
            var faculties = db.Faculties.Include(f => f.Institution).Where(n=>n.InstitutionId == institution.InstitutionId);
            return View(faculties.ToList());
        }

        // GET: Faculties/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var faculty = db.Faculties.Find(id);
            if (faculty == null)
                return HttpNotFound();
            return View(faculty);
        }

        // GET: Faculties/Create
        public ActionResult Create()
        {
            ViewBag.InstitutionId = new SelectList(db.Institutions, "InstitutionId", "Name");
            return View();
        }

        // POST: Faculties/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FacultyId,Name")] Faculty faculty,FormCollection collectedValues)
        {
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            var institution = Session["institution"] as Institution;
            if (institution != null)
            {
                if (loggedinuser != null)
                {
                    if (ModelState.IsValid)
                    {
                        faculty.DateCreated = DateTime.Now;
                        faculty.DateLastModified = DateTime.Now;
                        faculty.LastModifiedBy = loggedinuser.AppUserId;
                        faculty.CreatedBy = loggedinuser.AppUserId;
                        faculty.InstitutionId = institution.InstitutionId;

                        var faculties = db.Faculties;
                        foreach (var item in faculties)
                        {
                            if (item.Name == collectedValues["Name"])
                            {
                                TempData["faculty"] = "Faculty name already exist, try another name!";
                                TempData["notificationtype"] = NotificationTypeEnum.Error.ToString();
                                return RedirectToAction("Index");
                            }
                        }
                        db.Faculties.Add(faculty);
                        db.SaveChanges();
                        TempData["faculty"] = "You have successfully created a faculty";
                        TempData["notificationtype"] = NotificationTypeEnum.Success.ToString();
                        return RedirectToAction("Index");
                    }
                    TempData["faculty"] = "Session Expired,Login Again";
                    TempData["notificationtype"] = NotificationTypeEnum.Info.ToString();
                    return RedirectToAction("SelectInstitution","Home");
                }
                TempData["faculty"] = "Session Expired,Login Again";
                TempData["notificationtype"] = NotificationTypeEnum.Info.ToString();
                return RedirectToAction("SelectInstitution", "Home");
            }
            ViewBag.InstitutionId = new SelectList(db.Institutions, "InstitutionId", "Name", faculty.InstitutionId);
            return View(faculty);
        }

        // GET: Faculties/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var faculty = db.Faculties.Find(id);
            if (faculty == null)
                return HttpNotFound();
            ViewBag.InstitutionId = new SelectList(db.Institutions, "InstitutionId", "Name", faculty.InstitutionId);
            return View(faculty);
        }

        // POST: Faculties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = "FacultyId,Name,InstitutionId,CreatedBy,DateCreated")] Faculty faculty,FormCollection collectedValues)
        {
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            if (loggedinuser != null)
            {
                if (ModelState.IsValid)
                {
                    faculty.DateLastModified = DateTime.Now;
                    faculty.LastModifiedBy = loggedinuser.AppUserId;
                    var faculties = db.Faculties;
                    foreach (var item in faculties)
                    {
                        if (item.Name == collectedValues["Name"])
                        {
                            TempData["faculty"] = "Faculty name already exist, try another name!";
                            TempData["notificationtype"] = NotificationTypeEnum.Error.ToString();
                            return RedirectToAction("Index");
                        }
                    }
                    db.Entry(faculty).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["faculty"] = "You have successfully modified the faculty";
                    TempData["notificationtype"] = NotificationTypeEnum.Success.ToString();
                    return RedirectToAction("Index");
                }
            }
            else
            { 
                TempData["faculty"] = "Session Expired,Login Again";
                TempData["notificationtype"] = NotificationTypeEnum.Info.ToString();
                return RedirectToAction("SelectInstitution", "Home");
            }
            ViewBag.InstitutionId = new SelectList(db.Institutions, "InstitutionId", "Name", faculty.InstitutionId);
            return View(faculty);
        }

        // GET: Faculties/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var faculty = db.Faculties.Find(id);
            if (faculty == null)
                return HttpNotFound();
            return View(faculty);
        }

        // POST: Faculties/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            var faculty = db.Faculties.Find(id);
            db.Faculties.Remove(faculty);
            db.SaveChanges();
            TempData["faculty"] = "You have successfully deleted a faculty";
            TempData["notificationtype"] = NotificationTypeEnum.Success.ToString();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }
    }
}