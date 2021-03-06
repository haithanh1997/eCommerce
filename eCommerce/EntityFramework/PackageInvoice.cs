﻿using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eCommerce.EntityFramework
{
    public class PackageInvoice
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [DisplayName("Người dùng")]
        public virtual IdentityUser User { get; set; }
        [Required]
        [DisplayName("Gói bài đăng")]
        public virtual Package Package { get; set; }
        [Required]
        [DisplayName("Giá")]
        public int Price { get; set; }
        [Required]
        [DisplayName("Ngày lập hóa đơn")]
        public DateTime createdDate { get; set; }
        [DisplayName("Mã giao dịch")]
        public string transactionId { get; set; }
        [DisplayName("Code")]
        public string hashCode { get; set; }
    }
}