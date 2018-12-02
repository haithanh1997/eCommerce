using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace eCommerce
{
    public class MainDbContext : IdentityDbContext
    {
            public MainDbContext() : base("name=DefaultConnection")
            {
            }

        public System.Data.Entity.DbSet<eCommerce.EntityFramework.Category> Categories { get; set; }

        public System.Data.Entity.DbSet<eCommerce.EntityFramework.ProductType> ProductTypes { get; set; }

        public System.Data.Entity.DbSet<eCommerce.EntityFramework.Package> Packages { get; set; }

        public System.Data.Entity.DbSet<eCommerce.EntityFramework.Product> Products { get; set; }

        public System.Data.Entity.DbSet<eCommerce.EntityFramework.Invoice> Invoices { get; set; }

        public System.Data.Entity.DbSet<eCommerce.EntityFramework.InvoiceDetail> InvoiceDetails { get; set; }

        public System.Data.Entity.DbSet<eCommerce.EntityFramework.AdPackage> AdPackages { get; set; }

        public System.Data.Entity.DbSet<eCommerce.EntityFramework.MerchantStore> MerchantStores { get; set; }

        public System.Data.Entity.DbSet<eCommerce.EntityFramework.Cart> Carts { get; set; }
    }
}