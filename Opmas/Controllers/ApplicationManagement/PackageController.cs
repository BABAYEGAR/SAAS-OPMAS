using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Opmas.Data.DataContext.DataContext.AccessDataContext;
using Opmas.Data.Objects.Entities.AccessManagement;
using Opmas.Data.Objects.Entities.SystemManagement;
using Opmas.Data.Objects.Entities.User;
using Opmas.Data.Service.Enums;

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
                var allPackages = _db.Packages.ToList();
                package.DateCreated = DateTime.Now;
                package.DateLastModified = DateTime.Now;
                package.Amount = long.Parse(collection["Amount"]);
                package.Description = collection["Description"];
                package.Name = collection["Name"];
                package.Type = typeof(PackageType).GetEnumName(int.Parse(collection["Type"]));
               
                if (allPackages.Count >= 3)
                {
                    TempData["package"] =
                                  "You cannot added a package!";
                    TempData["notificationType"] = NotificationTypeEnum.Error.ToString();
                    return RedirectToAction("Index");
                }
                if (allPackages.Any(n => n.Type == package.Type))
                {
                    TempData["package"] =
                        "You cannot add this package because this type exist!";
                    TempData["notificationType"] = NotificationTypeEnum.Error.ToString();
                    return RedirectToAction("Index");
                }
                _db.Packages.Add(package);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(package);
        }

        // GET: Packages/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var package = _db.Packages.Find(id);
            if (package == null)
                return HttpNotFound();
            return View(package);
        }

        // POST: Packages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = "PackageId,Name,Amount,Description,Type,CreatedBy,DateCreated")] Package package)
        {
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            if (loggedinuser != null)
            {
                if (ModelState.IsValid)
                {
                    package.DateLastModified = DateTime.Now;
                    package.LastModifiedBy = loggedinuser.AppUserId;
                    _db.Entry(package).State = EntityState.Modified;
                    _db.SaveChanges();
                    TempData["package"] = "You have successfully modified the package";
                    TempData["notificationtype"] = NotificationTypeEnum.Success.ToString();
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["login"] = "Session Expired,Login Again";
                TempData["notificationtype"] = NotificationTypeEnum.Info.ToString();
                return RedirectToAction("Login", "Account");
            }
            return View(package);
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
