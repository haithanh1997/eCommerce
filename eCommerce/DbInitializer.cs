using eCommerce.EntityFramework;
using eCommerce.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace eCommerce
{
    public static class DbInitializer
    {
        public static void Seed(MainDbContext context, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> UserManager)
        {
            //Seed for Category
            IList<Category> defaultCategories = new List<Category>();

            defaultCategories.Add(new Category() { Name = "ASUS" });
            defaultCategories.Add(new Category() { Name = "ACER" });
            defaultCategories.Add(new Category() { Name = "MSI" });
            defaultCategories.Add(new Category() { Name = "DELL" });
            defaultCategories.Add(new Category() { Name = "HP" });
            defaultCategories.Add(new Category() { Name = "LENOVO" });

            context.Categories.AddRange(defaultCategories);
            context.SaveChanges();

            //Seed for ProductType
            IList<ProductType> defaultTypes = new List<ProductType>();

            defaultTypes.Add(new ProductType() { Name = "Gaming", Category = context.Categories.FirstOrDefault(x => x.Id == 1) });
            defaultTypes.Add(new ProductType() { Name = "Notebook", Category = context.Categories.FirstOrDefault(x => x.Id == 1) });
            defaultTypes.Add(new ProductType() { Name = "Workstation", Category = context.Categories.FirstOrDefault(x => x.Id == 1) });
            defaultTypes.Add(new ProductType() { Name = "Doanh nhân", Category = context.Categories.FirstOrDefault(x => x.Id == 1) });

            defaultTypes.Add(new ProductType() { Name = "Gaming", Category = context.Categories.FirstOrDefault(x => x.Id == 2) });
            defaultTypes.Add(new ProductType() { Name = "Notebook", Category = context.Categories.FirstOrDefault(x => x.Id == 2) });
            defaultTypes.Add(new ProductType() { Name = "Workstation", Category = context.Categories.FirstOrDefault(x => x.Id == 2) });
            defaultTypes.Add(new ProductType() { Name = "Doanh nhân", Category = context.Categories.FirstOrDefault(x => x.Id == 2) });

            defaultTypes.Add(new ProductType() { Name = "Gaming", Category = context.Categories.FirstOrDefault(x => x.Id == 3) });
            defaultTypes.Add(new ProductType() { Name = "Notebook", Category = context.Categories.FirstOrDefault(x => x.Id == 3) });
            defaultTypes.Add(new ProductType() { Name = "Workstation", Category = context.Categories.FirstOrDefault(x => x.Id == 3) });
            defaultTypes.Add(new ProductType() { Name = "Doanh nhân", Category = context.Categories.FirstOrDefault(x => x.Id == 3) });

            defaultTypes.Add(new ProductType() { Name = "Gaming", Category = context.Categories.FirstOrDefault(x => x.Id == 4) });
            defaultTypes.Add(new ProductType() { Name = "Notebook", Category = context.Categories.FirstOrDefault(x => x.Id == 4) });
            defaultTypes.Add(new ProductType() { Name = "Workstation", Category = context.Categories.FirstOrDefault(x => x.Id == 4) });
            defaultTypes.Add(new ProductType() { Name = "Doanh nhân", Category = context.Categories.FirstOrDefault(x => x.Id == 4) });

            defaultTypes.Add(new ProductType() { Name = "Gaming", Category = context.Categories.FirstOrDefault(x => x.Id == 5) });
            defaultTypes.Add(new ProductType() { Name = "Notebook", Category = context.Categories.FirstOrDefault(x => x.Id == 5) });
            defaultTypes.Add(new ProductType() { Name = "Workstation", Category = context.Categories.FirstOrDefault(x => x.Id == 5) });
            defaultTypes.Add(new ProductType() { Name = "Doanh nhân", Category = context.Categories.FirstOrDefault(x => x.Id == 5) });

            defaultTypes.Add(new ProductType() { Name = "Gaming", Category = context.Categories.FirstOrDefault(x => x.Id == 6) });
            defaultTypes.Add(new ProductType() { Name = "Notebook", Category = context.Categories.FirstOrDefault(x => x.Id == 6) });
            defaultTypes.Add(new ProductType() { Name = "Workstation", Category = context.Categories.FirstOrDefault(x => x.Id == 6) });
            defaultTypes.Add(new ProductType() { Name = "Doanh nhân", Category = context.Categories.FirstOrDefault(x => x.Id == 6) });

            context.ProductTypes.AddRange(defaultTypes);
            context.SaveChanges();

            // In Startup iam creating first Admin Role and creating a default Admin User    
            if (!roleManager.RoleExists("Admin"))
            {

                // first we create Admin rool   
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
            }

            // creating Creating Moderator role    
            if (!roleManager.RoleExists("Moderator"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Moderator";
                roleManager.Create(role);
            }
            //Here we create a Admin super user who will maintain the website                  

            var admin = new ApplicationUser();
            admin.UserName = "admin@gmail.com";
            admin.Email = "admin@gmail.com";

            string adminPWD = "123456";

            var chkUser = UserManager.Create(admin, adminPWD);

            //Add default User to Role Admin   
            if (chkUser.Succeeded)
            {
                UserManager.AddToRole(admin.Id, "Admin");
                UserManager.AddToRole(admin.Id, "Moderator");
            }

            var defaultMod = new ApplicationUser();
            defaultMod.UserName = "nguyenthientam317@gmail.com";
            defaultMod.Email = "nguyenthientam317@gmail.com";

            string modPWD = "Mod1@3456";

            var chkMod = UserManager.Create(defaultMod, modPWD);

            //Add default User to Role   
            if (chkUser.Succeeded)
            {
                UserManager.AddToRole(defaultMod.Id, "Moderator");
            }

            // creating Creating Merchant role    
            if (!roleManager.RoleExists("Merchant"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Merchant";
                roleManager.Create(role);

                var user = new ApplicationUser();
                user.UserName = "lmht534@gmail.com";
                user.Email = "lmht534@gmail.com";

                string userPWD = "Merchant1@3456";

                var chkMerchant = UserManager.Create(user, userPWD);

                //Add default User to Role  
                if (chkMerchant.Succeeded)
                {
                    UserManager.AddToRole(user.Id, "Merchant");
                }
            }
            context.SaveChanges();
        }
    }
}