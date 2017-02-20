using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Opmas.Data.DataContext.DataContext.EmployeeDataContext;
using Opmas.Data.DataContext.DataContext.LeaveDataContext;
using Opmas.Data.DataContext.DataContext.PaymentDataContext;
using Opmas.Data.Objects.Entities.SystemManagement;
using Opmas.Data.Objects.Entities.User;
using Opmas.Data.Objects.Payment;
using Opmas.Data.Service.Enums;

namespace Opmas.Controllers.Payment
{
    public class PaymentDeductionRequestsController : Controller
    {
        private readonly PaymentDeductionRequestDataContext db = new PaymentDeductionRequestDataContext();
        private EmployeeDataContext dbc = new EmployeeDataContext();

        // GET: PaymentDeductionRequests
        public ActionResult Index()
        {
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            var paymentDeductionRequests =
                db.PaymentDeductionRequests.Where(n => n.InstitutionId == loggedinuser.InstitutionId)
                    .Include(p => p.Employee);
            return View(paymentDeductionRequests.ToList());
        }

        // GET: EmployeePaymentDeductionRequest
        public ActionResult EmployeePaymentDeductionRequest(long? id)
        {
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            var paymentDeductionRequests =
                db.PaymentDeductionRequests.Where(
                        n => (n.InstitutionId == loggedinuser.InstitutionId) && (n.EmployeeId == id))
                    .Include(p => p.Employee);
            return View(paymentDeductionRequests.ToList());
        }

        // GET: PaymentDeductionRequests/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var paymentDeductionRequest = db.PaymentDeductionRequests.Find(id);
            if (paymentDeductionRequest == null)
                return HttpNotFound();
            return View(paymentDeductionRequest);
        }

        // GET: PaymentDeductionRequests/GrantRequest
        public ActionResult GrantRequest(long? id)
        {
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            var request = db.PaymentDeductionRequests.Find(id);
            request.Status = PaymentDeductionRequestStatus.Granted.ToString();
            request.DateLastModified = DateTime.Now;
            if (loggedinuser != null) request.LastModifiedBy = loggedinuser.AppUserId;
            db.Entry(request).State = EntityState.Modified;
            db.SaveChanges();
            if (loggedinuser != null)
            {
                ApplicationNotification notify = new ApplicationNotification
                {
                    AssignedTo = request.CreatedBy,
                    Description = "Your payment deduction request has been granted!",
                    CreatedBy = loggedinuser.AppUserId,
                    DateCreated = DateTime.Now,
                    InstitutionId = loggedinuser.InstitutionId,
                    NotificationType = ApplicationNotificationType.PaymentDeductionRequest.ToString(),
                    ItemId = request.PaymentDeductionRequestId
                };
                dbc.ApplicationNotifications.Add(notify);
                dbc.SaveChanges();
            }
            TempData["deductionrequest"] = "you have succesfully granted the payment deduction request!";
            TempData["notificationtype"] = NotificationTypeEnum.Success.ToString();
            return RedirectToAction("Index");
        }

        // GET: PaymentDeductionRequests/StopDeduction
        public ActionResult StopDeduction(long? id)
        {
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            var request = db.PaymentDeductionRequests.Find(id);
            request.Status = PaymentDeductionRequestStatus.Cancel.ToString();
            request.DateLastModified = DateTime.Now;
            if (loggedinuser != null) request.LastModifiedBy = loggedinuser.AppUserId;
            db.Entry(request).State = EntityState.Modified;
            db.SaveChanges();
            TempData["deductionrequest"] = "you have succesfully stopped the deduction!";
            TempData["notificationtype"] = NotificationTypeEnum.Success.ToString();
            if (loggedinuser != null)
                return RedirectToAction("EmployeePaymentDeductionRequest", new {id = loggedinuser.EmployeeId});
            return RedirectToAction("Index");
        }

