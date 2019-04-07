using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EziBuy.Models
{
    public class ProductViewModel
    {
        [Key]
        public int ProductViewId{ get; set; }//productviewId

        [Required(AllowEmptyStrings = false, ErrorMessage = "Product Name Required")]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Image Required")]
        [Display(Name = "Image URL")]
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Image Required")]
        [Display(Name = "Product Image")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase ImageUpload { get; set; }

        public string AltText { get; set; }

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

        [Required(AllowEmptyStrings = false, ErrorMessage = "Product Id Required")]
        public int ProductId { get; set; }

        //public IEnumerable<SelectListItem> CategoryList { get; set; }
       

    }
}