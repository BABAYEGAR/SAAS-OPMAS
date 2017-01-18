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
    public class DepartmentsController : Controller
    {
        private DepartmentDataContext db = new DepartmentDataContext();

        // GET: Departments
        public ActionResult Index()
        {
            var institution = Session["institution"] as Institution;
            var departments = db.Departments.Include(d => d.Faculty).Include(d => d.Institution).Where(n=>n.InstitutionId == institution.InstitutionId);
            return View(departments.ToList());
        }
        // GET: FacultyDepartments
        public ActionResult FacultyDepartments(long? id)
        {
            var institution = Session["institution"] as Institution;
            var departments = db.Departments.Where(n=>n.FacultyId == id).Include(d => d.Faculty).Include(d => d.Institution).Where(n => n.InstitutionId == institution.InstitutionId);
            return View("Index",departments.ToList());
        }

        // GET: Departments/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // GET: Departments/Create
        public ActionResult Create()
        {
            ViewBag.FacultyId = new SelectList(db.Faculties, "FacultyId", "Name");
            ViewBag.InstitutionId = new SelectList(db.Universities, "InstitutionId", "Name");
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DepartmentId,Name,FacultyId")] Department department,FormCollection collectedValues)
        {
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            var institution = Session["institution"] as Institution;
            if (institution != null)
            { 
                if (ModelState.IsValid)
                {
                    department.InstitutionId = institution.InstitutionId;

                    var departments = db.Departments;
                    foreach (var item in departments)
                    {
                        if (item.Name == collectedValues["Name"])
                        {
                            TempData["department"] = "Department name already exist, try another name!";
                            TempData["notificationtype"] = NotificationTypeEnum.Error.ToString();
                            return RedirectToAction("Index");
                        }
                    }

                    db.Departments.Add(department);
                    db.SaveChanges();
                    TempData["department"] = "You have successfully created a department";
                    TempData["notificationtype"] = NotificationTypeEnum.Success.ToString();
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["department"] = "Session Expired,Login Again";
                TempData["notificationtype"] = NotificationTypeEnum.Info.ToString();
                return RedirectToAction("SelectInstitution", "Home");
            }

            ViewBag.FacultyId = new SelectList(db.Faculties, "FacultyId", "Name", department.FacultyId);
            ViewBag.InstitutionId = new SelectList(db.Universities, "InstitutionId", "Name", department.InstitutionId);
            return View(department);
        }

        // GET: Departments/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            ViewBag.FacultyId = new SelectList(db.Faculties, "FacultyId", "Name", department.FacultyId);
            ViewBag.InstitutionId = new SelectList(db.Universities, "InstitutionId", "Name", department.InstitutionId);
            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DepartmentId,Name,FacultyId,InstitutionId")] Department department,FormCollection collectedValues)
        {
            if (ModelState.IsValid)
            {
                var departments = db.Departments;
                foreach (var item in departments)
                {
                    if (item.Name == collectedValues["Name"])
                    {
                        TempData["department"] = "Department name already exist, try another name!";
                        TempData["notificationtype"] = NotificationTypeEnum.Error.ToString();
                        return RedirectToAction("Index");
                    }
                }
                db.Entry(department).State = EntityState.Modified;
                db.SaveChanges();
                TempData["department"] = "You have successfully modified the department";
                TempData["notificationtype"] = NotificationTypeEnum.Success.ToString();
                return RedirectToAction("Index");
            }
            ViewBag.FacultyId = new SelectList(db.Faculties, "FacultyId", "Name", department.FacultyId);
            ViewBag.InstitutionId = new SelectList(db.Universities, "InstitutionId", "Name", department.InstitutionId);
            return View(department);
        }

        // GET: Departments/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Department department = db.Departments.Find(id);
            db.Departments.Remove(department);
            db.SaveChanges();
            TempData["department"] = "You have successfully deleted a department";
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
