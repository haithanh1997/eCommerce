using eCommerce.EntityFramework;
using eCommerce.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCommerce.Controllers
{
    public class CustomersController : Controller
    {
        MainDbContext db = new MainDbContext();

        private IdentityUser CurrentUser
        {
            get { return db.Users.FirstOrDefault(x => x.UserName.Equals(User.Identity.Name)); }
        }

        // GET: Customers
        public ActionResult Index()
        {
            return View();
        }
		public ActionResult Cart()
		{
			return View();
		}
		public ActionResult UserInfo()
		{
            if(CurrentUser != null)
            {
                var cart = db.Carts.FirstOrDefault(x => x.User.Id == CurrentUser.Id);
                if (cart == null)
                    return RedirectToAction("Index", "Cart");

                return View(new UserInfoModel() {
                    TotalAmount = db.CartItems.Where(x => x.Cart.Id == cart.Id).ToList().Sum(x => x.ItemAmount),
                });
            }

            return View();
        }

        [HttpPost]
        public ActionResult Payment(UserInfoModel model)
        {
            if (CurrentUser != null)
            {
                var cart = db.Carts.FirstOrDefault(x => x.User.Id == CurrentUser.Id);
                if (cart == null)
                    return RedirectToAction("Index", "Cart");
                
                model.TotalAmount = db.CartItems.Where(x => x.Cart.Id == cart.Id).ToList().Sum(x => x.ItemAmount);
                if (ModelState.IsValid)
                {
                    var transactionId = "HOADON" + (new Random()).Next(100000);
                    while (db.Invoices.Any(x => x.TransactionId.Equals(transactionId)))
                        transactionId = "HOADON" + (new Random()).Next(100000);
                    if (model.Payment == EntityFramework.PaymentMethod.COD)
                    {
                        var invoice = new Invoice()
                        {
                            User = CurrentUser,
                            Address = model.Address,
                            createdDate = DateTime.Now,
                            DeliveryMethod = DeliveryMethod.Fast,
                            Description = model.Description,
                            PaymentMethod = model.Payment,
                            Status = ProductStatus.NotValidated,
                            Total = model.TotalAmount,
                            TransactionId = transactionId,
                            Name = model.UserName,
                            Email = model.Email,
                            Phone = model.Phone
                        };
                        db.Invoices.Add(invoice);
                        db.SaveChanges();

                        var cartItems = db.CartItems.Where(x => x.Cart.Id == cart.Id).ToList();
                        foreach (var item in cartItems)
                        {
                            var invoiceDetail = new InvoiceDetail()
                            {
                                Invoice = invoice,
                                Price = item.Price,
                                Product = item.Product,
                                Quantity = item.Quantity
                            };
                            db.InvoiceDetails.Add(invoiceDetail);
                        }
                        db.SaveChanges();

                        // Clear Shopping Cart
                        db.CartItems.RemoveRange(cartItems);
                        db.Carts.Remove(cart);
                        db.SaveChanges();
                        Session["InvoiceId"] = invoice.Id;
                        return RedirectToAction("PaymentComplete", "Customers");
                    }
                    else
                    {
                        var paymentUrl = eCommerce.Static.Payment.PaymentApi(transactionId, model.TotalAmount);
                        return Redirect(paymentUrl);

                    }
                }

                return View("UserInfo", model);
            }

            return View("UserInfo", model);
        }


        public ActionResult PaymentComplete()
        {
            if(Session["InvoiceId"] != null)
            {
                var invoiceId = (long)Session["InvoiceId"];
                Session.Remove("InvoiceId");
                return View(new PaymentCompleteModel()
                {
                    InvoiceId = invoiceId
                });
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Favorites()
		{
			return View();
		}
	}
}