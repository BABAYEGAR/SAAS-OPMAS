﻿using System;
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
            ViewBag.InstitutionId = new SelectList(db.Institutions, "InstitutionId", "Name");
            return View();
        }

        // POST: Roles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RoleId,Name,ManageRolePriviledges,ManagePackages,ManageInstitutions,ManageFaculties,ManageDepartments,ManageUnits,ManageEmployees,ManageAppUsers,ManageAdminAppUsers,ManageAllInstitutions")] Role role)
        {
            var institution = Session["institution"] as Institution;
            if (ModelState.IsValid)
            {
                
                if (institution != null) role.InstitutionId = institution.InstitutionId;
                db.Roles.Add(role);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.InstitutionId = new SelectList(db.Institutions.Where(n=>n.Name != "Platform Administrator" && n.InstitutionId == institution.InstitutionId ), "InstitutionId", "Name", role.InstitutionId);
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
            ViewBag.InstitutionId = new SelectList(db.Institutions, "InstitutionId", "Name", role.InstitutionId);
            return View(role);
        }

        // POST: Roles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RoleId,Name,ManageRolePriviledges,ManageAdminAppUsers,ManageAllInstitutions,ManagePackages,ManageInstitutions,ManageFaculties,ManageDepartments,ManageUnits,ManageEmployees,ManageAppUsers,InstitutionId")] Role role)
        {
            var institution = Session["institution"] as Institution;
            if (ModelState.IsValid)
            {
                db.Entry(role).State = EntityState.Modified;
                db.SaveChanges();
                Session["role"] = role;
                return RedirectToAction("Index");
            }
            ViewBag.InstitutionId = new SelectList(db.Institutions.Where(n => n.Name != "Platform Administrator" && n.InstitutionId == institution.InstitutionId), "InstitutionId", "Name", role.InstitutionId);
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