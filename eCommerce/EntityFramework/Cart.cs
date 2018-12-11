using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eCommerce.EntityFramework
{
    public class Cart
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [DisplayName("Người dùng")]
        public virtual IdentityUser User { get; set; }
    }
}