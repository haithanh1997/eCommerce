using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eCommerce.EntityFramework
{
    public class Category
    {
        [Key]
        [DisplayName("Mã danh mục")]
        public long Id { get; set; }

        [Required(ErrorMessage = "Trường {0} bắt buộc nhập.")]
        [DisplayName("Tên danh mục")]
        public string Name { get; set; }

        [DisplayName("Không hiển thị")]
        public bool isDisabled { get; set; }
    }
}