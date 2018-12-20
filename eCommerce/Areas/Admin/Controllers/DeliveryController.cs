using eCommerce.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
		public ActionResult SendEmail(long id)
		{

			try
			{


				var invoice_detail = db.InvoiceDetails.Where(x => x.Invoice.Id == id).ToList();
				string table = null;
				foreach (var i in invoice_detail)
				{
					int giam = ((i.Price * i.Product.discountValue) / 100) * i.Quantity;
					int thanhtien = (i.Price - (i.Price * i.Product.discountValue) / 100) * i.Quantity;
					table +=

								"<tr>" +
									   "<td>" +

											  "<strong>" + " " + i.Product.Name + "</strong>" +

									   "</ td >" +

									  "<td align='left'>" + "<span>" + " " + i.Price + "&nbsp;₫</span></td>" +
									  "<td>" + "<span>" + " " + i.Quantity + "&nbsp;₫</span></td>" +
									  "<td>" + "<span>" + " " + giam + "&nbsp;₫</span></td>" +
									  "<td>" + "<span>" + " " + thanhtien + "&nbsp;₫</span></td>" +
							  "</tr>";


				}

				string content = System.IO.File.ReadAllText(Server.MapPath("~/Areas/Admin/Assets/MailTemplate.html"));
				var model = db.Invoices.Find(id);
				content = content.Replace("{{UserName}}", model.Name);
				content = content.Replace("{{Email}}", model.Email);
				content = content.Replace("{{Address}}", model.Address);
				content = content.Replace("{{PhoneNumber}}", model.Phone);
				content = content.Replace("{{PaymentMethod}}", model.PaymentMethod.ToString());
				content = content.Replace("{{Table}}", table);
				content = content.Replace("{{Price}}", invoice_detail.Sum(x => x.Price).ToString());
				content = content.Replace("{{DiscountValue}}", ((invoice_detail.Sum(x => x.Price) * invoice_detail.Sum(x => x.Product.discountValue)) / 100).ToString());
				content = content.Replace("{{Total}}", (invoice_detail.Sum(x => x.Price) - ((invoice_detail.Sum(x => x.Price) * invoice_detail.Sum(x => x.Product.discountValue)) / 100)).ToString());
				content = content.Replace("{State}", "Đơn hàng đang được vận chuyển");
				//Test SMTP

				//create a object to hold the message
				MailMessage newMessage = new MailMessage();

				//Now create the full message
				newMessage.To.Add("nguyenthientam317@gmail.com");
				newMessage.From = new MailAddress("rendoleo317@gmail.com");
				newMessage.Subject = "Trạng thái đơn hàng";
				newMessage.IsBodyHtml = true;
				newMessage.Body = content;


				//Create the SMTP Client object, which do the actual sending
				SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
				{
					Credentials = new NetworkCredential("rendoleo317@gmail.com", "tfvzcxzscphvqeki"),
					EnableSsl = true
				};

				//now send the message
				client.Send(newMessage);

				return RedirectToAction("Details", "Delivery", new { id = model.Id });

			}
			catch (Exception ex)
			{
				return HttpNotFound();
			}

		}
	}
}