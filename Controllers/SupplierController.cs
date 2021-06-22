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
    public class SupplierController : Controller
    {
        DataContext db = new DataContext();
        // GET: Supplier
        public ActionResult Index()
        {
            var data = db.Bag_Suppliers.SqlQuery("select * from Bag_Suppliers").ToList();
            return View(data);
        }

        // GET: Supplier/Create
        public ActionResult Create()
        {
            List<Colors> colors = db.Colors.ToList();
            foreach (Colors c in colors.ToList())
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
        public ActionResult Create(Bag_Suppliers collection)
        {
            try
            {
                List<Colors> colors = db.Colors.ToList();
                foreach (Colors c in colors.ToList())
                {
                    if (c.Available == false)
                    {
                        colors.Remove(c);
                    }
                }
                ViewBag.Color = new SelectList(colors, "Color", "Color");

                List<object> lst = new List<object>();
                lst.Add(collection.Supplier_Code);
                lst.Add(collection.Supplier_Name);
                lst.Add(collection.Contact_Person);
                lst.Add(collection.Email_Address);
                lst.Add(collection.Telephone_Number);
                lst.Add(collection.Color);


                object[] allitems = lst.ToArray();
                int output = db.Database.ExecuteSqlCommand("insert into Bag_Suppliers(Supplier_Code,Supplier_Name,Contact_Person,Email_Address,Telephone_Number,Color) values(@p0,@p1,@p2,@p3,@p4,@p5)", allitems);
                if (output > 0)
                {
                    ViewBag.msg = "Bag_Supplier is added";
                    db.Database.ExecuteSqlCommand("Update Colors SET Available=0 WHERE Color=@p0", collection.Color);
                    return View();
                }
                else
                {

                    return View();
                }
            }
            catch
            {
                ViewBag.msg = "Something went wrong Bag_Supplier is not added (be sure that there are not other supplier with the same Code!)";
                return View();
            }
        }

        // GET: Supplier/Edit/5
        public ActionResult Edit(int id)
        {
            List<Colors> colors = db.Colors.ToList();
            foreach (Colors c in colors.ToList())
            {
                if (c.Available == false)
                {
                    colors.Remove(c);
                }
            }
            ViewBag.Color = new SelectList(colors, "Color", "Color");
            var data = db.Bag_Suppliers.SqlQuery("select * from Bag_Suppliers where Supplier_Code=@p0", id).SingleOrDefault();
            return View(data);
        }

        // POST: Supplier/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Bag_Suppliers collection)
        {
            try
            {
                List<Colors> colors = db.Colors.ToList();
                foreach (Colors c in colors.ToList())
                {
                    if (c.Available == false)
                    {
                        colors.Remove(c);
                    }
                }
                ViewBag.Color = new SelectList(colors, "Color", "Color");

                List<object> list = new List<object>();

                list.Add(collection.Supplier_Name);
                list.Add(collection.Contact_Person);
                list.Add(collection.Email_Address);
                list.Add(collection.Telephone_Number);
                list.Add(collection.Color);
                list.Add(id);


                object[] objectarray = list.ToArray();
                db.Database.ExecuteSqlCommand("Update Colors SET Available=1 FROM Colors AS C INNER JOIN Bag_Suppliers " +
                "AS B ON C.Color=B.Color WHERE B.Supplier_Code=@p0", id);
                int output = db.Database.ExecuteSqlCommand("update Bag_Suppliers set Supplier_Name=@p0,Contact_Person=@p1,Email_Address=@p2,Telephone_Number=@p3,Color=@p4 where Supplier_Code=@p5", objectarray);
                db.Database.ExecuteSqlCommand("Update Colors SET Available=0 WHERE Color=@p0", collection.Color);

                if (output > 0)
                {
                    ViewBag.Usermsg = "Supplier ID " + collection.Supplier_Code + " is updated!";
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
            var colorlist = db.Colors.ToList();
            ViewBag.Color = new SelectList(colorlist, "Color", "Color");
            var data = db.Bag_Suppliers.SqlQuery("select * from Bag_Suppliers where Supplier_Code=@p0", id).SingleOrDefault();
            return View(data);
        }

        // POST: Supplier/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                db.Database.ExecuteSqlCommand("Update Colors SET Available=1 FROM Colors AS C INNER JOIN Bag_Suppliers " +
                  "AS B ON C.Color=B.Color WHERE B.Supplier_Code=@p0", id);
                var userlist = db.Database.ExecuteSqlCommand("delete from Bag_Suppliers where Supplier_Code=@p0", id);

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
