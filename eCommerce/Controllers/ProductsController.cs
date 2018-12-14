﻿using eCommerce.EntityFramework;
using eCommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCommerce.Controllers
{
    public class ProductsController : Controller
    {
        MainDbContext db = new MainDbContext();
        // GET: Products
        public ActionResult ProductsIndex(ProductFilterParam param)
        {
            var query = db.Products.Where(x => x.Category.Name == param.name);
            // Get Default Products without searching for price because we must display defaultMax and defaultMin on Max-Min slide bar
            var defaultProducts = new List<Product>();
            foreach(var item in query)
            {
                defaultProducts.Add(item);
            }
            if (param.type != null)
            {
                foreach (var typeId in param.type)
                {
                    var model = db.Products.Where(x => x.Type.Id == typeId && x.Category.Name == param.name);
                    foreach(var item in model)
                    {
                        if(!defaultProducts.Contains(item))
                        {
                            defaultProducts.Add(item);
                        }
                    }
                }
            }

            if(param.drive != null)
            {
                foreach(var drive in param.drive)
                {
                    var model = db.Products.Where(x => x.hardDrive.Contains(drive) && x.Category.Name == param.name);
                    foreach (var item in model)
                    {
                        if (!defaultProducts.Contains(item))
                        {
                            defaultProducts.Add(item);
                        }
                    }
                }
            }

            if (param.cpu != null)
            {
                foreach (var cpu in param.cpu)
                {
                    var model = db.Products.Where(x => x.CPU.Contains(cpu) && x.Category.Name == param.name);
                    foreach (var item in model)
                    {
                        if (!defaultProducts.Contains(item))
                        {
                            defaultProducts.Add(item);
                        }
                    }
                }
            }

            if (param.ram != null)
            {
                foreach (var ram in param.ram)
                {
                    var model = db.Products.Where(x => x.RAM.Contains(ram) && x.Category.Name == param.name);
                    foreach (var item in model)
                    {
                        if (!defaultProducts.Contains(item))
                        {
                            defaultProducts.Add(item);
                        }
                    }
                }
            }

            if (param.size != null)
            {
                foreach (var size in param.size)
                {
                    var model = db.Products.Where(x => x.screenType.Contains(size) && x.Category.Name == param.name);
                    foreach (var item in model)
                    {
                        if (!defaultProducts.Contains(item))
                        {
                            defaultProducts.Add(item);
                        }
                    }
                }
            }
            

            // Final Products display client-side
            var products = defaultProducts;
            if (param.min != 0 || param.max != 0)
                products = defaultProducts.Where(x => x.Price >= param.min && x.Price <= param.max).ToList();

            return View(new ProductIndexModel()
            {
                Categories = db.Categories.Where(x => !x.isDisabled).ToList(),
                Types = db.ProductTypes.Where(x => x.isDisabled == false).ToList(),
                Drives = new List<Drive> { new Drive { Id = "1", Name = "SSD" }, new Drive { Id = "2", Name = "HDD" } },
                CPUs = new List<CPU> { new CPU { Id = "1", Name = "i3" }, new CPU { Id = "2", Name = "i5" }, new CPU { Id = "3", Name = "i7" } },
                Rams = new List<RAM> { new RAM { Id = "1", Name = "4GB" }, new RAM { Id = "2", Name = "8GB" }, new RAM { Id = "3", Name = "16GB" } },
                Sizes = new List<Size> { new Size { Id = "1", Name = "14"}, new Size { Id = "2", Name = "15.6" }, new Size { Id = "3", Name = "17" } },
                Products = products,
                Filter = param,
                defaultMax = defaultProducts.Count > 0 ? defaultProducts.Max(x => x.Price) : 9999999,
                defaultMin = defaultProducts.Count > 0 ? (defaultProducts.Min(x => x.Price) == defaultProducts.Max(x => x.Price) ? 0 : defaultProducts.Min(x => x.Price)) : 0,
            });
        }
        public ActionResult Types()
		{
			return View();
		}
		public ActionResult Detail()
		{
			return View();
		}
	}
}