using eCommerce.Areas.Admin.Models;
using eCommerce.Areas.Merchant.Models;
using eCommerce.Controllers;
using eCommerce.EntityFramework;
using eCommerce.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static eCommerce.Controllers.ManageController;

namespace eCommerce.Areas.Merchant.Controllers
{
    [System.Web.Mvc.Authorize(Roles = "Merchant")]
    public class MerchantHomeController : Controller
    {
		private MainDbContext db = new MainDbContext();
        private IdentityUser CurrentUser
        {
            get { return db.Users.FirstOrDefault(x => x.UserName.Equals(User.Identity.Name)); }
        }

        // Dashboard
        public ActionResult Index(string id)
        {
            ProfitCalculation(id);
            Increasement(id);
            var package = db.MerchantStores.FirstOrDefault(x => x.User.Id == id).Package;
            ViewBag.Package = package;
            int count = 0;
            var product = db.Products.Where(x => x.Store.User.Id == id && x.isDisabled == false);
            foreach(var item in product)
            {
                count += 1;
            }
            ViewBag.Product = count;
            var model = db.InvoiceDetails.Where(x => x.Product.Store.User.Id == id && x.Invoice.Status == ProductStatus.NotValidated).ToList();
            return View(model);
        }

        public void ProfitCalculation(string id)
        {
            //daily revenue
            var model0 = db.InvoiceDetails.Where(x => x.Product.Store.User.Id == id && x.Invoice.Status == ProductStatus.Delivered &&
            x.isDisabled == false && x.Invoice.createdDate.Day == DateTime.Now.Day && x.Invoice.createdDate.Month == DateTime.Now.Month &&
            x.Invoice.createdDate.Year == DateTime.Now.Year);
            int daily = 0,dailyI = 0;
            foreach (var item in model0)
            {
                daily += item.Price * item.Quantity;
                dailyI += 1;
            }
            ViewBag.DailyProfit = daily / 1000000;
            ViewBag.DailyBill = dailyI;
            // end daily revenue

            //weekly revenue
            var model1 = db.InvoiceDetails.Where(x => x.Product.Store.User.Id == id && x.Invoice.Status == ProductStatus.Delivered &&
            x.isDisabled == false && x.Invoice.createdDate >= DbFunctions.AddDays(DateTime.Now, -7));
            int weekly = 0, weeklyI = 0 ;
            foreach (var item in model1)
            {
                weekly += item.Price * item.Quantity;
                weeklyI += 1;
            }
            ViewBag.WeeklyProfit = weekly / 1000000;
            ViewBag.WeeklyBill = weeklyI;
            //end weekly revenue

            //monthly revenue
            var model2 = db.InvoiceDetails.Where(x => x.Product.Store.User.Id == id && x.Invoice.Status == ProductStatus.Delivered &&
            x.isDisabled == false && x.Invoice.createdDate >= DbFunctions.AddDays(DateTime.Now, -30));
            int monthly = 0,mothlyI = 0;
            foreach (var item in model2)
            {
                monthly += item.Price * item.Quantity;
                mothlyI += 1;
            }
            ViewBag.MonthlyProfit = monthly / 1000000;
            ViewBag.MonthlyBill = mothlyI;
            //end monthly revenue

            //90 days revenue
            var model3 = db.InvoiceDetails.Where(x => x.Product.Store.User.Id == id && x.Invoice.Status == ProductStatus.Delivered &&
            x.isDisabled == false && x.Invoice.createdDate >= DbFunctions.AddDays(DateTime.Now, -90));
            int threeMonths = 0,threeMonthsI = 0;
            foreach (var item in model3)
            {
                threeMonths += item.Price * item.Quantity;
                threeMonthsI += 1;
            }
            ViewBag.ThreeMonthsProfit = threeMonths / 1000000;
            ViewBag.ThreeMonthsBill = threeMonthsI;
            //end 90 days revenue

            //whole year revenue
            var model4 = db.InvoiceDetails.Where(x => x.Product.Store.User.Id == id && x.Invoice.Status == ProductStatus.Delivered &&
            x.isDisabled == false && x.Invoice.createdDate.Year == DateTime.Now.Year);
            int wholeYear = 0,wholeYearI = 0;
            foreach (var item in model4)
            {
                wholeYear += item.Price * item.Quantity;
                wholeYearI += 1;
            }
            ViewBag.WholeYearProfit = wholeYear / 1000000;
            ViewBag.WholeYearBill = wholeYearI;
            //end whole year revenue
        }

