using eCommerce.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCommerce.Areas.Merchant.Controllers
{
    [Authorize(Roles = "Merchant")]
    public class PackageController : Controller
    {
        MainDbContext db = new MainDbContext();
        // GET: Merchant/Package
        public ActionResult Index()
        {
            var model = db.Packages.Where(x => x.isDisabled == false).ToList();
            foreach (var item in model)
            {
                item.Price /= 1000;
            }
            return View(model);
        }
        public ActionResult PurchasePackage(long id)
        {
            return View();
        }
	    
		public ActionResult Ad()
		{
			var model = db.Packages.Where(x => x.isDisabled == false).ToList();
			foreach (var item in model)
			{
				item.Price /= 1000;
			}
			return PartialView(model);
		}
		public ActionResult AdProduct(string id)
		{
			var model = db.Products.Where(x => x.isDisabled == false && x.AdType != AdType.Default).ToList();
			return View(model);
		}
	}
}