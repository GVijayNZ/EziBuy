using EziBuy.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EziBuy.Controllers
{
    [Authorize(Roles ="Admin")]
    public class CarouselController : Controller
    {
        [HttpGet]
        public ActionResult DisplayCarouselDetails()
        {
            using (UserDbContext userDbContext = new UserDbContext())
            {
                var carouselDetailsList = userDbContext.HomeCarouselContext.ToList();
                ViewBag.carouselList = carouselDetailsList;
                return View(carouselDetailsList);
            }
        }

        //Create Carousel Image
        [HttpGet]
        public ActionResult CreateImage()
        {
            return PartialView("CreateCarouselImagePartial",new ProductViewModel());
        }

        [HttpPost]
       
        public ActionResult CreateImage(ProductViewModel model)
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
                var image = new HomeCarousel
                {
                    Caption = model.Caption,
                    AltText = model.AltText
                    
                    
                };
               
                if (model.ImageUpload != null && model.ImageUpload.ContentLength > 0)
                {
                    var uploadDir = "~/CarouselImages";
                    var imagePath = Path.Combine(Server.MapPath(uploadDir), model.ImageUpload.FileName);
                    var imageUrl = Path.Combine(uploadDir, model.ImageUpload.FileName);
                    model.ImageUpload.SaveAs(imagePath);
                    image.ImageUrl = imageUrl;
                }

                //UserDbContext db = new UserDbContext();
                using(UserDbContext db=new UserDbContext())
                {
                    db.HomeCarouselContext.Add(image);
                    db.SaveChanges();

                }
                return RedirectToAction("DisplayCarouselDetails");
            }

            //return View(model);
            return RedirectToAction("DisplayCarouselDetails");
        }

        [HttpGet]
        public ActionResult EditCarouselDetail(int imageId)
        {
            using (UserDbContext userDbContext = new UserDbContext())
            {
                var carouselInfo = userDbContext.HomeCarouselContext.Find(imageId);
                if (carouselInfo == null)
                {
                    return new HttpNotFoundResult();
                }
                ViewBag.imageId = imageId;
                var imageViewModel = new ProductViewModel
                {
                    
                    ImageUrl = carouselInfo.ImageUrl,
                    AltText = carouselInfo.AltText,
                    Caption = carouselInfo.Caption,
                    
                };
                return PartialView("EditCarouselInformationPartial", imageViewModel);
            }
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult EditCarouselDetail(int imageId, ProductViewModel imageViewModel)
        {
            if (!ModelState.IsValid)
            {
                UserDbContext userDbContext = new UserDbContext();
                var carouselDetail = userDbContext.HomeCarouselContext.Find(imageId);
                if (carouselDetail == null)
                {
                    return new HttpNotFoundResult();
                }
                carouselDetail.Id= imageId;

                carouselDetail.AltText = imageViewModel.AltText;
                carouselDetail.Caption = imageViewModel.Caption;
                
                userDbContext.SaveChanges();
                return RedirectToAction("DisplayCarouselDetails");
            }
            return RedirectToAction("DisplayCarouselDetails");
        }


        // Editi Carousel Image
        [HttpGet]
        public ActionResult EditCarouselImage(int imageId)
        {
            using (UserDbContext userDbContext = new UserDbContext())
            {
                var carouselInfo = userDbContext.HomeCarouselContext.Find(imageId);
                if (carouselInfo == null)
                {
                    return new HttpNotFoundResult();
                }
                ViewBag.imageId = imageId;
                var imageViewModel = new ProductViewModel
                {
                    ImageUrl = carouselInfo.ImageUrl,
                };
                return PartialView("EditCarouselImagePartial", imageViewModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCarouselImage(int imageId, ProductViewModel imageViewModel)
        {
            var validImageTypes = new string[]
            {
                 "image/gif",
                 "image/jpeg",
                 "image/jpg",
                 "image/png",
                 "image/bmp"
            };
            if (imageViewModel.ImageUpload != null || imageViewModel.ImageUpload.ContentLength > 0 && !validImageTypes.Contains(imageViewModel.ImageUpload.ContentType))
            {
                ModelState.AddModelError("ImageUpload", "Please choose either a GIF, JPG or PNG image.");
            }
            if (!ModelState.IsValid)
            {
                UserDbContext userDbContext = new UserDbContext();
                var carouselDetail = userDbContext.HomeCarouselContext.Find(imageId);
                if (carouselDetail == null)
                {
                    return new HttpNotFoundResult();
                }
                carouselDetail.Id = imageId;

                if (imageViewModel.ImageUpload != null && imageViewModel.ImageUpload.ContentLength > 0)
                {
                    var uploadDir = "~/CarouselImages";
                    var imagePath = Path.Combine(Server.MapPath(uploadDir), imageViewModel.ImageUpload.FileName);
                    var imageUrl = Path.Combine(uploadDir, imageViewModel.ImageUpload.FileName);
                    imageViewModel.ImageUpload.SaveAs(imagePath);
                    carouselDetail.ImageUrl = imageUrl;
                }
                else
                {
                    carouselDetail.ImageUrl = imageViewModel.ImageUrl;

                }
                userDbContext.SaveChanges();
                return RedirectToAction("DisplayCarouselDetails");
            }
            return RedirectToAction("DisplayCarouselDetails");
        }



        //Delete product
        public ActionResult DeleteCarousel(int id)
        {
            using (UserDbContext userDbContext = new UserDbContext())
            {
                HomeCarousel carouselDetails = userDbContext.HomeCarouselContext.Where(x => x.Id == id).SingleOrDefault();
                //bool result = false;
                if (carouselDetails != null)
                {
                    userDbContext.HomeCarouselContext.Remove(carouselDetails);
                    userDbContext.SaveChanges();
                }
                return View("DisplayCarouselDetails");
            }

        }
    }
}