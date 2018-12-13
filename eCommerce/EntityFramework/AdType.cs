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
        [Description("POP 7D")]
        Pop7D = 1,
        [Description("POP 7D")]
        Pop15D = 2,
        [Description("POP 7D")]
        Pop30D = 3,
        [Description("REC 7D")]
        Rec7D = 4,
        [Description("REC 15D")]
        Rec15D = 5,
        [Description("REC 30D")]
        Rec30D = 6,
    }
}