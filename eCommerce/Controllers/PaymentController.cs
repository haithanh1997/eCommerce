﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCommerce.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment
        public ActionResult PaymentIndex()
        {
            return View();
        }

    }
}