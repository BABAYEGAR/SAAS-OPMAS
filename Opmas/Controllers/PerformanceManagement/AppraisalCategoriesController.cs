using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Opmas.Data.DataContext.DataContext.EmployeeDataContext;
using Opmas.Data.DataContext.DataContext.PerformanceDataContext;
using Opmas.Data.Objects.Entities.User;
using Opmas.Data.Objects.Mappings;
using Opmas.Data.Objects.PerformanceManagement;
using Opmas.Data.Service.Enums;

namespace Opmas.Controllers.PerformanceManagement
{
    public class AppraisalCategoriesController : Controller
    {
        private AppraisalCategoryDataContext db = new AppraisalCategoryDataContext();
        private EmployeeDataContext dbc = new EmployeeDataContext();

        // GET: AppraisalCategories
        public ActionResult Index()
        {
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            var appraisalCategories = db.AppraisalCategories.Where(n=>n.InstitutionId == loggedinuser.InstitutionId).Include(a => a.Institution);
            return View(appraisalCategories.ToList());
        }
        // GET: AssignPosition
        public ActionResult AssignPosition(long? id)
        {
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            var employmentPositions = dbc.EmploymentPositions.Where(n => n.InstitutionId == loggedinuser.InstitutionId);
            ViewBag.Id = id;
            return View(employmentPositions);
        }
        // POST: SubmitAssignedPositionList
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitAssignedPositionList(int[] table_records, FormCollection collectedValues)
        {
            var allMappings = db.AppraisalPositionMappings.ToList();
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            var categoryId = Convert.ToInt64(collectedValues["id"]);
            if (table_records != null)
            {
                var length = table_records.Length;
                for (var i = 0; i < length; i++)
                {
                    var id = table_records[i];
                    if (
                        allMappings.Any(
                            n =>
                                (n.EmploymentPositionId == id) && (n.InstitutionId == loggedinuser?.InstitutionId) &&
                                (n.AppraisalCategoryId == categoryId)))
                    {
                    }
                    else
                    {
                        if (loggedinuser?.InstitutionId != null)
                        {
                            var appraisalPositionMapping = new AppraisalPositionMapping()
                            {
                                AppraisalCategoryId = categoryId,
                                EmploymentPositionId = id,
                                InstitutionId = (long)loggedinuser.InstitutionId,
                                DateCreated = DateTime.Now,
                                DateLastModified = DateTime.Now,
                                LastModifiedBy = loggedinuser.AppUserId,
                                CreatedBy = loggedinuser.AppUserId


                            };
                            db.AppraisalPositionMappings.Add(appraisalPositionMapping);
                            db.SaveChanges();
                            TempData["employmentposition"] = "you have succesfully assigned the employment position(s) to the appraisal category!";
                            TempData["notificationtype"] = NotificationTypeEnum.Success.ToString();
                        }
                        else
                        {
                            TempData["login"] = "Session has expired, Login and try again!";
                            TempData["notificationtype"] = NotificationTypeEnum.Success.ToString();
                            return RedirectToAction("Login", "Account");
                        }
                    }
                }
            }
            else
            {
                TempData["employmentposition"] = "no employment position has been selected!";
                TempData["notificationtype"] = NotificationTypeEnum.Error.ToString();
                return RedirectToAction("AssignPosition", new { id = categoryId });
            }
            return RedirectToAction("AssignPosition", new { id = categoryId });
        }
        // GET: AppraisalCategories/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppraisalCategory appraisalCategory = db.AppraisalCategories.Find(id);
            if (appraisalCategory == null)
            {
                return HttpNotFound();
            }
            return View(appraisalCategory);
        }

        // GET: AppraisalCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AppraisalCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AppraisalCategoryId,Name,Description,MaximumScore")] AppraisalCategory appraisalCategory)
        {
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            if (ModelState.IsValid)
            {
                appraisalCategory.DateCreated = DateTime.Now;
                appraisalCategory.DateLastModified = DateTime.Now;
                appraisalCategory.CurrentScore = 0;
                if (loggedinuser != null)
                {
                    appraisalCategory.CreatedBy = loggedinuser.AppUserId;
                    appraisalCategory.LastModifiedBy = loggedinuser.AppUserId;
                    if (loggedinuser.InstitutionId != null)
                        appraisalCategory.InstitutionId = (long)loggedinuser.InstitutionId;
                }
                db.AppraisalCategories.Add(appraisalCategory);
                db.SaveChanges();
                TempData["appraisalcategory"] = "you have succesfully added a new appraisal category!";
                TempData["notificationtype"] = NotificationTypeEnum.Success.ToString();
                return RedirectToAction("Index");
            }
            return View(appraisalCategory);
        }

        // GET: AppraisalCategories/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppraisalCategory appraisalCategory = db.AppraisalCategories.Find(id);
            if (appraisalCategory == null)
            {
                return HttpNotFound();
            }
            return View(appraisalCategory);
        }

        // POST: AppraisalCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AppraisalCategoryId,Name,Description,InstitutionId,MaximumScore,CurrentScore,CreatedBy,DateCreated")] AppraisalCategory appraisalCategory)
        {
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            if (ModelState.IsValid)
            {
                appraisalCategory.DateLastModified = DateTime.Now;
                if (loggedinuser != null) appraisalCategory.LastModifiedBy = loggedinuser.AppUserId;
                db.Entry(appraisalCategory).State = EntityState.Modified;
                db.SaveChanges();
                TempData["appraisalcategory"] = "you have succesfully modified the appraisal category!";
                TempData["notificationtype"] = NotificationTypeEnum.Success.ToString();
                return RedirectToAction("Index");
            }
            return View(appraisalCategory);
        }

        // GET: AppraisalCategories/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppraisalCategory appraisalCategory = db.AppraisalCategories.Find(id);
            if (appraisalCategory == null)
            {
                return HttpNotFound();
            }
            return View(appraisalCategory);
        }

        // POST: AppraisalCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            AppraisalCategory appraisalCategory = db.AppraisalCategories.Find(id);
            db.AppraisalCategories.Remove(appraisalCategory);
            db.SaveChanges();
            TempData["appraisalcategory"] = "you have succesfully deleted the appraisal category!";
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
