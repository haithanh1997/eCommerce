﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCommerce.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult ProductsIndex()
        {
            return View();
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