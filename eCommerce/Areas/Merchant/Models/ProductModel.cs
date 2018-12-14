using eCommerce.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCommerce.Areas.Merchant.Models
{
	public class ProductModel
	{
		[Key]
		public long ProductId { get; set; } // Đặt tên khác giùm

		[DisplayName("Cửa hàng")] 
		public virtual MerchantStore Store { get; set; }

		[Required]
		[DisplayName("Tên sản phẩm")]
		public string Name { get; set; }

		[Required]
		[DisplayName("Giá")]
		public int Price { get; set; }

		[Required]
		[DisplayName("Số lượng còn lại")]
		public int Quantity { get; set; }

		[DisplayName("Thương Hiệu")]
		public long CategorySelectedId { get; set; }

		[DisplayName("Loại sản phẩm")]
		public long TypeSelectedId { get; set; }


		[Required]
		[DisplayName("Giá trị giảm (%)")]
		public int discountValue { get; set; }

		//public string Provider { get; set; }
		[Required]
		[DisplayName("Ghi chú")]
		public string Description { get; set; }

		[Required]
		[DisplayName("Vi xử lý")]
		public string CPU { get; set; }

		[Required]
		[DisplayName("Bộ nhớ RAM")]
		public string RAM { get; set; }

		[Required]
		[DisplayName("Ổ cứng")]
		public string hardDrive { get; set; }

		[Required]
		[DisplayName("Màn hình")]
		public string screenType { get; set; }

		[Required]
		[DisplayName("Card đồ họa")]
		public string GPU { get; set; }

		[Required]
		[DisplayName("Cổng đọc")]
		public string IOPort { get; set; }


		[Required]
		[DisplayName("Hệ điều hành")]
		public string OS { get; set; }

		[Required]
		[DisplayName("Kiểu thiết kế")]
		public string DesignType { get; set; }

		[Required]
		[DisplayName("Trọng lượng")]
		public double Size { get; set; }

		[DisplayName("Ngày đăng")]
		public DateTime updateDate { get; set; }
		//public Nullable<DateTime> deletedDate { get; set; }
		[Required]
		[DisplayName("Ảnh 1")]
		public string Image1 { get; set; }

		[Required]
		[DisplayName("Ảnh 2")]
		public string Image2 { get; set; }

		[Required]
		[DisplayName("Ảnh 3")]
		public string Image3 { get; set; }

		[DisplayName("Quảng cáo")]
		public virtual AdType AdType { get; set; }

		[Required]
		[DisplayName("Trạng thái")]
		public bool isDisabled { get; set; }

		public List<SelectListItem> Type { get; set; }

		public List<SelectListItem> Category { get; set; }
	}
}