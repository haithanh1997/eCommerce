using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eCommerce.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace eCommerce.Areas.Admin.Controllers
{
	[Authorize(Roles = "Moderator")]
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
		[HttpPost]
		public ActionResult Confirm(string id, UserManager<ApplicationUser> UserManager)
		{
			UserManager.AddToRoles(id, "Merchant");
			return View();
		}
		public ActionResult Detail()
		{
			return View();
		}
		[Authorize(Roles = "Admin")]
		public ActionResult Block()
		{
			return View();
		}
		[Authorize(Roles = "Admin")]
		public ActionResult Delete()
		{
			return View();

		}
		

	}
}