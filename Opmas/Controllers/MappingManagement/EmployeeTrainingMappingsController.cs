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

namespace Opmas.Controllers.MappingManagement
{
    public class EmployeeTrainingMappingsController : Controller
    {
        private EmployeeTrainingMappingDataContext db = new EmployeeTrainingMappingDataContext();

        // GET: EmployeeTrainingMappings
        public ActionResult Index()
        {
            var employeeTrainingMappings = db.EmployeeTrainingMappings.Include(e => e.Employees).Include(e => e.EmployeeTrainings).Include(e => e.Institution);
            return View(employeeTrainingMappings.ToList());
        }

        // GET: EmployeeTrainingMappings/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeTrainingMapping employeeTrainingMapping = db.EmployeeTrainingMappings.Find(id);
            if (employeeTrainingMapping == null)
            {
                return HttpNotFound();
            }
            return View(employeeTrainingMapping);
        }

        // GET: EmployeeTrainingMappings/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeId");
            ViewBag.EmployeeTrainingId = new SelectList(db.EmployeeTrainings, "EmployeeTrainingId", "Title");
            ViewBag.InstitutionId = new SelectList(db.Institutions, "InstitutionId", "Name");
            return View();
        }

        // POST: EmployeeTrainingMappings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeTrainingMappingId,EmployeeId,EmployeeTrainingId,InstitutionId,CreatedBy,DateCreated,DateLastModified,LastModifiedBy")] EmployeeTrainingMapping employeeTrainingMapping)
        {
            if (ModelState.IsValid)
            {
                db.EmployeeTrainingMappings.Add(employeeTrainingMapping);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeId", employeeTrainingMapping.EmployeeId);
            ViewBag.EmployeeTrainingId = new SelectList(db.EmployeeTrainings, "EmployeeTrainingId", "Title", employeeTrainingMapping.EmployeeTrainingId);
            ViewBag.InstitutionId = new SelectList(db.Institutions, "InstitutionId", "Name", employeeTrainingMapping.InstitutionId);
            return View(employeeTrainingMapping);
        }

        // GET: EmployeeTrainingMappings/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeTrainingMapping employeeTrainingMapping = db.EmployeeTrainingMappings.Find(id);
            if (employeeTrainingMapping == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeId", employeeTrainingMapping.EmployeeId);
            ViewBag.EmployeeTrainingId = new SelectList(db.EmployeeTrainings, "EmployeeTrainingId", "Title", employeeTrainingMapping.EmployeeTrainingId);
            ViewBag.InstitutionId = new SelectList(db.Institutions, "InstitutionId", "Name", employeeTrainingMapping.InstitutionId);
            return View(employeeTrainingMapping);
        }

        // POST: EmployeeTrainingMappings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeTrainingMappingId,EmployeeId,EmployeeTrainingId,InstitutionId,CreatedBy,DateCreated,DateLastModified,LastModifiedBy")] EmployeeTrainingMapping employeeTrainingMapping)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employeeTrainingMapping).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeId", employeeTrainingMapping.EmployeeId);
            ViewBag.EmployeeTrainingId = new SelectList(db.EmployeeTrainings, "EmployeeTrainingId", "Title", employeeTrainingMapping.EmployeeTrainingId);
            ViewBag.InstitutionId = new SelectList(db.Institutions, "InstitutionId", "Name", employeeTrainingMapping.InstitutionId);
            return View(employeeTrainingMapping);
        }

        // GET: EmployeeTrainingMappings/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeTrainingMapping employeeTrainingMapping = db.EmployeeTrainingMappings.Find(id);
            if (employeeTrainingMapping == null)
            {
                return HttpNotFound();
            }
            return View(employeeTrainingMapping);
        }

        // POST: EmployeeTrainingMappings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            EmployeeTrainingMapping employeeTrainingMapping = db.EmployeeTrainingMappings.Find(id);
            db.EmployeeTrainingMappings.Remove(employeeTrainingMapping);
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
