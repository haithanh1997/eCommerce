using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eCommerce.EntityFramework
{
	public class SlideShow
	{
		[Key]
		public long Id { get; set; }
		public IdentityUser User { get; set; }
		public AdInvoice AdInvoice { get; set; }
		[Display(Name ="Tựa đề")]
		public string Titled { get; set; }
		[Display(Name = "Tóm tắt")]
		public string Summary { get; set; }
		[Display(Name = "Ảnh")]
		public string Image { get; set; }
		[Display(Name = "Hiển thị")]
		public bool isDisable { get; set; }
	}
}