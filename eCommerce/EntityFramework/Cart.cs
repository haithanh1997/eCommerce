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
        [Required]
        [DisplayName("Sản phẩm")]
        public virtual Product Product { get; set; }
        //[Required]
        //[DisplayName("Loại sản phẩm")]
        //public virtual ProductType Type { get; set; }
        [Required]
        [DisplayName("Số lượng")]
        public int Quantity { get; set; }
        [Required]
        [DisplayName("Đơn giá")]
        public int Price { get; set; }
    }
}