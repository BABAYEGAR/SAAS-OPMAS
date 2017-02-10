using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Opmas.Data.DataContext.DataContext.PaymentDataContext;
using Opmas.Data.Objects.Payment;

namespace Opmas.Controllers.Payment
{
    public class PaymentAllowancesController : Controller
    {
        private PaymentAllowanceDataContext db = new PaymentAllowanceDataContext();

        // GET: PaymentAllowances
        public ActionResult Index()
        {
            var paymentAllowances = db.PaymentAllowances.Include(p => p.Institution);
            return View(paymentAllowances.ToList());
        }

        // GET: PaymentAllowances/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentAllowance paymentAllowance = db.PaymentAllowances.Find(id);
            if (paymentAllowance == null)
            {
                return HttpNotFound();
            }
            return View(paymentAllowance);
        }

        // GET: PaymentAllowances/Create
        public ActionResult Create()
        {
            ViewBag.InstitutionId = new SelectList(db.Institutions, "InstitutionId", "Name");
            return View();
        }

        // POST: PaymentAllowances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PaymentAllowanceId,Name,Amount,InstitutionId,CreatedBy,DateCreated,DateLastModified,LastModifiedBy")] PaymentAllowance paymentAllowance)
        {
            if (ModelState.IsValid)
            {
                db.PaymentAllowances.Add(paymentAllowance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.InstitutionId = new SelectList(db.Institutions, "InstitutionId", "Name", paymentAllowance.InstitutionId);
            return View(paymentAllowance);
        }

        // GET: PaymentAllowances/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentAllowance paymentAllowance = db.PaymentAllowances.Find(id);
            if (paymentAllowance == null)
            {
                return HttpNotFound();
            }
            ViewBag.InstitutionId = new SelectList(db.Institutions, "InstitutionId", "Name", paymentAllowance.InstitutionId);
            return View(paymentAllowance);
        }

        // POST: PaymentAllowances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PaymentAllowanceId,Name,Amount,InstitutionId,CreatedBy,DateCreated,DateLastModified,LastModifiedBy")] PaymentAllowance paymentAllowance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paymentAllowance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InstitutionId = new SelectList(db.Institutions, "InstitutionId", "Name", paymentAllowance.InstitutionId);
            return View(paymentAllowance);
        }

        // GET: PaymentAllowances/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentAllowance paymentAllowance = db.PaymentAllowances.Find(id);
            if (paymentAllowance == null)
            {
                return HttpNotFound();
            }
            return View(paymentAllowance);
        }

        // POST: PaymentAllowances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            PaymentAllowance paymentAllowance = db.PaymentAllowances.Find(id);
            db.PaymentAllowances.Remove(paymentAllowance);
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
