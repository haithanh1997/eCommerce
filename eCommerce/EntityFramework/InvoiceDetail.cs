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
        [Required(ErrorMessage = "Trường {0} bắt buộc nhập.")]
        [DisplayName("Mã hóa đơn")]
        public virtual Invoice Invoice { get; set; }
        [Required(ErrorMessage = "Trường {0} bắt buộc nhập.")]
        [DisplayName("Mã sản phẩm")]
        public virtual Product Product { get; set; }
        //[Required]
        //[DisplayName("Loại sản phẩm")]
        //public virtual ProductType Type { get; set; }
        [Required(ErrorMessage = "Trường {0} bắt buộc nhập.")]
        [DisplayName("Số lượng")]
        public int Quantity { get; set; }
       // public string Provider { get; set; }
       [Required(ErrorMessage = "Trường {0} bắt buộc nhập.")]
       [DisplayName("Đơn giá")]
        public int Price { get; set; }
        [DisplayName("Xác nhận")]
       public bool isDisabled { get; set; }
    }
}