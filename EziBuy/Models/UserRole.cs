using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EziBuy.Models
{
    public class UserRole
    {
        public int Id { get; set; }

        [Required]
        public string RoleName { get; set; }

        //as foreign key
        List<User> user { get; set; }
    }
}