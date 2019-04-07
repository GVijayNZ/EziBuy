using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EziBuy.Models;

namespace EziBuy.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        //This method is used to display the left category bar. 
        //This creates a partial vie which is saved in shared folder 
        //so that all method view can access it using render method as indexer Index

        public ActionResult CategoryNavBarList()
        {
            using(UserDbContext db=new UserDbContext())
            {
                var categoryBarList = db.ProductCategorieContext.ToList();
                return PartialView("~/Views/Shared/CategoryNavBarList.cshtml", categoryBarList);
            }
        }

        public ActionResult Carousel()
        {
            using (UserDbContext db = new UserDbContext())
            {
                var carouselList = db.HomeCarouselContext.ToList();
                return PartialView("~/Views/Shared/CarouselView.cshtml", carouselList);
            }

        }

        //This method is used to display the product list when click on each category. 
        
        public ActionResult BrowseCategoryProduct( int categoryId)
        {
            using (UserDbContext db = new UserDbContext())
            {
                var categoryProducts = db.ProductDetailsContext.Where(x => x.CategoryId == categoryId).ToList();
                return View(categoryProducts);
            }
   
        }

        public ActionResult ProductDescription(int productId)
        {
            using (UserDbContext db = new UserDbContext())
            {
                var productImages = db.ProductImageContext.Where(x => x.ProductId == productId).ToList();
                var productdefaultImage = (from productImage in db.ProductImageContext
                                           where productImage.ProductId == productId
                                           select productImage.ImageUrl).FirstOrDefault();
                ViewBag.defaultImage = productdefaultImage;
                TempData["ProductId"] = productId;
                return View(productImages);
            }
               
        }

        public ActionResult Details()
        {
            using (UserDbContext db = new UserDbContext())
            {
                var productId = Convert.ToUInt32(TempData["ProductId"]);
                var productDetail = db.ProductDetailsContext.Where(x => x.Id == productId).SingleOrDefault();

                return PartialView("DetailsPartial", productDetail);
            }
                
        }
        
       
        
    }
}