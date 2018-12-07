using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCommerce.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LowQuantityProduct()
        {
            return View();
        }
        public ActionResult BestSeller()
        {
            return View();
        }
        public ActionResult Inventory()
        {
            return View();
        }
        public ActionResult Profit()
        {
            return View();
        }
       

    }
}