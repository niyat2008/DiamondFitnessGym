using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; 
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using DiamondFitnessGym.Controllers;

namespace DiamondFitnessGym.Controllers
{
    public class HomeController : Controller
    {
        
        private string emails = @"gm@daimondfit.com,df@daimondfit.com,daimondfit@daimondfit.com,co@daimondfit.com,info@daimondfit.com";
        private string server = @"niyat.com.sa";
        private string email = @"a.shehata@niyat.com.sa";
        private string pass = @"pM3^43rn";

        
        [Route("ar")]
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }

        //[Route("")]
        //public ActionResult CommingSoon()
        //{
        //    return View();
        //}
        [Route("ArFContact")]
        public ActionResult ArFContact()
        {
            return View();
        }
        [Route("EnFContact")]
        public ActionResult EnFContact()
        {
            return View();
        }
        [Route("subscribe")]
        [HttpGet]
        public ActionResult subscribe(string email)
        {
            if (email == null)
            {
                return Json(false);
            }

            EmailManager emailManager = new EmailManager(this.server,this.email, this.pass);


            if (emailManager.SendMailSubscriction(email, emails))
            {
                return Json(true);
            }
            else
            {
                return Json(false);
            }

        }
        public class EmailModel
        {
            public string Email { get; set; }
        }
        //[Route("ar")]
        //public ActionResult Index()
        //{
        //    return View();
        //}

        [Route("ar/Book")]
        public ActionResult ArContact()
        {
            return View();
        }

        [Route("ar/Terms")]
        public ActionResult ArTerms()
        {
            return View();
        }

        [Route("ar/Promotions")]
        public ActionResult ArPromotions()
        {
            return View();
        }

        [Route("ar/Faqs")]
        public ActionResult ArFaq()
        {
            return View();
        }

        [Route("en")]
        public ActionResult IndexEnglish()
        {
            return View();
        }
        [Route("en/Promotions")]
        public ActionResult EnPromotions()
        {
            return View();
        }
        [Route("en/Book")]

        public ActionResult EnContact()
        {
            ViewBag.Get = true;
            return View();
        }
        [Route("en/Terms")]
        public ActionResult EnTerms()
        {
            return View();
        }
        [Route("en/Faqs")]
        public ActionResult EnFaq()
        {
            return View();
        }


        [Route("en/Book")]
        [HttpPost]
        public ActionResult EnSendMail(MailModel mailModel)
        {

            if (!ModelState.IsValid)
            {
                return View("EnContact", mailModel);
            }

            var email = mailModel.Email ?? "user@none.com";

            EmailManager emailManager = new EmailManager(this.server, this.email, this.pass);


            if (emailManager.SendMail(email, emails, mailModel))
            {
                ViewBag.Get = false;

                ViewBag.Added = true;
                return View("EnContact");
            }
            else
            {
                ViewBag.Get = false;
                ViewBag.Added = false;
                return View("EnContact");
            }


        }

        [Route("ar/Book")]
        [HttpPost]
        public ActionResult ArSendMail(MailModel mailModel)
        {
            if (!ModelState.IsValid)
            {
                return View("ArContact", mailModel);
            }

            var email = mailModel.Email ?? "user@none.com";

            EmailManager emailManager = new EmailManager(this.server, this.email, this.pass);

            var res = emailManager.SendMail(email, emails, mailModel);
            if (res)
            {
                ViewBag.Get = false;

                ViewBag.Added = true;
                return View("ArContact");
            }
            else
            {
                ViewBag.Get = false;

                ViewBag.Added = false;
                return View("ArContact");
            }
        }
    }
}
public  class EmailManager
{
    public string server { get; set; }
    public string emailHost { get; set; }
    public string password { get; set; }


    public EmailManager(string emailServer, string emailHost, string password)
    {
        this.emailHost = emailHost;
        this.server = emailServer;
        this.password = password;
    }

    public bool SendMail(string from, string to, MailModel mailModel)
    {
        SmtpClient smtpClient = new SmtpClient();
        smtpClient.Host = this.server;
        smtpClient.Port = 587;


        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtpClient.Credentials = new System.Net.NetworkCredential(emailHost, password);
        smtpClient.UseDefaultCredentials = false;

        MailMessage mailMessage = new MailMessage(from, to);
        mailMessage.Subject = mailModel.FirstName + " " + mailModel.secondName + " | DaimondFit Client Application";
        mailMessage.Body = String.Format(
            "The Details of Client \n"+ 
            "Name: {0} {1} \n" +
            "Mobile: {2} \n" +
            "National ID: {3} \n" +
            "Subscription: {4} \n" +
            "Age: {5} \n" +
            "City: {6} \n" +
            "Email: {7} \n"+ 
            "ExtraService: {8} \n", mailModel.FirstName,
            mailModel.secondName,
            mailModel.Phone,
            mailModel.NationalId,
            mailModel.Period,
            mailModel.Age, 
            mailModel.City,
            mailModel.Email,
            mailModel.ExtraServices).ToString();
        try
        {
            smtpClient.Send(mailMessage);
        }
        catch (Exception e)
        {
            var x = e;
            return false;
        }


        return  true;
    }

    internal bool SendMailSubscriction(string from, string to)
    {
        SmtpClient smtpClient = new SmtpClient();
        smtpClient.Host = server;
        smtpClient.Port = 587;
        smtpClient.EnableSsl = true;

        smtpClient.Credentials = new System.Net.NetworkCredential(emailHost, password);

        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        MailMessage mailMessage = new MailMessage(from, to);
        mailMessage.Subject = from  + " | DaimondFit Client Subscribe";
        mailMessage.Body = String.Format(
            "The Details of Client: {0}  ", from).ToString();
        try
        {
            smtpClient.Send(mailMessage);
        }
        catch (Exception e)
        {
            return false;
        }
        return true;

    }
}
