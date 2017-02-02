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

namespace Opmas.Controllers.TrainingManagement
{
    public class TrainingCategoriesController : Controller
    {
        private TrainingCategoryDataContext db = new TrainingCategoryDataContext();

        // GET: TrainingCategories
        public ActionResult Index()
        {
            var trainingCategory = db.TrainingCategory.Include(t => t.Institution);
            return View(trainingCategory.ToList());
        }

        // GET: TrainingCategories/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrainingCategory trainingCategory = db.TrainingCategory.Find(id);
            if (trainingCategory == null)
            {
                return HttpNotFound();
            }
            return View(trainingCategory);
        }

        // GET: TrainingCategories/Create
        public ActionResult Create()
        {
            ViewBag.InstitutionId = new SelectList(db.Institutions, "InstitutionId", "Name");
            return View();
        }

        // POST: TrainingCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TrainingCategoryId,Name,InstitutionId,CreatedBy,DateCreated,DateLastModified,LastModifiedBy")] TrainingCategory trainingCategory)
        {
            if (ModelState.IsValid)
            {
                db.TrainingCategory.Add(trainingCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.InstitutionId = new SelectList(db.Institutions, "InstitutionId", "Name", trainingCategory.InstitutionId);
            return View(trainingCategory);
        }

        // GET: TrainingCategories/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrainingCategory trainingCategory = db.TrainingCategory.Find(id);
            if (trainingCategory == null)
            {
                return HttpNotFound();
            }
            ViewBag.InstitutionId = new SelectList(db.Institutions, "InstitutionId", "Name", trainingCategory.InstitutionId);
            return View(trainingCategory);
        }

        // POST: TrainingCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TrainingCategoryId,Name,InstitutionId,CreatedBy,DateCreated,DateLastModified,LastModifiedBy")] TrainingCategory trainingCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trainingCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InstitutionId = new SelectList(db.Institutions, "InstitutionId", "Name", trainingCategory.InstitutionId);
            return View(trainingCategory);
        }

        // GET: TrainingCategories/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrainingCategory trainingCategory = db.TrainingCategory.Find(id);
            if (trainingCategory == null)
            {
                return HttpNotFound();
            }
            return View(trainingCategory);
        }

        // POST: TrainingCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            TrainingCategory trainingCategory = db.TrainingCategory.Find(id);
            db.TrainingCategory.Remove(trainingCategory);
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
