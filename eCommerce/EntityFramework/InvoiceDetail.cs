using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eCommerce.EntityFramework
{
    public class InvoiceDetail
    {
        [Key]
        public long Id { get; set; }
        public virtual Invoice Invoice { get; set; }
        public virtual Product Product { get; set; }
        public virtual ProductType Type { get; set; }
        public int Quantity { get; set; }
       // public string Provider { get; set; }
       // public int Price { get; set; }
       public bool isDisabled { get; set; }
    }
}