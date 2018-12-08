using eCommerce.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace eCommerce.Areas.Admin.Controllers
{
    [Authorize(Roles = "Moderator")]
    public class AdminHomeController : Controller
    {
        MainDbContext db = new MainDbContext();
        // GET: Admin/Home
        public ActionResult Index()
        {
            ViewBag.CountNew = GetInvoice();
            ViewBag.NotValidatedStore = GetNotValidatedStore();
            ViewBag.CustomerNum = GetCustomer();
            return View();
        }

        public ActionResult AllUsers()
        {
            var model = (from user in db.Users
                                  select new
                                  {
                                      UserId = user.Id,
                                      Username = user.UserName,
                                      Email = user.Email,
                                      RoleNames = (from userRole in user.Roles
                                                   join role in db.Roles on userRole.RoleId
                                                   equals role.Id
                                                   select role.Name).ToList()
                                  }).ToList().Select(p => new UserWithRole()
                                  {
                                      Id = p.UserId,
                                      Name = p.Username,
                                      Email = p.Email,
                                      Role = string.Join(",", p.RoleNames)
                                  });
            return View(model);
        }
        public int GetInvoice()
        {
            int count = 0;
            var model = db.Invoices.Where(x => x.Status == EntityFramework.ProductStatus.Validated);
            foreach(var item in model)
            {
                count++;
            }
            return count;
        }
        public int GetNotValidatedStore()
        {
            int count = 0;
            var model = db.MerchantStores.Where(x => x.isDisabled == false);
            foreach(var item in model)
            {
                count++;
            }
            return count;
        }

        public int GetCustomer()
        {
            int count = 0;
            var model = db.Users;
            foreach(var item in model)
            {
                count++;
            }
            return count;
        }
    }
    public class UserWithRole
    {
        [DisplayName("Mã người dùng")]
        public string Id { get; set; }
        [DisplayName("Tên người dùng")]
        public string Name { get; set; }
        [DisplayName("Email")]
        public string Email { get; set; }
        [DisplayName("Quyền hạn")]
        public string Role { get; set; }
    }
}