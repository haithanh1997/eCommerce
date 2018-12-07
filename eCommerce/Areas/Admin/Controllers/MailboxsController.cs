using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCommerce.Areas.Admin.Controllers
{
    public class MailboxsController : Controller
    {
        // GET: Admin/Mailboxs
        public ActionResult Index()
        {
            return View();
        }
    }
}