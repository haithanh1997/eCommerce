using eCommerce.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace eCommerce.Areas.Merchant.Controllers
{
    [Authorize(Roles = "Merchant")]
    public class MerchantHomeController : Controller
    {
		private MainDbContext db = new MainDbContext();

		// GET: Admin/Products
		public ActionResult Index(string id)
		{
			return View();
		}
		public ActionResult Account()
        {
            return View();
        }
        public ActionResult NewInvoices()
        {
            return View();
        }
        public ActionResult OldInvoices()
        {
            return View();
        }
        public ActionResult Profit()
        {
            return View();
        }
        public ActionResult ManageProduct(string id)
        {
            return View(db.Products.Where(x => x.Store.User.Id == id));
        }
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
        public ActionResult Create(string id)
        {

            ViewData["CategoryId"] = new SelectList(db.Categories, "Id", "Name");
            ViewData["storeId"] = new SelectList(db.MerchantStores.Where(x=>x.User.Id == id ), "Id", "Name");
            ViewBag.Type_Id = new SelectList(db.ProductTypes, "Id", "Name", ViewData["CategoryId"]);

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            var categoryId = long.Parse(Request.Form["CategoryId"]);
            var Type_Id = long.Parse(Request.Form["Type_Id"]);
            var storeId = long.Parse(Request.Form["StoreId"]);
            product.Category = db.Categories.FirstOrDefault(x => x.Id == categoryId);
            product.Type = db.ProductTypes.FirstOrDefault(x => x.Id == Type_Id);
            product.Store = db.MerchantStores.FirstOrDefault(x => x.Id == storeId);
            if (ModelState.IsValid)
            {
                var f1 = Request.Files["Image1"];
                var f2 = Request.Files["Image2"];
                var f3 = Request.Files["Image3"];
                if (f1 != null && f1.ContentLength > 0)
                {
                    var path = Server.MapPath("~/MerchantProduct/" + f1.FileName);
                    f1.SaveAs(path);
                    product.Image1 = "/MerchantProduct/" + f1.FileName;
                }
                if (f2 != null && f2.ContentLength > 0)
                {
                    var path = Server.MapPath("~/MerchantProduct/" + f2.FileName);
                    f2.SaveAs(path);
                    product.Image2 = "/MerchantProduct/" + f2.FileName;
                }
                if (f3 != null && f3.ContentLength > 0)
                {
                    var path = Server.MapPath("~/MerchantProduct/" + f3.FileName);
                    f3.SaveAs(path);
                    product.Image3 = "/MerchantProduct/" + f3.FileName;
                }
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["CategoryId"] = new SelectList(db.Categories, "Id", "Name");
            ViewData["storeId"] = new SelectList(db.MerchantStores, "Id", "Name");
            ViewBag.Type_Id = new SelectList(db.ProductTypes, "Id", "Name");
            return View();
        }
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product model = db.Products.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            ViewData["CategoryId"] = new SelectList(db.Categories, "Id", "Name");
            ViewData["storeId"] = new SelectList(db.MerchantStores, "Id", "Name");
            ViewBag.Type_Id = new SelectList(db.ProductTypes, "Id", "Name");
            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product model)
        {
            var categoryId = long.Parse(Request.Form["CategoryId"]);
            var Type_Id = long.Parse(Request.Form["Type_Id"]);
            var storeId = long.Parse(Request.Form["StoreId"]);
            model.Category = db.Categories.FirstOrDefault(x => x.Id == categoryId);
            model.Type = db.ProductTypes.FirstOrDefault(x => x.Id == Type_Id);
            model.Store = db.MerchantStores.FirstOrDefault(x => x.Id == storeId);
            if (ModelState.IsValid)
            {
                var f1 = Request.Files["Image1"];
                var f2 = Request.Files["Image2"];
                var f3 = Request.Files["Image3"];
                if (f1 != null && f1.ContentLength > 0)
                {
                    var path = Server.MapPath("~/MerchantImages/" + f1.FileName);
                    f1.SaveAs(path);
                    model.Image1 = "/MerchantImages/" + f1.FileName;
                }
                if (f2 != null && f2.ContentLength > 0)
                {
                    var path = Server.MapPath("~/MerchantImages/" + f2.FileName);
                    f2.SaveAs(path);
                    model.Image2 = "/MerchantImages/" + f2.FileName;
                }
                if (f3 != null && f3.ContentLength > 0)
                {
                    var path = Server.MapPath("~/MerchantImages/" + f3.FileName);
                    f3.SaveAs(path);
                    model.Image3 = "/MerchantImages/" + f3.FileName;
                }
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["CategoryId"] = new SelectList(db.Categories, "Id", "Name");
            ViewBag.Type_Id = new SelectList(db.ProductTypes, "Id", "Name");
            ViewData["storeId"] = new SelectList(db.MerchantStores, "Id", "Name");
            return View(model);
        }
        public ActionResult LowQuantity(string id)
        {
            return View(db.Products.Where(x=>x.Price <= 2 && x.Store.User.Id== id));
        }
    }
}