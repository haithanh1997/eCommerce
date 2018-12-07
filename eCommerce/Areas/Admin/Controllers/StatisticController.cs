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
        public ActionResult Index()
        {
            DateTime now = DateTime.Now.Date;
            return View(db.Invoices.Where(x => x.createdDate.Date == now && x.isDisabled == false).ToList());
        }
    }
}