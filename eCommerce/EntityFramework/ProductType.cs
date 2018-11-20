using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eCommerce.EntityFramework
{
    public class ProductType
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public virtual Category Category { get; set; }
    }
}