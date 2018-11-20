using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace eCommerce.EntityFramework
{
    public enum PaymentMethod
    {
        [Description("Online")]
        Online=1,
        [Description("COD")]
        COD=2,
    }
}