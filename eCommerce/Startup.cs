﻿using eCommerce.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(eCommerce.Startup))]
namespace eCommerce
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }

        // In this method we will create default User roles and Admin user for login   
        private void createRolesandUsers()
        {
            MainDbContext context = new MainDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (context.Database.CreateIfNotExists())
            {
                DbInitializer.Seed(context, roleManager, UserManager);
            }


        //    // In Startup iam creating first Admin Role and creating a default Admin User    
        //    if (!roleManager.RoleExists("Admin"))
        //    {

        //        // first we create Admin rool   
        //        var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
        //        role.Name = "Admin";
        //        roleManager.Create(role);

        //        //Here we create a Admin super user who will maintain the website                  

        //        var user = new ApplicationUser();
        //        user.UserName = "admin";
        //        user.Email = "admin@gmail.com";

        //        string userPWD = "123456";

        //        var chkUser = UserManager.Create(user, userPWD);

        //        //Add default User to Role Admin   
        //        if (chkUser.Succeeded)
        //        {
        //            var result1 = UserManager.AddToRole(user.Id, "Admin");

        //        }
        //    }

        //    // creating Creating Moderator role    
        //    if (!roleManager.RoleExists("Moderator"))
        //    {
        //        var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
        //        role.Name = "Moderator";
        //        roleManager.Create(role);

        //    }

        //    // creating Creating Merchant role    
        //    if (!roleManager.RoleExists("Merchant"))
        //    {
        //        var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
        //        role.Name = "Merchant";
        //        roleManager.Create(role);
        //    }

        //    //creating Creating User role
        //    if (!roleManager.RoleExists("User"))
        //    {
        //        var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
        //        role.Name = "User";
        //        roleManager.Create(role);
        //    }
        //    context.SaveChanges();
        }
    }
}
