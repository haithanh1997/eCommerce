using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eCommerce.EntityFramework
{
    public class Product
    {
        [Key]
        public long Id { get; set; }
        public MerchantStore Store { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public int Quantity { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual ProductType Type { get; set; }
        public int discountValue { get; set; }
        //public string Provider { get; set; }
        public string Description { get; set; }
        public string CPU { get; set; }
        public string RAM { get; set; }
        public string hardDrive { get; set; }
        public string screenType { get; set; }
        public string GPU { get; set; }
        public string IOPort { get; set; }
        public string OS { get; set; }
        public string DesignType { get; set; }
        public float Size { get; set; }

        public DateTime updateDate { get; set; }
        public DateTime deletedDate { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public bool isDisabled { get; set; }
    }
}