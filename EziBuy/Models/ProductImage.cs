using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EziBuy.Models
{
    public class ProductImage
    {
        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Image Required")]
        [Display(Name = "Image URL")]
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Product Required")]
        public int ProductId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Product Name")]
        [Display(Name ="Product Name")]
        public string ProductName { get; set; }
    }
}