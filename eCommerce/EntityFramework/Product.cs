using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eCommerce.EntityFramework
{
    public class Product
    {
        [Key]
        public long Id { get; set; }

        [DisplayName("Cửa hàng")]
        public virtual MerchantStore Store { get; set; }

        [Required(ErrorMessage = "Trường {0} bắt buộc nhập.")]
        [DisplayName("Tên sản phẩm")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Trường {0} bắt buộc nhập.")]
        [DisplayName("Giá")]
        public int Price { get; set; }

        [Required(ErrorMessage = "Trường {0} bắt buộc nhập.")]
        [DisplayName("Số lượng còn lại")]
        public int Quantity { get; set; }

        [DisplayName("Danh mục")]
        public virtual Category Category { get; set; }

        [DisplayName("Loại sản phẩm")]
        public virtual ProductType Type { get; set; }

        [Required(ErrorMessage = "Trường {0} bắt buộc nhập.")]
        [DisplayName("Giá trị giảm (%)")]
        public int discountValue { get; set; }

        //public string Provider { get; set; }
        [Required(ErrorMessage = "Trường {0} bắt buộc nhập.")]
        [DisplayName("Ghi chú")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Trường {0} bắt buộc nhập.")]
        [DisplayName("Vi xử lý")]
        public string CPU { get; set; }

        [Required(ErrorMessage = "Trường {0} bắt buộc nhập.")]
        [DisplayName("Bộ nhớ RAM")]
        public string RAM { get; set; }

        [Required(ErrorMessage = "Trường {0} bắt buộc nhập.")]
        [DisplayName("Ổ cứng")]
        public string hardDrive { get; set; }

        [Required(ErrorMessage = "Trường {0} bắt buộc nhập.")]
        [DisplayName("Màn hình")]
        public string screenType { get; set; }

        [Required(ErrorMessage = "Trường {0} bắt buộc nhập.")]
        [DisplayName("Card đồ họa")]
        public string GPU { get; set; }

        [Required(ErrorMessage = "Trường {0} bắt buộc nhập.")]
        [DisplayName("Cổng đọc")]
        public string IOPort { get; set; }


        [Required(ErrorMessage = "Trường {0} bắt buộc nhập.")]
        [DisplayName("Hệ điều hành")]
        public string OS { get; set; }

        [Required(ErrorMessage = "Trường {0} bắt buộc nhập.")]
        [DisplayName("Kiểu thiết kế")]
        public string DesignType { get; set; }

        [Required(ErrorMessage = "Trường {0} bắt buộc nhập.")]
        [DisplayName("Trọng lượng")]
        public double Size { get; set; }
      
        [DisplayName("Ngày đăng")]
        public DateTime updateDate { get; set; }
        //public Nullable<DateTime> deletedDate { get; set; }
        [Required(ErrorMessage = "Trường {0} bắt buộc nhập.")]
        [DisplayName("Ảnh 1")]
        public string Image1 { get; set; }

        [Required(ErrorMessage = "Trường {0} bắt buộc nhập.")]
        [DisplayName("Ảnh 2")]
        public string Image2 { get; set; }

        [Required(ErrorMessage = "Trường {0} bắt buộc nhập.")]
        [DisplayName("Ảnh 3")]
        public string Image3 { get; set; }

        [DisplayName("Quảng cáo")]
        public AdType AdType { get; set; }

        [DisplayName("Đánh giá")]
        public int Rating { get; set; }

        [Required(ErrorMessage = "Trường {0} bắt buộc nhập.")]
        [DisplayName("Trạng thái")]
        public bool isDisabled { get; set; }
    }
}