using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eCommerce.EntityFramework
{
    public class MerchantRepaidHistory
    {
        [Key]
        public long Id { get; set; }

        public virtual IdentityUser Merchant { get; set; }

        public int Total { get; set; }

        public DateTime CreatedDate { get; set; }

        public string TransactionId { get; set; }

        public string Ticket { get; set; }

        public bool IsTemporary { get; set; }
    }
}