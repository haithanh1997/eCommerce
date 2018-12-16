using eCommerce.EntityFramework;
using eCommerce.Models;
using eCommerce.Static;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
                if(model.status == 1)
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

						// Thêm số lần đăng cho Store
						var store = db.MerchantStores.Where(x => x.User.Id == CurrentUser.Id).FirstOrDefault();
						store.Package = store.Package + invoice.Package.Times;
						db.Entry(store).State = EntityState.Modified;

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
		public ActionResult AdCreate(string id ,long ad)
		{
			var model = db.Products.Where(x => x.Store.User.Id == id && x.AdType == AdType.No).ToList();
			ViewBag.adtype = ad;
			return View(model);
		}
		// Tạo SlideShow
		public ActionResult AdCreatSlideShow(string id, long ad)
		{ 
			var model1 = db.AdInvoices.Where(x => x.User.Id == id && x.AdPackage.Id == ad && x.Status == false).FirstOrDefault();
			ViewBag.adtype = model1.AdPackage.AdType;
			ViewBag.InvoiceId = model1.Id;
			model1.Status = true;
			db.Entry(model1).State = EntityState.Modified;
			db.SaveChanges();
			return View();
		}

		//Trang danh sách các quảng cáo đang chạy
		public ActionResult AdList(string id)
		{
			var model = db.Products.Where(x => x.Store.User.Id == id && x.AdType == AdType.Hot || x.AdType == AdType.New || x.AdType == AdType.Encourage ).ToList();
			return View(model);
		}
		//Trang danh sách các SlideShow đang chạy
		public ActionResult SlideShowList(string id)
		{
			var model = db.SlideShows.Where(x => x.AdInvoice.User.Id == id ).ToList();
			return View(model);
		}
		//Gỡ SlideShow
		public ActionResult EditSlideShow(long id)
		{
			var model = db.SlideShows.Find(id);
			db.SlideShows.Remove(model);
			db.SaveChanges();
			return RedirectToAction("SlideShowList", new { id = model.User.Id });
		}

		// Gỡ quảng cáo
		public ActionResult Edit(long id)
		{
			var model = db.Products.Find(id);
			model.AdType = AdType.No;
			db.Entry(model).State = EntityState.Modified;
			db.SaveChanges();
			return RedirectToAction("AdList",new {id = model.Store.User.Id });
		}

		//Chỉ định sản phẩm quảng cáo
		public ActionResult Create(long id , long ad , string user)
		{
			var model = db.Products.Find(id);
			var model1 = db.AdInvoices.Where(x => x.User.Id == user && x.AdPackage.Id == ad && x.Status == false).FirstOrDefault();
			model.AdType = model1.AdPackage.AdType;
			model.AdExpriedDate = model1.ExpiredDate;
			model1.Status = true;
			db.Entry(model).State = EntityState.Modified;
			db.Entry(model1).State = EntityState.Modified;
			db.SaveChanges();
			return RedirectToAction("AdManage", new { id = model.Store.User.Id });
		}
		[HttpPost]
		public ActionResult CreateSlideShow(SlideShow model,string id, long invoice)
		{
			var userid = id;
			ViewBag.UserID = id;
			bool exists = System.IO.Directory.Exists(Server.MapPath("~/MerchantSlide/" + ViewBag.UserID));

			if (!exists)
				System.IO.Directory.CreateDirectory(Server.MapPath("~/MerchantSlide/" + ViewBag.UserID));

			var f1 = Request.Files["Image1"];
			if (f1 != null && f1.ContentLength > 0)
			{
				string fullPath = Request.MapPath(model.Image);
				if (System.IO.File.Exists(fullPath))
				{
					System.IO.File.Delete(fullPath);
				}
				var path = Server.MapPath("~/MerchantSlide/" + ViewBag.UserID + "/" + f1.FileName);
				f1.SaveAs(path);
				model.Image = "/MerchantSlide/" + ViewBag.UserID + "/" + f1.FileName;
			}

			model.User = db.Users.Where(x => x.Id == id).FirstOrDefault();
			model.AdInvoice = db.AdInvoices.Where(x => x.User.Id == id && x.Status == true && x.Id == invoice ).FirstOrDefault();
			model.isDisable = false;
			db.SlideShows.Add(model);
			db.SaveChanges();
			return RedirectToAction("SlideShowList", new {id = userid});
		}
	}
}