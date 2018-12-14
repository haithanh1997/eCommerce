using eCommerce.EntityFramework;
using eCommerce.Models;
using eCommerce.Static;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCommerce.Areas.Merchant.Controllers
{
    [Authorize(Roles = "Merchant")]
    public class PackageController : Controller
    {
        MainDbContext db = new MainDbContext();

        private IdentityUser CurrentUser
        {
            get { return db.Users.FirstOrDefault(x => x.UserName.Equals(User.Identity.Name)); }
        }

        // GET: Merchant/Package
        public ActionResult Index()
        {
            var model = db.Packages.Where(x => x.isDisabled == false).ToList();
            foreach (var item in model)
            {
                item.Price /= 1000;
            }
            return View(model);
        }
        public ActionResult PurchasePackage(long id)
        {
            var adPackage = db.Packages.FirstOrDefault(x => x.Id == id);
            var transactionId = "PACKAGE" + (new Random()).Next(100000) + "ID" + adPackage.Id;
            while (db.Invoices.Any(x => x.TransactionId.Equals(transactionId)))
                transactionId = "PACKAGE" + (new Random()).Next(100000) + "ID" + adPackage.Id;
            return Redirect(Payment.MerchantPaymentApi(transactionId, adPackage.Price));
        }

        public ActionResult PurchaseAd(long id)
        {
            var ad = db.AdPackages.FirstOrDefault(x => x.Id == id);
            var transactionId = "ADVERTISE" + (new Random()).Next(100000) + "ID" + ad.Id;
            while (db.Invoices.Any(x => x.TransactionId.Equals(transactionId)))
                transactionId = "ADVERTISE" + (new Random()).Next(100000) + "ID" + ad.Id;
            return Redirect(Payment.MerchantPaymentApi(transactionId, ad.Price));
        }

        //  Trang mua  quảng cáo
        public ActionResult Ad()
		{

			var model = db.AdPackages.Where(x => x.isDisabled == false).ToList();
			foreach (var item in model)
			{
				item.Price /= 1000;
			}
			return View(model);
		}

		// Trang Thêm hóa đơn quảng cáo (được gọi khi thanh toán Online xong)
		public ActionResult CreateInvoice(OnlinePaymentModel model)
		{
            if (CurrentUser != null)
            {
                if(model.status == 0)
                {
                    if(model.transactionID.Contains("PACKAGE"))
                    {
                        var adPackageId = long.Parse(model.transactionID.Substring(model.transactionID.IndexOf("ID") + 2));
                        PackageInvoice invoice = new PackageInvoice()
                        {
                            Package = db.Packages.FirstOrDefault(x => x.Id == adPackageId),
                            User = CurrentUser,
                            Price = (from c in db.AdPackages.Where(c => c.Id == adPackageId) select c.Price).FirstOrDefault(),
                            createdDate = DateTime.Now,
                            transactionId = model.transactionID,
                            hashCode = model.ticket,
                        };
                        db.PackageInvoices.Add(invoice);
                        db.SaveChanges();

                        return RedirectToAction("PaymentComplete", "Package");
                    }
                    else
                    {
                        var adId = long.Parse(model.transactionID.Substring(model.transactionID.IndexOf("ID") + 2));
                        var createdDate = DateTime.Now;
                        AdInvoice invoice = new AdInvoice()
                        {
                            AdPackage = db.AdPackages.FirstOrDefault(x => x.Id == adId),
                            User = CurrentUser,
                            Price = (from c in db.AdPackages.Where(c => c.Id == adId) select c.Price).FirstOrDefault(),
                            createdDate = createdDate,
                            ExpiredDate = createdDate.AddDays((from c in db.AdPackages.Where(c => c.Id == adId) select c.Period).FirstOrDefault()),
                            Status = false,
                            transactionId = model.transactionID,
                            hashCode = model.ticket,
                        };
                        db.AdInvoices.Add(invoice);
                        db.SaveChanges();
                        
                        return RedirectToAction("AdManage", new { id = adId });
                    }
                }
            }
            return RedirectToAction("Index", "Package");
        }

        public ActionResult PaymentComplete()
        {
            return View();
        }

		// Trang hiển thị danh sách hiện có
		public ActionResult AdManage(string id)
		{
			
			var model = db.AdInvoices.Where(x => x.User.Id == id && x.Status == false).ToList();
			foreach (var item in model)
			{
				item.Price /= 1000;
			}
			return View(model);
		}

		// Trang set up cho quảng cáo
		public ActionResult AdCreate(string id)
		{
			var model = db.Products.Where(x => x.Store.User.Id == id && x.AdType == AdType.No).ToList();
			return View(model);
		}

		//Trang danh sách các quảng cáo đang chạy
		public ActionResult AdList(string id)
		{
			var model = db.Products.Where(x => x.Store.User.Id == id && x.AdType == AdType.Hot || x.AdType == AdType.New || x.AdType == AdType.Encourage || x.AdType == AdType.FlashSale ).ToList();
			return View(model);
		}
	}
}