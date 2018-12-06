using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eCommerce.EntityFramework
{
    public class MerchantStore
    {
        [Key]
        public long Id { get; set; }

       [Required]
        [DisplayName("Mã chủ cửa hàng")]
        public virtual IdentityUser User { get; set; }
       [Required]
        [DisplayName("Tên cửa hàng")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Địa chỉ")]
        public string Address { get; set; }
        [Required]
        [DisplayName("Mã đăng ký kinh doanh")]
        public string BusinessRegistrationCode { get; set; }
        [Required]
        [DisplayName("Mã số thuế")]
        public string TaxRegistrationCode { get; set; }
        [Required]
        [DisplayName("Mã số thẻ")]
        public string CreditCardNumber { get; set; }
        [Required]
		//[DataType(DataType.CreditCard)]
		[DisplayName("Số tài khoản")]
        public string CardTradeNumber { get; set; }
        [Required]
        //[DataType(DataType.PhoneNumber)]
        [DisplayName("Số điện thoại")]
        public string PhoneNumber { get; set; }
        [Required]
        [DisplayName("Phương thức giao hàng")]
        public DeliveryMethod DeliveryMethod { get; set; }
        [Required]
		//[DataType(DataType.Date)]
        [DisplayName("Ngày thành lập")]
        public DateTime createdDate { get; set; }
        [Required]
        [DisplayName("Tên ngân hàng")]
        public string BankName { get; set; }
        [Required]
        [DisplayName("Ảnh cửa hàng")]
        public string Image1 { get; set; }
        [Required]
        [DisplayName("CMND mặt trước")]
        public string Image2 { get; set; }
        [Required]
        [DisplayName("CMND mặt sau")]
        public string Image3 { get; set; }
        [Required]
        [DisplayName("GPKD mặt trước")]
        public string Image4 { get; set; }
        [Required]
        [DisplayName("GPKD mặt sau")]
        public string Image5 { get; set; }
    }
}