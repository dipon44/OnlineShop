using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Models.Product
{
    public class ProductTag
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Product Tag")]
        public string ProductTagName { get; set; }
    }
}
