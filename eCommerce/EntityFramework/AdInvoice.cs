using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eCommerce.EntityFramework
{
    public class AdInvoice
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [DisplayName("Người dùng")]
        public virtual IdentityUser User { get; set; }
        [Required]
        [DisplayName("Gói quảng cáo")]
        public virtual AdPackage AdPackage { get; set; }
        [Required]
        [DisplayName("Giá")]
        public int Price { get; set; }
        [Required]
        [DisplayName("Ngày lập hóa đơn")]
        public DateTime createdDate { get; set; }

		[DisplayName("Ngày hét hạn")]
		public DateTime ExpiredDate { get; set; }

		[Required]
        [DisplayName("Trạng thái")]
        public bool Status { get; set; }

        [DisplayName("Mã giao dịch")]
        public string transactionId { get; set; }

        [DisplayName("Code")]
        public string hashCode { get; set; }
    }
}