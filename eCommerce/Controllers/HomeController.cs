using eCommerce.Models;
using eCommerce.Static;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace eCommerce.Controllers
{
    public class HomeController : Controller
    {
        MainDbContext db = new MainDbContext();
        public ActionResult Index()
        {

			//Test SMTP

			//create a object to hold the message
			//MailMessage newMessage = new MailMessage();



			//Now create the full message
			//newMessage.To.Add(recipentAddress);
			//newMessage.From = senderAddress;
			//newMessage.Subject = "Tieu de";
			//newMessage.Body = "Noi dung";

			//Create the SMTP Client object, which do the actual sending
			//SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
			//{
			//    Credentials = new NetworkCredential("rendoleo317@gmail.com", "tfvzcxzscphvqeki"),
			//    EnableSsl = true
			//};

			//now send the message
			//client.Send(newMessage);

	
			var model = db.Products.ToList();
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [ChildActionOnly]
        public ActionResult Categories()
        {
            var model = db.Categories.Where(x => x.isDisabled == false).ToList();
            return PartialView(model);
        }
		[ChildActionOnly]
		public ActionResult SlideShow()
		{

			var model = db.SlideShows.ToList();
			return PartialView(model);
		}
    }
}