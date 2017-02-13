using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Opmas.Data.DataContext.DataContext.TrainingDataContext;
using Opmas.Data.Objects.Entities.User;
using Opmas.Data.Objects.Training;
using Opmas.Data.Service.Enums;

namespace Opmas.Controllers.TrainingManagement
{
    public class TrainingCategoriesController : Controller
    {
        private TrainingCategoryDataContext db = new TrainingCategoryDataContext();

        // GET: TrainingCategories
        public ActionResult Index()
        {
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            var trainingCategory = db.TrainingCategory.Where(n=>n.InstitutionId == loggedinuser.InstitutionId).Include(t => t.Institution);
            return View(trainingCategory.ToList());
        }

        // GET: TrainingCategories/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrainingCategory trainingCategory = db.TrainingCategory.Find(id);
            if (trainingCategory == null)
            {
                return HttpNotFound();
            }
            return View(trainingCategory);
        }

        // GET: TrainingCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TrainingCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TrainingCategoryId,Name,Description")] TrainingCategory trainingCategory)
        {
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            if (ModelState.IsValid)
            {
                trainingCategory.DateCreated = DateTime.Now;
                trainingCategory.DateLastModified = DateTime.Now;
                if (loggedinuser != null)
                {
                    trainingCategory.CreatedBy = loggedinuser.AppUserId;
                    trainingCategory.LastModifiedBy = loggedinuser.AppUserId;
                    if (loggedinuser.InstitutionId != null)
                        trainingCategory.InstitutionId = (long) loggedinuser.InstitutionId;
                }
                db.TrainingCategory.Add(trainingCategory);
                db.SaveChanges();
                TempData["trainingcategory"] = "you have succesfully added a new training category!";
                TempData["notificationtype"] = NotificationTypeEnum.Success.ToString();
                return RedirectToAction("Index");
            }

            return View(trainingCategory);
        }

        // GET: TrainingCategories/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrainingCategory trainingCategory = db.TrainingCategory.Find(id);
            if (trainingCategory == null)
            {
                return HttpNotFound();
            }
            return View(trainingCategory);
        }

        // POST: TrainingCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TrainingCategoryId,Name,InstitutionId,CreatedBy,DateCreated,Description")] TrainingCategory trainingCategory)
        {
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            if (ModelState.IsValid)
            {
                trainingCategory.DateLastModified = DateTime.Now;
                if (loggedinuser != null) trainingCategory.LastModifiedBy = loggedinuser.AppUserId;
                db.Entry(trainingCategory).State = EntityState.Modified;
                db.SaveChanges();
                TempData["trainingcategory"] = "you have succesfully modified a training category!";
                TempData["notificationtype"] = NotificationTypeEnum.Success.ToString();
                return RedirectToAction("Index");
            }
            return View(trainingCategory);
        }

        // GET: TrainingCategories/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrainingCategory trainingCategory = db.TrainingCategory.Find(id);
            if (trainingCategory == null)
            {
                return HttpNotFound();
            }
            return View(trainingCategory);
        }

        // POST: TrainingCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            TrainingCategory trainingCategory = db.TrainingCategory.Find(id);
            db.TrainingCategory.Remove(trainingCategory);
            db.SaveChanges();
            TempData["trainingcategory"] = "you have succesfully deleted a training category!";
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
