using eCommerce.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eCommerce.Areas.Merchant.Models
{
	public class Services
	{
		[Key]
		[Display(Name ="Mã của hàng")]
		public long Macuahang { get; set; }

		[Display(Name = "Mã gói quảng cáo")]
		public long MaQuangCao { get; set; }

		[Display(Name = "Mã gói bài đăng")]
		public long MaBaiDang { get; set; }

		public AdPackage AdPackage { get; set; }
		public MerchantStore Store { get; set; }
		public Package Package { get; set; }
	}
}