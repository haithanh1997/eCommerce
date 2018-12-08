using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCommerce.Areas.Merchant.Controllers
{
    [Authorize(Roles = "Merchant")]
    public class MerchantHomeController : Controller
    {
        // GET: Merchant/MerchantHome
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Account()
        {
            return View();
        }
        public ActionResult NewInvoices()
        {
            return View();
        }
        public ActionResult OldInvoices()
        {
            return View();
        }
        public ActionResult Profit()
        {
            return View();
        }
        public ActionResult ManageProduct()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
    }
}