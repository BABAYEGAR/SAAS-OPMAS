using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Opmas.Data.DataContext.DataContext.EmployeeDataContext;
using Opmas.Data.DataContext.DataContext.PaymentDataContext;
using Opmas.Data.Objects.Entities.User;
using Opmas.Data.Objects.Mappings;
using Opmas.Data.Objects.Payment;
using Opmas.Data.Service.Enums;

namespace Opmas.Controllers.Payment
{
    public class PaymentDeductionsController : Controller
    {
        private PaymentDeductionDataContext db = new PaymentDeductionDataContext();
        private EmployeeDataContext dbc = new EmployeeDataContext();

        // GET: PaymentDeductions
        public ActionResult Index()
        {
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            var paymentDeduction = db.PaymentDeduction.Where(n=>n.InstitutionId == loggedinuser.InstitutionId).Include(p => p.Institution);
            return View(paymentDeduction.ToList());
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
            var allMappings = db.PositionDeductionMappings.ToList();
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            var paymentDeductionId = Convert.ToInt64(collectedValues["id"]);
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
                                (n.PaymentDeductionId == paymentDeductionId)))
                    {
                    }
                    else
                    {
                        if (loggedinuser?.InstitutionId != null)
                        {
                            var positionDeductionMapping = new PositionDeductionMapping
                            {
                                PaymentDeductionId = paymentDeductionId,
                                EmploymentPositionId = id,
                                InstitutionId = (long)loggedinuser.InstitutionId,
                                DateCreated = DateTime.Now,
                                DateLastModified = DateTime.Now,
                                LastModifiedBy = loggedinuser.AppUserId,
                                CreatedBy = loggedinuser.AppUserId
                                

                            };
                            db.PositionDeductionMappings.Add(positionDeductionMapping);
                            db.SaveChanges();
                            TempData["employmentposition"] = "you have succesfully assigned the employment position(s) to the payment deduction item!";
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
                return RedirectToAction("AssignPosition", new { id = paymentDeductionId });
            }
            return RedirectToAction("AssignPosition", new { id = paymentDeductionId });
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
            return View();
        }

        // POST: PaymentDeductions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PaymentDeductionId,Name,Rate")] PaymentDeduction paymentDeduction)
        {
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            if (ModelState.IsValid)
            {
                paymentDeduction.DateCreated = DateTime.Now;
                paymentDeduction.DateLastModified = DateTime.Now;
                if (loggedinuser != null)
                {
                    paymentDeduction.CreatedBy = loggedinuser.AppUserId;
                    paymentDeduction.LastModifiedBy = loggedinuser.AppUserId;
                    if (loggedinuser.InstitutionId != null)
                        paymentDeduction.InstitutionId = (long)loggedinuser.InstitutionId;
                }
                db.PaymentDeduction.Add(paymentDeduction);
                db.SaveChanges();
                TempData["deduction"] = "you have succesfully added a new payment deduction item!";
                TempData["notificationtype"] = NotificationTypeEnum.Success.ToString();                                                                                                                                                                                         
                return RedirectToAction("Index");
            }
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
            return View(paymentDeduction);
        }

        // POST: PaymentDeductions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PaymentDeductionId,Name,Rate,InstitutionId,CreatedBy,DateCreated")] PaymentDeduction paymentDeduction)
        {
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            if (ModelState.IsValid)
            {
                paymentDeduction.DateLastModified = DateTime.Now;
                if (loggedinuser != null) paymentDeduction.LastModifiedBy = loggedinuser.AppUserId;
                db.Entry(paymentDeduction).State = EntityState.Modified;
                db.SaveChanges();
                TempData["deduction"] = "you have succesfully modified the payment deduction item!";
                TempData["notificationtype"] = NotificationTypeEnum.Success.ToString();
                return RedirectToAction("Index");
            }
            ViewBag.InstitutionId = new SelectList(db.Institutions, "InstitutionId", "Name", paymentDeduction.InstitutionId);
            return View(paymentDeduction);
        }
        // GET: PaymentDeductions/RemoveMapping/5
        public ActionResult RemoveMapping(long id)
        {
            var deduction = Session["paymentdeduction"] as PaymentDeduction;
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            var paymentDeduction =
                db.PositionDeductionMappings.SingleOrDefault(
                    n =>
                        (n.EmploymentPositionId == id) && (n.InstitutionId == loggedinuser.InstitutionId) &&
                        (n.PaymentDeductionId == deduction.PaymentDeductionId));
            long deductionId = 0;
            if (paymentDeduction != null)
                if (deduction != null) deductionId = deduction.PaymentDeductionId;
            db.PositionDeductionMappings.Remove(paymentDeduction);
            db.SaveChanges();
            Session["paymentdeduction"] = null;
            return RedirectToAction("AssignPosition", new { id = deductionId });
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
            TempData["deduction"] = "you have succesfully deleted the payment deduction item!";
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
