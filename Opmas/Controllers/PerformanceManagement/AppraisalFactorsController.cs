using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Opmas.Data.DataContext.DataContext.PerformanceDataContext;
using Opmas.Data.Objects.Entities.User;
using Opmas.Data.Objects.PerformanceManagement;
using Opmas.Data.Service.Enums;

namespace Opmas.Controllers.PerformanceManagement
{
    public class AppraisalFactorsController : Controller
    {
        private AppraisalFactorDataContext _db = new AppraisalFactorDataContext();

        // GET: AppraisalFactors
        public ActionResult Index()
        {
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            var appraisalFactors = _db.AppraisalFactors.Where(n=>n.InstitutionId == loggedinuser.InstitutionId).Include(a => a.AppraisalCategory).Include(a => a.Institution);
            return View(appraisalFactors.ToList());
        }

        // GET: AppraisalFactors/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppraisalFactor appraisalFactor = _db.AppraisalFactors.Find(id);
            if (appraisalFactor == null)
            {
                return HttpNotFound();
            }
            return View(appraisalFactor);
        }

        // GET: AppraisalFactors/Create
        public ActionResult Create()
        {
            ViewBag.AppraisalCategoryId = new SelectList(_db.AppraisalCategories, "AppraisalCategoryId", "Name");
            return View();
        }

        // POST: AppraisalFactors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AppraisalFactorId,Factor,Description,FactorScore,AppraisalCategoryId")] AppraisalFactor appraisalFactor)
        {
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            if (ModelState.IsValid)
            {
                appraisalFactor.DateCreated = DateTime.Now;
                appraisalFactor.DateLastModified = DateTime.Now;
                if (loggedinuser != null)
                {
                    appraisalFactor.CreatedBy = loggedinuser.AppUserId;
                    appraisalFactor.LastModifiedBy = loggedinuser.AppUserId;
                    if (loggedinuser.InstitutionId != null)
                        appraisalFactor.InstitutionId = (long) loggedinuser.InstitutionId;
                    _db.AppraisalFactors.Add(appraisalFactor);
                    _db.SaveChanges();
                    TempData["appraisalfactor"] = "you have succesfully added a new appraisal factor!";
                    TempData["notificationtype"] = NotificationTypeEnum.Success.ToString();
                    return RedirectToAction("Index");

                }
            }

            ViewBag.AppraisalCategoryId = new SelectList(_db.AppraisalCategories, "AppraisalCategoryId", "Name", appraisalFactor.AppraisalCategoryId);
            return View(appraisalFactor);
        }

        // GET: AppraisalFactors/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppraisalFactor appraisalFactor = _db.AppraisalFactors.Find(id);
            if (appraisalFactor == null)
            {
                return HttpNotFound();
            }
            ViewBag.AppraisalCategoryId = new SelectList(_db.AppraisalCategories, "AppraisalCategoryId", "Name", appraisalFactor.AppraisalCategoryId);
            return View(appraisalFactor);
        }

        // POST: AppraisalFactors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AppraisalFactorId,Factor,Description,FactorScore,InstitutionId,AppraisalCategoryId,CreatedBy,DateCreated")] AppraisalFactor appraisalFactor)
        {
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            if (ModelState.IsValid)
            {
                appraisalFactor.DateLastModified = DateTime.Now;
                if (loggedinuser != null) appraisalFactor.LastModifiedBy = loggedinuser.AppUserId;
                _db.Entry(appraisalFactor).State = EntityState.Modified;
                _db.SaveChanges();
                TempData["appraisalfactor"] = "you have succesfully modified the appraisal factor!";
                TempData["notificationtype"] = NotificationTypeEnum.Success.ToString();
                return RedirectToAction("Index");
            }
            ViewBag.AppraisalCategoryId = new SelectList(_db.AppraisalCategories, "AppraisalCategoryId", "Name", appraisalFactor.AppraisalCategoryId);
            return View(appraisalFactor);
        }

        // GET: AppraisalFactors/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppraisalFactor appraisalFactor = _db.AppraisalFactors.Find(id);
            if (appraisalFactor == null)
            {
                return HttpNotFound();
            }
            return View(appraisalFactor);
        }

        // POST: AppraisalFactors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            AppraisalFactor appraisalFactor = _db.AppraisalFactors.Find(id);
            _db.AppraisalFactors.Remove(appraisalFactor);
            _db.SaveChanges();
            TempData["appraisalfactor"] = "you have succesfully modified the appraisal factor!";
            TempData["notificationtype"] = NotificationTypeEnum.Success.ToString();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
