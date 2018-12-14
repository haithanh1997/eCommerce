using eCommerce.EntityFramework;
using System.Collections.Generic;

namespace eCommerce.Models
{
    public class ProductIndexModel
    {
        public List<Product> Products { get; set; }

        public List<Category> Categories { get; set; }

        public List<ProductType> Types { get; set; }

        public List<Drive> Drives { get; set; }
        public List<CPU> CPUs { get; set; }
        public List<RAM> Rams { get; set; }
        public List<Size> Sizes { get; set; }
        public ProductFilterParam Filter { get; set; }

        public int defaultMin { get; set; }

        public int defaultMax { get; set; }
    }

    public class ProductFilterParam
    {
        public string name { get; set; }
        public List<long> type { get; set; }
        public decimal min { get; set; }

        public decimal max { get; set; }
        public List<string> drive { get; set; }
        public List<string> cpu { get; set; }
        public List<string> ram { get;set; }
        public List<string> size { get; set; }
    }

    public class Drive
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class CPU
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class RAM
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class Size
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}