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