using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eCommerce.Areas.Admin.Models
{
    public class StatisticDailyModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public int Total { get; set; }
    }
}