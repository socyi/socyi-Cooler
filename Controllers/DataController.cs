using Cooler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Cooler.Controllers
{
   [Authorize(Roles="Super")]
    public class DataController : Controller
    {
        // GET: Data

        DataContext db = new DataContext();
        public ActionResult Index()
        {
            var data = db.Users.SqlQuery("select * from Users").ToList();
           
            return View(data);
        }

        // GET: Data/Details/5
        public ActionResult Details(int id)
        {
            var data = db.Users.SqlQuery("select * from Users where UserID=@p0",id).SingleOrDefault();
            return View(data);
            
        }

        // GET: Data/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Data/Create
        [HttpPost]
        public ActionResult Create(User collection)
        {
            try
            {

                List<object> lst = new List<object>();
                lst.Add(collection.Username);
                lst.Add(collection.Password);
                lst.Add(collection.Role);
                object[] allitems = lst.ToArray();
                int output = db.Database.ExecuteSqlCommand("insert into Users(Username,Password,Role) values(@p0,@p1,@p2)", allitems);
                if (output > 0)
                {
                    ViewBag.msg = "User is added";

                }
               // return View();
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.msg = "User " + collection.Username + " is not added!";
                return View();
            }
        }

        // GET: Data/Edit/5
        public ActionResult Edit(int id)
        {
            var data = db.Users.SqlQuery("select * from Users where UserID=@p0", id).SingleOrDefault();
            return View(data);
        }

        // POST: Data/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, User collection)
        {
            try
            {
                List<object> list = new List<object>();
                list.Add(collection.Username);
                list.Add(collection.Password);
               
                list.Add(collection.Role); 
                list.Add(collection.UserID);
                object[] objectarray = list.ToArray();

                int output = db.Database.ExecuteSqlCommand("update Users set Username=@p0,Password=@p1,Role=@p2 where UserID=@p3", objectarray);
                if(output > 0)
                {
                    ViewBag.msg = "User ID " + collection.UserID + " is updated!";
                }
                //return View();
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.msg = "User " + collection.Username + " is not updated!";
                return View();
            }
        }

        // GET: Data/Delete/5
        public ActionResult Delete(int id)
        {
            var data = db.Users.SqlQuery("select * from Users where UserID=@p0", id).SingleOrDefault();
            return View(data);
        }

        // POST: Data/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var userlist = db.Database.ExecuteSqlCommand("delete from Users where UserID=@p0", id);
                if(userlist != 0)
                {
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch
            {
                return View();
            }
        }
    }
}
