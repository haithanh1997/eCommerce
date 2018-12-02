using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace eCommerce.EntityFramework
{
    public enum ProductStatus
    {
        [Description("NotValidated")]
        NotValidated = 0,
        [Description("Validated")]
        Validated = 1,
        [Description("Processing")]
        Processing = 2,
        [Description("Delivering")]
        Delivering = 3,
        [Description("Delivered")]
        Delivered = 4,
    }
}