        public void Increasement(string id)
        {
            var before = db.InvoiceDetails.Where(x => x.Product.Store.User.Id == id && x.Invoice.Status == ProductStatus.Delivered &&
            x.isDisabled == false && x.Invoice.createdDate >= DbFunctions.AddMonths(DateTime.Now, -1));
            int lastMonth = 0;
            foreach(var item in before)
            {
                lastMonth += item.Price * item.Quantity;
            }
            int thisMonth = 0;
            var after = db.InvoiceDetails.Where(x => x.Product.Store.User.Id == id && x.Invoice.Status == ProductStatus.Delivered &&
            x.isDisabled == false && x.Invoice.createdDate.Month == DateTime.Now.Month);
            foreach (var item in after)
            {
                thisMonth += item.Price * item.Quantity;
            }
            int increased = thisMonth - lastMonth;
            double percent = lastMonth / (lastMonth + increased) * 100;
            ViewBag.Percent = percent;
        }
	
		// Function hiển thị Các sản phẩm được mua của Merchant
        public ActionResult NewInvoices(string id)
        {
			var model = db.InvoiceDetails.Where(x => x.Product.Store.User.Id == id && x.Invoice.Status == ProductStatus.NotValidated).ToList();
            return View(model);
        }
	
		public ActionResult Confirm(long product,long invoice)
		{
			var model = db.InvoiceDetails.FirstOrDefault(x => x.Invoice.Id == invoice && x.Product.Id == product);
            if(model.isDisabled == false)
            {
                model.isDisabled = true;
            }
        
		
			db.Entry(model).State = EntityState.Modified;
			db.SaveChanges();
            
            var email = CurrentUser.Email;
            var invoiceId = invoice;
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<SignalRHub>().Clients.All.addNewMessageToAdmin(email, invoiceId);
            return RedirectToAction("NewInvoices",new { id = model.Product.Store.User.Id });
			//return View();
		}

