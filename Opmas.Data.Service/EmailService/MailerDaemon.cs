﻿using System;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Web;
using Opmas.Data.Objects.Entities.User;

namespace Opmas.Data.Service.EmailService
{
    public class MailerDaemon
    {
        /// <summary>
        ///     This method sends an email containing a username and password to a newly created user
        /// </summary>
        /// <param name="user"></param>
        public void NewUser(AppUser user)
        {
            var message = new MailMessage
            {
                From = new MailAddress(""),
                Subject = "New User Details",
                Priority = MailPriority.High,
                SubjectEncoding = Encoding.UTF8,
                Body = GetEmailBody_NewUserCreated(user),
                IsBodyHtml = true
            };
            //message.To.Add(Config.DevEmailAddress);
            message.To.Add(user.Email);
            try
            {
                new SmtpClient().Send(message);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        /// <summary>
        ///     Html page content for the new user email
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private static string GetEmailBody_NewUserCreated(AppUser user)
        {
            return
                new StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplates/NewUserCreated.html")).ReadToEnd()
                    .Replace("DISPLAYNAME", user.Firstname)
                    .Replace("USERNAME", user.Email)
                    .Replace("PASSWORD", user.Password)
                    .Replace("URL", "http://10.10.15.77/bhuinfo/Account/Login")
                    .Replace("ROLE", "")
                    .Replace("FROM", "");
        }
        /// <summary>
        ///     This method sends an email containing a username and password to a newly created user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="loggedinuser"></param>
        public void UserDeleted(AppUser user,AppUser loggedinuser)
        {
            var message = new MailMessage
            {
                From = new MailAddress(""),
                Subject = "Delete Action Details",
                Priority = MailPriority.High,
                SubjectEncoding = Encoding.UTF8,
                Body = GetEmailBody_UserDeleted(user,loggedinuser),
                IsBodyHtml = true
            };
            //message.To.Add(Config.DevEmailAddress);
            message.To.Add(loggedinuser.Email);
            try
            {
                new SmtpClient().Send(message);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        /// <summary>
        ///     Html page content for the new user email
        /// </summary>
        /// <param name="user"></param>
        /// <param name="loggedinuser"></param>
        /// <returns></returns>
        private static string GetEmailBody_UserDeleted(AppUser user,AppUser loggedinuser)
        {
            
            return
                new StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplates/UserDeleted.html")).ReadToEnd()
                    .Replace("DISPLAYNAME", user.DisplayName)
                    .Replace("USERNAME", user.Email)
                    .Replace("URL", "http://10.10.15.77/bhuinfo/Account/Login")
                    .Replace("DeletedBy", loggedinuser.DisplayName)
                    .Replace("Date", DateTime.Now.ToShortDateString())
                    .Replace("Time", DateTime.Now.ToShortTimeString())
                    .Replace("FROM", "");
        }

        public void ResetUserPassword(AppUser user)
        {
            var message = new MailMessage();

            message.From = new MailAddress("");
            message.To.Add(user.Email);
            message.Subject = "New Password";
            message.Priority = MailPriority.High;
            message.SubjectEncoding = Encoding.UTF8;
            message.Body = GetEmailBody_NewPasswordCreated(user);
            message.IsBodyHtml = true;
            try
            {
                new SmtpClient().Send(message);
            }
            catch (Exception)
            {
                // ignored
            }
        }
        /// <summary>
        /// This contains the html and passed values for the password reset emails
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private static string GetEmailBody_NewPasswordCreated(AppUser user)
        {
            return
                new StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplates/ResetPassword.html"))
                    .ReadToEnd()
                    .Replace("FROM", "")
                    .Replace("DISPLAYNAME", user.Firstname)
                    .Replace("URL", "http://10.10.15.77/bhuinfo/Account/ResetPassword/" + user.AppUserId);
        }
        /// <summary>
        /// This method sends emails to the support of the bhuinfo application
        /// </summary>
        /// <param name="senderName"></param>
        /// <param name="senderMessage"></param>
        /// <param name="email"></param>
        public void ContactUs(string senderName,string senderMessage,string email)
        {
            var message = new MailMessage();
            message.From = new MailAddress(email);
            message.To.Add("");
            message.Subject = "New Contact";
            message.Priority = MailPriority.High;
            message.SubjectEncoding = Encoding.UTF8;
            message.Body = GetEmailBody_ContactUs(senderName, senderMessage);
            message.IsBodyHtml = true;
            try
            {
                new SmtpClient().Send(message);
            }
            catch (Exception)
            {
                // ignored
            }
        }
        /// <summary>
        /// This method contains the html and passed values for the contact us email
        /// </summary>
        /// <param name="senderName"></param>
        /// <param name="senderMessage"></param>
        /// <returns></returns>
        private static string GetEmailBody_ContactUs(string senderName, string senderMessage)
        {
            return
                new StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplates/ContactUs.html"))
                    .ReadToEnd()
                    .Replace("DISPLAYNAME", senderName)
                    .Replace("URL", "http://10.10.15.77/bhuinfo")
                    .Replace("MESSAGE", senderMessage);
        }
    }
}