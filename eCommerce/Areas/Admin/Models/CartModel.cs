using eCommerce.EntityFramework;
using System.Collections.Generic;

namespace eCommerce.Areas.Admin.Models
{
    public class AlertModel
    {
        public int Quantity { get; set; }

        public List<Invoice> Invoices { get; set; }
    }
}