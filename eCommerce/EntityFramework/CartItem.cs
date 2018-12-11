using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace eCommerce.EntityFramework
{
    public class CartItem
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public virtual Cart Cart { get; set; }

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

        public decimal ItemAmount
        {
            get
            {
                return Product.Price * Quantity;
            }
        }
    }
}