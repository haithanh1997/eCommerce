using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eCommerce.EntityFramework
{
    public class Invoice
    {
        [Key]
        public long Id { get; set; }
        public virtual IdentityUser User { get; set; }
        public string Address { get; set; }
        public int Total { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public string Description { get; set; }
        public DateTime createdDate { get; set; }
        public string Status { get; set; }
        public bool isDisabled { get; set; }
    }
}