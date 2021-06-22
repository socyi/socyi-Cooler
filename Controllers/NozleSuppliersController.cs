using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cooler.Models;
using System.Web.Security;

namespace Cooler.Controllers
{
    [Authorize(Roles = "Admin,Super")]
    public class NozleSuppliersController : Controller
    {
        DataContext db = new DataContext();
        // GET: Supplier
        public ActionResult Index()
        {
            var data = db.Nozle_Suppliers.SqlQuery("select * from Nozle_Suppliers").ToList();
            return View(data);
        }

        // GET: Supplier/Create
        public ActionResult Create()
        {
            List<Nozle_Colors> colors = db.Nozle_Colors.ToList();
            foreach (Nozle_Colors c in colors.ToList())
            {   
                if (c.Available == false)
                {
                    colors.Remove(c);
                }
            }
            ViewBag.Color = new SelectList(colors, "Color", "Color");



            return View();
        }

        // POST: Supplier/Create
        [HttpPost]
        public ActionResult Create(Nozle_Suppliers collection)
        {
            try
            {
                List<Nozle_Colors> colors = db.Nozle_Colors.ToList();
                foreach (Nozle_Colors c in colors.ToList())
                {
                    if (c.Available == false)
                    {
                        colors.Remove(c);
                    }
                }
                ViewBag.Color = new SelectList(colors, "Color", "Color");

                List<object> lst = new List<object>();
           
                lst.Add(collection.Supplier);
                lst.Add(collection.Color);


                object[] allitems = lst.ToArray();
                int output = db.Database.ExecuteSqlCommand("insert into Nozle_Suppliers(Supplier,Color) values(@p0,@p1)", allitems);
                if (output > 0)
                {
                    ViewBag.msg = "Supplier is added";
                    db.Database.ExecuteSqlCommand("Update Nozle_Colors SET Available=0 WHERE Color=@p0", collection.Color);
                    return View();
                }
                else
                {

                    return View();
                }
            }
            catch(Exception e)
            {
                ViewBag.msg = e.ToString();
                return View();
            }
        }

        // GET: Supplier/Edit/5
        public ActionResult Edit(int id)
        {
            List<Nozle_Colors> colors = db.Nozle_Colors.ToList();
            foreach (Nozle_Colors c in colors.ToList())
            {
                if (c.Available == false)
                {
                    colors.Remove(c);
                }
            }
            ViewBag.Color = new SelectList(colors, "Color", "Color");
            var data = db.Nozle_Suppliers.SqlQuery("select * from Nozle_Suppliers where Supplier_ID=@p0", id).SingleOrDefault();
            return View(data);
        }

        // POST: Supplier/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Nozle_Suppliers collection)
        {
            try
            {
                List<Nozle_Colors> colors = db.Nozle_Colors.ToList();
                foreach (Nozle_Colors c in colors.ToList())
                {
                    if (c.Available == false)
                    {
                        colors.Remove(c);
                    }
                }
                ViewBag.Color = new SelectList(colors, "Color", "Color");

                List<object> list = new List<object>();
                list.Add(collection.Supplier);
                list.Add(collection.Color);
                list.Add(id);


                object[] objectarray = list.ToArray();
                db.Database.ExecuteSqlCommand("Update Nozle_Colors SET Available=1 FROM Colors AS C INNER JOIN Nozle_Suppliers " +
                "AS B ON C.Color=B.Color WHERE B.Supplier_ID=@p0", id);
                int output = db.Database.ExecuteSqlCommand("update Nozle_Suppliers set Supplier=@p0,Color=@p1 where Supplier_ID=@p2", objectarray);
                db.Database.ExecuteSqlCommand("Update Nozle_Colors SET Available=0 WHERE Color=@p0", collection.Color);

                if (output > 0)
                {
                    ViewBag.Usermsg = "Supplier ID " + collection.Supplier_ID + " is updated!";
                }
                //return View();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Supplier/Delete/5
        public ActionResult Delete(int id)
        {
            var colorlist = db.Nozle_Colors.ToList();
            ViewBag.Color = new SelectList(colorlist, "Color", "Color");
            var data = db.Nozle_Suppliers.SqlQuery("select * from Nozle_Suppliers where Supplier_ID=@p0", id).SingleOrDefault();
            return View(data);
        }

        // POST: Supplier/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                db.Database.ExecuteSqlCommand("Update Nozle_Colors SET Available=1 FROM Colors AS C INNER JOIN Nozle_Suppliers " +
                  "AS B ON C.Color=B.Color WHERE B.Supplier_ID=@p0", id);
                var userlist = db.Database.ExecuteSqlCommand("delete from Nozle_Suppliers where Supplier_ID=@p0", id);

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
