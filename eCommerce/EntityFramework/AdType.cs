using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace eCommerce.EntityFramework
{
    public enum AdType
    {
		[Description("Không quảng cáo")]
		No = 0,
		[Description("Sản phẩm mới")]
        New = 1,
        [Description("Sản phẩm hot")]
        Hot = 2,
		[Description("Promotion")]
		SlideShow = 3,
		[Description("Khuyến khích")]
		Encourage = 4,
	}
}