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
    public class StoresController : Controller
    {
        private MainDbContext db = new MainDbContext();

        // GET: Admin/Stores
        public ActionResult Index()
        {
            return View(db.MerchantStores.ToList());
        }

        // GET: Admin/Stores/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MerchantStore merchantStore = db.MerchantStores.Find(id);
            if (merchantStore == null)
            {
                return HttpNotFound();
            }
            return View(merchantStore);
        }

        // GET: Admin/Stores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Stores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Address,createdDate,Image1,Image2,Image3,Image4")] MerchantStore merchantStore)
        {
            if (ModelState.IsValid)
            {
                db.MerchantStores.Add(merchantStore);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(merchantStore);
        }

        // GET: Admin/Stores/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MerchantStore merchantStore = db.MerchantStores.Find(id);
            if (merchantStore == null)
            {
                return HttpNotFound();
            }
            return View(merchantStore);
        }

        // POST: Admin/Stores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Address,createdDate,Image1,Image2,Image3,Image4")] MerchantStore merchantStore)
        {
            if (ModelState.IsValid)
            {
                db.Entry(merchantStore).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(merchantStore);
        }

        // GET: Admin/Stores/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MerchantStore merchantStore = db.MerchantStores.Find(id);
            if (merchantStore == null)
            {
                return HttpNotFound();
            }
            return View(merchantStore);
        }

        // POST: Admin/Stores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            MerchantStore merchantStore = db.MerchantStores.Find(id);
            db.MerchantStores.Remove(merchantStore);
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
