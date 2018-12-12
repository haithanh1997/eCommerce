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
    public class InvoicesController : Controller
    {
        private MainDbContext db = new MainDbContext();

        // GET: Admin/Invoices
        public ActionResult Index()
        {
            var model = db.Invoices.Where(x=>x.Status == ProductStatus.NotValidated).ToList();
            var model1 = db.InvoiceDetails.Where(x=>x.Invoice.Status == ProductStatus.NotValidated).ToList();
            foreach ( var i in model )
            {
                
                foreach(var j in model1)
                {
                    if(i.Id == j.Id &&  j.isDisabled == false )
                    {
                        continue;
                    }
                }
                i.Status = ProductStatus.Validated;
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
            }
            return View(db.Invoices.Where(x => x.Status == ProductStatus.Validated || x.Status == ProductStatus.Delivered).ToList());
        }

        // GET: Admin/Invoices/Details/5
        public ActionResult Details(long? id)
        {
			ViewBag.ngaytao = (from x in db.Invoices.Where(x => x.Id == id) select x.createdDate).FirstOrDefault();
			ViewBag.cuahang = (from x in db.InvoiceDetails.Where(x => x.Id == id) select x.Product.Name).FirstOrDefault();
			ViewBag.nguoimua = (from x in db.Invoices.Where(x => x.Id == id) select x.User.UserName).FirstOrDefault();
			ViewBag.diachi = (from x in db.Invoices.Where(x => x.Id == id) select x.Address).FirstOrDefault();
			ViewBag.sdt = (from x in db.Invoices.Where(x => x.Id == id) select x.User.PhoneNumber).FirstOrDefault();
			ViewBag.email = (from x in db.Invoices.Where(x => x.Id == id) select x.User.Email).FirstOrDefault();
			ViewBag.madonhang = (from x in db.Invoices.Where(x => x.Id == id) select x.Id).FirstOrDefault();
			ViewBag.giaohang = (from x in db.Invoices.Where(x => x.Id == id) select x.DeliveryMethod).FirstOrDefault();
			ViewBag.thanhtoan = (from x in db.Invoices.Where(x => x.Id == id) select x.PaymentMethod).FirstOrDefault();
			ViewBag.tong = (from x in db.Invoices.Where(x => x.Id == id) select x.Total).FirstOrDefault();
	

			if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           var invoice_detail = db.InvoiceDetails.Where(x=>x.Invoice.Id == id).ToList();
            if (invoice_detail == null)
            {
                return HttpNotFound();
            }
            return View(invoice_detail);
        }
		
		public ActionResult ChangeStatus(long? id)
		{
			if (ModelState.IsValid)
			{
				var model = db.Invoices.Find(id);
				model.Status = ProductStatus.Processing;
				db.Entry(model).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View();
		}
		// GET: Admin/Invoices/Create
		public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Invoices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Address,Total,PaymentMethod,DeliveryMethod,Description,createdDate,Status,isDisabled")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                db.Invoices.Add(invoice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(invoice);
        }

        // GET: Admin/Invoices/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // POST: Admin/Invoices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Address,Total,PaymentMethod,DeliveryMethod,Description,createdDate,Status,isDisabled")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invoice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(invoice);
        }

        // GET: Admin/Invoices/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // POST: Admin/Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Invoice invoice = db.Invoices.Find(id);
            db.Invoices.Remove(invoice);
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
