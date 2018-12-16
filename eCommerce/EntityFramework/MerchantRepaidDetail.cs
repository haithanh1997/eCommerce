using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eCommerce.EntityFramework
{
    public class MerchantRepaidDetail
    {
        [Key]
        public long Id { get; set; }

        public virtual MerchantRepaidHistory History { get; set; }

        public virtual InvoiceDetail InvoiceDetail { get; set; }
    }
}