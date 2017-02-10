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
    public class PaymentDeductionsController : Controller
    {
        private PaymentDeductionDataContext db = new PaymentDeductionDataContext();

        // GET: PaymentDeductions
        public ActionResult Index()
        {
            var paymentDeduction = db.PaymentDeduction.Include(p => p.Institution);
            return View(paymentDeduction.ToList());
        }

        // GET: PaymentDeductions/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentDeduction paymentDeduction = db.PaymentDeduction.Find(id);
            if (paymentDeduction == null)
            {
                return HttpNotFound();
            }
            return View(paymentDeduction);
        }

        // GET: PaymentDeductions/Create
        public ActionResult Create()
        {
            ViewBag.InstitutionId = new SelectList(db.Institutions, "InstitutionId", "Name");
            return View();
        }

        // POST: PaymentDeductions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PaymentDeductionId,Name,Rate,InstitutionId,CreatedBy,DateCreated,DateLastModified,LastModifiedBy")] PaymentDeduction paymentDeduction)
        {
            if (ModelState.IsValid)
            {
                db.PaymentDeduction.Add(paymentDeduction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.InstitutionId = new SelectList(db.Institutions, "InstitutionId", "Name", paymentDeduction.InstitutionId);
            return View(paymentDeduction);
        }

        // GET: PaymentDeductions/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentDeduction paymentDeduction = db.PaymentDeduction.Find(id);
            if (paymentDeduction == null)
            {
                return HttpNotFound();
            }
            ViewBag.InstitutionId = new SelectList(db.Institutions, "InstitutionId", "Name", paymentDeduction.InstitutionId);
            return View(paymentDeduction);
        }

        // POST: PaymentDeductions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PaymentDeductionId,Name,Rate,InstitutionId,CreatedBy,DateCreated,DateLastModified,LastModifiedBy")] PaymentDeduction paymentDeduction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paymentDeduction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InstitutionId = new SelectList(db.Institutions, "InstitutionId", "Name", paymentDeduction.InstitutionId);
            return View(paymentDeduction);
        }

        // GET: PaymentDeductions/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentDeduction paymentDeduction = db.PaymentDeduction.Find(id);
            if (paymentDeduction == null)
            {
                return HttpNotFound();
            }
            return View(paymentDeduction);
        }

        // POST: PaymentDeductions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            PaymentDeduction paymentDeduction = db.PaymentDeduction.Find(id);
            db.PaymentDeduction.Remove(paymentDeduction);
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
