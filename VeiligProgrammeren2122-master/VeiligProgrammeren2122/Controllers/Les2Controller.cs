using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace VeiligProgrammeren2122.Controllers
{
    public class Les2Controller : Controller
    {
        public IActionResult Index()
        {
            
            string secret = StaticMethods.ShiftCypher("abc123", 2);
            return View();
        }

        public ActionResult Info()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Info(string name , int age)
        {
            long? contentLength = Request.ContentLength;
            if(contentLength > 1000)
            {
                ViewBag.Error = "Too many characters";
            }
            return View();
        }

        public ActionResult Login()
        {
            if(Request.Cookies["vp_username"] != null)
            {
                ViewBag.UserName = Request.Cookies["vp_username"];
            }
            return View();
        }

        public ActionResult UpdatePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UpdatePassword(string username , string oldpassword , 
            string newpassword , string confirmpassword)
        {
            ViewBag.Result = StaticMethods.ChangePassword(username, oldpassword, newpassword, confirmpassword);
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username , string password)
        {
            if(StaticMethods.CheckLogin(username , password))
            {
                return RedirectToAction("Privacy", "Home");
            }
            ViewBag.Error = "Login failed";
            return View();
        }
    }
}
