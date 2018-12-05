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
    public class AdminProductsController : Controller
    {
        private MainDbContext db = new MainDbContext();

        // GET: Admin/Products
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        // GET: Admin/Products/Details/5
	
        public ActionResult Details(long? id, string user_id) 
		{
			ViewBag.userName = (from x in db.MerchantStores.Where(x => x.User.Id == user_id) select x.User.UserName).FirstOrDefault();

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

       


        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Price,Quantity,brandName,discountValue,Description,CPU,RAM,hardDrive,screenType,GPU,IOPort,OS,DesignType,Size,uploadDate,updateDate,deletedDate,Image1,Image2,Image3,isDisabled")] Product product)
        {
            if (ModelState.IsValid)
            {
				product.isDisabled = true ;
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

      

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
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
