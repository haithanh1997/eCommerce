﻿using eCommerce.EntityFramework;
using eCommerce.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using System.Linq;
using System.Web.Mvc;

namespace eCommerce.Controllers
{
    public class CartController : Controller
    {
        // GET: Shops
        MainDbContext db = new MainDbContext();
        private IdentityUser CurrentUser
        {
            get { return db.Users.FirstOrDefault(x => x.UserName.Equals(User.Identity.Name)); }
        }

        public ActionResult Index()
        {
            if (CurrentUser != null)
            {
                var cart = db.Carts.FirstOrDefault(x => x.User.Id == CurrentUser.Id);
                if (cart != null)
                {
                    var cartItems = db.CartItems.Where(x => x.Cart.Id == cart.Id).ToList();
                    return View(new CartViewModel()
                    {
                        CartItems = cartItems,
                        TotalQuantity = cartItems != null ? cartItems.Sum(x => x.Quantity) : 0,
                        TotalAmount = cartItems != null ? cartItems.Sum(x => x.ItemAmount) : 0
                    });
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddToCart(long Id, int Quantity = 1)
        {
            if (CurrentUser != null)
            {
                var cart = db.Carts.FirstOrDefault(x => x.User.Id == CurrentUser.Id);
                var product = db.Products.FirstOrDefault(x => x.Id == Id);

                // User haven't already had cart
                if (cart == null)
                {
                    cart = new EntityFramework.Cart()
                    {
                        User = CurrentUser,
                    };
                    db.Carts.Add(cart);
                    db.SaveChanges();
                }

                if (product != null)
                {
                    var cartItem = db.CartItems.FirstOrDefault(x => x.Cart.Id == cart.Id && x.Product.Id == Id);

                    if (cartItem == null)
                    {
                        cartItem = new CartItem()
                        {
                            Cart = cart,
                            Product = product,
                            Price = product.Price,
                            Quantity = Quantity
                        };
                        db.CartItems.Add(cartItem);
                    }
                    else
                    {
                        cartItem.Quantity += Quantity;
                    }
                    db.SaveChanges();

                    var cartItems = db.CartItems.Where(x => x.Cart.Id == cart.Id).ToList();
                    return Json(new CartResponseModel<UpdateCartModel>()
                    {
                        Result = true,
                        Message = "Thêm giỏ hàng thành công!",
                        Data = new UpdateCartModel()
                        {
                            Id = product.Id,
                            Image = product.Image1,
                            Price = string.Format("{0:0,0}", product.Price),
                            Quantity = cartItem.Quantity,
                            Url = "",
                            TotalQuantity = cartItems.Sum(x => x.Quantity),
                            TotalAmount = string.Format("{0:0,0}", cartItems.Sum(x => x.ItemAmount))
                        }
                    }, JsonRequestBehavior.AllowGet);
                }
                return Json(new CartResponseModel() { Result = false, Message = "Sản phẩm đã bị xóa!" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new CartResponseModel() { Result = false, Message = "Vui lòng đăng nhập!" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateToCart(UpdateCartRequestModel model)
        {
            if (CurrentUser != null)
            {
                var cart = db.Carts.FirstOrDefault(x => x.User.Id == CurrentUser.Id);
                var product = db.Products.FirstOrDefault(x => x.Id == model.Id);

                if (product != null && cart != null)
                {
                    var cartItem = db.CartItems.FirstOrDefault(x => x.Cart.Id == cart.Id && x.Product.Id == model.Id);
                    cartItem.Quantity += model.Quantity;

                    var cartItems = db.CartItems.Where(x => x.Cart.Id == cart.Id).ToList();
                    return Json(new CartResponseModel<UpdateCartModel>()
                    {
                        Result = true,
                        Message = "Thêm giỏ hàng thành công!",
                        Data = new UpdateCartModel()
                        {
                            Id = product.Id,
                            Image = product.Image1,
                            Price = string.Format("{0:0,0}", product.Price),
                            Quantity = cartItem.Quantity,
                            Url = "",
                            TotalQuantity = cartItems.Sum(x => x.Quantity),
                            TotalAmount = string.Format("{0:0,0}", cartItems.Sum(x => x.ItemAmount))
                        }
                    }, JsonRequestBehavior.AllowGet);
                }

                return Json(new CartResponseModel() { Result = false, Message = "Có lỗi xảy ra!" }, JsonRequestBehavior.AllowGet);

            }
            return Json(new CartResponseModel() { Result = false, Message = "Vui lòng đăng nhập!" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RemoveToCart(long Id)
        {
            if (CurrentUser != null)
            {
                var cart = db.Carts.FirstOrDefault(x => x.User.Id == CurrentUser.Id);
                var cartItem = db.CartItems.FirstOrDefault(x => x.Cart.Id == cart.Id && x.Product.Id == Id);


                // User haven't already had cart
                if (cart != null && cartItem != null)
                {
                    db.CartItems.Remove(cartItem);
                    db.SaveChanges();

                    var cartItems = db.CartItems.Where(x => x.Cart.Id == cart.Id).ToList();
                    return Json(new CartResponseModel<UpdateCartModel>()
                    {
                        Result = true,
                        Message = "Đã xóa sản phẩm khỏi giỏ hàng!",
                        Data = new UpdateCartModel()
                        {
                            Id = Id,
                            TotalQuantity = cartItems.Sum(x => x.Quantity),
                            TotalAmount = string.Format("{0:0,0}", cartItems.Sum(x => x.ItemAmount))
                        }
                    }, JsonRequestBehavior.AllowGet);
                }
                return Json(new CartResponseModel() { Result = false, Message = "Có lỗi xảy ra!" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new CartResponseModel() { Result = false, Message = "Vui lòng đăng nhập!" }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Cart()
        {
            if (CurrentUser != null)
            {
                var cart = db.Carts.FirstOrDefault(x => x.User.Id == CurrentUser.Id);
                if (cart != null)
                {
                    var cartItems = db.CartItems.Where(x => x.Cart.Id == cart.Id).ToList();
                    return PartialView(new UserCartModel()
                    {
                        CartItems = cartItems,
                        TotalQuantity = cartItems != null ? cartItems.Sum(x => x.Quantity) : 0,
                        TotalAmount = cartItems != null ? cartItems.Sum(x => x.ItemAmount) : 0
                    });
                }
            }
            return PartialView(null);
        }
    }
}