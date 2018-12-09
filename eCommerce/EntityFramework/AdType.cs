using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace eCommerce.EntityFramework
{
    public enum AdType
    {
        [Description("Default")]
        Default = 0,
        [Description("Mức PRE")]
        Premium = 1,
        [Description("Mức STD")]
        Standard = 2,
    }
}