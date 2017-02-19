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
            ViewBag.returnUrl = false;
            var institution = Session["institution"] as Institution;
            var departments = db.Departments.Include(d => d.Faculty).Include(d => d.Institution).Where(n=>n.InstitutionId == institution.InstitutionId);
            return View(departments.ToList());
        }
        // POST: FacultyDepartments/AssignDepartmentManager
        public ActionResult AssignDepartmentManager(FormCollection collectedValues)
        {
            var departmentId = Convert.ToInt64(collectedValues["DepartmentId"]);
            var employeeId = Convert.ToInt64(collectedValues["EmployeeId"]);
            var department = db.Departments.Find(departmentId);
            department.EmployeeId = employeeId;
            db.Entry(department).State = EntityState.Modified;
            db.SaveChanges();
            TempData["department"] = "you have succesfully assigned the line manager for the department!";
            TempData["notificationtype"] = NotificationTypeEnum.Success.ToString();
            return RedirectToAction("Index");
        }

        // GET: FacultyDepartments
        public ActionResult FacultyDepartments(long? id)
        {
            ViewBag.returnUrl = true;
            ViewBag.faculty = id;
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
            var institutionStructureSession = Session["institutionstructure"] as InstitutionStructure;
            if (institutionStructureSession != null && institutionStructureSession.Faculty)
            {
                ViewBag.FacultyId = new SelectList(db.Faculties, "FacultyId", "Name");
            }
            ViewBag.InstitutionId = new SelectList(db.Universities, "InstitutionId", "Name");
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DepartmentId,Name,FacultyId,EmployeeId")] Department department , FormCollection collectedValues)
        {
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            var institution = Session["institution"] as Institution;
            long facultyId = 0;
            var institutionStructureSession = Session["institutionstructure"] as InstitutionStructure;
            if (institutionStructureSession != null && institutionStructureSession.Faculty)
            {
                 facultyId = Convert.ToInt64(collectedValues["FacultyId"]);
            }
            bool returnUrl = Convert.ToBoolean(collectedValues["returnUrl"]);
            if (institution != null)
            { 
                if (ModelState.IsValid)
                {
                    department.InstitutionId = institution.InstitutionId;
                    db.Departments.Add(department);
                    db.SaveChanges();
                    TempData["department"] = "You have successfully created a department";
                    TempData["notificationtype"] = NotificationTypeEnum.Success.ToString();
                    if (institutionStructureSession != null && institutionStructureSession.Faculty)
                    {
                        var facultyDepartments =
                            db.Departments.Where(n => n.FacultyId == facultyId)
                                .Include(d => d.Faculty)
                                .Include(d => d.Institution)
                                .Where(n => n.InstitutionId == institution.InstitutionId);
                        if (returnUrl)
                        {
                            return View("Index", facultyDepartments);
                        }
                    }
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["department"] = "Session Expired,Login Again";
                TempData["notificationtype"] = NotificationTypeEnum.Info.ToString();
                return RedirectToAction("SelectInstitution", "Home");
            }
            if (institutionStructureSession != null && institutionStructureSession.Faculty)
            {
                ViewBag.FacultyId = new SelectList(db.Faculties, "FacultyId", "Name", department.FacultyId);
            }
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
        public ActionResult Edit([Bind(Include = "DepartmentId,Name,FacultyId,InstitutionId,EmployeeId")] Department department)
        {
            if (ModelState.IsValid)
            {
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
