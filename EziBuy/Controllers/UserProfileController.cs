using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EziBuy.Models;

namespace EziBuy.Controllers
{
    [Authorize(Roles ="User , Admin")]
    public class UserProfileController : Controller
    {
        // GET: UserProfile
        public ActionResult UserInformation()
        {
            using (UserDbContext userDb = new UserDbContext())
            {
                var emailId = Session["EmailId"].ToString();
                var userInfo = userDb.UserContext.Where(x => x.EmailId == emailId).FirstOrDefault();
                return View(userInfo);
            }

        }


        public ActionResult EditUserInformation(User user)
        {
            using (UserDbContext userDb = new UserDbContext())
            {
                var userInfo = userDb.UserContext.Where(x => x.Id == user.Id).FirstOrDefault();

                if (userInfo != null)
                {
                    userInfo.FirstName = user.FirstName;
                    userInfo.LastName = user.LastName;
                    userInfo.EmailId = user.EmailId;
                    userInfo.Gender = user.Gender;
                    userInfo.DateOfBirth = user.DateOfBirth;
                    userInfo.Password = user.Password;
                    userDb.SaveChanges();
                }

                return RedirectToAction("UserInformation");
            }

        }

    }
}