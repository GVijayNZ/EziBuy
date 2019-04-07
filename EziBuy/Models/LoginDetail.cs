using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EziBuy.Models
{
    public class LoginDetail
    {
        [Key]
        [Display(Name = "Email Id")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide Email Id")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Please provide valid Email Address")]
        public string EmailId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter Password")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Please enter atleast 6 characters")]
        public string Password { get; set; }

        [Display(Name ="Remember Me")]
        public bool RememberMe { get; set; }

    }
}