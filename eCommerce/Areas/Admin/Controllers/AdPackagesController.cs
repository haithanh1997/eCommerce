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
    public class AdPackagesController : Controller
    {
        private MainDbContext db = new MainDbContext();

        // GET: Admin/AdPackages
        public ActionResult Index()
        {
            return View(db.AdPackages.ToList());
        }

        // GET: Admin/AdPackages/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdPackage adPackage = db.AdPackages.Find(id);
            if (adPackage == null)
            {
                return HttpNotFound();
            }
            return View(adPackage);
        }

        // GET: Admin/AdPackages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdPackages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Price,ExpiredDate,isDisabled")] AdPackage adPackage)
        {
            if (ModelState.IsValid)
            {
                db.AdPackages.Add(adPackage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(adPackage);
        }

        // GET: Admin/AdPackages/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdPackage adPackage = db.AdPackages.Find(id);
            if (adPackage == null)
            {
                return HttpNotFound();
            }
            return View(adPackage);
        }

        // POST: Admin/AdPackages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Price,ExpiredDate,isDisabled")] AdPackage adPackage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adPackage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(adPackage);
        }

        // GET: Admin/AdPackages/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdPackage adPackage = db.AdPackages.Find(id);
            if (adPackage == null)
            {
                return HttpNotFound();
            }
            return View(adPackage);
        }

        // POST: Admin/AdPackages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            AdPackage adPackage = db.AdPackages.Find(id);
            db.AdPackages.Remove(adPackage);
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
