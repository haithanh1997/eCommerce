using eCommerce.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCommerce.Areas.Admin.Models
{
    public class ProductTypeModel
    {
        [Key]
        [DisplayName("Mã loại")]
        public long Id { get; set; }

        [Required]
        [DisplayName("Tên loại")]
        public string Name { get; set; }
        
        public List<SelectListItem> Categories { get; set; }


        [DisplayName("Không hiển thị")]
        public bool isDisabled { get; set; }
    }
}