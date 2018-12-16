using eCommerce.EntityFramework;
using eCommerce.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace eCommerce.Controllers
{
    public class ShopsController : Controller
    {
		
        MainDbContext db = new MainDbContext();
        // GET: Shops
        public ActionResult ShopsIndex(long id,ProductFilterParam param)
        {
            int pageNo = 0;
            pageNo = param.page == null ? 1 : int.Parse(param.page.ToString());
            ViewBag.GetId = id;
            var shop = db.MerchantStores.FirstOrDefault(x => x.Id == id);
            ViewBag.ShopName = shop.Name;
            var query = db.Products.Where(x => x.Store.Id == id);
            foreach(var item in query)
            {
                if(item.discountValue != 0)
                {
                    int newPrice = item.Price - (item.Price * item.discountValue / 100);
                    ViewBag.NewPrice = newPrice;
                }
            }

            // Get Default Products without searching for price because we must display defaultMax and defaultMin on Max-Min slide bar
            var defaultProducts = new List<Product>();
            if (param.name == null && param.type == null && param.drive == null && param.cpu == null && param.ram == null && param.size == null)
            {
                foreach (var item in query)
                {
                    defaultProducts.Add(item);
                }
            }
            if (param.type != null)
            {

                foreach (var typeId in param.type)
                {
                    var model = query.Where(x => x.Type.Id == typeId);
                    foreach (var item in model)
                    {
                        if (!defaultProducts.Contains(item))
                        {
                            defaultProducts.Add(item);
                        }
                    }
                }
            }

            if (param.drive != null)
            {
                foreach (var drive in param.drive)
                {
                    var model = query.Where(x => x.hardDrive.Contains(drive));
                    foreach (var item in model)
                    {
                        if (!defaultProducts.Contains(item))
                        {
                            defaultProducts.Add(item);
                        }
                    }
                }
            }

            if (param.cpu != null)
            {
                foreach (var cpu in param.cpu)
                {
                    var model = query.Where(x => x.CPU.Contains(cpu));
                    foreach (var item in model)
                    {
                        if (!defaultProducts.Contains(item))
                        {
                            defaultProducts.Add(item);
                        }
                    }
                }
            }

            if (param.ram != null)
            {
                foreach (var ram in param.ram)
                {
                    var model = query.Where(x => x.RAM.Contains(ram));
                    foreach (var item in model)
                    {
                        if (!defaultProducts.Contains(item))
                        {
                            defaultProducts.Add(item);
                        }
                    }
                }
            }

            if (param.size != null)
            {
                foreach (var size in param.size)
                {
                    var model = query.Where(x => x.screenType.Contains(size));
                    foreach (var item in model)
                    {
                        if (!defaultProducts.Contains(item))
                        {
                            defaultProducts.Add(item);
                        }
                    }
                }
            }


            // Final Products display client-side
            var products = defaultProducts;
            int totalProducts = products.Count();
            int itemPerPage = 5;
            int pageEnd = pageNo * itemPerPage;
            int skip = pageEnd - itemPerPage;
            var items = products.Skip(skip).Take(itemPerPage);
            Pager<Product> pager = new Pager<Product>(items.AsQueryable(), pageNo, itemPerPage, totalProducts, param.name);

            return View(new ProductIndexModel()
            {
                Categories = db.Categories.Where(x => !x.isDisabled).ToList(),
                Types = db.ProductTypes.Where(x => x.isDisabled == false).ToList(),
                Drives = new List<Drive> { new Drive { Id = "1", Name = "SSD" }, new Drive { Id = "2", Name = "HDD" } },
                CPUs = new List<CPU> { new CPU { Id = "1", Name = "i3" }, new CPU { Id = "2", Name = "i5" }, new CPU { Id = "3", Name = "i7" } },
                Rams = new List<RAM> { new RAM { Id = "1", Name = "4GB" }, new RAM { Id = "2", Name = "8GB" }, new RAM { Id = "3", Name = "16GB" } },
                Sizes = new List<Size> { new Size { Id = "1", Name = "14" }, new Size { Id = "2", Name = "15.6" }, new Size { Id = "3", Name = "17" } },
                Products = pager,
                Filter = param
            });
        }
		
        // GET: /Account/Register
		[Authorize]
        public ActionResult Register()
        {
			
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(MerchantStore model,string userid)
        {
            //var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            //var user_id = "";
            if (ModelState.IsValid)
            {
                //var email = Request.Form["email"];
                //var pass = Request.Form["pass"];
                //var user = new ApplicationUser { UserName = email, Email = email };
                //var result =  userManager.Create(user, pass);
               //if (result.Succeeded)
                //{
                    //user_id = user.Id;
                    MerchantStore store = new MerchantStore();
                    store.User =  db.Users.FirstOrDefault(x => x.Id == userid);
                    store.Name = model.Name;
                    store.Address = model.Address;
                    store.BusinessRegistrationCode = model.BusinessRegistrationCode;
                    store.TaxRegistrationCode = model.TaxRegistrationCode;
                    store.CardTradeNumber = model.CardTradeNumber;
                    store.CreditCardNumber = model.CreditCardNumber;
                    store.PhoneNumber = model.PhoneNumber;
                    store.DeliveryMethod = model.DeliveryMethod;
                    store.createdDate = DateTime.Now;
                    store.BankName = model.BankName;
                    store.isDisabled = false;

                    var f = Request.Files["Image1"];
                    var f1 = Request.Files["Image2"];
                    var f2 = Request.Files["Image3"];
                    var f3 = Request.Files["Image4"];
                    var f4 = Request.Files["Image5"];
                    if (f != null && f.ContentLength > 0)
                    {
                        var path = Server.MapPath("~/MerchantImages/" + f.FileName);
                        f.SaveAs(path);
                        store.Image1 = "/MerchantImages/" + f.FileName;
                    }
                    if (f1 != null && f1.ContentLength > 0)
                    {
                        var path = Server.MapPath("~/MerchantImages/" + f1.FileName);
                        f1.SaveAs(path);
                        store.Image2 = "/MerchantImages/" + f1.FileName;
                    }
                    if (f2 != null && f2.ContentLength > 0)
                    {
                        var path = Server.MapPath("~/MerchantImages/" + f2.FileName);
                        f2.SaveAs(path);
                        store.Image3 = "/MerchantImages/" + f2.FileName;
                    }
                    if (f3 != null && f3.ContentLength > 0)
                    {
                        var path = Server.MapPath("~/MerchantImages/" + f3.FileName);
                        f3.SaveAs(path);
                        store.Image4 = "/MerchantImages/" + f3.FileName;
                    }
                    if (f4 != null && f4.ContentLength > 0)
                    {
                        var path = Server.MapPath("~/MerchantImages/" + f4.FileName);
                        f4.SaveAs(path);
                        store.Image5 = "/MerchantImages/" + f4.FileName;
                    }

                    db.MerchantStores.Add(store);
                    db.SaveChanges();
                    return RedirectToAction("Index","Home");
                //}
            }

            // If we got this far, something failed, redisplay form
            return View();
        }
    }
}