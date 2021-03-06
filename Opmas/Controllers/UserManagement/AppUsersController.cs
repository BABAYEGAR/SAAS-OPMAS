﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BhuInfo.Data.Service.Encryption;
using BhuInfo.Data.Service.TextFormatter;
using Opmas.Data.DataContext.DataContext.EmployeeDataContext;
using Opmas.Data.DataContext.DataContext.UserDataContext;
using Opmas.Data.Objects.Entities.SystemManagement;
using Opmas.Data.Objects.Entities.User;
using Opmas.Data.Service.Enums;
using Opmas.Data.Service.FileUploader;

namespace Opmas.Controllers.UserManagement
{
    public class AppUsersController : Controller
    {
        private AppUserDataContext db = new AppUserDataContext();
        private RoleDataContext dbc = new RoleDataContext();

        // GET: AppUsers
        public ActionResult Index()
        {
            var institution = Session["institution"] as Institution;
            var appUsers = db.AppUsers.Include(a => a.Employee).Include(n=>n.Institution);
            return View(appUsers.ToList().Where(n=> institution != null && n.InstitutionId == institution.InstitutionId));
        }
        // GET: ViewProfile
        public ActionResult ViewProfile(long? id)
        {
            var appUser = db.AppUsers.Find(id);
            return View(appUser);
        }
        // GET: GetAdminAppUsers
        public ActionResult GetAdminAppUsers()
        {
            var appUsers = db.AppUsers.Include(n => n.Institution);
            var adminUsers =new  List<AppUser>();
            foreach (var item in appUsers)
            {
                if (item.RoleId != null)
                {
                    var userRole = dbc.Roles.Find(item.RoleId);
                    if((userRole.Name == "Platform Administrator"  || userRole.Name == "Institution Administrator"))
                    {
                        adminUsers.Add(item);
                    }
                }
            }
            return View("Index",adminUsers);
        }
        // GET: AppUsers/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppUser appUser = db.AppUsers.Find(id);
            if (appUser == null)
            {
                return HttpNotFound();
            }
            return View(appUser);
        }

        // GET: AppUsers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AppUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AppUserId,Firstname,Middlename,Lastname,Email,Mobile")] AppUser appUser,FormCollection collectedValues)
        {
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            var institution = Session["institution"] as Institution;
            if (ModelState.IsValid)
            {
                if (loggedinuser != null && institution != null)
                {
                    HttpPostedFileBase profileImage = Request.Files["avatar-2"];
                    appUser.EmployeeId = loggedinuser.EmployeeId;
                    appUser.InstitutionId = institution.InstitutionId;
                    appUser.DateLastModified = DateTime.Now;
                    appUser.DateCreated = DateTime.Now;
                    appUser.LastModifiedBy = loggedinuser.AppUserId;
                    appUser.CreatedBy = loggedinuser.AppUserId;
                    appUser.AppUserImage = new FileUploader().UploadFile(profileImage, UploadType.ProfileImage);
                    //generate password and convert to md5 hash
                    var password = Membership.GeneratePassword(8, 1);
                    var hashPassword = new Md5Ecryption().ConvertStringToMd5Hash(password.Trim());
                    appUser.Password = new RemoveCharacters().RemoveSpecialCharacters(hashPassword);
                }
                db.AppUsers.Add(appUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(appUser);
        }

        // GET: AppUsers/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppUser appUser = db.AppUsers.Find(id);
            if (appUser == null)
            {
                return HttpNotFound();
            }
            return View(appUser);
        }

        // POST: AppUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AppUserId,Firstname,Middlename,Lastname,InstitutionId,Email,Mobile,DepartmentId,RoleId,FacultyId,Password,Role,AppUserImage,FingerPrint,EmployeeId,CreatedBy,DateCreated,LastModifiedBy")] AppUser appUser,FormCollection collectedValues)
        {
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            if (ModelState.IsValid)
            {
                HttpPostedFileBase profileImage = Request.Files["avatar-2"];
                if (profileImage != null && profileImage.FileName == "")
                {
                    appUser.AppUserImage = collectedValues["AppUserImage"];
                }
                else
                {
                    appUser.AppUserImage = new FileUploader().UploadFile(profileImage, UploadType.ProfileImage);
                }
                appUser.DateLastModified = DateTime.Now;
                if (loggedinuser != null) appUser.LastModifiedBy = loggedinuser.AppUserId;
                db.Entry(appUser).State = EntityState.Modified;
                db.SaveChanges();
                Session["opmasloggedinuser"] = appUser;
                if (loggedinuser != null && loggedinuser.AppUserId == appUser.AppUserId)
                {
                    return RedirectToAction("Dashboard", "Home");
                }
                return RedirectToAction("Index");
            }
            return View(appUser);
        }

        // GET: AppUsers/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppUser appUser = db.AppUsers.Find(id);
            if (appUser == null)
            {
                return HttpNotFound();
            }
            return View(appUser);
        }

        // POST: AppUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            AppUser appUser = db.AppUsers.Find(id);
            db.AppUsers.Remove(appUser);
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
