using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCommerce.Areas.Admin.Controllers
{
    public class ChatboxController : Controller
    {
        // GET: Admin/Chatbox
        public ActionResult Index()
        {
            return View();
        }
    }
}