using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using EziBuy.Models;

namespace EziBuy.Controllers
{
    //[Authorize (Roles ="User")]
    public class RegistrationAndLoginController : Controller
    {

        public UserDbContext userDbContext = new UserDbContext();

        // GET: RegistrationAndLogin
        //Home page-EziBuy
        public ActionResult Index()
        {
            return View(userDbContext.UserContext.ToList());

        }

        //GET:Registration
        public ActionResult Registration()
        {
            return View();
        }

        //POST:Registration
        [HttpPost]
        public ActionResult Registration(User user)
        {
            if (ModelState.IsValid)
            {
                var isEmailExist = userDbContext.UserContext.Where(x => x.EmailId == user.EmailId).FirstOrDefault();
                if(isEmailExist==null)
                {
                    user.RoleId = 2;
                    userDbContext.UserContext.Add(user);
                    userDbContext.SaveChanges();
                    ModelState.Clear();
                    ViewBag.Message = user.FirstName + " " + user.LastName + " Successfully Registered";
                }
                else
                {
                    //ModelState.Clear();
                    ViewBag.Message = "EmailId already exist";
                }
            }
            return View();
        }

        //GET:Login
        public ActionResult Login()
        {
            LoginDetail loginDetail = new LoginDetail();
            if(Request.Cookies["LoginCookie"]!=null)
            {
                //if cookie is not null, accessing cookie from server and display on email field 
                loginDetail.EmailId = Request.Cookies["LoginCookie"].Values["EmailId"];
            }
            return View(loginDetail);
        }

        //POST:Login
        [HttpPost]
        public ActionResult Login(LoginDetail login)
        {

            using (UserDbContext userDb = new UserDbContext())
            {
                bool isValid = userDb.UserContext.Any(x => x.EmailId == login.EmailId && x.Password == login.Password);
                if(isValid)
                {
                    //use Authentication form
                    FormsAuthentication.SetAuthCookie(login.EmailId, false);
                    Session["EmailId"] = login.EmailId.ToString();
                    //store cookies if clicked remember me
                    if(login.RememberMe)
                    {
                        HttpCookie cookie = new HttpCookie("LoginCookie");
                        cookie.Values["EmailId"] = login.EmailId.ToString();
                        //cookie.Values["lastLoginDate"] = DateTime.Now.ToShortDateString();

                        cookie.Expires = DateTime.Now.AddMinutes(30);
                        Response.Cookies.Add(cookie);
                        return RedirectToAction("MainPage");
                    }
                    else
                    {
                        return RedirectToAction("MainPage");
                    }

                }
                ModelState.AddModelError("", "Invalid logins");
                return View();
            }
        }

        //GET:MainPage
        //Page where user directed to after logged in
        public ActionResult MainPage()
        {
            if (Session["EmailId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        //GET:LogOut
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Login");
        }

        
        public ActionResult About()
        {
            return View();
        }
    }
}