using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EziBuy.Models
{
    public class ProductDetails
    {
        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings =false, ErrorMessage ="Product Name Required")]
        [Display(Name ="Product Name")]
        public string ProductName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Image Required")]
        [Display(Name = "Image URL")]
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }

        public string AltText { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Caption Required")]
        [DataType(DataType.Html)]
        public string Caption { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Price Required")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Product Description Required")]
        [Display(Name = "Product Description")]
        public string ProductDescription { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Category Id Required")]
        public int CategoryId { get; set; }

        
        //using productid as foreign key in productimage
        List<ProductImage> ProductImages { get; set; }


    }
}