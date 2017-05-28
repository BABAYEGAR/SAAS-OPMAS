using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Opmas.Data.DataContext.DataContext.EmployeeDataContext;
using Opmas.Data.DataContext.DataContext.PaymentDataContext;
using Opmas.Data.Objects.Entities.User;
using Opmas.Data.Objects.Payment;
using Opmas.Data.Service.Enums;

namespace Opmas.Controllers.Payment
{
    public class PaymentAllowancesController : Controller
    {
        private PaymentAllowanceDataContext db = new PaymentAllowanceDataContext();
        private EmployeeDataContext dbc = new EmployeeDataContext();

        // GET: PaymentAllowances
        public ActionResult Index()
        {
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            var paymentAllowances = db.PaymentAllowances.Where(n=>n.InstitutionId == loggedinuser.InstitutionId).Include(p => p.Institution);
            return View(paymentAllowances.ToList());
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
            var allMappings = db.PositionAllowanceMappings.ToList();
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            var paymentAllowanceId = Convert.ToInt64(collectedValues["id"]);
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
                                (n.PaymentAllowanceId == paymentAllowanceId)))
                    {
                    }
                    else
                    {
                        if (loggedinuser?.InstitutionId != null)
                        {
                            var positionDeductionMapping = new PositionAllowanceMapping
                            {
                                PaymentAllowanceId = paymentAllowanceId,
                                EmploymentPositionId = id,
                                InstitutionId = (long)loggedinuser.InstitutionId,
                                DateCreated = DateTime.Now,
                                DateLastModified = DateTime.Now,
                                LastModifiedBy = loggedinuser.AppUserId,
                                CreatedBy = loggedinuser.AppUserId


                            };
                            db.PositionAllowanceMappings.Add(positionDeductionMapping);
                            db.SaveChanges();
                            TempData["message"] = "you have succesfully assigned the employment position(s) to the payment allowance item!";
                            TempData["notificationtype"] = NotificationTypeEnum.Success.ToString();
                        }
                        else
                        {
                            TempData["message"] = "Session has expired, Login and try again!";
                            TempData["notificationtype"] = NotificationTypeEnum.Success.ToString();
                            return RedirectToAction("Login", "Account");
                        }
                    }
                }
            }
            else
            {
                TempData["message"] = "no employment position has been selected!";
                TempData["notificationtype"] = NotificationTypeEnum.Error.ToString();
                return RedirectToAction("AssignPosition", new { id = paymentAllowanceId });
            }
            return RedirectToAction("AssignPosition", new { id = paymentAllowanceId });
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
            return View();
        }

        // POST: PaymentAllowances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PaymentAllowanceId,Name,Rate")] PaymentAllowance paymentAllowance)
        {
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            if (ModelState.IsValid)
            {
                paymentAllowance.DateCreated = DateTime.Now;
                paymentAllowance.DateLastModified = DateTime.Now;
                if (loggedinuser != null)
                {
                    paymentAllowance.CreatedBy = loggedinuser.AppUserId;
                    paymentAllowance.LastModifiedBy = loggedinuser.AppUserId;
                    if (loggedinuser.InstitutionId != null)
                        paymentAllowance.InstitutionId = (long)loggedinuser.InstitutionId;
                }
                db.PaymentAllowances.Add(paymentAllowance);
                db.SaveChanges();
                TempData["message"] = "you have succesfully added a new payment allowance item!";
                TempData["notificationtype"] = NotificationTypeEnum.Success.ToString();
                return RedirectToAction("Index");
            }
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
        public ActionResult Edit([Bind(Include = "PaymentAllowanceId,Name,Rate,InstitutionId,CreatedBy,DateCreated")] PaymentAllowance paymentAllowance)
        {
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            if (ModelState.IsValid)
            {
                paymentAllowance.DateLastModified = DateTime.Now;
                if (loggedinuser != null) paymentAllowance.LastModifiedBy = loggedinuser.AppUserId;
                db.Entry(paymentAllowance).State = EntityState.Modified;
                db.SaveChanges();
                TempData["message"] = "you have succesfully modified the payment allowance item!";
                TempData["notificationtype"] = NotificationTypeEnum.Success.ToString();
                return RedirectToAction("Index");
            }
            return View(paymentAllowance);
        }
        // GET: PaymentAllowances/RemoveMapping/5
        public ActionResult RemoveMapping(long id)
        {
            var allowance = Session["paymentallowance"] as PaymentAllowance;
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            var paymentAllowance =
                db.PositionAllowanceMappings.SingleOrDefault(
                    n =>
                        (n.EmploymentPositionId == id) && (n.InstitutionId == loggedinuser.InstitutionId) &&
                        (n.PaymentAllowanceId == allowance.PaymentAllowanceId));
            long allowanceId = 0;
            if (paymentAllowance != null)
                if (allowance != null) allowanceId = allowance.PaymentAllowanceId;
            db.PositionAllowanceMappings.Remove(paymentAllowance);
            db.SaveChanges();
            Session["paymentallowance"] = null;
            return RedirectToAction("AssignPosition", new { id = allowanceId });
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
            TempData["message"] = "you have succesfully deleted the payment allowance item!";
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
