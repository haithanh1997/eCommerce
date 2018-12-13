using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eCommerce.EntityFramework;

namespace eCommerce.Models
{
    public class HomeViewModel
    {
        public List<Product> ProductView { get; set; }
        public List<Category> CategoryView { get; set; }
    }
}