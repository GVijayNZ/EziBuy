using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EziBuy.Models;

namespace EziBuy.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        //Display list of categories
        [HttpGet]
        public ActionResult DisplayProductCategory()
        {
            using (UserDbContext userDbContext = new UserDbContext())
            {
                var productCategoryList = userDbContext.ProductCategorieContext.ToList();
                ViewBag.categoryList = productCategoryList;
                return View(productCategoryList);
            }
        }

        //post method used for add/edit category
        [HttpPost]
        public ActionResult DisplayProductCategory(ProductCategory productCategory)
        {
            try
            {
                using (UserDbContext userDbContext = new UserDbContext())
                {
                    if (productCategory.CategoryId > 0)
                    {
                        ProductCategory categoryDetail = userDbContext.ProductCategorieContext.Where(x => x.CategoryId == productCategory.CategoryId).FirstOrDefault();
                        categoryDetail.CategoryName = productCategory.CategoryName;
                        userDbContext.SaveChanges();
                    }
                    else
                    {
                        userDbContext.ProductCategorieContext.Add(productCategory);
                        userDbContext.SaveChanges();

                    }

                    return RedirectToAction("DisplayProductCategory");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //edit category
        public ActionResult EditProductCategory(int CategoryId)
        {
            using (UserDbContext userDbContext = new UserDbContext())
            {
                ProductCategory productCategory = new ProductCategory();
                if (CategoryId > 0)
                {
                    var categoryInfo = userDbContext.ProductCategorieContext.Where(x => x.CategoryId == CategoryId).FirstOrDefault();
                    productCategory.CategoryId = categoryInfo.CategoryId;
                    productCategory.CategoryName = categoryInfo.CategoryName;
                }
                return PartialView("CategoryPartial", productCategory);//partial view is displayed in modal body
            }

        }

        //delete category
        public ActionResult DeleteCategory(int CategoryId)
        {
            using (UserDbContext userDbContext = new UserDbContext())
            {
                ProductCategory productCategory = userDbContext.ProductCategorieContext.Where(x => x.CategoryId == CategoryId).SingleOrDefault();
                //bool result = false;
                if (productCategory != null)
                {
                    userDbContext.ProductCategorieContext.Remove(productCategory);
                    userDbContext.SaveChanges();
                }
                return View("DisplayProductCategory");
            }

        }

        //display product list in each category
        public ActionResult CategoryProductsList(int categoryId)
        {
            using (UserDbContext db = new UserDbContext())
            {
                List<ProductDetails> productList = db.ProductDetailsContext.Where(x => x.CategoryId == categoryId).ToList();
                //to display category name in model, need to pass it to partial view
                var categoryName = (from category in db.ProductCategorieContext
                                   where category.CategoryId == categoryId
                                   select category.CategoryName).SingleOrDefault();
                ViewBag.productCategoryName = categoryName;
                return View(productList);
                
                //return PartialView("CategoryProductsListPartial", productList);
            }

        }

    }
}