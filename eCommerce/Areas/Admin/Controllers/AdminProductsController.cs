using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using eCommerce;
using eCommerce.EntityFramework;

namespace eCommerce.Areas.Admin.Controllers
{
	[Authorize(Roles = "Moderator")]
	public class AdminProductsController : Controller
    {
        private MainDbContext db = new MainDbContext();

        // GET: Admin/Products
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        // GET: Admin/Products/Details/5
	
        public ActionResult Details(long? id) 
		{
			

			if (id == null)
			{
			 return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Product product = db.Products.Find(id);
			if (product == null)
			{
			return HttpNotFound();
			}
			return View(product);
			
		}

		//[HttpPost]
		//[ValidateAntiForgeryToken]
		public ActionResult Block(long? id)
        {
            if (ModelState.IsValid)
            {
				var product = db.Products.Find(id);
				product.isDisabled = true ;
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
			return View();
        }
		//[HttpPost]
		//[ValidateAntiForgeryToken]
		
		
		public ActionResult Sale(long? id)
		{
			if (ModelState.IsValid)
			{
				var product = db.Products.Find(id);
				product.isDisabled = false;
				db.Entry(product).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View();
		}



		//[HttpPost]
		//[ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin")]
		[ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
