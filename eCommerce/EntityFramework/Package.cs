using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eCommerce.EntityFramework
{
    public class Package
    {
        [Key]
        [DisplayName("Mã gói bài đăng")]
        public long Id { get; set; }
        [Required]
        [DisplayName("Tên gói")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Giá gói (VNĐ)")]
        public int Price { get; set; }
        [Required]
        [DisplayName("Số lần đăng")]
        public int Times { get; set; }
        [Required]
        [DisplayName("Thời hạn")]
        public int Days { get; set; }
        [DisplayName("Trạng thái")]
        public bool isDisabled { get; set; }
    }
}