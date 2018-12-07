using eCommerce.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCommerce.Areas.Admin.Controllers
{
    public class CustomerController : Controller
    {
        MainDbContext db = new MainDbContext();
        // GET: Admin/Customer
        public ActionResult Index()
        {
            
            return View();
        }
    }
}