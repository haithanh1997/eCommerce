using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eCommerce.EntityFramework
{
    public class Package
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Times { get; set; }
        public int Days { get; set; }
        public bool isDisabled { get; set; }
    }
}