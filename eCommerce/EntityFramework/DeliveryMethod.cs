using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace eCommerce.EntityFramework
{
    public enum DeliveryMethod
    {
        [Description("Fast")]
        Fast=1,
        [Description("Standard")]
        Standard=2,
    }
}