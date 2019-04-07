using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EziBuy.Models
{
    public class ProductCategory
    {
        //public ProductCategory(){
        //    this.ProductDetails = new HashSet<ProductDetails>();
        //    }
        [Key]
        public int CategoryId { get; set; }

        [Required(AllowEmptyStrings =false, ErrorMessage ="Category Name required")]
        [Display(Name ="Category Name" )]
        public string CategoryName { get; set; }

        List<ProductDetails> ProductDetails { get; set; }
        //public virtual ICollection<ProductDetails> ProductDetails { get; set; }
    }
}