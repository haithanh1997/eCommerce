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
        [Required(ErrorMessage = "Trường {0} bắt buộc nhập.")]
        [DisplayName("Người dùng")]
        public virtual IdentityUser User { get; set; }
        [Required(ErrorMessage = "Trường {0} bắt buộc nhập.")]
        [DisplayName("Họ tên")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Trường {0} bắt buộc nhập.")]
        [DisplayName("Địa chỉ")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Trường {0} bắt buộc nhập.")]
        [DisplayName("Số điện thoại")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Trường {0} bắt buộc nhập.")]
        [DisplayName("Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Trường {0} bắt buộc nhập.")]
        [DisplayName("Tổng tiền")]
        public int Total { get; set; }
        [Required(ErrorMessage = "Trường {0} bắt buộc nhập.")]
        [DisplayName("Hình thức thanh toán")]
        public PaymentMethod PaymentMethod { get; set; }
        [Required(ErrorMessage = "Trường {0} bắt buộc nhập.")]
        [DisplayName("Hình thức giao hàng")]
        public DeliveryMethod DeliveryMethod { get; set; }
        [DisplayName("Ghi chú")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Trường {0} bắt buộc nhập.")]
        [DataType(DataType.Date,ErrorMessage = "{0} nhập sai định dạng.")]
        [DisplayName("Ngày tạo")]
        public DateTime createdDate { get; set; }
        [Required(ErrorMessage = "Trường {0} bắt buộc nhập.")]
        [DisplayName("Trạng thái")]
        public ProductStatus Status { get; set; }
        [DisplayName("Không hiển thị")]
        public bool isDisabled { get; set; }
        [DisplayName("Mã giao dịch")]
        public string TransactionId { get; set; }
    }
}