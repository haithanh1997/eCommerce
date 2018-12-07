using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCommerce.Areas.Admin.Controllers
{
    [Authorize(Roles = "Moderator")]
    public class StatisticController : Controller
    {
        MainDbContext db = new MainDbContext();
        // GET: Admin/Statistic
        public ActionResult Daily()
        {
            int total = 0;
            var model = db.Invoices.Where(x => x.isDisabled == false &&
                        x.createdDate.Day == DateTime.Now.Day &&
                        x.createdDate.Month == DateTime.Now.Month &&
                        x.createdDate.Year == DateTime.Now.Year &&
                        (x.PaymentMethod == EntityFramework.PaymentMethod.Online ||
                        x.Status == EntityFramework.ProductStatus.Delivered)).ToList();
            foreach(var item in model)
            {
                total = total + item.Total;
            }
            ViewBag.Total = total;
            return View(model);
        }

        public ActionResult Monthly()
        {
            int total = 0;
            var model = db.Invoices.Where(x => x.isDisabled == false &&
                        x.createdDate.Month == DateTime.Now.Month &&
                        x.createdDate.Year == DateTime.Now.Year &&
                        x.Status == EntityFramework.ProductStatus.Delivered).ToList();
            foreach (var item in model)
            {
                total = total + item.Total;
            }
            ViewBag.Total = total;
            return View(model);
        }

        public ActionResult Year()
        {
            int total = 0;
            var model = db.Invoices.Where(x => x.isDisabled == false &&
                        x.createdDate.Year == DateTime.Now.Year &&
                        x.Status == EntityFramework.ProductStatus.Delivered).ToList();
            foreach (var item in model)
            {
                total = total + item.Total;
            }
            ViewBag.Total = total;
            return View(model);
        }
    }
}