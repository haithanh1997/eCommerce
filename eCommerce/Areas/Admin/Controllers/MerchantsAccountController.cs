using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCommerce.Areas.Admin.Controllers
{
    public class MerchantsAccountController : Controller
    {
        // GET: Admin/MerchantsAccount
        public ActionResult Index()
        {
            return View();
        }
    }
}