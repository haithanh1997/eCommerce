using eCommerce.Areas.Merchant.Models;
using eCommerce.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace eCommerce.Areas.Merchant.Controllers
{
    [Authorize(Roles = "Merchant")]
    public class MerchantHomeController : Controller
    {
		private MainDbContext db = new MainDbContext();

		// Dashboard
		public ActionResult Index(string id)
		{
			return View();
		}
		// THông tin tài khoản 
		public ActionResult Account()
        {
            return View();
        }
		// Function hiển thị Các sản phẩm được mua của Merchant
        public ActionResult NewInvoices(string id)
        {
			var model = db.InvoiceDetails.Where(x => x.Product.Store.User.Id == id && x.Invoice.Status == ProductStatus.NotValidated).ToList();
            return View(model);
        }
		// Function theo dõi Các sản phẩm được mua của Merchant
		public ActionResult Follow(string id)
		{
			var model = db.InvoiceDetails.Where(x => x.Product.Store.User.Id == id && (x.Invoice.Status == ProductStatus.Delivered || x.Invoice.Status == ProductStatus.Processing || x.Invoice.Status == ProductStatus.Delivering)).ToList();
			return View();
		}
		// Function hiển thị Các sản phẩm đã giao xong của Merchant
		public ActionResult OldInvoices(string id)
        {
			var model = db.InvoiceDetails.Where(x => x.Product.Store.User.Id == id && x.Invoice.Status == ProductStatus.Delivered).ToList();
			return View(model);
        }
		//Lợi nhuận thu đươc cưa thanh toán Merchant
        public ActionResult Profit(string id)
        {
			long profit = 0;
			var model = db.InvoiceDetails.Where(x => x.Product.Store.User.Id == id && x.Invoice.Status == ProductStatus.Delivered && x.Invoice.isDisabled == false).ToList();
			foreach(var i in model)
			{
				profit += (i.Product.Price * i.Quantity);
			}
			return View(profit);
        }
		//Hiển thị sản phẩm đăng bán của Merchant
        public ActionResult ManageProduct(string id)
        {
            return View(db.Products.Where(x => x.Store.User.Id == id));
        }
		//Chi tiết sản phẩm
        public ActionResult Details(long? id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);

        }
		// Tạo sản phẩm
        public ActionResult Create(string id)
        {

			return View(new ProductModel() {
				Category = db.Categories.Select(x => new SelectListItem() {
					Text = x.Name,
					Value = x.Id.ToString()
				}).ToList(),
				Type = db.ProductTypes.Select(x => new SelectListItem()
				{
					Text = x.Name,
					Value = x.Id.ToString()
				}).ToList(),
				Store = db.MerchantStores.FirstOrDefault(x=>x.User.Id == id),
				
			 });

        }
        [HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(ProductModel model)
        {
			Product product = new Product();
            if (ModelState.IsValid)
            {
				product.Id = model.Id;
				product.Store = model.Store;
				product.Name = model.Name;
				product.Price = model.Price;
				product.Quantity = model.Quantity;
				product.Category = db.Categories.FirstOrDefault(x => x.Id == model.CategorySelectedId);
				product.Type = db.ProductTypes.FirstOrDefault(x => x.Id == model.TypeSelectedId);
				product.discountValue = model.discountValue;
				product.Description = model.Description;
				product.CPU = model.CPU;
				product.RAM = model.RAM;
				product.hardDrive = model.hardDrive;
				product.screenType = model.screenType;
				product.GPU = model.GPU;
				product.IOPort = model.IOPort;
				product.OS = model.OS;
				product.DesignType = model.DesignType;
				product.Size = model.Size;
				product.updateDate = DateTime.Now;
				product.AdType = model.AdType;
				product.isDisabled = model.isDisabled;

				var f1 = Request.Files["Image1"];
                var f2 = Request.Files["Image2"];
                var f3 = Request.Files["Image3"];
                if (f1 != null && f1.ContentLength > 0)
                {
                    var path = Server.MapPath("~/MerchantProduct/" + f1.FileName);
                    f1.SaveAs(path);
                    product.Image1 = "/MerchantProduct/" + f1.FileName;
                }
                if (f2 != null && f2.ContentLength > 0)
                {
                    var path = Server.MapPath("~/MerchantProduct/" + f2.FileName);
                    f2.SaveAs(path);
                    product.Image2 = "/MerchantProduct/" + f2.FileName;
                }
                if (f3 != null && f3.ContentLength > 0)
                {
                    var path = Server.MapPath("~/MerchantProduct/" + f3.FileName);
                    f3.SaveAs(path);
                    product.Image3 = "/MerchantProduct/" + f3.FileName;
                }
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
			var model1 =  new ProductModel()
			{
				Category = db.Categories.Select(x => new SelectListItem()
				{
					Text = x.Name,
					Value = x.Id.ToString()
				}).ToList(),
				Type = db.ProductTypes.Select(x => new SelectListItem()
				{
					Text = x.Name,
					Value = x.Id.ToString()
				}).ToList(),
			};
			return View(model1);
        }

		// Sửa thông tin sản phẩm
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product model = db.Products.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
			ProductModel model1 = new ProductModel()
			{
				Id = model.Id,
				Store = model.Store,
				Name = model.Name,
				Price = model.Price,
				Quantity = model.Quantity,
				Category = db.Categories.Select(x => new SelectListItem()
				{
					Text = x.Name,
					Value = x.Id.ToString()
				}).ToList(),
				Type = db.ProductTypes.Select(x => new SelectListItem()
				{
					Text = x.Name,
					Value = x.Id.ToString()
				}).ToList(),
				discountValue = model.discountValue,
				Description = model.Description,
				CPU = model.CPU,
				RAM = model.RAM,
				hardDrive = model.hardDrive,
				screenType = model.screenType,
				GPU = model.GPU,
				IOPort = model.IOPort,
				OS = model.OS,
				DesignType = model.DesignType,
				Size = model.Size,
				updateDate = model.updateDate,
				Image1 = model.Image1,
				Image2 = model.Image2,
				Image3 = model.Image3,
				AdType = model.AdType,
				isDisabled = model.isDisabled
			};
			return View(model1);

        }

        [HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(ProductModel model)
			{
			Product product = db.Products.Find(model.Id);
			if (ModelState.IsValid)
            {

				product.Id = model.Id;
				product.Store = model.Store;
				product.Name = model.Name;
				product.Price = model.Price;
				product.Quantity = model.Quantity;
				product.Category = db.Categories.FirstOrDefault(x => x.Id == model.CategorySelectedId);
				product.Type = db.ProductTypes.FirstOrDefault(x => x.Id == model.TypeSelectedId);
				product.discountValue = model.discountValue;
				product.Description = model.Description;
				product.CPU = model.CPU;
				product.RAM = model.RAM;
				product.hardDrive = model.hardDrive;
				product.screenType = model.screenType;
				product.GPU = model.GPU;
				product.IOPort = model.IOPort;
				product.OS = model.OS;
				product.DesignType = model.DesignType;
				product.Size = model.Size;
				product.updateDate = DateTime.Now;
				product.Image1 = model.Image1;
				product.Image2 = model.Image2;
				product.Image3 = model.Image3;
				product.AdType = model.AdType;
				product.isDisabled = model.isDisabled;

				var f1 = Request.Files["Image1"];
                var f2 = Request.Files["Image2"];
                var f3 = Request.Files["Image3"];
                if (f1 != null && f1.ContentLength > 0)
                {
                    var path = Server.MapPath("~/MerchantImages/" + f1.FileName);
                    f1.SaveAs(path);
					product.Image1 = "/MerchantImages/" + f1.FileName;
                }
                if (f2 != null && f2.ContentLength > 0)
                {
                    var path = Server.MapPath("~/MerchantImages/" + f2.FileName);
                    f2.SaveAs(path);
					product.Image2 = "/MerchantImages/" + f2.FileName;
                }
                if (f3 != null && f3.ContentLength > 0)
                {
                    var path = Server.MapPath("~/MerchantImages/" + f3.FileName);
                    f3.SaveAs(path);
					product.Image3 = "/MerchantImages/" + f3.FileName;
                }
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

			// Hàng trả về khi thay đổi thất bại :))
			ProductModel model1 = new ProductModel()
			{
				Id = model.Id,
				Store = model.Store,
				Name = model.Name,
				Price = model.Price,
				Quantity = model.Quantity,
				Category = db.Categories.Select(x => new SelectListItem()
				{
					Text = x.Name,
					Value = x.Id.ToString()
				}).ToList(),
				Type = db.ProductTypes.Select(x => new SelectListItem()
				{
					Text = x.Name,
					Value = x.Id.ToString()
				}).ToList(),
				discountValue = model.discountValue,
				Description = model.Description,
				CPU = model.CPU,
				RAM = model.RAM,
				hardDrive = model.hardDrive,
				screenType = model.screenType,
				GPU = model.GPU,
				IOPort = model.IOPort,
				OS = model.OS,
				DesignType = model.DesignType,
				Size = model.Size,
				updateDate = model.updateDate,
				Image1 = model.Image1,
				Image2 = model.Image2,
				Image3 = model.Image3,
				AdType = model.AdType,
				isDisabled = model.isDisabled
			};
			return View(model1);
			
        }
		// Sản phẩm đang bán có số lượng ít
        public ActionResult LowQuantity(string id)
        {
            return View(db.Products.Where(x=>x.Price <= 2 && x.Store.User.Id== id));
        }
		public ActionResult test(string id)
		{
			return View(db.Products.Where(x => x.Price <= 2 && x.Store.User.Id == id));
		}

	}
}