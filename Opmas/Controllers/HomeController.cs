﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Opmas.Data.DataContext.DataContext.AccessDataContext;
using Opmas.Data.DataContext.DataContext.EmployeeDataContext;
using Opmas.Data.DataContext.DataContext.SystemDataContext;
using Opmas.Data.DataContext.DataContext.UserDataContext;
using Opmas.Data.Factory.ApplicationManagement;
using Opmas.Data.Objects.Entities.AccessManagement;
using Opmas.Data.Objects.Entities.Employee;
using Opmas.Data.Objects.Entities.SystemManagement;
using Opmas.Data.Objects.Entities.User;
using Opmas.Data.Service.Enums;

namespace Opmas.Controllers
{
    public class HomeController : Controller
    {
        private readonly InstitutionDataContext _dbInstitution = new InstitutionDataContext();
        private readonly PackageDataContext _dbPackage = new PackageDataContext();
        private readonly AppUserDataContext _dbAppUser = new AppUserDataContext();
        private readonly RoleDataContext _dbRole = new RoleDataContext();
        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult SelectInstitution()
        {
            Session["institution"] = null;
            ViewBag.Institutions = new SelectList(_dbInstitution.Institutions, "InstitutionId", "Name");
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult SelectInstitution(Institution institution,FormCollection collectedValues)
        {
            var institutionId = Convert.ToInt64(collectedValues["InstitutionId"]);
            var accessCode = collectedValues["AccessCode"];
            institution = _dbInstitution.Institutions.Find(institutionId);
            if (institution.AccessCode == accessCode)
            {
                Session["institution"] = institution;
            }
            else
            {
                ViewBag.Institutions = new SelectList(_dbInstitution.Institutions, "InstitutionId", "Name");
                TempData["access"] = "Access code doesn't match institution!Try Again";
                TempData["notificationType"] = NotificationTypeEnum.Error.ToString();
                return View(institution);
            }

            return RedirectToAction("PersonalData", "EmployeeManagement");
        }
        // GET: CheckOut
        public ActionResult CheckOut(long? id)
        {
            var package = _dbPackage.Packages.Find(id);
            Session["package"] = package;
            return View();
        }
        // POST: CheckOut
        [HttpPost ]
        [ValidateAntiForgeryToken]
        public ActionResult CheckOut(FormCollection collectedValues, Institution institution)
        {
            //var institution = Session["institution"] as Institution;
            var package = Session["package"] as Package;
            var selectedDuration = typeof(SubscriptionDuration).GetEnumName(int.Parse(collectedValues["SubscriptionDuration"]));
            institution.ContactEmail = collectedValues["ContactEmail"];
            institution.ContactNumber = collectedValues["ContactNumber"];
            institution.AccessCode = collectedValues["AccessCode"];
            institution.Location = collectedValues["Location"];
            institution.Name = collectedValues["Name"];
            institution.Motto = collectedValues["Motto"];
            institution.SubscriprionStartDate = DateTime.Now;
            if (selectedDuration == SubscriptionDuration.OneMonth.ToString())
            {
                institution.SubscriptonEndDate = institution.SubscriprionStartDate.AddMonths(1);
            }
            if (selectedDuration == SubscriptionDuration.SixMonths.ToString())
            {
                institution.SubscriptonEndDate = institution.SubscriprionStartDate.AddMonths(6);
            }
            if (selectedDuration == SubscriptionDuration.OneYear.ToString())
            {
                institution.SubscriptonEndDate = institution.SubscriprionStartDate.AddYears(1);
            }
            if (selectedDuration == SubscriptionDuration.TwoYears.ToString())
            {
                institution.SubscriptonEndDate = institution.SubscriprionStartDate.AddYears(2);
            }
            if (selectedDuration == SubscriptionDuration.ThreeYears.ToString())
            {
                institution.SubscriptonEndDate = institution.SubscriprionStartDate.AddYears(3);
            }
            if (package != null) institution.PackageId = package.PackageId;
            //save institution
            _dbInstitution.Institutions.Add(institution);
            _dbInstitution.SaveChanges();

            //store in session
            Session["institution"] = institution;
            Role role = new Role();
            role.InstitutionId = institution.InstitutionId;
            role.ManageAdminAppUsers = false;
            role.ManageAllInstitutions = false;
            role.ManageInstitutions = true;
            role.ManageDepartments = true;
            role.ManageEmployees = true;
            role.ManageFaculties = true;
            role.ManageRolePriviledges = true;
            role.ManageAppUsers = true;
            role.ManagePackages = false;
            role.ManageUnits = true;
            role.Name = "Institution Administrator";

            _dbRole.Roles.Add(role);
            _dbRole.SaveChanges();
            Session["role"] = role;

            //initialize appuser object
            AppUser appUser = new AppUser
            {
                Firstname = institution.Name,
                Lastname = institution.Name,
                Email = institution.ContactEmail,
                InstitutionId = institution.InstitutionId,
                Mobile = institution.ContactNumber,
                EmployeeId = null,
                Password = Membership.GeneratePassword(6, 0),
                DateCreated = DateTime.Now,
                DateLastModified = DateTime.Now,
                LastModifiedBy = 1,
                CreatedBy = 1,
                RoleId = role.RoleId
            };
            //save appuser
            _dbAppUser.AppUsers.Add(appUser);
            _dbAppUser.SaveChanges();
            TempData["login"] = "You have successfully subscribed to opmas!";
            TempData["notificationType"] = NotificationTypeEnum.Success.ToString();
            return RedirectToAction("Login","Account");
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}