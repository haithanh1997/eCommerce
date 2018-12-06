using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eCommerce.EntityFramework
{
    public class AdPackage
    {
        [Key]
        public long Id { get; set; }
        //public virtual IdentityUser User { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Position { get; set; }
        public int Period { get; set; }
        //public DateTime ExpiredDate { get; set; }
        public bool isDisabled { get; set; }
    }
}