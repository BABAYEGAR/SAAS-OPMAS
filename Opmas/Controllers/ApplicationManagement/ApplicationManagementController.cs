using System;
using System.Configuration;
using System.Web.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Opmas.Controllers.ApplicationManagement
{
    public class ApplicationManagementController : Controller
    {
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Checkout(long amount, string email, string phone, string customer_id, string name)
        {
            /**These were passed in from the checkout build up which I have elsewhere**/
            ViewBag.Amount = amount;
            ViewBag.Email = email;
            ViewBag.Phone = phone;
            ViewBag.CustomerId = customer_id + " - " + name;
            bool useCallback = Convert.ToBoolean(ConfigurationManager.AppSettings["pwqusecallback"]);

            /**Set a Page that I want the payment gateway to redirect to after payment**/
            /**NOTE THAT: you can embed an identifier for the transaction (like a unique reference or order number or session Id) in the redirectUrl as a querystring param**/
            //if (useCallback)
            //    ViewBag.RedirectUrl = Utility.GetSiteRoot(true, "siteroot") + "/Advanced/ValidatePaymentWithCallback" + "?unique=" + customer_id;
            //else
            //    ViewBag.RedirectUrl = Utility.GetSiteRoot(true, "siteroot") + "/Advanced/ValidatePaymentWithoutCallback" + "?unique=" + customer_id;
            ViewBag.PaymentCode = ConfigurationManager.AppSettings["pwqpaymentcode"];
            ViewBag.TestMode = ConfigurationManager.AppSettings["pwqtestmode"];

            /**Preserve the amount I expect to be paid in memory. Also Preserve the redirect Url I am setting**/
            Session["amount"] = amount;
            Session["redirectUrl"] = ViewBag.RedirectUrl;
            Session["customer_id"] = customer_id;
            return View();
        }
    }
}