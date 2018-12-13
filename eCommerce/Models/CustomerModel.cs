using eCommerce.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eCommerce.Models
{
    public class UserInfoModel
    {
        [Required(ErrorMessage = "Vui lòng nhập họ tên!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ!")]
        public string Address { get; set; }

        [RegularExpression("^[0-9]{10,11}$", ErrorMessage = "Số điện thoại không hợp lệ!")]
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại!")]
        public string Phone { get; set; }

        public int TotalAmount { get; set; }

        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Định dạng Email không hợp lệ!")]
        [Required(ErrorMessage = "Vui lòng nhập Email!")]
        public string Email { get; set; }
        
        public PaymentMethod Payment { get; set; }
        
        public bool AgreePolicy { get; set; }

        public string Description { get; set; }
    }

    public class PaymentCompleteModel
    {
        public long InvoiceId { get; set; }
    }

    public class OnlinePaymentModel
    {
        public string transactionID { get; set; }

        public int status { get; set; }

        public string time { get; set; }

        public string ticket { get; set; }
    }
}