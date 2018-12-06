using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using eCommerce.EntityFramework;
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
			
			return View(db.MerchantStores.Where(x=>x.isDisabled == true).ToList());
		}

		public ActionResult Confirmation()
        {
			return View(db.MerchantStores.Where(x => x.isDisabled == false).ToList());
        }

		
		public ActionResult Confirm(long? id)
		{
			//UserManager<ApplicationUser> UserManager;
			if (ModelState.IsValid)
			{
				var store = db.MerchantStores.Find(id);
				//UserManager.AddToRoles(store.User.Id, "Merchant");
				store.isDisabled = true;
				db.Entry(store).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View();
		}


		public ActionResult Detail(long? id)
		{

			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			MerchantStore store = db.MerchantStores.Find(id);
			if (store == null)
			{
				return HttpNotFound();
			}
			return View(store);

		}
		[Authorize(Roles = "Admin")]
		public ActionResult Block(long? id)
		{
			//UserManager<ApplicationUser> UserManager;
			if (ModelState.IsValid)
			{
				var store = db.MerchantStores.Find(id);
				//UserManager.AddToRoles(store.User.Id, "Merchant");
				store.isDisabled = false;
				db.Entry(store).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Confirmation");
			}
			return View();
		}
		[Authorize(Roles = "Admin")]
		[ActionName("Delete")]
		public ActionResult DeleteConfirmed(long id)
		{
			MerchantStore store = db.MerchantStores.Find(id);
			db.MerchantStores.Remove(store);
			db.SaveChanges();
			return RedirectToAction("Index");
		}


	}
}