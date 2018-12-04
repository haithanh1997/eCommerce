using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace eCommerce.Areas.Admin.Controllers
{
    public class MerchantsAccountController : Controller
    {
		private MainDbContext db = new MainDbContext();
		// GET: Admin/MerchantsAccount
		public ActionResult Index()
		{
			return View(db.MerchantStores.ToList());
		}
		public ActionResult Confirmation()
        {
			return View(db.MerchantStores.ToList());
        }
		public ActionResult Detail()
		{
			return View();
		}
		

	}
}