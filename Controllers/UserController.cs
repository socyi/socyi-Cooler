using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cooler.Models;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Web.Security;
namespace Cooler.Controllers

{
    
    public class UserController : Controller
    {

        
        [Authorize(Roles = "Super")]
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Login(User model, string returnUrl)
        {
            DataContext db = new DataContext();
            var dataItem = db.Users.Where(x => x.Username == model.Username && x.Password == model.Password).FirstOrDefault();
            if(dataItem != null)
            {
                FormsAuthentication.SetAuthCookie(dataItem.Username, false);
                if(dataItem.Role != "Admin" && dataItem.Role != "Super")
                {
                    Session["Role"] = "User";
                    return Redirect("Home/User");

                }
                else
                {
                    Session["Role"] = "Admin";
                    return Redirect("Home/Admin");
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid user or password");

                return View();
            }

           
        }
       
        
        public ActionResult Logout()
        {
            //Set the cookie to expire
            FormsAuthentication.SignOut();
            return View("Login");
        }
        public ActionResult Error()
        {
           
            
            return View();
        }
    }
}