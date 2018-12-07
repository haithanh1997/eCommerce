using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace eCommerce.EntityFramework
{
    public enum ProductStatus
    {
        [Description("Chưa xác nhận")]
        NotValidated = 0,
        [Description("Đã xác nhận")]
        Validated = 1,
        [Description("Đang xử lý")]
        Processing = 2,
        [Description("Đang giao")]
        Delivering = 3,
        [Description("Đã giao")]
        Delivered = 4,
	
	}
}