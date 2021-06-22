using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cooler.Models;
using Compartment = Cooler.Models.Compartment;
namespace Cooler.Controllers
{
    [Authorize(Roles = "Admin,Super")]
    public class CompartmentController : Controller
    {
        DataContext db = new DataContext();
        // GET: Default
        public ActionResult Index()
        {
            var data = db.Compartments.SqlQuery("select * from Compartments").ToList();
            return View(data);
        }


        // GET: Default/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Default/Create
        [HttpPost]
        public ActionResult Create(Compartment collection)
        {
            try
            {
                List<object> lst = new List<object>();
                lst.Add(collection.Compartment_ID);
                lst.Add(collection.No_Of_Beams);
                lst.Add(collection.No_Of_Nozles);
                lst.Add(collection.fans_No);

                object[] allitems = lst.ToArray();

                int output = db.Database.ExecuteSqlCommand("insert into Compartments" +
                    "(Compartment_ID,No_Of_Beams, No_Of_Nozles, fans_No) values(@p0,@p1,@p2,@p3)", allitems);
                if (output > 0)
                {
                    ViewBag.msg = "Compartment added!";


                }
                return View();

            }
            catch
            {
                ViewBag.msg = "Something went wrong, Compartment not added.";
                return View();
            }
        }

        // GET: Default/Edit/5
        public ActionResult Edit(int id)
        {
            var data = db.Compartments.SqlQuery("select * from Compartments where Compartment_ID=@p0", id).SingleOrDefault();
            return View();
        }

        // POST: Default/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Compartment collection)
        {
            try
            {
                List<object> list = new List<object>();
                list.Add(collection.No_Of_Beams);
                list.Add(collection.No_Of_Nozles);
                list.Add(collection.fans_No);
                list.Add(id);

                object[] objectarray = list.ToArray();

                int output = db.Database.ExecuteSqlCommand("update Compartments set No_Of_Beams=@p0, No_Of_Nozles=@p1, fans_No=@p2 where Compartment_ID=@p3", objectarray);
                if (output > 0)
                {
                    ViewBag.msg = "Compartment with ID " + collection.Compartment_ID + " updated!";
                }
                return View();
                //return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Default/Delete/5
        public ActionResult Delete(int id)
        {
            var data = db.Compartments.SqlQuery("select * from Compartments where Compartment_ID=@p0", id).SingleOrDefault();
            return View(data);
        }

        // POST: Default/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Compartment collection)
        {
            {
                try
                {
                    var list = db.Database.ExecuteSqlCommand("delete from Compartments where Compartment_ID=@p0", id);
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
}
