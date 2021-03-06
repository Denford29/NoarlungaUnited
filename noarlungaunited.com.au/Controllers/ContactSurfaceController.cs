﻿using noarlungaunited.com.au.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Umbraco.Core.Logging;
using Umbraco.Web.Mvc;

namespace noarlungaunited.com.au.Controllers
    {
    public class ContactSurfaceController : SurfaceController
        {

        public ActionResult GetFooterContactForm()
            {
            return PartialView("Contact/FooterForm");
            }

        [HttpPost]
        public ActionResult FooterContactSubmit(ContactModel formContactModel)
            {
            var formData = Request.Form;
            var captchaRequest = formData["g-recaptcha-response"];
            if (string.IsNullOrWhiteSpace(captchaRequest) || !ModelState.IsValid)
                {
                TempData["contactError"] =
                    "Opps... Form Error, There was an error with your details please check them and try again.";
                return CurrentUmbracoPage();
                }

            try
                {
                const string mailBody =
                    "Thank you for contacting Noarlunga United Soccer Club, we have recieved your enquiry and a member of the team will get back to you as soon as possible.<br /> <br />Regards, <br /> Noarlunga United Team";
                var userEmailMessage = new MailMessage
                {
                    Subject = "Your Contact on Noarlunga United Soccer Club",
                    Body = mailBody,
                    From = new MailAddress("support@noarlungaunited.com.au", "Noarlunga United")
                };
                userEmailMessage.To.Add(new MailAddress(formContactModel.EmailAddress.Trim(),
                    formContactModel.FullName.Trim()));
                userEmailMessage.Bcc.Add("denfordmutseriwa@yahoo.com");
                userEmailMessage.IsBodyHtml = true;
                var userSmtpClient = new SmtpClient();
                userSmtpClient.Send(userEmailMessage);

                string adminMailBody =
                    "The contact form has been submitted on the website with the details below. <br /><br />";
                adminMailBody += "Full Name : " + formContactModel.FullName.Trim() + "<br />";
                adminMailBody += "Email Address : " + formContactModel.EmailAddress.Trim() + "<br />";
                adminMailBody += "Phone Number : " + formContactModel.PhoneNumber.Trim() + "<br />";
                adminMailBody += "Message : " + formContactModel.Message.Trim() + "<br />";
                adminMailBody += "<br />Regards,<br />Noarlunga United Team";

                var adminEmaillMessage = new MailMessage
                {
                    Subject = "The contact form has been submited on Noarlunga United Soccer Club",
                    Body = adminMailBody,
                    From = new MailAddress("support@noarlungaunited.com.au", "Noarlunga United")
                };
                adminEmaillMessage.To.Add(new MailAddress("monikakay@optusnet.com.au", "Monika"));
                adminEmaillMessage.Bcc.Add("denfordmutseriwa@yahoo.com");
                adminEmaillMessage.IsBodyHtml = true;
                var adminSmtpClient = new SmtpClient();
                adminSmtpClient.Send(adminEmaillMessage);
                }
            catch (Exception ex)
                {
                var errorMessage = ex.Message + "<br /><br />" + ex.StackTrace + "<br /><br />" + ex.InnerException;
                LogHelper.Error(MethodBase.GetCurrentMethod().DeclaringType, errorMessage, ex);

                var errorEmaillMessage = new MailMessage
                {
                    Subject = "Website error on Noarlunga United Soccer Club",
                    Body = errorMessage,
                    From = new MailAddress("support@noarlungaunited.com.au", "Noarlunga United")
                };
                errorEmaillMessage.To.Add(new MailAddress("denfordmutseriwa@yahoo.com", "Denford"));
                errorEmaillMessage.IsBodyHtml = true;
                var errorSmtpClient = new SmtpClient();
                errorSmtpClient.Send(errorEmaillMessage);

                TempData["contactError"] =
                    "Opps... Contact Error, there was a problem submitting your request.";
                return RedirectToCurrentUmbracoPage();
                }

            TempData["contactSuccess"] =
                "Your contact request has been submitted successfully, a member of the team will get in touch with you shortly...";
            return CurrentUmbracoPage();
            }

        }
    }