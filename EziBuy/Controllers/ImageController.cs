using EziBuy.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EziBuy.Controllers
{
    public class ImageController : Controller
    {
        [HttpGet]
        public ActionResult DisplayProductImages()
        {
            using (UserDbContext userDbContext = new UserDbContext())
            {
                var productImagesList = userDbContext.ProductImageContext.ToList();
                ViewBag.ImageList = productImagesList;
                return View(productImagesList);
            }
        }

        [HttpGet]
        public ActionResult AddProductImages()
        {
            UserDbContext db = new UserDbContext();
            ViewBag.ProductDetails = db.ProductDetailsContext.ToList();
            return PartialView("AddProductImagesPartial", new ProductViewModel());
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult AddProductImages(ProductViewModel productViewModel)
        {
            UserDbContext db = new UserDbContext();
            var validImageTypes = new string[]
                {
                 "image/gif",
                 "image/jpeg",
                 "image/bmp",
                 "image/png",
                 "image/jpg"
                };
            if (productViewModel.ImageUpload == null || productViewModel.ImageUpload.ContentLength == 0)
            {
                ModelState.AddModelError("ImageUpload", "This field is required");
            }
            else if (!validImageTypes.Contains(productViewModel.ImageUpload.ContentType))
            {
                ModelState.AddModelError("ImageUpload", "Please choose either a GIF, JPG, JPEG, BMP or PNG image.");
            }
            var productDetail = db.ProductDetailsContext.Where(x => x.Id == productViewModel.ProductId).SingleOrDefault();
            

            if (!ModelState.IsValid)
            {
                ProductImage productImages = new ProductImage                  
                {
                 
                    ProductId = productViewModel.ProductId,
                    ProductName=productDetail.ProductName//using product name from database not by user
                };

                if (productViewModel.ImageUpload != null && productViewModel.ImageUpload.ContentLength > 0)
                {
                    var uploadDir = "~/ProductImages/";
                    var imagePath = Path.Combine(Server.MapPath(uploadDir), productViewModel.ImageUpload.FileName);
                    var imageUrl = Path.Combine(uploadDir, productViewModel.ImageUpload.FileName);
                    productViewModel.ImageUpload.SaveAs(imagePath);
                    productImages.ImageUrl = imageUrl;
                }
                
               db.ProductImageContext.Add(productImages);
                db.SaveChanges();
                return RedirectToAction("DisplayProductImages");
            }
            return View("DisplayProductImages");
        }

        public ActionResult DeleteProductImage(int productImageId)
        {
            using (UserDbContext db= new UserDbContext())
            {
                ProductImage productImages = db.ProductImageContext.Where(x => x.Id == productImageId).SingleOrDefault();                //bool result = false;
                if (productImages != null)
                {
                    db.ProductImageContext.Remove(productImages);
                    db.SaveChanges();
                }
                return View("DisplayProductImages");
            }

        }

        //[HttpGet]
        //public ActionResult EditProductDetail(int productId)
        //{
        //    using (UserDbContext userDbContext = new UserDbContext())
        //    {
        //        var productInfo = userDbContext.ProductDetailContext.Find(productId);
        //        if (productInfo == null)
        //        {
        //            return new HttpNotFoundResult();
        //        }
        //        ViewBag.productId = productId;
        //        var productViewModel = new ProductViewModel
        //        {
        //            ProductName = productInfo.ProductName,
        //            ImageUrl = productInfo.ImageUrl,
        //            AltText = productInfo.AltText,
        //            Caption = productInfo.Caption,
        //            ProductDescription = productInfo.ProductDescription,
        //            CategoryId = productInfo.CategoryId,
        //        };
        //        return PartialView("EditProductInformationPartial", productViewModel);
        //    }
        //}

        //[HttpPost]
        ////[ValidateAntiForgeryToken]
        //public ActionResult EditProductDetail(int productId, ProductViewModel productViewModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        UserDbContext userDbContext = new UserDbContext();
        //        var productDetail = userDbContext.ProductDetailContext.Find(productId);
        //        if (productDetail == null)
        //        {
        //            return new HttpNotFoundResult();
        //        }
        //        productDetail.Id = productId;
        //        productDetail.ProductName = productViewModel.ProductName;
        //        productDetail.AltText = productViewModel.AltText;
        //        productDetail.Caption = productViewModel.Caption;
        //        productDetail.ProductDescription = productViewModel.ProductDescription;
        //        productDetail.CategoryId = productViewModel.CategoryId;
        //        userDbContext.SaveChanges();
        //        return RedirectToAction("DisplayProductList");
        //    }
        //    return RedirectToAction("DisplayProductList");
        //}

        
        [HttpGet]
        public ActionResult EditProductImage(int productImageId)
        {
            using (UserDbContext db = new UserDbContext())
            {
                var productImage = db.ProductImageContext.Find(productImageId);
                if (productImage == null)
                {
                    return new HttpNotFoundResult();
                }
                ViewBag.ImageId = productImageId;
                var productViewModel = new ProductViewModel
                {
                    ImageUrl = productImage.ImageUrl,
                };
                return PartialView("EditProductImagePartial", productViewModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProductImage(int productImageId, ProductViewModel productViewModel)
        {
            var validImageTypes = new string[]
            {
                 "image/gif",
                 "image/jpeg",
                 "image/jpg",
                 "image/png",
                 "image/bmp"
            };
            if (productViewModel.ImageUpload != null || productViewModel.ImageUpload.ContentLength > 0 && !validImageTypes.Contains(productViewModel.ImageUpload.ContentType))
            {
                ModelState.AddModelError("ImageUpload", "Please choose either a GIF, JPG or PNG image.");
            }
            if (!ModelState.IsValid)
            {
                UserDbContext db = new UserDbContext();
                var productImage = db.ProductImageContext.Find(productImageId);
                if (productImage == null)
                {
                    return new HttpNotFoundResult();
                }
                productImage.Id = productImageId;

                if (productViewModel.ImageUpload != null && productViewModel.ImageUpload.ContentLength > 0)
                {
                    var uploadDir = "~/ProductImages";
                    var imagePath = Path.Combine(Server.MapPath(uploadDir), productViewModel.ImageUpload.FileName);
                    var imageUrl = Path.Combine(uploadDir, productViewModel.ImageUpload.FileName);
                    productViewModel.ImageUpload.SaveAs(imagePath);
                    productImage.ImageUrl = imageUrl;
                }
                else
                {
                    productImage.ImageUrl = productViewModel.ImageUrl;

                }
                db.SaveChanges();
                return RedirectToAction("DisplayProductImages");
            }
            return RedirectToAction("DisplayProductImages");
        }

    }
}