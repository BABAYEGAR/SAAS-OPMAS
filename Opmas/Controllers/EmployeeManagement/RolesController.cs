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
using Opmas.Data.Objects.Entities.SystemManagement;
using Opmas.Data.Service.Enums;

namespace Opmas.Controllers.EmployeeManagement
{
    public class RolesController : Controller
    {
        private RoleDataContext db = new RoleDataContext();

        // GET: Roles
        public ActionResult Index()
        {
            var institution = Session["institution"] as Institution;
            var roles = db.Roles.Where(n => n.Name != "Platform Administrator"&& n.Name != "Institution Administrator" && n.InstitutionId == institution.InstitutionId).Include(r => r.Institution);
            return View(roles.ToList());
        }

        // GET: Roles/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // GET: Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Roles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RoleId,Name,Description,ManageEmploymentTypes,ManageEmploymentPositions,ManageRolePriviledges,ManagePackages,ManageInstitutions,ManageFaculties,ManageDepartments," +
                                                   "ManageUnits,ManageTraining,ManageTrainingTypes,ManageEmployees,ManageAppUsers,ManageAdminAppUsers,ManageAllInstitutions")] Role role,FormCollection collectedValues)
        {
            var institution = Session["institution"] as Institution;
            if (ModelState.IsValid)
            {
                
                if (institution != null) role.InstitutionId = institution.InstitutionId;
                role.RoleType = typeof(RoleType).GetEnumName(int.Parse(collectedValues["RoleType"]));

                var roles = db.Roles;
                foreach (var item in roles)
                {
                    if (item.Name == collectedValues["Name"])
                    {
                        TempData["message"] = "Role already exist, try another role name!";
                        TempData["notificationtype"] = NotificationTypeEnum.Error.ToString();
                        return View(role);
                    }   
                }
                db.Roles.Add(role);
                db.SaveChanges();
                TempData["message"] = "You have successfully created a role!";
                TempData["notificationtype"] = NotificationTypeEnum.Success.ToString();
                return RedirectToAction("Index");
            }

            return View(role);
        }

        // GET: Roles/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: Roles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RoleId,Name,ManageEmploymentTypes,Description,ManageEmploymentPositions,ManageTraining,ManageTrainingTypes,ManageRolePriviledges,RoleType,ManageAdminAppUsers,ManageAllInstitutions,ManagePackages,ManageInstitutions,ManageFaculties,ManageDepartments,ManageUnits,ManageEmployees,ManageAppUsers,InstitutionId")] Role role,FormCollection collectedValues)
        {
            var institution = Session["institution"] as Institution;
            if (ModelState.IsValid)
            {
                db.Entry(role).State = EntityState.Modified;
                db.SaveChanges();
                TempData["message"] = "You have successfully modified a role!";
                TempData["notificationtype"] = NotificationTypeEnum.Success.ToString();
                return RedirectToAction("Index");
            }
            return View(role);
        }

        // GET: Roles/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Role role = db.Roles.Find(id);
            db.Roles.Remove(role);
            db.SaveChanges();
            TempData["message"] = "You have successfully deleted a role!";
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
