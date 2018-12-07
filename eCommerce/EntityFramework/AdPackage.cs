using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eCommerce.EntityFramework
{
    public class AdPackage
    {
        [Key]
        [DisplayName("Mã gói quảng cáo")]
        public long Id { get; set; }
        //public virtual IdentityUser User { get; set; }
        [Required]
        [DisplayName("Tên gói")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Giá gói (VNĐ)")]
        public int Price { get; set; }
        [Required]
        [DisplayName("Vị trí đặt")]
        public string Position { get; set; }
        [Required]
        [DisplayName("Thời hạn (ngày)")]
        public int Period { get; set; }
        //public DateTime ExpiredDate { get; set; }
        [Required]
        [DisplayName("Trạng thái")]
        public bool isDisabled { get; set; }
    }
}