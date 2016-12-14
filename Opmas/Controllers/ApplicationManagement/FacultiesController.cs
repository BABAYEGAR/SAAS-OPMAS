using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Opmas.Data.DataContext.DataContext.SystemDataContext;
using Opmas.Data.Objects.Entities.SystemManagement;
using Opmas.Data.Objects.Entities.User;

namespace Opmas.Controllers.ApplicationManagement
{
    public class FacultiesController : Controller
    {
        private readonly FacultyDataContext db = new FacultyDataContext();

        // GET: Faculties
        public ActionResult Index()
        {
            var faculties = db.Faculties.Include(f => f.Institution);
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
        public ActionResult Create([Bind(Include = "FacultyId,Name")] Faculty faculty)
        {
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            var institution = Session["institution"] as Institution;
            if (institution != null)
                if (loggedinuser != null)
                {
                    if (ModelState.IsValid)
            {
               
                        faculty.DateCreated = DateTime.Now;
                        faculty.DateLastModified = DateTime.Now;
                        faculty.LastModifiedBy = loggedinuser.AppUserId;
                        faculty.CreatedBy = loggedinuser.AppUserId;
                        faculty.InstitutionId = institution.InstitutionId;
                        db.Faculties.Add(faculty);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }

                
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
            [Bind(Include = "FacultyId,Name,InstitutionId,CreatedBy,DateCreated,DateLastModified,LastModifiedBy")] Faculty faculty)
        {
            if (ModelState.IsValid)
            {
                db.Entry(faculty).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
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