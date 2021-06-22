using System;
using System.Collections.Generic;
using System.Linq;
using Cooler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Cooler.Controllers
{
    [Authorize(Roles = "Admin,Super")]
    public class FiberController : Controller
    {
        DataContext db = new DataContext();
        // GET: Fiber
        public ActionResult Index()
        {
            var data = db.Fiber_Brand.SqlQuery("select * from Fiber_Brand").ToList();
            return View(data);
        }

       

        // GET: Fiber/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Fiber/Create
        [HttpPost]
        public ActionResult Create(Fiber_Brand collection)
        {
            try
            {
                List<object> lst = new List<object>();
                lst.Add(collection.Fiber_Supplier);
                object[] allitems = lst.ToArray();
                int output = db.Database.ExecuteSqlCommand("insert into Fiber_Brand(Fiber_Supplier) values(@p0)", allitems);
                if (output > 0)
                {
                    ViewBag.msg = "Bag is added";

                }
                // return View();
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Fiber/Edit/5
        public ActionResult Edit(int id)
        {
            var data = db.Fiber_Brand.SqlQuery("select * from Fiber_Brand where Fiber_Code=@p0", id).SingleOrDefault();
            return View(data);
        }

        // POST: Fiber/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Fiber_Brand collection)
        {
            try
            {
                List<object> list = new List<object>();
                list.Add(collection.Fiber_Supplier);
                list.Add(id);
                object[] objectarray = list.ToArray();

                int output = db.Database.ExecuteSqlCommand("update Fiber_Brand set Fiber_Supplier=@p0 where Fiber_Code=@p1", objectarray);
                if (output > 0)
                {
                    ViewBag.Usermsg = "bag ID " + collection.Fiber_Code + " is updated!";
                }
                //return View();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Fiber/Delete/5
        public ActionResult Delete(int id)
        {
            var data = db.Fiber_Brand.SqlQuery("select * from Fiber_Brand where Fiber_Code=@p0", id).SingleOrDefault();
            return View(data);
        }

        // POST: Fiber/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Fiber_Brand collection)
        {
            try
            {
                var userlist = db.Database.ExecuteSqlCommand("delete from Fiber_Brand where Fiber_Code=@p0", id);
                if (userlist != 0)
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
