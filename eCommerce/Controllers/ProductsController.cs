using eCommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCommerce.Controllers
{
    public class ProductsController : Controller
    {
        MainDbContext db = new MainDbContext();
        // GET: Products
        public ActionResult ProductsIndex()
        {
            return View(new ProductIndexModel()
            {
                Products = db.Products.ToList(),
            });
        }
        public ActionResult Types()
		{
			return View();
		}
		public ActionResult Detail()
		{
			return View();
		}
	}
}