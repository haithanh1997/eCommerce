using eCommerce.EntityFramework;
using eCommerce.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace eCommerce.Controllers
{
    public class ProductsController : Controller
    {
        MainDbContext db = new MainDbContext();
        // GET: Products
        public ActionResult ProductsIndex(ProductFilterParam param)
        {
            int pageNo = 0;
            pageNo = param.page == null ? 1 : int.Parse(param.page.ToString());

            var query = db.Products.Where(x => x.Category.Name == param.name);
            ViewBag.CategoryName = param.name;
            // Get Default Products without searching for price because we must display defaultMax and defaultMin on Max-Min slide bar
            var defaultProducts = new List<Product>();
            if(param.type == null && param.drive == null && param.cpu == null && param.ram == null && param.size == null)
            {
                foreach (var item in query)
                {
                    defaultProducts.Add(item);
                }
            }
            if (param.type != null)
            {
                
                foreach (var typeId in param.type)
                {
                    var model = query.Where(x => x.Type.Id == typeId);
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
                    var model = query.Where(x => x.hardDrive.Contains(drive));
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
                    var model = query.Where(x => x.CPU.Contains(cpu));
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
                    var model = query.Where(x => x.RAM.Contains(ram));
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
                    var model = query.Where(x => x.screenType.Contains(size));
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

            int totalProducts = products.Count();
            int itemPerPage = 5;
            int pageEnd = pageNo * itemPerPage;
            int skip = pageEnd - itemPerPage;
            var items = products.Skip(skip).Take(itemPerPage);
            Pager<Product> pager = new Pager<Product>(items.AsQueryable(), pageNo, itemPerPage, totalProducts, param.name);

            return View(new ProductIndexModel()
            {
                Categories = db.Categories.Where(x => !x.isDisabled).ToList(),
                Types = db.ProductTypes.Where(x => x.isDisabled == false).ToList(),
                Drives = new List<Drive> { new Drive { Id = "1", Name = "SSD" }, new Drive { Id = "2", Name = "HDD" } },
                CPUs = new List<CPU> { new CPU { Id = "1", Name = "i3" }, new CPU { Id = "2", Name = "i5" }, new CPU { Id = "3", Name = "i7" } },
                Rams = new List<RAM> { new RAM { Id = "1", Name = "4GB" }, new RAM { Id = "2", Name = "8GB" }, new RAM { Id = "3", Name = "16GB" } },
                Sizes = new List<Size> { new Size { Id = "1", Name = "14"}, new Size { Id = "2", Name = "15.6" }, new Size { Id = "3", Name = "17" } },
                Products = pager,
                Filter = param,
                defaultMax = defaultProducts.Count > 0 ? defaultProducts.Max(x => x.Price) : 9999999,
                defaultMin = defaultProducts.Count > 0 ? (defaultProducts.Min(x => x.Price) == defaultProducts.Max(x => x.Price) ? 0 : defaultProducts.Min(x => x.Price)) : 0,
            });
        }
        public ActionResult Types()
		{
			return View();
		}
		public ActionResult Detail(long id)
		{
            var model = db.Products.FirstOrDefault(x => x.Id == id);
            ViewBag.NewPrice = model.Price - (model.Price * model.discountValue / 100);
			return View(model);
		}

        public ActionResult Search()
        {
            int count = 0;
            string search = Request.Form["search"];
            var model = db.Products.Where(x => x.Name.Contains(search)).ToList();
            foreach(var item in model)
            {
                count++;
            }
            ViewBag.Count = count;
            return View(model);
        }
        [ChildActionOnly]
        public ActionResult RelatedProducts(long id)
        {
            var model = db.Products.Where(x => x.Type.Id == id).ToList();
            foreach(var item in model)
            {
                if(item.discountValue != 0)
                {
                    int newPrice = item.Price - (item.Price * item.discountValue / 100);
                    ViewBag.Price = newPrice;
                }
            }
            return PartialView(model);
        }
	}
}