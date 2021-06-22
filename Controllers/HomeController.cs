        using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cooler.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        // GET: Home
        [HttpGet]
        public ActionResult User()
        {
            return View();
        }
        
        [Authorize(Roles = "Admin,Super")]
        
        public ActionResult Admin()
        {
            return View();
        }
    }
}