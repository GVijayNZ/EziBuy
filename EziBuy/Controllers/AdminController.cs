using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EziBuy.Models;

namespace EziBuy.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }
        //User Details

        [HttpGet]
        public ActionResult UsersDetail()
        {
            using (UserDbContext userDbContext = new UserDbContext())
            {
                var userList = userDbContext.UserContext.ToList();
                ViewBag.userDetailList = userList;
                return View(userList);
            }
        }

        [HttpPost]
        public ActionResult UsersDetail(User user)
        {
            try
            {
                using (UserDbContext userDbContext = new UserDbContext())
                {
                    if (user.Id > 0)
                    {
                        User userInformation = userDbContext.UserContext.Where(x => x.Id == user.Id).FirstOrDefault();
                        userInformation.FirstName = user.FirstName;
                        userInformation.LastName = user.LastName;
                        userInformation.EmailId = user.EmailId;
                        userInformation.Gender = user.Gender;
                        userInformation.DateOfBirth = user.DateOfBirth;
                        userInformation.Password = user.Password;
                        userDbContext.SaveChanges();
                    }
                    else
                    {
                        var isEmailExist = userDbContext.UserContext.Where(x => x.EmailId == user.EmailId).SingleOrDefault();
                        if (isEmailExist == null)
                        {
                            user.RoleId = 2;
                            userDbContext.UserContext.Add(user);
                            userDbContext.SaveChanges();
                        }
                        else
                        {

                            ViewBag.Message = "Email-Id already exist.";
                        }
                    }

                    return RedirectToAction("UsersDetail");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public ActionResult EditUserInformation(int UserId)
        {
            using (UserDbContext userDbContext = new UserDbContext())
            {
                User user = new User();
                if (UserId > 0)
                {
                    var userInfo = userDbContext.UserContext.Where(x => x.Id == UserId).FirstOrDefault();
                    user.Id = userInfo.Id;
                    user.FirstName = userInfo.FirstName;
                    user.LastName = userInfo.LastName;
                    user.EmailId = userInfo.EmailId;
                    user.Gender = userInfo.Gender;
                    user.DateOfBirth = userInfo.DateOfBirth;
                    user.Password = userInfo.Password;
                }
                return PartialView("EditUserInformationPartial", user);
            }

        }

        public ActionResult DeleteUser(int userId)
        {
            using (UserDbContext userDbContext = new UserDbContext())
            {
                User user = userDbContext.UserContext.Where(x => x.Id == userId).SingleOrDefault();                //bool result = false;
                if (user != null)
                {
                    userDbContext.UserContext.Remove(user);
                    userDbContext.SaveChanges();
                }
                return View("UsersDetail");
            }

        }

        
       
    }
}