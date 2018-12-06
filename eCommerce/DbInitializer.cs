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
            var id = "";
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
                    id = user.Id;
                    UserManager.AddToRole(user.Id, "Merchant");
                }
            }

            //creating a normal customer with no role
            var user_id = "";
            var client = new ApplicationUser();
            client.UserName = "client@gmail.com";
            client.Email = "client@gmail.com";

            string clientPWD = "Client1@3456";
            var chkClient = UserManager.Create(client, clientPWD);
            if(chkClient.Succeeded)
            {
                user_id = client.Id;
            }

            context.SaveChanges();

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

            //Seed for advertise package
            IList<AdPackage> defaultAdPackages = new List<AdPackage>();
            defaultAdPackages.Add(new AdPackage() { Name = "Gói quảng cáo 1", Price = 900000, Position = "FrontPage", Period = 30, isDisabled = false });
            defaultAdPackages.Add(new AdPackage() { Name = "Gói quảng cáo 2", Price = 500000, Position = "LeftNav", Period = 30, isDisabled = false });
            defaultAdPackages.Add(new AdPackage() { Name = "Gói quảng cáo 3", Price = 500000, Position = "RightNav", Period = 30, isDisabled = false });

            context.AdPackages.AddRange(defaultAdPackages);
            context.SaveChanges();

            //Seed for Merchant Stores
            IList<MerchantStore> defaultStores = new List<MerchantStore>();
			defaultStores.Add(new MerchantStore() { User = context.Users.FirstOrDefault(x => x.Id == id), Name = "Cửa hàng Phú Dái",
				Address = "Chèn sau", BusinessRegistrationCode = "0xxxxxxxxx",
				TaxRegistrationCode = "0xxxxxxxx", CardTradeNumber = "7652xxxxxxxx",
				CreditCardNumber = "1600xxxxxx", PhoneNumber = "0396372123",
				DeliveryMethod = DeliveryMethod.Fast, createdDate = DateTime.Now,
				BankName = "Agribank", Image1 = "~/Assets/img/new01.jpg",
				Image2 = "/Assets/img/...", Image3 = "/Assets/img/...",
				Image4 = "/Assets/img/...", Image5 = "/Assets/img/...", isDisabled = false
            });

            context.MerchantStores.AddRange(defaultStores);
            context.SaveChanges();

            //Seed for Product
            IList<Product> defaultProducts = new List<Product>();

            defaultProducts.Add(new Product()
            {
                Store = context.MerchantStores.FirstOrDefault(x => x.Id == 1),
                Name = "Laptop A",
                Price = 20000000,
                Quantity = 5,
                Category = context.Categories.FirstOrDefault(x => x.Id == 1),
                Type = context.ProductTypes.FirstOrDefault(x => x.Id == 1),
                discountValue = 0,
                Description = "sản phẩm test",
                CPU = "i7-7700K",
                RAM = "16GB DDR4",
                hardDrive = "SSD Samsung gì đó 256GB",
                screenType = "FHD 144Hz IPS",
                GPU = "GTX 1070Ti OC 6GB",
                IOPort = "2 cổng USB 3.0 bla bla",
                OS = "Windows 10",
                DesignType = "đéo biết",
                Size = 2.3F,
                updateDate = DateTime.Now,
                Image1 = "~/Assets/img/product1.png",
                Image2 = "~/Assets/img/product2.png",
                Image3 = "~/Assets/img/product3.png",
                isDisabled = false
            });

            defaultProducts.Add(new Product()
            {
                Store = context.MerchantStores.FirstOrDefault(x => x.Id == 1),
                Name = "Laptop B",
                Price = 17000000,
                Quantity = 4,
                Category = context.Categories.FirstOrDefault(x => x.Id == 2),
                Type = context.ProductTypes.FirstOrDefault(x => x.Id == 3),
                discountValue = 0,
                Description = "sản phẩm test 2nd",
                CPU = "i7-4720HQ",
                RAM = "8GB DDR3",
                hardDrive = "SSD Samsung gì đó 128GB",
                screenType = "FHD 14.7' 60Hz",
                GPU = "GTX 960M 4GB DDR5",
                IOPort = "2 cổng USB 3.0 bla bla",
                OS = "Windows 7",
                DesignType = "đéo biết",
                Size = 2.1F,
                updateDate = DateTime.Now,
                Image1 = "/Assets/img/product4.png",
                Image2 = "/Assets/img/product5.png",
                Image3 = "/Assets/img/product6.png",
                isDisabled = false
            });

            context.Products.AddRange(defaultProducts);
            context.SaveChanges();

            //Seed for invoice and details
            IList<Invoice> defaultInvoices = new List<Invoice>();
            defaultInvoices.Add(new Invoice() {
                User = context.Users.FirstOrDefault(x => x.Id == user_id),
                Address = "Nhà tao",
                Total = 37000000,
                PaymentMethod = PaymentMethod.COD,
                DeliveryMethod = DeliveryMethod.Standard,
                Description = "Giàu vl mua 2 cái lap 1 lúc",
                createdDate = DateTime.Now,
                Status = ProductStatus.Delivered,
                isDisabled = false,
            });

            defaultInvoices.Add(new Invoice()
            {
                User = context.Users.FirstOrDefault(x => x.Id == user_id),
                Address = "Nhà thằng phú",
                Total = 17000000,
                PaymentMethod = PaymentMethod.Online,
                DeliveryMethod = DeliveryMethod.Fast,
                Description = "Trả tiền online đồ",
                createdDate = DateTime.Now,
                Status = ProductStatus.Processing,
                isDisabled = false,
            });

            context.Invoices.AddRange(defaultInvoices);
            context.SaveChanges();

            IList<InvoiceDetail> defaultInvoiceDetails = new List<InvoiceDetail>();
            defaultInvoiceDetails.Add(new InvoiceDetail() {
                Invoice = context.Invoices.FirstOrDefault(x => x.Id == 1),
                Product = context.Products.FirstOrDefault(x => x.Id == 2),
                Quantity = 1,
                Price = 17000000,
                isDisabled = false
            });

            defaultInvoiceDetails.Add(new InvoiceDetail(){
                Invoice = context.Invoices.FirstOrDefault(x => x.Id == 1),
                Product = context.Products.FirstOrDefault(x => x.Id == 1),
                Quantity = 1,
                Price = 20000000,
                isDisabled = false
            });

            defaultInvoiceDetails.Add(new InvoiceDetail()
            {
                Invoice = context.Invoices.FirstOrDefault(x => x.Id == 2),
                Product = context.Products.FirstOrDefault(x => x.Id == 2),
                Quantity = 1,
                Price = 17000000,
                isDisabled = false
            });

            context.InvoiceDetails.AddRange(defaultInvoiceDetails);
            context.SaveChanges();
        }
    }
}