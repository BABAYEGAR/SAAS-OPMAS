using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Opmas.Data.DataContext.DataContext.EmployeeDataContext;
using Opmas.Data.Objects.Entities.Employee;
using Opmas.Data.Objects.Entities.User;
using Opmas.Data.Service.Enums;

namespace Opmas.Controllers.EmployeeManagement
{
    public class EmploymentCategoriesController : Controller
    {
        private EmploymentCategoryDataContext db = new EmploymentCategoryDataContext();

        // GET: EmploymentCategories
        public ActionResult Index()
        {
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            var employmentCategorys = db.EmploymentCategorys.Where(n=>n.InstitutionId == loggedinuser.InstitutionId).Include(e => e.Institution);
            return View(employmentCategorys.ToList());
        }

        // GET: EmploymentCategories/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmploymentCategory employmentCategory = db.EmploymentCategorys.Find(id);
            if (employmentCategory == null)
            {
                return HttpNotFound();
            }
            return View(employmentCategory);
        }

        // GET: EmploymentCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmploymentCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmploymentCategoryId,Name,Description")] EmploymentCategory employmentCategory)
        {
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            if (ModelState.IsValid)
            {
                employmentCategory.DateCreated = DateTime.Now;
                employmentCategory.DateLastModified = DateTime.Now;
                if (loggedinuser != null)
                {
                    employmentCategory.CreatedBy = loggedinuser.AppUserId;
                    employmentCategory.LastModifiedBy = loggedinuser.AppUserId;
                    if (loggedinuser.InstitutionId != null)
                        employmentCategory.InstitutionId = (long)loggedinuser.InstitutionId;
                }
                db.EmploymentCategorys.Add(employmentCategory);
                db.SaveChanges();
                TempData["employmentcategory"] = "you have succesfully created a new Employement Category!";
                TempData["notificationtype"] = NotificationTypeEnum.Success.ToString();
                return RedirectToAction("Index");
            }
            return View(employmentCategory);
        }

        // GET: EmploymentCategories/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmploymentCategory employmentCategory = db.EmploymentCategorys.Find(id);
            if (employmentCategory == null)
            {
                return HttpNotFound();
            }
            return View(employmentCategory);
        }

        // POST: EmploymentCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmploymentCategoryId,Name,InstitutionId,CreatedBy,DateCreated,Description")] EmploymentCategory employmentCategory)
        {
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            if (ModelState.IsValid)
            {
                employmentCategory.DateLastModified = DateTime.Now;
                if (loggedinuser != null) employmentCategory.LastModifiedBy = loggedinuser.AppUserId;
                
                TempData["employmentcategory"] = "you have succesfully modified the Employement Category!";
                TempData["notificationtype"] = NotificationTypeEnum.Success.ToString();
                db.Entry(employmentCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employmentCategory);
        }

        // GET: EmploymentCategories/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmploymentCategory employmentCategory = db.EmploymentCategorys.Find(id);
            if (employmentCategory == null)
            {
                return HttpNotFound();
            }
            return View(employmentCategory);
        }

        // POST: EmploymentCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            EmploymentCategory employmentCategory = db.EmploymentCategorys.Find(id);
            db.EmploymentCategorys.Remove(employmentCategory);
            db.SaveChanges();
            TempData["employmentcategory"] = "you have succesfully deleted the Employement Category!";
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
