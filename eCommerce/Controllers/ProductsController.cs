﻿using eCommerce.Models;
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
            if(param.type != null)
            {
                foreach (var typeId in param.type)
                {
                    query = query.Where(x => x.Type.Id == typeId);
                }
            }

            if(param.drive != null)
            {
                foreach(var drive in param.drive)
                {
                    query = query.Where(x => x.hardDrive.Contains(drive));
                }
            }

            if (param.cpu != null)
            {
                foreach (var cpu in param.cpu)
                {
                    query = query.Where(x => x.CPU.Contains(cpu));
                }
            }

            if (param.ram != null)
            {
                foreach (var ram in param.ram)
                {
                    query = query.Where(x => x.RAM.Contains(ram));
                }
            }

            if (param.size != null)
            {
                foreach (var size in param.size)
                {
                    query = query.Where(x => x.screenType.Contains(size));
                }
            }

            var products = query.ToList();
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