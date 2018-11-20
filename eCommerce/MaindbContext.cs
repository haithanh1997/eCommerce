using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace eCommerce
{
    public class MainDbContext : DbContext
    {
            public MainDbContext() : base("name=DefaultConnection")
            {
                //Database.SetInitializer(new DbInitialize());
            }

        //    public System.Data.Entity.DbSet<eCommerce.EntityFramework.Category> Categories { get; set; }

        //    public DbSet<Article> Articles { get; set; }

        //    public DbSet<Invoice> Invoices { get; set; }

        //    public DbSet<AdPackage> AdPackages { get; set; }

        //    public DbSet<Cart> Carts { get; set; }

        //    public DbSet<InvoiceDetail> InvoiceDetails { get; set; }

        //    public DbSet<Product> Products { get; set; }

        //    public DbSet<ProductType> ProductTypes { get; set; }
        //    protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //    {
        //        modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        //        modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
        //        modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
        //        modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
        //    }
    }
}