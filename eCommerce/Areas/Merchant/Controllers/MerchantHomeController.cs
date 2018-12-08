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
		private MainDbContext db = new MainDbContext();

		// GET: Admin/Products
		public ActionResult Index(string id)
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