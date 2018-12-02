using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eCommerce.EntityFramework
{
    public class InvoiceDetail
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [DisplayName("Mã hóa đơn")]
        public virtual Invoice Invoice { get; set; }
        [Required]
        [DisplayName("Mã sản phẩm")]
        public virtual Product Product { get; set; }
        //[Required]
        //[DisplayName("Loại sản phẩm")]
        //public virtual ProductType Type { get; set; }
        [Required]
        [DisplayName("Số lượng")]
        public int Quantity { get; set; }
       // public string Provider { get; set; }
       [Required]
       [DisplayName("Đơn giá")]
        public int Price { get; set; }
        [DisplayName("Không hiển thị")]
       public bool isDisabled { get; set; }
    }
}