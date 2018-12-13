using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace eCommerce.EntityFramework
{
    public class CartItem
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "Trường {0} bắt buộc nhập.")]
        public virtual Cart Cart { get; set; }

        [Required(ErrorMessage = "Trường {0} bắt buộc nhập.")]
        [DisplayName("Sản phẩm")]
        public virtual Product Product { get; set; }
        //[Required]
        //[DisplayName("Loại sản phẩm")]
        //public virtual ProductType Type { get; set; }
        [Required(ErrorMessage = "Trường {0} bắt buộc nhập.")]
        [DisplayName("Số lượng")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Trường {0} bắt buộc nhập.")]
        [DisplayName("Đơn giá")]
        public int Price { get; set; }

        public int ItemAmount
        {
            get
            {
                return Product.Price * Quantity;
            }
        }
    }
}