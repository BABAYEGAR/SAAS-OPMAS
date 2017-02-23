using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Opmas.Data.DataContext.DataContext.MappingDataContext;
using Opmas.Data.Objects.Mappings;

namespace Opmas.Controllers.MappingManagement
{
    public class AppraisalPositionMappingsController : Controller
    {
        private AppraisalPositionMappingDataContext db = new AppraisalPositionMappingDataContext();

        // GET: AppraisalPositionMappings
        public ActionResult Index()
        {
            var appraisalPositionMappings = db.AppraisalPositionMappings.Include(a => a.AppraisalCategory).Include(a => a.EmploymentPosition);
            return View(appraisalPositionMappings.ToList());
        }

        // GET: AppraisalPositionMappings/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppraisalPositionMapping appraisalPositionMapping = db.AppraisalPositionMappings.Find(id);
            if (appraisalPositionMapping == null)
            {
                return HttpNotFound();
            }
            return View(appraisalPositionMapping);
        }

        // GET: AppraisalPositionMappings/Create
        public ActionResult Create()
        {
            ViewBag.AppraisalCategoryId = new SelectList(db.AppraisalCategories, "AppraisalCategoryId", "Name");
            ViewBag.EmploymentPositionId = new SelectList(db.EmploymentPosition, "EmploymentPositionId", "Name");
            return View();
        }

        // POST: AppraisalPositionMappings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AppraisalPositionMappingId,AppraisalCategoryId,EmploymentPositionId,CreatedBy,DateCreated,DateLastModified,LastModifiedBy")] AppraisalPositionMapping appraisalPositionMapping)
        {
            if (ModelState.IsValid)
            {
                db.AppraisalPositionMappings.Add(appraisalPositionMapping);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AppraisalCategoryId = new SelectList(db.AppraisalCategories, "AppraisalCategoryId", "Name", appraisalPositionMapping.AppraisalCategoryId);
            ViewBag.EmploymentPositionId = new SelectList(db.EmploymentPosition, "EmploymentPositionId", "Name", appraisalPositionMapping.EmploymentPositionId);
            return View(appraisalPositionMapping);
        }

        // GET: AppraisalPositionMappings/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppraisalPositionMapping appraisalPositionMapping = db.AppraisalPositionMappings.Find(id);
            if (appraisalPositionMapping == null)
            {
                return HttpNotFound();
            }
            ViewBag.AppraisalCategoryId = new SelectList(db.AppraisalCategories, "AppraisalCategoryId", "Name", appraisalPositionMapping.AppraisalCategoryId);
            ViewBag.EmploymentPositionId = new SelectList(db.EmploymentPosition, "EmploymentPositionId", "Name", appraisalPositionMapping.EmploymentPositionId);
            return View(appraisalPositionMapping);
        }

        // POST: AppraisalPositionMappings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AppraisalPositionMappingId,AppraisalCategoryId,EmploymentPositionId,CreatedBy,DateCreated,DateLastModified,LastModifiedBy")] AppraisalPositionMapping appraisalPositionMapping)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appraisalPositionMapping).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AppraisalCategoryId = new SelectList(db.AppraisalCategories, "AppraisalCategoryId", "Name", appraisalPositionMapping.AppraisalCategoryId);
            ViewBag.EmploymentPositionId = new SelectList(db.EmploymentPosition, "EmploymentPositionId", "Name", appraisalPositionMapping.EmploymentPositionId);
            return View(appraisalPositionMapping);
        }

        // GET: AppraisalPositionMappings/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppraisalPositionMapping appraisalPositionMapping = db.AppraisalPositionMappings.Find(id);
            if (appraisalPositionMapping == null)
            {
                return HttpNotFound();
            }
            return View(appraisalPositionMapping);
        }

        // POST: AppraisalPositionMappings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            AppraisalPositionMapping appraisalPositionMapping = db.AppraisalPositionMappings.Find(id);
            db.AppraisalPositionMappings.Remove(appraisalPositionMapping);
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
