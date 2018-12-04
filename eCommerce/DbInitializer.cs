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

            defaultCategories.Add(new Category() { Name = "ASUS" , isDisabled = false });
            defaultCategories.Add(new Category() { Name = "ACER", isDisabled = false });
            defaultCategories.Add(new Category() { Name = "MSI", isDisabled = false });
            defaultCategories.Add(new Category() { Name = "DELL", isDisabled = false });
            defaultCategories.Add(new Category() { Name = "HP", isDisabled = false });
            defaultCategories.Add(new Category() { Name = "LENOVO", isDisabled = false });
            defaultCategories.Add(new Category() { Name = "DUMMY", isDisabled = true });

            context.Categories.AddRange(defaultCategories);
            context.SaveChanges();

            //Seed for ProductType
            IList<ProductType> defaultTypes = new List<ProductType>();

            defaultTypes.Add(new ProductType() { Name = "Gaming", Category = context.Categories.FirstOrDefault(x => x.Id == 1), isDisabled = false });
            defaultTypes.Add(new ProductType() { Name = "Notebook", Category = context.Categories.FirstOrDefault(x => x.Id == 1), isDisabled = false });
            defaultTypes.Add(new ProductType() { Name = "Workstation", Category = context.Categories.FirstOrDefault(x => x.Id == 1), isDisabled = false });
            defaultTypes.Add(new ProductType() { Name = "Doanh nhân", Category = context.Categories.FirstOrDefault(x => x.Id == 1), isDisabled = false });

            defaultTypes.Add(new ProductType() { Name = "Gaming", Category = context.Categories.FirstOrDefault(x => x.Id == 2), isDisabled = false });
            defaultTypes.Add(new ProductType() { Name = "Notebook", Category = context.Categories.FirstOrDefault(x => x.Id == 2), isDisabled = false });
            defaultTypes.Add(new ProductType() { Name = "Workstation", Category = context.Categories.FirstOrDefault(x => x.Id == 2), isDisabled = false });
            defaultTypes.Add(new ProductType() { Name = "Doanh nhân", Category = context.Categories.FirstOrDefault(x => x.Id == 2), isDisabled = false });

            defaultTypes.Add(new ProductType() { Name = "Gaming", Category = context.Categories.FirstOrDefault(x => x.Id == 3), isDisabled = false });
            defaultTypes.Add(new ProductType() { Name = "Notebook", Category = context.Categories.FirstOrDefault(x => x.Id == 3), isDisabled = false });
            defaultTypes.Add(new ProductType() { Name = "Workstation", Category = context.Categories.FirstOrDefault(x => x.Id == 3), isDisabled = false });
            defaultTypes.Add(new ProductType() { Name = "Doanh nhân", Category = context.Categories.FirstOrDefault(x => x.Id == 3), isDisabled = false });

            defaultTypes.Add(new ProductType() { Name = "Gaming", Category = context.Categories.FirstOrDefault(x => x.Id == 4), isDisabled = false });
            defaultTypes.Add(new ProductType() { Name = "Notebook", Category = context.Categories.FirstOrDefault(x => x.Id == 4), isDisabled = false });
            defaultTypes.Add(new ProductType() { Name = "Workstation", Category = context.Categories.FirstOrDefault(x => x.Id == 4), isDisabled = false });
            defaultTypes.Add(new ProductType() { Name = "Doanh nhân", Category = context.Categories.FirstOrDefault(x => x.Id == 4), isDisabled = false });

            defaultTypes.Add(new ProductType() { Name = "Gaming", Category = context.Categories.FirstOrDefault(x => x.Id == 5), isDisabled = false });
            defaultTypes.Add(new ProductType() { Name = "Notebook", Category = context.Categories.FirstOrDefault(x => x.Id == 5), isDisabled = false });
            defaultTypes.Add(new ProductType() { Name = "Workstation", Category = context.Categories.FirstOrDefault(x => x.Id == 5), isDisabled = false });
            defaultTypes.Add(new ProductType() { Name = "Doanh nhân", Category = context.Categories.FirstOrDefault(x => x.Id == 5), isDisabled = false });

            defaultTypes.Add(new ProductType() { Name = "Gaming", Category = context.Categories.FirstOrDefault(x => x.Id == 6), isDisabled = false });
            defaultTypes.Add(new ProductType() { Name = "Notebook", Category = context.Categories.FirstOrDefault(x => x.Id == 6), isDisabled = false });
            defaultTypes.Add(new ProductType() { Name = "Workstation", Category = context.Categories.FirstOrDefault(x => x.Id == 6), isDisabled = false });
            defaultTypes.Add(new ProductType() { Name = "Doanh nhân", Category = context.Categories.FirstOrDefault(x => x.Id == 6), isDisabled = false });

            context.ProductTypes.AddRange(defaultTypes);
            context.SaveChanges();

            //Seed for Packages
            IList<Package> defaultPackages = new List<Package>();

            defaultPackages.Add(new Package() { Name = "Cơ bản" , Price = 299000 , Times = 10 , Days = 7 , isDisabled = false });
            defaultPackages.Add(new Package() { Name = "Doanh nghiệp" , Price = 999000 , Times = 40 , Days = 7 , isDisabled = false });
            defaultPackages.Add(new Package() { Name = "Tiêu chuẩn" , Price = 599000 , Times = 25 , Days = 7 , isDisabled = false });

            context.Packages.AddRange(defaultPackages);
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

            //Add default User to Role Admin haha 
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