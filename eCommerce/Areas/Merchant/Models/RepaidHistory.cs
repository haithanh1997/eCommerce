using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eCommerce.Areas.Merchant.Models
{
    public class RepaidHistory
    {
        public DateTime createDate { get; set; }

        public int Total { get; set; }

        public string TransactionId { get; set; }
    }
}