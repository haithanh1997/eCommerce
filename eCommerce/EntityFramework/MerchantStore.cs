using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eCommerce.EntityFramework
{
    public class MerchantStore
    {
        [Key]
        public long Id { get; set; }
        public virtual IdentityUser User { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime createdDate { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public string Image4 { get; set; }
    }
}