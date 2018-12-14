using eCommerce.EntityFramework;
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
            return View();
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
		public ActionResult CreateInvoice(long ad, string Id)
		{
			AdInvoice invoice = new AdInvoice();
			invoice.AdPackage = db.AdPackages.FirstOrDefault(x => x.Id == ad);
			invoice.User = db.Users.FirstOrDefault(x => x.Id == Id);
			invoice.Price = (from c in db.AdPackages.Where(c => c.Id == ad) select c.Price).FirstOrDefault();
			invoice.createdDate = DateTime.Now;
			invoice.ExpiredDate = invoice.createdDate.AddDays((from c in db.AdPackages.Where(c => c.Id == ad) select c.Period).FirstOrDefault());
			invoice.Status = false;
			//invoice.transactionId = ;
			//invoice.hashCode = ;
			db.AdInvoices.Add(invoice);
			db.SaveChanges();
			return RedirectToAction("AdManage",new { id = Id});
		}

		// Trang hiển thị danh sách hiện có
		public ActionResult AdManage( string id)
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