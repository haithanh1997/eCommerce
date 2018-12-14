using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eCommerce.EntityFramework
{
	public class SlideShow
	{
		public IdentityUser User { get; set; }
		public AdInvoice AdInvoice { get; set; }
		public string Titled { get; set; }
		public string Summary { get; set; }
		public string Image { get; set; }
	}
}