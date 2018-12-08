using eCommerce.EntityFramework;
using eCommerce.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
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
        public ActionResult ShopsIndex()
        {
            return View();
        }
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(MerchantStore model)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var user_id = "";
            if (ModelState.IsValid)
            {
                var email = Request.Form["email"];
                var pass = Request.Form["pass"];
                var user = new ApplicationUser { UserName = email, Email = email };
                var result =  userManager.Create(user, pass);
                if (result.Succeeded)
                {
                    user_id = user.Id;
                    MerchantStore store = new MerchantStore();
                    store.User = db.Users.FirstOrDefault(x => x.Id == user_id);
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
                }
            }

            // If we got this far, something failed, redisplay form
            return View();
        }
    }
}