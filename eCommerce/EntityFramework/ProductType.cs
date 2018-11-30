using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eCommerce.EntityFramework
{
    public class ProductType
    {
        [Key]
        [DisplayName("Mã loại")]
        public long Id { get; set; }

        [Required]
        [DisplayName("Tên loại")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Thuộc mã danh mục")]
        public virtual Category Category { get; set; }

        [DisplayName("Không hiển thị")]
        public bool isDisabled { get; set; }
    }
}