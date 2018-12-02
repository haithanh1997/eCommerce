using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using eCommerce;
using eCommerce.Areas.Admin.Models;
using eCommerce.EntityFramework;

namespace eCommerce.Areas.Admin.Controllers
{
    [Authorize(Roles = "Moderator")]
    public class ProductTypesController : Controller
    {
        private MainDbContext db = new MainDbContext();

        // GET: Admin/ProductTypes
        public ActionResult Index()
        {
            return View(db.ProductTypes.ToList());
        }

        // GET: Admin/ProductTypes/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductType productType = db.ProductTypes.Find(id);
            if (productType == null)
            {
                return HttpNotFound();
            }
            return View(productType);
        }

        [Authorize(Roles = "Admin")]
        // GET: Admin/ProductTypes/Create
        public ActionResult Create()
        {
            return View(new ProductTypeModel() {
                Categories = db.Categories.Select(x => new SelectListItem() {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList()
            });
        }

        // POST: Admin/ProductTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(ProductTypeModel productType)
        {
            if (ModelState.IsValid)
            {
                var entity = new ProductType()
                {
                    Name = productType.Name,
                    isDisabled = productType.isDisabled,
                    Category = db.Categories.FirstOrDefault(x => x.Id == productType.CategorySelectedId)
                };
                db.ProductTypes.Add(entity);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            productType.Categories = db.Categories.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            return View(productType);
        }

        [Authorize(Roles = "Admin")]
        // GET: Admin/ProductTypes/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var entity = db.ProductTypes.Find(id);
            ProductTypeModel productType = new ProductTypeModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                CategorySelectedId = entity.Category.Id,
                Categories = db.Categories.Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList()
            };
            if (productType == null)
            {
                return HttpNotFound();
            }
            return View(productType);
        }

        [Authorize(Roles = "Admin")]
        // POST: Admin/ProductTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductTypeModel productType)
        {
            if (ModelState.IsValid)
            {
                var entity = db.ProductTypes.FirstOrDefault(x => x.Id == productType.Id);
                entity.Name = productType.Name;
                entity.isDisabled = productType.isDisabled;
                entity.Category = db.Categories.FirstOrDefault(x => x.Id == productType.CategorySelectedId);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            productType.Categories = db.Categories.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
            return View(productType);
        }

        [Authorize(Roles = "Admin")]
        // GET: Admin/ProductTypes/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductType productType = db.ProductTypes.Find(id);
            if (productType == null)
            {
                return HttpNotFound();
            }
            return View(productType);
        }

        [Authorize(Roles = "Admin")]
        // POST: Admin/ProductTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            ProductType productType = db.ProductTypes.Find(id);
            db.ProductTypes.Remove(productType);
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
