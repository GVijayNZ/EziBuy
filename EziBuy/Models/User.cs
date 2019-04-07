using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EziBuy.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "First Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "First Name is required")]
        [RegularExpression(@"^[a-zA-Z]+[a-zA-Z]*$", ErrorMessage = "Only Alphabets are allowed.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Last Name is required")]
        [RegularExpression(@"^[a-zA-Z]+[a-zA-Z]*$", ErrorMessage = "Only Alphabets are allowed.")]
        public string LastName { get; set; }

        [Display(Name = "Email Id")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide Email Id")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Please provide valid Email Address")]
        public string EmailId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select one option")]
        public string Gender { get; set; }

        [Display(Name = "Date of Birth")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter Password")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Please enter atleast 6 characters")]
        public string Password { get; set; }

        //foreign key RoleId
        public int RoleId { get; set; }
    }
}