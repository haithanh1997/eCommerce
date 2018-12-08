using eCommerce.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace eCommerce.Areas.Admin.Controllers
{
    public class DeliveryController : Controller
    {
		MainDbContext db = new MainDbContext();
        // GET: Admin/Delivery
        public ActionResult Index(long? id)
        {
			
			return View(db.Invoices.Where(x=>x.Status==ProductStatus.Processing || x.Status == ProductStatus.Delivering).ToList());
		}

		public ActionResult ChangeStatus(long? id)
		{
			if (ModelState.IsValid)
			{
				var model = db.Invoices.Find(id);
                if(model.Status == ProductStatus.Delivering)
                {
                    model.Status = ProductStatus.Delivered;
                }
				else if(model.Status == ProductStatus.Processing)
                {
                    model.Status = ProductStatus.Delivering;
                }

				db.Entry(model).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View();
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
			var invoice_detail = db.InvoiceDetails.Where(x => x.Invoice.Id == id).ToList();
			if (invoice_detail == null)
			{
				return HttpNotFound();
			}
			return View(invoice_detail);
		}
	}
}