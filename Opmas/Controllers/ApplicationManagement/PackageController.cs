﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Opmas.Data.DataContext.DataContext.AccessDataContext;
using Opmas.Data.Objects.Entities.AccessManagement;

namespace Opmas.Controllers.ApplicationManagement
{
    public class PackageController : Controller
    {
        private PackageDataContext _db = new PackageDataContext();
        // GET: Package
        public ActionResult Index()
        {
            
            return View(_db.Packages.ToList());
        }

        // GET: Package/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Package/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Package/Create
        [HttpPost]
        public ActionResult Create(Package package,FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                package.DateCreated = DateTime.Now;
                package.DateLastModified = DateTime.Now;
                package.Amount = long.Parse(collection["Amount"]);
                package.Name = collection["Name"];
                _db.Packages.Add(package);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(package);
        }

        // GET: Package/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Package/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Package/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Package/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}