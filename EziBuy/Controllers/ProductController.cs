using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EziBuy.Models;

namespace EziBuy.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {

        //Products Details

        [HttpGet]
        public ActionResult DisplayProductDetails()
        {
            using (UserDbContext userDbContext = new UserDbContext())
            {
                var productDetailsList = userDbContext.ProductDetailsContext.ToList();
                ViewBag.productList = productDetailsList;
                return View(productDetailsList);
            }
        }

        //Crete product
        [HttpGet]
        public ActionResult CreateProduct()
        {
            UserDbContext db = new UserDbContext();
            ViewBag.CategoryDetails = db.ProductCategorieContext.ToList();
            return PartialView("CreateProductPartial", new ProductViewModel());
        }

        [HttpPost]

        public ActionResult CreateProduct(ProductViewModel model)
        {
            var validImageTypes = new string[]
            {
                "image/gif",
                "image/jpeg",
                "image/pjpeg",
                "image/png"
            };

            if (model.ImageUpload == null || model.ImageUpload.ContentLength == 0)
            {
                ModelState.AddModelError("ImageUpload", "This field is required");
            }
            else if (!validImageTypes.Contains(model.ImageUpload.ContentType))
            {
                ModelState.AddModelError("ImageUpload", "Please choose either a GIF, JPG or PNG image.");
            }

            if (!ModelState.IsValid)
            {
                var image = new ProductDetails
                {
                    ProductName = model.ProductName,
                    AltText = model.AltText,
                    Caption = model.Caption,
                    ProductDescription = model.ProductDescription,
                    CategoryId = model.CategoryId
                };
                //ViewBag.CategoryId = new ProductDetails(image, "CategoryId");

                if (model.ImageUpload != null && model.ImageUpload.ContentLength > 0)
                {
                    var uploadDir = "~/ProductImages";
                    var imagePath = Path.Combine(Server.MapPath(uploadDir), model.ImageUpload.FileName);
                    var imageUrl = Path.Combine(uploadDir, model.ImageUpload.FileName);
                    model.ImageUpload.SaveAs(imagePath);
                    image.ImageUrl = imageUrl;
                }

                UserDbContext db = new UserDbContext();
                db.ProductDetailsContext.Add(image);
                db.SaveChanges();
                return RedirectToAction("DisplayProductDetails");
            }

            return RedirectToAction("DisplayProductDetails");
        }

        [HttpGet]
        public ActionResult EditProductInformation(int productId)
        {
            using (UserDbContext userDbContext = new UserDbContext())
            {
                var productInfo = userDbContext.ProductDetailsContext.Find(productId);
                if (productInfo == null)
                {
                    return new HttpNotFoundResult();
                }
                ViewBag.productId = productId;
                var productViewModel = new ProductViewModel
                {
                    ProductName = productInfo.ProductName,
                    ImageUrl = productInfo.ImageUrl,
                    AltText = productInfo.AltText,
                    Caption = productInfo.Caption,
                    ProductDescription = productInfo.ProductDescription,
                    CategoryId = productInfo.CategoryId,
                };
                return PartialView("EditProductInformationPartial", productViewModel);
            }
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult EditProductInformation(int productId, ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid)
            {
                UserDbContext userDbContext = new UserDbContext();
                var productDetail = userDbContext.ProductDetailsContext.Find(productId);
                if (productDetail == null)
                {
                    return new HttpNotFoundResult();
                }
                productDetail.Id = productId;
                productDetail.ProductName = productViewModel.ProductName;
                productDetail.AltText = productViewModel.AltText;
                productDetail.Caption = productViewModel.Caption;
                productDetail.ProductDescription = productViewModel.ProductDescription;
                productDetail.CategoryId = productViewModel.CategoryId;
                userDbContext.SaveChanges();
                return RedirectToAction("DisplayProductDetails");
            }
            return RedirectToAction("DisplayProductDetails");
        }


        [HttpGet]
        public ActionResult EditProductImage(int productId)
        {
            using (UserDbContext userDbContext = new UserDbContext())
            {
                var productInfo = userDbContext.ProductDetailsContext.Find(productId);
                if (productInfo == null)
                {
                    return new HttpNotFoundResult();
                }
                ViewBag.productId = productId;
                var productViewModel = new ProductViewModel
                {
                    ImageUrl = productInfo.ImageUrl,
                };
                return PartialView("EditProductImagePartial", productViewModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProductImage(int productId, ProductViewModel productViewModel)
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
                UserDbContext userDbContext = new UserDbContext();
                var productDetail = userDbContext.ProductDetailsContext.Find(productId);
                if (productDetail == null)
                {
                    return new HttpNotFoundResult();
                }
                productDetail.Id = productId;

                if (productViewModel.ImageUpload != null && productViewModel.ImageUpload.ContentLength > 0)
                {
                    var uploadDir = "~/ProductImages";
                    var imagePath = Path.Combine(Server.MapPath(uploadDir), productViewModel.ImageUpload.FileName);
                    var imageUrl = Path.Combine(uploadDir, productViewModel.ImageUpload.FileName);
                    productViewModel.ImageUpload.SaveAs(imagePath);
                    productDetail.ImageUrl = imageUrl;
                }
                else
                {
                    productDetail.ImageUrl = productViewModel.ImageUrl;

                }
                userDbContext.SaveChanges();
                return RedirectToAction("DisplayProductDetails");
            }
            return RedirectToAction("DisplayProductDetails");
        }
        //Delete product
        public ActionResult DeleteProduct(int id)
        {
            using (UserDbContext userDbContext = new UserDbContext())
            {
                ProductDetails productDetails = userDbContext.ProductDetailsContext.Where(x => x.Id == id).SingleOrDefault();
                //bool result = false;
                if (productDetails != null)
                {
                    userDbContext.ProductDetailsContext.Remove(productDetails);
                    userDbContext.SaveChanges();
                }
                return View("DisplayProductDetails");
            }

        }
    }
}