using eCommerce.Areas.Admin.Models;
using eCommerce.Models;
using eCommerce.Static;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCommerce.Areas.Admin.Controllers
{
    [Authorize(Roles = "Moderator")]
    public class StatisticController : Controller
    {
        MainDbContext db = new MainDbContext();
        // GET: Admin/Statistic
		public ActionResult LowQuatity()
		{
			return View(db.Products.Where(x=>x.Quantity <=2).ToList());
		}
		public ActionResult Notify()
		{
			return View();
		}
        public ActionResult Daily()
        {
            int total = 0;
            var model = db.InvoiceDetails.Where(x => x.isDisabled == false &&
                        x.isPaymented == false &&
                        x.Invoice.Status == EntityFramework.ProductStatus.Delivered).GroupBy(x => x.Product.Store.Id).Select(x => new StatisticDailyModel() {
                            Name = x.FirstOrDefault().Product.Store.Name,
                            Id = x.FirstOrDefault().Product.Store.User.Id,
                            Email = x.FirstOrDefault().Product.Store.User.Email,
                            Total = x.Sum(y => y.Price)
                        }).ToList();
            foreach(var item in model)
            {
                total = total + item.Total;
            }
            ViewBag.Total = total;
            return View(model);
        }

        public ActionResult RePaid(string id)
        {
            var invoiceDetails = db.InvoiceDetails.Where(x => x.isDisabled == false &&
                       x.isPaymented == false &&
                       x.Product.Store.User.Id == id &&
                       x.Invoice.Status == EntityFramework.ProductStatus.Delivered).ToList();
            var total = invoiceDetails.Sum(x => x.Price);
            while(total > 50000000)
            {
                if(invoiceDetails != null)
                {
                    invoiceDetails.RemoveAt(0);
                    invoiceDetails.Sum(x => x.Price);
                }
            }

            var transactionId = "MERCHANTREPAID" + (new Random()).Next(100000);
            while (db.Invoices.Any(x => x.TransactionId.Equals(transactionId)))
                transactionId = "MERCHANTREPAID" + (new Random()).Next(100000);

            var mcHistory = new EntityFramework.MerchantRepaidHistory()
            {
                CreatedDate = DateTime.Now,
                Merchant = db.Users.FirstOrDefault(x => x.Id == id),
                IsTemporary = true,
                TransactionId = transactionId,
                Total = total,
            };
            db.MerchantRepaidHistorys.Add(mcHistory);
            foreach (var item in invoiceDetails)
                if(item!=null)
                    db.MerchantRepaidDetails.Add(new EntityFramework.MerchantRepaidDetail()
                    {
                        History = mcHistory,
                        InvoiceDetail = item,
                    });
            db.SaveChanges();
            return Redirect(Payment.RepaidPaymentApi(transactionId, total));
        }

        public ActionResult CreateRePaid(OnlinePaymentModel model)
        {
            
            if (model.status == 0)
            {
                var merchantRepaid = db.MerchantRepaidHistorys.FirstOrDefault(x => x.TransactionId.Equals(model.transactionID));
                merchantRepaid.IsTemporary = false;
                merchantRepaid.Ticket = model.ticket;

                var invoiceDetails = db.MerchantRepaidDetails.Where(x => x.History.Id == merchantRepaid.Id).ToList();
                foreach (var item in invoiceDetails)
                    item.InvoiceDetail.isPaymented = true;

                db.SaveChanges();

                 return RedirectToAction("Daily", "Statistic");
            }
            return RedirectToAction("Index", "Package");
        }

        public ActionResult Monthly()
        {
            int total = 0;
            var model = db.Invoices.Where(x => x.isDisabled == false &&
                        x.createdDate.Month == DateTime.Now.Month &&
                        x.createdDate.Year == DateTime.Now.Year &&
                        x.Status == EntityFramework.ProductStatus.Delivered).ToList();
            foreach (var item in model)
            {
                total = total + item.Total;
            }
            ViewBag.Total = total;
            return View(model);
        }

        public ActionResult Year()
        {
            int total = 0;
            var model = db.Invoices.Where(x => x.isDisabled == false &&
                        x.createdDate.Year == DateTime.Now.Year &&
                        x.Status == EntityFramework.ProductStatus.Delivered).ToList();
            foreach (var item in model)
            {
                total = total + item.Total;
            }
            ViewBag.Total = total;
            return View(model);
        }

        public ActionResult DailyPackage()
        {
            int total = 0;
            var model = db.PackageInvoices.Where(x => x.createdDate.Day == DateTime.Now.Day &&
                         x.createdDate.Month == DateTime.Now.Month &&
                         x.createdDate.Year == DateTime.Now.Year).ToList();
            foreach(var item in model)
            {
                total += item.Price;
            }
            ViewBag.Total = total;
            return View(model);
        }

        public ActionResult MonthlyPackage()
        {
            int total = 0;
            var model = db.PackageInvoices.Where(x => x.createdDate.Month == DateTime.Now.Month &&
                         x.createdDate.Year == DateTime.Now.Year).ToList();
            foreach (var item in model)
            {
                total += item.Price;
            }
            ViewBag.Total = total;
            return View(model);
        }

        public ActionResult YearPackage()
        {
            int total = 0;
            var model = db.PackageInvoices.Where(x => x.createdDate.Year == DateTime.Now.Year).ToList();
            foreach (var item in model)
            {
                total += item.Price;
            }
            ViewBag.Total = total;
            return View(model);
        }

        public ActionResult DailyAd()
        {
            int total = 0;
            var model = db.AdInvoices.Where(x => x.createdDate.Day == DateTime.Now.Day &&
                         x.createdDate.Month == DateTime.Now.Month &&
                         x.createdDate.Year == DateTime.Now.Year).ToList();
            foreach (var item in model)
            {
                total += item.Price;
            }
            ViewBag.Total = total;
            return View(model);
        }

        public ActionResult MonthlyAd()
        {
            int total = 0;
            var model = db.AdInvoices.Where(x => x.createdDate.Month == DateTime.Now.Month &&
                         x.createdDate.Year == DateTime.Now.Year).ToList();
            foreach (var item in model)
            {
                total += item.Price;
            }
            ViewBag.Total = total;
            return View(model);
        }

        public ActionResult YearAd()
        {
            int total = 0;
            var model = db.AdInvoices.Where(x => x.createdDate.Year == DateTime.Now.Year).ToList();
            foreach (var item in model)
            {
                total += item.Price;
            }
            ViewBag.Total = total;
            return View(model);
        }
    }
}