		// Function theo dõi Các sản phẩm được mua của Merchant
		public ActionResult Follow(string id)
		{
			var model = db.InvoiceDetails.Where(x => x.Product.Store.User.Id == id 
                                            && (x.Invoice.Status == ProductStatus.Validated || x.Invoice.Status == ProductStatus.Processing || x.Invoice.Status == ProductStatus.Delivering) 
                                            && x.isDisabled == true).ToList();
			return View(model);
		}
		// Function hiển thị Các sản phẩm đã giao xong của Merchant
		public ActionResult OldInvoices(string id)
        {
			var model = db.InvoiceDetails.Where(x => x.Product.Store.User.Id == id && x.Invoice.Status == ProductStatus.Delivered && x.isDisabled == true).ToList();
			return View(model);
        }
		//Lợi nhuận thu đươc cưa thanh toán Merchant
        public ActionResult Profit(string id)
        {
			ProfitCalculation(id);

            //Tiền Doanh thu trong tháng
            //int month = DateTime.Now.Month;
            //int year = DateTime.Now.Year;
            long profit = 0;
			var model = db.InvoiceDetails.Where(x => x.Product.Store.User.Id == id && x.Invoice.Status == ProductStatus.Delivered).ToList();
			foreach(var i in model)
			{
				profit += (i.Price * i.Quantity);
			}

            // Tiền được chuyển trong tháng.
            long profit1 = db.MerchantRepaidDetails.Where(x => x.History.Merchant.Id == id).Sum(x => x.InvoiceDetail.Price);
            
            // Tiền chưa được chuyển.
            long profit2 = 0;
			var model2 = db.InvoiceDetails.Where(x => x.Product.Store.User.Id == id && x.Invoice.Status == ProductStatus.Delivered && x.isPaymented == false).ToList();
			foreach (var i in model2)
			{
				profit2 += (i.Price * i.Quantity);
			}
			ViewBag.Profit_Now = Math.Round(((double)profit / 1000000), 1);
            ViewBag.Profit_Trade = Math.Round(((double)profit1 / 1000000), 1);
            ViewBag.Profit_NotTrade = Math.Round(((double)profit2 / 1000000), 1);

            return View(db.MerchantRepaidHistorys.Where(x => x.Merchant.Id == CurrentUser.Id).Select(x => new RepaidHistory() {
                createDate = x.CreatedDate,
                Total = x.Total,
                TransactionId = x.TransactionId
            }).ToList());
        }
		//Hiển thị sản phẩm đăng bán của Merchant
        public ActionResult ManageProduct(string id)
        {
            return View(db.Products.Where(x => x.Store.User.Id == id && x.isDisabled == false));
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
			var model = db.MerchantStores.FirstOrDefault(x => x.User.Id == id);
            ViewBag.Package = model.Package;
            ViewBag.AlertMessage = "Bạn không còn đủ gói tin để đăng thêm sản phẩm !";
            if(model.Package <= 0)
            {
                return RedirectToAction("Index", "MerchantHome", new { Id = id });
            }
            else
            {
                return View(new ProductModel()
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
                    Store = model
				});
			}
        }
        [HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(ProductModel model,string id)
        {
			
			ViewBag.UserID = id;
			Product product = new Product();
            if (ModelState.IsValid)
            {
				// Trừ số lần đăng của cửa hàng
				var num = db.MerchantStores.FirstOrDefault(x => x.User.Id == id);
				num.Package = num.Package - 1;
				db.Entry(num).State = EntityState.Modified;


				product.Store = db.MerchantStores.FirstOrDefault(x=>x.User.Id == id);
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
				product.AdExpriedDate = DateTime.Now;
				product.PackeageExpiredDate = product.updateDate.AddDays((from c in db.PackageInvoices.Where(c => c.User.Id == id) select c.Package.Days).FirstOrDefault());
				product.AdType = EntityFramework.AdType.No;
                product.isDisabled = false;

                bool exists = System.IO.Directory.Exists(Server.MapPath("~/MerchantProduct/"+ViewBag.UserID));

                if (!exists)
                    System.IO.Directory.CreateDirectory(Server.MapPath("~/MerchantProduct/" + ViewBag.UserID));


                var f1 = Request.Files["Image1"];
                var f2 = Request.Files["Image2"];
                var f3 = Request.Files["Image3"];
                if (f1 != null && f1.ContentLength > 0)
                {
                    var path = Server.MapPath("~/MerchantProduct/" + ViewBag.UserID + "/" + f1.FileName);
                    f1.SaveAs(path);
                    product.Image1 = "/MerchantProduct/" + ViewBag.UserID + "/" + f1.FileName;
                }
                if (f2 != null && f2.ContentLength > 0)
                {
                    var path = Server.MapPath("~/MerchantProduct/" + ViewBag.UserID + "/" + f2.FileName);
                    f2.SaveAs(path);
                    product.Image2 = "/MerchantProduct/" + ViewBag.UserID + "/" + f2.FileName;
                }
                if (f3 != null && f3.ContentLength > 0)
                {
                    var path = Server.MapPath("~/MerchantProduct/" + ViewBag.UserID + "/" + f2.FileName);
                    f3.SaveAs(path);
                    product.Image3 = "/MerchantProduct/" + ViewBag.UserID + "/" + f2.FileName;
                }
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("ManageProduct",new { id = ViewBag.UserID });
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
				ProductId = model.Id,
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
				AdExpriedDate = model.AdExpriedDate,
				PackeageExpiredDate = model.PackeageExpiredDate,
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
			Product product = db.Products.Find(model.ProductId);
            ViewBag.UserID = db.MerchantStores.FirstOrDefault(x => x.Id == model.Store.Id).User.Id;
			if (ModelState.IsValid)
            {

				product.Id = model.ProductId;
				product.Store = db.MerchantStores.FirstOrDefault(x=>x.Id == model.Store.Id);
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
				product.AdExpriedDate = model.AdExpriedDate;
				product.PackeageExpiredDate = model.PackeageExpiredDate;
                product.Image1 = model.Image1;
                product.Image2 = model.Image2;
                product.Image3 = model.Image3;
                product.AdType = model.AdType;
				product.isDisabled = model.isDisabled;

                bool exists = System.IO.Directory.Exists(Server.MapPath("~/MerchantProduct/" + ViewBag.UserID));

                if (!exists)
                    System.IO.Directory.CreateDirectory(Server.MapPath("~/MerchantProduct/" + ViewBag.UserID));

                var f1 = Request.Files["Image1"];
                var f2 = Request.Files["Image2"];
                var f3 = Request.Files["Image3"];
                if (f1 != null && f1.ContentLength > 0)
                {
                    string fullPath = Request.MapPath(model.Image1);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                    var path = Server.MapPath("~/MerchantProduct/" + ViewBag.UserID + "/" + f1.FileName);
                    f1.SaveAs(path);
                    product.Image1 = "/MerchantProduct/" + ViewBag.UserID + "/" + f1.FileName;
                }
                if (f2 != null && f2.ContentLength > 0)
                {
                    string fullPath = Request.MapPath(model.Image2);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                    var path = Server.MapPath("~/MerchantProduct/" + ViewBag.UserID + "/" + f2.FileName);
                    f2.SaveAs(path);
                    product.Image2 = "/MerchantProduct/" + ViewBag.UserID + "/" + f2.FileName;
                }
                if (f3 != null && f3.ContentLength > 0)
                {
                    string fullPath = Request.MapPath(model.Image3);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                    var path = Server.MapPath("~/MerchantProduct/" + ViewBag.UserID + "/" + f3.FileName);
                    f3.SaveAs(path);
                    product.Image3 = "/MerchantProduct/" + ViewBag.UserID + "/" + f3.FileName;
                }
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ManageProduct",new { id = product.Store.User.Id });
            }

			// Hàng trả về khi thay đổi thất bại :))
			ProductModel model1 = new ProductModel()
			{
				ProductId = model.ProductId,
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
            return View(db.Products.Where(x=>x.Quantity <= 2 && x.Store.User.Id== id));
        }

        // GET: Admin/Categories/Delete/5
        public ActionResult Delete(long id)
        {
            if(ModelState.IsValid)
            {
                var product = db.Products.Find(id);
                product.isDisabled = true;
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }


		// THông tin tài khoản 
		public ActionResult Account(string id)
		{
			var StoreId = db.MerchantStores.Where(x => x.User.Id == id).FirstOrDefault();
			var user = db.Users.Find(id);
			MerchantStore store = db.MerchantStores.Find(StoreId.Id);
			MerchantStoreModel model = new MerchantStoreModel();

			model.MaCuaHang = store.Id;
			model.Address = store.Address;
			model.BusinessRegistrationCode = store.BusinessRegistrationCode;
			model.TaxRegistrationCode = store.TaxRegistrationCode;
			model.CardTradeNumber = store.CardTradeNumber;
			model.CreditCardNumber = store.CreditCardNumber;
			model.BankName = store.BankName;
			model.createdDate = store.createdDate;
			model.DeliveryMethod = store.DeliveryMethod;
			model.Name = store.Name;
			model.Package = store.Package;
			model.PhoneNumber = store.PhoneNumber;
			model.Image1 = store.Image1;
			model.Image2 = store.Image2;
			model.Image3 = store.Image3;
			model.Image4 = store.Image4;
			model.Image5 = store.Image5;
			model.isDisabled = store.isDisabled;
			model.User = user;
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			if (model == null)
			{
				return HttpNotFound();
			}
			return View(model);
	
		}
		//=============================================================================================================
		
	
		
        //===========================================================================================================================
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Account(MerchantStoreModel model )
		{
			MerchantStore store = db.MerchantStores.Find(model.MaCuaHang);
			model.User = db.Users.Find(model.User.Id);
			if (ModelState.IsValid)
			{
				store.Id = model.MaCuaHang;
				store.Address = model.Address;
				store.BusinessRegistrationCode = model.BusinessRegistrationCode;
				store.TaxRegistrationCode = model.TaxRegistrationCode;
				store.CardTradeNumber = model.CardTradeNumber;
				store.CreditCardNumber = model.CreditCardNumber;
				store.BankName = model.BankName;
				store.createdDate = model.createdDate;
				store.DeliveryMethod = model.DeliveryMethod;
				store.Name = model.Name;
				store.Package = model.Package;
				store.PhoneNumber = model.PhoneNumber;
				store.Image1 = model.Image1;
				store.Image2 = model.Image2;
				store.Image3 = model.Image3;
				store.Image4 = model.Image4;
				store.Image5 = model.Image5;
				store.isDisabled = model.isDisabled;
				store.User = model.User;

				ViewBag.UserID = db.MerchantStores.FirstOrDefault(x => x.User.Id == store.User.Id);
				bool exists = System.IO.Directory.Exists(Server.MapPath("~/MerchantImages/" + ViewBag.UserID));

				if (!exists)
					System.IO.Directory.CreateDirectory(Server.MapPath("~/MerchantImages/" + ViewBag.UserID));

				var f1 = Request.Files["Image1"];
				var f2 = Request.Files["Image2"];
				var f3 = Request.Files["Image3"];
				var f4 = Request.Files["Image4"];
				var f5 = Request.Files["Image5"];

				if (f1 != null && f1.ContentLength > 0)
				{
					string fullPath = Request.MapPath(model.Image1);
					if (System.IO.File.Exists(fullPath))
					{
						System.IO.File.Delete(fullPath);
					}
					var path = Server.MapPath("~/MerchantImages/" + ViewBag.UserID + "/" + f1.FileName);
					f1.SaveAs(path);
					store.Image1 = "/MerchantImages/" + ViewBag.UserID + "/" + f1.FileName;
				}
				if (f2 != null && f2.ContentLength > 0)
				{
					string fullPath = Request.MapPath(model.Image2);
					if (System.IO.File.Exists(fullPath))
					{
						System.IO.File.Delete(fullPath);
					}
					var path = Server.MapPath("~/MerchantImages/" + ViewBag.UserID + "/" + f2.FileName);
					f2.SaveAs(path);
					store.Image2 = "/MerchantImages/" + ViewBag.UserID + "/" + f2.FileName;
				}
				if (f3 != null && f3.ContentLength > 0)
				{
					string fullPath = Request.MapPath(model.Image3);
					if (System.IO.File.Exists(fullPath))
					{
						System.IO.File.Delete(fullPath);
					}
					var path = Server.MapPath("~/MerchantImages/" + ViewBag.UserID + "/" + f3.FileName);
					f3.SaveAs(path);
					store.Image3 = "/MerchantImages/" + ViewBag.UserID + "/" + f3.FileName;
				}
				if (f4 != null && f4.ContentLength > 0)
				{
					string fullPath = Request.MapPath(model.Image3);
					if (System.IO.File.Exists(fullPath))
					{
						System.IO.File.Delete(fullPath);
					}
					var path = Server.MapPath("~/MerchantImages/" + ViewBag.UserID + "/" + f4.FileName);
					f4.SaveAs(path);
					store.Image4 = "/MerchantImages/" + ViewBag.UserID + "/" + f4.FileName;
				}
				if (f5 != null && f5.ContentLength > 0)
				{
					string fullPath = Request.MapPath(model.Image3);
					if (System.IO.File.Exists(fullPath))
					{
						System.IO.File.Delete(fullPath);
					}
					var path = Server.MapPath("~/MerchantImages/" + ViewBag.UserID + "/" + f5.FileName);
					f5.SaveAs(path);
					store.Image5 = "/MerchantImages/" + ViewBag.UserID + "/" + f5.FileName;
				}

				db.Entry(store).State = EntityState.Modified;
				db.SaveChanges();

				return RedirectToAction("Account",new {id = model.User.Id });
			}
			
			return View(model);
		}
		[ChildActionOnly]
		public ActionResult BankAccount()
		{
			return PartialView("BankAccount");
		}
       
        public ActionResult Alert()
        {
            var invoices = db.InvoiceDetails.Where(x => x.Invoice.Status == ProductStatus.NotValidated && x.Product.Store.User.Id == CurrentUser.Id && !x.Product.isDisabled).Select(x => x.Invoice).Distinct().ToList();
            return View(new AlertModel()
            {
                Quantity = invoices.Count,
                Invoices = invoices,
                MerchantId = CurrentUser.Id
            });
        }
      
	}
	
}