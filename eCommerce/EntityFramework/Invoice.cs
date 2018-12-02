using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eCommerce.EntityFramework
{
    public class Invoice
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [DisplayName("Người dùng")]
        public virtual IdentityUser User { get; set; }
        [Required]
        [DisplayName("Địa chỉ")]
        public string Address { get; set; }
        [Required]
        [DisplayName("Tổng tiền")]
        public int Total { get; set; }
        [Required]
        [DisplayName("Hình thức thanh toán")]
        public PaymentMethod PaymentMethod { get; set; }
        [Required]
        [DisplayName("Hình thức giao hàng")]
        public DeliveryMethod DeliveryMethod { get; set; }
        [DisplayName("Ghi chú")]
        public string Description { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Ngày tạo")]
        public DateTime createdDate { get; set; }
        [Required]
        [DisplayName("Trạng thái")]
        public ProductStatus Status { get; set; }
        [DisplayName("Không hiển thị")]
        public bool isDisabled { get; set; }
    }
}