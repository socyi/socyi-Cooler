using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cooler.Models;

namespace Cooler.Controllers
{
    [Authorize(Roles = "Admin,Super")]
    public class ManufacturerController : Controller
    {
        DataContext db = new DataContext();
        // GET: Manufacturer
        public ActionResult Index()
        {
            var data = db.Manufacturers.SqlQuery("select * from Manufacturers").ToList();
            return View(data);
        }

       

        // GET: Manufacturer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Manufacturer/Create
        [HttpPost]
        public ActionResult Create(Manufacturer collection)
        {
            try
            {
     
                List<object> lst = new List<object>();
                lst.Add(collection.Manufacturer_Name);
         
                object[] allitems = lst.ToArray();

                int output = db.Database.ExecuteSqlCommand("insert into Manufacturers" +
                    "(Manufacturer_Name) values(@p0)", allitems);
                if (output > 0)
                {
                    ViewBag.msg = "Manufacturer added!";


                }
                return View();

            }
            catch
            {
                ViewBag.msg = "Something went wrong, Manufacturer not added.";
                return View();
            }
        }

        // POST: Manufacturer/Edit/5
 
        public ActionResult Edit(int id)
        {
            var data = db.Manufacturers.SqlQuery("select * from Manufacturers where Manufacturer_Code=@p0", id).SingleOrDefault();
            return View(data);
        }

        // POST: Data/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Manufacturer collection)
        {
            try
            {
                List<object> list = new List<object>();
                list.Add(collection.Manufacturer_Name);
                list.Add(id);
                
                object[] objectarray = list.ToArray();

                int output = db.Database.ExecuteSqlCommand("update Manufacturers set Manufacturer_Name=@p0 where Manufacturer_Code=@p1", objectarray);
                if (output > 0)
                {
                    ViewBag.msg = "Manufacturer with Code " + collection.Manufacturer_Code + " updated!";
                }
                return View();
                //return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        // GET: Data/Delete/5
        public ActionResult Delete(int id)
        {
            var data = db.Manufacturers.SqlQuery("select * from Manufacturers where Manufacturer_Code=@p0", id).SingleOrDefault();
            return View(data);
        }

        // POST: Data/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Manufacturer collection)
        {
            try
            {
                var list = db.Database.ExecuteSqlCommand("delete from Manufacturers where Manufacturer_Code=@p0", id);
                if (list != 0)
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
