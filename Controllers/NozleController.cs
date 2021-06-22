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
    public class NozleController : Controller
    {
        DataContext db = new DataContext();
        // GET: Nozle
        public ActionResult Index()
        {
            var data = db.Nozle_Types.SqlQuery("select * from Nozle_Types").ToList();
            return View(data);
        }


        // GET: Nozle/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Nozle/Create
        [HttpPost]
        public ActionResult Create(Nozle_Types collection)
        {
            try
            {
                List<object> lst = new List<object>();
                lst.Add(collection.Nozle_ID);
                lst.Add(collection.Nozle_Type);
                lst.Add(collection.Nozle_Description);
                lst.Add(collection.Nozle_no_of_Spare_Parts);
                object[] allitems = lst.ToArray();
                int output = db.Database.ExecuteSqlCommand("insert into Nozle_Types(Nozle_ID,Nozle_Type,Nozle_Description,Nozle_no_of_Spare_Parts) values(@p0,@p1,@p2,@p3)", allitems);
                if (output > 0)
                {
                    ViewBag.msg = "Bag is added";

                }
                // return View();
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.msg = "Something is Wrong";
                return View();
            }
        }

        // GET: Nozle/Edit/5
        public ActionResult Edit(int id)
        {
            var data = db.Nozle_Types.SqlQuery("select * from Nozle_Types where Nozle_ID=@p0", id).SingleOrDefault();
            return View(data);
        }

        // POST: Nozle/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Nozle_Types collection)
        {
            try
            {
                List<object> list = new List<object>();
                list.Add(collection.Nozle_Type);
                list.Add(collection.Nozle_Description);
                list.Add(collection.Nozle_no_of_Spare_Parts);
                list.Add(id);
                object[] objectarray = list.ToArray();

                int output = db.Database.ExecuteSqlCommand("update Nozle_Types set Nozle_Type=@p0,Nozle_Description=@p1,Nozle_no_of_Spare_Parts=@p2 where Nozle_ID=@p3", objectarray);
                if (output > 0)
                {
                    ViewBag.Usermsg = "bag ID " + collection.Nozle_ID + " is updated!";
                }
                //return View();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Nozle/Delete/5
        public ActionResult Delete(int id)
        {
            var data = db.Nozle_Types.SqlQuery("select * from Nozle_Types where Nozle_ID=@p0", id).SingleOrDefault();
            return View(data);
        }

        // POST: Nozle/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var userlist = db.Database.ExecuteSqlCommand("delete from Nozle_Types where Nozle_ID=@p0", id);
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
