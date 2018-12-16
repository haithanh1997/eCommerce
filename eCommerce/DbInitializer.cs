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

            defaultTypes.Add(new ProductType() { Name = "Gaming",  isDisabled = false });
            defaultTypes.Add(new ProductType() { Name = "Notebook", isDisabled = false });
            defaultTypes.Add(new ProductType() { Name = "Workstation", isDisabled = false });
            defaultTypes.Add(new ProductType() { Name = "Doanh nhân", isDisabled = false });


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
            defaultAdPackages.Add(new AdPackage() { Name = "Promote 1", Price = 200000, AdType = AdType.Encourage, Period = 7, isDisabled = false });
            defaultAdPackages.Add(new AdPackage() { Name = "Promote 3", Price = 350000, AdType = AdType.Hot, Period = 7, isDisabled = false });
            defaultAdPackages.Add(new AdPackage() { Name = "Promote 4", Price = 350000, AdType = AdType.New, Period = 7, isDisabled = false });
            defaultAdPackages.Add(new AdPackage() { Name = "Event", Price = 599000, AdType = AdType.SlideShow, Period = 30, isDisabled = false });
         

            context.AdPackages.AddRange(defaultAdPackages);
            context.SaveChanges();

			// Seed for AdInvoice
			IList<AdInvoice> defaultAdInvoice = new List<AdInvoice>();
			defaultAdInvoice.Add(new AdInvoice()
			{
				User = context.Users.FirstOrDefault(x=>x.Id == id),
				AdPackage = context.AdPackages.FirstOrDefault(x => x.Id == 1),
				Price = 200000,
				createdDate = DateTime.Now,
				ExpiredDate = DateTime.Now.AddDays((from c in context.AdPackages.Where(c => c.Id == 1) select c.Period).FirstOrDefault()),
				Status = false
			});
			defaultAdInvoice.Add(new AdInvoice()
			{
				User = context.Users.FirstOrDefault(x => x.Id == id),
				AdPackage = context.AdPackages.FirstOrDefault(x => x.Id == 2),
				Price = 200000,
				createdDate = DateTime.Now,
				ExpiredDate = DateTime.Now.AddDays((from c in context.AdPackages.Where(c => c.Id ==2) select c.Period).FirstOrDefault()),
				Status = false
			});
			defaultAdInvoice.Add(new AdInvoice()
			{
				User = context.Users.FirstOrDefault(x => x.Id == id ),
				AdPackage = context.AdPackages.FirstOrDefault(x => x.Id == 3),
				Price = 350000,
				createdDate = DateTime.Now,
				ExpiredDate = DateTime.Now.AddDays((from c in context.AdPackages.Where(c => c.Id == 3) select c.Period).FirstOrDefault()),
				Status = false
			});
			defaultAdInvoice.Add(new AdInvoice()
			{
				User = context.Users.FirstOrDefault(x => x.Id == id),
				AdPackage = context.AdPackages.FirstOrDefault(x => x.Id == 4),
				Price = 350000,
				createdDate = DateTime.Now,
				ExpiredDate = DateTime.Now.AddDays((from c in context.AdPackages.Where(c => c.Id == 4) select c.Period).FirstOrDefault()),
				Status = false
			});
			defaultAdInvoice.Add(new AdInvoice()
			{
				User = context.Users.FirstOrDefault(x => x.Id == id),
				AdPackage = context.AdPackages.FirstOrDefault(x => x.Id == 5),
				Price = 599000,
				createdDate = DateTime.Now,
				ExpiredDate = DateTime.Now.AddDays((from c in context.AdPackages.Where(c => c.Id == 5) select c.Period).FirstOrDefault()),
				Status = false
			});



			context.AdInvoices.AddRange(defaultAdInvoice);
			context.SaveChanges();

			//Seed for Merchant Stores
			IList<MerchantStore> defaultStores = new List<MerchantStore>();
			defaultStores.Add(new MerchantStore() { User = context.Users.FirstOrDefault(x => x.Id == id), Name = "Cửa hàng Infinity",
				Address = "362 Nguyễn Chí Thanh P16 Q11 TPHCM", BusinessRegistrationCode = "0310941649",
				TaxRegistrationCode = "0102859048", CardTradeNumber = "9704-0509-0070-8986",
				CreditCardNumber = "160035391322", PhoneNumber = "0396372123",
				DeliveryMethod = DeliveryMethod.Fast, createdDate = new DateTime(2011,3,12),
				BankName = "Agribank", Package = 50, Image1 = "~/Assets/img/new_1.png",
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
                Name = "LAPTOP HP PROBOOK 4540s",
                Price = 5600000,
                Quantity = 25,
                Category = context.Categories.FirstOrDefault(x => x.Id == 5),
                Type = context.ProductTypes.FirstOrDefault(x => x.Id == 2),
                discountValue = 0,
                Description = "hàng xách tay nước ngoài,máy nguyên zin 100 % có window bản quyền,chưa qua sửa chữa hay thay đổi gì",
                CPU = "Core i5-3320m 2.6GHz TurboBoost 3.0GHz",
                RAM = "4GB",
                hardDrive = "HDD 320GB",
                screenType = "15.6' HD chống chói",
                GPU = "Intel HD4000",
                IOPort = "2 cổng USB,cổng tai nghe,cổng VGA,LAN",
                OS = "Windows 7",
                DesignType = "HP Notebook",
                Size = 2.13,
                updateDate = new DateTime(2018, 12, 1),
                Image1 = "/Assets/img/product1-01.jpg",
                Image2 = "/Assets/img/product1-01.jpg",
                Image3 = "/Assets/img/product1-01.jpg",
                AdType = AdType.Hot,
                Rating = 4,
                isDisabled = false
            });

            defaultProducts.Add(new Product()
            {
                Store = context.MerchantStores.FirstOrDefault(x => x.Id == 1),
                Name = "Laptop Lenovo Thinkpad T440",
                Price = 8900000,
                Quantity = 2,
                Category = context.Categories.FirstOrDefault(x => x.Id == 6),
                Type = context.ProductTypes.FirstOrDefault(x => x.Id == 2),
                discountValue = 10,
                Description = "Dòng máy Laptop Thinkpad T440 siêu bền giá rẻ vỏ nhôm màu đen, xách tay giá rẻ, đời máy core i5 thế hệ thứ 4 nguyên zin rất đẹp.",
                CPU = "I7–4600u 2.1ghz",
                RAM = "8GB DDR3",
                hardDrive = "SSD 240GB",
                screenType = "14' FHD LED chống chói",
                GPU = "Intel hd 4400",
                IOPort = "2 cổng USB 3.0 bla bla",
                OS = "Windows 7",
                DesignType = "2 cổng USB,cổng tai nghe,cổng HDMI,LAN",
                Size = 2.1,
                updateDate = new DateTime(2018,12,10),
                Image1 = "/Assets/img/product2-01.jpg",
                Image2 = "/Assets/img/product2-01.jpg",
                Image3 = "/Assets/img/product2-02.jpg",
				AdType = AdType.No,
                Rating = 3,
				isDisabled = false
            });

            context.Products.AddRange(defaultProducts);
            context.SaveChanges();

            //Seed for invoice and details
            IList<Invoice> defaultInvoices = new List<Invoice>();
            defaultInvoices.Add(new Invoice() {
                User = context.Users.FirstOrDefault(x => x.Id == user_id),
                Address = "254/11/8 CXBT P8 Q11",
                Total = 8900000*3,
                PaymentMethod = PaymentMethod.COD,
                DeliveryMethod = DeliveryMethod.Standard,
                Description = "Khách mua nhiều , giao hàng ngay lập tức",
                createdDate = new DateTime(2018,12,12),
                Status = ProductStatus.Delivered,
                isDisabled = false,
                Name = "Thành",
                Email = "client@gmail.com",
                Phone = "0908205083",
                TransactionId = "186946987497432112"
            });

            defaultInvoices.Add(new Invoice()
            {
                User = context.Users.FirstOrDefault(x => x.Id == user_id),
                Address = "254/11/8 CXBT P8 Q11",
                Total = 8900000 * 1,
                PaymentMethod = PaymentMethod.COD,
                DeliveryMethod = DeliveryMethod.Fast,
                Description = "Giao 1 laptop lúc 6h chiều cùng ngày",
                createdDate = new DateTime(2018, 12, 13),
                Status = ProductStatus.Delivered,
                isDisabled = false,
                Name = "Thành",
                Email = "client@gmail.com",
                Phone = "0908205083",
                TransactionId = "2863214875467471133"
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
                Name = "Cương",
                Email = "tamtam@gmail.com",
                Phone = "0909090909",
                TransactionId = "DOANXEM4"
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
                Status = ProductStatus.NotValidated,
                isDisabled = false,
                Name = "Thành",
                Email = "tamtam@gmail.com",
                Phone = "0909090909",
                TransactionId = "DOANXEM3"
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
                Status = ProductStatus.Delivering,
                isDisabled = false,
                Name = "Tâm",
                Email = "tamtam@gmail.com",
                Phone = "0909090909",
                TransactionId = "DOANXEM2"
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
                Status = ProductStatus.Delivered,
                isDisabled = false,
                Name = "Tâm",
                Email = "tamtam@gmail.com",
                Phone = "0909090909",
                TransactionId = "DOANXEM1"
            });

            defaultInvoices.Add(new Invoice()
            {
                User = context.Users.FirstOrDefault(x => x.Id == user_id),
                Address = "Nhà thằng phú",
                Total = 17000000,
                PaymentMethod = PaymentMethod.Online,
                DeliveryMethod = DeliveryMethod.Fast,
                Description = "Trả tiền online đồ",
                createdDate = new DateTime(2018,11,12),
                Status = ProductStatus.Delivered,
                isDisabled = false,
                Name = "Tâm",
                Email = "tamtam@gmail.com",
                Phone = "0909090909",
                TransactionId = "DOANXEM0"
            });

            defaultInvoices.Add(new Invoice()
            {
                User = context.Users.FirstOrDefault(x => x.Id == user_id),
                Address = "Nhà thằng phú",
                Total = 17000000,
                PaymentMethod = PaymentMethod.Online,
                DeliveryMethod = DeliveryMethod.Fast,
                Description = "Trả tiền online đồ",
                createdDate = new DateTime(2018, 11, 15),
                Status = ProductStatus.Delivered,
                isDisabled = false,
                Name = "Tâm",
                Email = "tamtam@gmail.com",
                Phone = "0909090909",
                TransactionId = "DOANXEM-1"
            });

            context.Invoices.AddRange(defaultInvoices);
            context.SaveChanges();

            IList<InvoiceDetail> defaultInvoiceDetails = new List<InvoiceDetail>();
            defaultInvoiceDetails.Add(new InvoiceDetail() {
                Invoice = context.Invoices.FirstOrDefault(x => x.Id == 1),
                Product = context.Products.FirstOrDefault(x => x.Id == 2),
                Quantity = 3,
                Price = 8900000,
                isDisabled = false
            });

            defaultInvoiceDetails.Add(new InvoiceDetail()
            {
                Invoice = context.Invoices.FirstOrDefault(x => x.Id == 2),
                Product = context.Products.FirstOrDefault(x => x.Id == 2),
                Quantity = 1,
                Price = 8900000,
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

            defaultInvoiceDetails.Add(new InvoiceDetail()
            {
                Invoice = context.Invoices.FirstOrDefault(x => x.Id == 3),
                Product = context.Products.FirstOrDefault(x => x.Id == 2),
                Quantity = 1,
                Price = 17000000,
                isDisabled = false
            });

            defaultInvoiceDetails.Add(new InvoiceDetail()
            {
                Invoice = context.Invoices.FirstOrDefault(x => x.Id == 4),
                Product = context.Products.FirstOrDefault(x => x.Id == 2),
                Quantity = 1,
                Price = 17000000,
                isDisabled = false
            });

            defaultInvoiceDetails.Add(new InvoiceDetail()
            {
                Invoice = context.Invoices.FirstOrDefault(x => x.Id == 5),
                Product = context.Products.FirstOrDefault(x => x.Id == 2),
                Quantity = 1,
                Price = 17000000,
                isDisabled = false
            });

            defaultInvoiceDetails.Add(new InvoiceDetail()
            {
                Invoice = context.Invoices.FirstOrDefault(x => x.Id == 6),
                Product = context.Products.FirstOrDefault(x => x.Id == 2),
                Quantity = 1,
                Price = 17000000,
                isDisabled = false
            });

            defaultInvoiceDetails.Add(new InvoiceDetail()
            {
                Invoice = context.Invoices.FirstOrDefault(x => x.Id == 7),
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