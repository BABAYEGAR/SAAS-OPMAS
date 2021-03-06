﻿using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Opmas.Data.DataContext.DataContext.SystemDataContext;
using Opmas.Data.Factory.ApplicationManagement;
using Opmas.Data.Objects.Entities.SystemManagement;
using Opmas.Data.Service.Enums;
using Opmas.Data.Service.FileUploader;

namespace Opmas.Controllers.ApplicationManagement
{
    public class InstitutionsController : Controller
    {
        private InstitutionDataContext db = new InstitutionDataContext();

        // GET: Institutions
        public ActionResult Index()
        {
            ViewBag.Header = "List Of Institutions";
            return View(db.Institutions.ToList());
        }

        // GET: InstitutionsByCategory
        public ActionResult GetInstitutionsByCategory()
        {
            var institutions = new InstitutionFactory().GetListOfInstitutions();
            return View("Index",institutions);
        }
        // GET: GetActiveInstitutions
        public ActionResult GetActiveInstitutions()
        {
            ViewBag.Header = "List Of Active Institutions";
            var institutions = new InstitutionFactory().GetListOfInstitutions();
            return View("Index", institutions.Where(n=>n.SubscriptonEndDate > DateTime.Now));
        }
        // GET: GetInActiveInstitutions
        public ActionResult GetInActiveInstitutions()
        {
            ViewBag.Header = "List Of Inactive Institutions";
            var institutions = new InstitutionFactory().GetListOfInstitutions();
            return View("Index", institutions.Where(n => n.SubscriptonEndDate < DateTime.Now));
        }
        // GET: Institutions/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Institution institution = db.Institutions.Find(id);
            if (institution == null)
            {
                return HttpNotFound();
            }
            return View(institution);
        }

        // GET: Institutions/Create
        public ActionResult Create()
        {
            ViewBag.PackageId = new SelectList(db.Packages, "PackageId", "Name");
            return View();
        }

        // POST: Institutions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InstitutionId,AccessCode,Name,Motto,Logo,Location,ContactNumber,ContactEmail,PackageId")] Institution institution,FormCollection collectedValues)
        {
            HttpPostedFileBase logo = Request.Files["logo"];
            if (ModelState.IsValid)
            {
                institution.Logo = new FileUploader().UploadFile(logo, UploadType.Logo);
                institution.SubscriprionStartDate = DateTime.Now;
                institution.SubscriptonEndDate = institution.SubscriprionStartDate.AddYears(1);
                db.Institutions.Add(institution);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PackageId = new SelectList(db.Packages, "PackageId", "Name", institution.PackageId);
            return View(institution);
        }

        // GET: Institutions/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Institution institution = db.Institutions.Find(id);
            if (institution == null)
            {
                return HttpNotFound();
            }
            ViewBag.PackageId = new SelectList(db.Packages, "PackageId", "Name", institution.PackageId);
            return View(institution);
        }

        // POST: Institutions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InstitutionId,Name,SubscriprionStartDate,SubscriptonEndDate,AccessCode,Motto,Location,ContactNumber,ContactEmail,PackageId")] Institution institution,FormCollection collectedValues)
        {
            HttpPostedFileBase logo = Request.Files["logo"];
            if (ModelState.IsValid)
            {
                if (collectedValues["Logo"] != null)
                {
                    institution.Logo = collectedValues["Logo"];
                }
                if (logo != null && logo.FileName == "")
                {
                    institution.Logo = collectedValues["Logo"];
                }
                else
                {
                    institution.Logo = new FileUploader().UploadFile(logo, UploadType.Logo);
                }
                
               
                db.Entry(institution).State = EntityState.Modified;
                db.SaveChanges();
                Session["institution"] = institution;
                return RedirectToAction("Dashboard","Home");
            }
            ViewBag.PackageId = new SelectList(db.Packages, "PackageId", "Name", institution.PackageId);
            return View(institution);
        }

        // GET: Institutions/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Institution institution = db.Institutions.Find(id);
            if (institution == null)
            {
                return HttpNotFound();
            }
            return View(institution);
        }

        // POST: Institutions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Institution institution = db.Institutions.Find(id);
            db.Institutions.Remove(institution);
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
