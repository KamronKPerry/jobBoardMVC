using FPJobBoard.UI.Models;
using System;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;

namespace FPJobBoard.UI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult About()
        {
            ViewBag.Message = "What We Do:";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Title = "Send Us A Message!";

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult /*Contact*/Index(ContactViewModel contact)
        {

            if (ModelState.IsValid)
            {
                string body = $"Name: {contact.Name}<br/>Email: {contact.Email}<br/>Subject: {contact.Subject} - From JobBoard <br/>Message: {contact.Message}";
                //create and configure the MailMessage object
                MailMessage mm = new MailMessage("postmaster@perryk.net", "kamkperry@outlook.com", contact.Subject + " - From JobBoard <br/>", body);
                //Format the body of the email for HTML
                //cc:/bcc: tp your current used amail address - Optional
                //mm.CC.Add(""); //Secondary Email address. - shows in the email
                //mm.Bcc.Add(""); //secondary email address - Does not show in the email
                //define the body as HTML
                mm.IsBodyHtml = true;
                //priority - Optional
                mm.Priority = MailPriority.High;
                //ReplyToList to send an email back to the use instead of the host
                mm.ReplyToList.Add(contact.Email);
                //setup SMTP Server *server that sends email
                SmtpClient client = new SmtpClient("mail.perryk.net");

                client.Credentials = new NetworkCredential("postmaster@perryk.net", "SaPHira1");

                using (client)
                {
                    try
                    {
                        client.Send(mm);
                        ViewBag.Title = "Thanks!";
                        ViewBag.ErrorMessage = "Your message was sent.";
                    }
                    catch (Exception ex)
                    {
                        //fails capture the reason
                        ViewBag.Title = "Sorry!";
                        ViewBag.ErrorMessage = "There was an error sending your message. Please try again later";
                        ViewBag.Admin = "Message send failure: " + ex.StackTrace;
                    }
                    finally
                    {
                        //not used
                        //ALWAYS EXECUTES - releases and objects back to the runtime for use
                    }

                }
                //return null;
                return View("ContactConfirmation", contact);
            }
            return View(contact);

        }
    }
}
