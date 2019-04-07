using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EziBuy.Models
{
    public class UserDbContext:DbContext
    {
        //to access User table in DB
        public DbSet<User> UserContext { get; set; }

        //to access UserRole table in DB
        public DbSet<UserRole> UserRoleContext { get; set; }

        public DbSet<ProductDetails> ProductDetailsContext { get; set; }

        public DbSet<ProductCategory> ProductCategorieContext { get; set; }

        public DbSet<HomeCarousel> HomeCarouselContext { get; set; }

        public DbSet<ProductImage> ProductImageContext { get; set; }

    }
}