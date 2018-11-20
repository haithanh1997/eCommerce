using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eCommerce.EntityFramework
{
    public class Cart
    {
        [Key]
        public long Id { get; set; }
        public virtual IdentityUser User { get; set; }
        public virtual Product Product { get; set; }
        public virtual ProductType Type { get; set; }
        public int Quantity { get; set; }
        //public int Price { get; set; }
    }
}