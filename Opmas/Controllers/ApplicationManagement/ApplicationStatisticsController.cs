using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Opmas.Data.DataContext.DataContext.SystemDataContext;
using Opmas.Data.Objects.Entities.SystemManagement;

namespace Opmas.Controllers.ApplicationManagement
{
    public class ApplicationStatisticsController : Controller
    {
        private ApplicationStatisticDataContext db = new ApplicationStatisticDataContext();

        // GET: ApplicationStatistics
        public ActionResult Index()
        {
            return View(db.ApplicationStatistics.ToList());
        }
        // GET: ApplicationStatistics/GetInstitutionSessions
        public ActionResult GetInstitutionSessions(long? id)
        {
            List<ApplicationStatistic> session;
            if (id != null)
            {
                session = db.ApplicationStatistics.Where(n => n.InstitutionId == id).ToList();
            }
            else
            {
                session = new List<ApplicationStatistic>();
            }
            return View("Index",session);
        }


        // GET: ApplicationStatistics/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationStatistic applicationStatistic = db.ApplicationStatistics.Find(id);
            if (applicationStatistic == null)
            {
                return HttpNotFound();
            }
            return View(applicationStatistic);
        }

        // GET: ApplicationStatistics/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ApplicationStatistics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ApplicationStatisticId,Action,DateOccured,InstitutionId,LoggedInUserId")] ApplicationStatistic applicationStatistic)
        {
            if (ModelState.IsValid)
            {
                db.ApplicationStatistics.Add(applicationStatistic);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(applicationStatistic);
        }

        // GET: ApplicationStatistics/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationStatistic applicationStatistic = db.ApplicationStatistics.Find(id);
            if (applicationStatistic == null)
            {
                return HttpNotFound();
            }
            return View(applicationStatistic);
        }

        // POST: ApplicationStatistics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ApplicationStatisticId,Action,DateOccured,InstitutionId,LoggedInUserId")] ApplicationStatistic applicationStatistic)
        {
            if (ModelState.IsValid)
            {
                db.Entry(applicationStatistic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(applicationStatistic);
        }

        // GET: ApplicationStatistics/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationStatistic applicationStatistic = db.ApplicationStatistics.Find(id);
            if (applicationStatistic == null)
            {
                return HttpNotFound();
            }
            return View(applicationStatistic);
        }

        // POST: ApplicationStatistics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            ApplicationStatistic applicationStatistic = db.ApplicationStatistics.Find(id);
            db.ApplicationStatistics.Remove(applicationStatistic);
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