        // POST: PaymentDeductionRequests/DenyRequest
        public ActionResult DenyRequest(FormCollection collectedValues)
        {
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            var id = Convert.ToInt64(collectedValues["PaymentDeductionRequestId"]);
            var request = db.PaymentDeductionRequests.Find(id);
            request.Status = PaymentDeductionRequestStatus.Denied.ToString();
            request.DateLastModified = DateTime.Now;
            request.Comment = collectedValues["Comment"];
            if (loggedinuser != null) request.LastModifiedBy = loggedinuser.AppUserId;
            db.Entry(request).State = EntityState.Modified;
            db.SaveChanges();
            if (loggedinuser != null)
            {
                ApplicationNotification notify = new ApplicationNotification
                {
                    AssignedTo = request.CreatedBy,
                    Description = "Your payment deduction request has been denied!",
                    CreatedBy = loggedinuser.AppUserId,
                    DateCreated = DateTime.Now,
                    InstitutionId = loggedinuser.InstitutionId,
                    NotificationType = ApplicationNotificationType.PaymentDeductionRequest.ToString(),
                    ItemId = request.PaymentDeductionRequestId
                };
                dbc.ApplicationNotifications.Add(notify);
                dbc.SaveChanges();
            }
            TempData["deductionrequest"] = "you have succesfully denied the payment deduction request!";
            TempData["notificationtype"] = NotificationTypeEnum.Success.ToString();
            return RedirectToAction("Index");
        }

        // GET: PaymentDeductionRequests/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PaymentDeductionRequests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "PaymentDeductionRequestId,Amount,RequestTitle,Reason,Comment")] PaymentDeductionRequest
                paymentDeductionRequest)
        {
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            if (ModelState.IsValid)
            {
                paymentDeductionRequest.DateCreated = DateTime.Now;
                paymentDeductionRequest.DateLastModified = DateTime.Now;
                paymentDeductionRequest.Status = PaymentDeductionRequestStatus.Pending.ToString();
                if (loggedinuser != null)
                {
                    paymentDeductionRequest.CreatedBy = loggedinuser.AppUserId;
                    paymentDeductionRequest.LastModifiedBy = loggedinuser.AppUserId;
                    paymentDeductionRequest.EmployeeId = loggedinuser.EmployeeId;
                    if (loggedinuser.InstitutionId != null)
                        paymentDeductionRequest.InstitutionId = (long) loggedinuser.InstitutionId;
                }
                db.PaymentDeductionRequests.Add(paymentDeductionRequest);
                db.SaveChanges();
                TempData["deductionrequest"] = "you have succesfully requested for a deduction in your payment!";
                TempData["notificationtype"] = NotificationTypeEnum.Success.ToString();
                return RedirectToAction("EmployeePaymentDeductionRequest");
            }
            return View(paymentDeductionRequest);
        }

        // GET: PaymentDeductionRequests/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var paymentDeductionRequest = db.PaymentDeductionRequests.Find(id);
            if (paymentDeductionRequest == null)
                return HttpNotFound();
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeId",
                paymentDeductionRequest.EmployeeId);
            return View(paymentDeductionRequest);
        }

        // POST: PaymentDeductionRequests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(
                 Include =
                     "PaymentDeductionRequestId,RequestTitle,Amount,Reason,Status,Comment,EmployeeId,CreatedBy,DateCreated,InstitutionId"
             )] PaymentDeductionRequest paymentDeductionRequest)
        {
            var loggedinuser = Session["opmasloggedinuser"] as AppUser;
            if (ModelState.IsValid)
            {
                paymentDeductionRequest.DateLastModified = DateTime.Now;
                if (loggedinuser != null) paymentDeductionRequest.LastModifiedBy = loggedinuser.AppUserId;
                db.Entry(paymentDeductionRequest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeId",
                paymentDeductionRequest.EmployeeId);
            return View(paymentDeductionRequest);
        }

        // GET: PaymentDeductionRequests/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var paymentDeductionRequest = db.PaymentDeductionRequests.Find(id);
            if (paymentDeductionRequest == null)
                return HttpNotFound();
            return View(paymentDeductionRequest);
        }

        // POST: PaymentDeductionRequests/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            var paymentDeductionRequest = db.PaymentDeductionRequests.Find(id);
            db.PaymentDeductionRequests.Remove(paymentDeductionRequest);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }
    }
}