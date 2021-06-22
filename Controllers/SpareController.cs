using Cooler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Cooler.Controllers
{
    [Authorize]
    public class SpareController : Controller
    {
        DataContext db = new DataContext();
        // GET: Spare
        public ActionResult Index()
        {
            var data = db.Nozle_Spares.SqlQuery("select * from Nozle_Spares").ToList();
            return View(data); 
        }

      
       

        // GET: Spare/Create
        public ActionResult Create()
        {
            var supplierlist = db.Nozle_Suppliers.ToList();
            ViewBag.Supplier_ID = new SelectList(supplierlist, "Supplier_ID", "Supplier");
            var nozlelist = db.Nozle_Types.ToList();
            ViewBag.Nozle_ID = new SelectList(nozlelist, "Nozle_ID", "Nozle_Type");
            return View();
        }

        // POST: Spare/Create
        [HttpPost]
        public ActionResult Create(Nozle_Spares collection)
        {
            try
            {
                var supplierlist = db.Nozle_Suppliers.ToList();
                ViewBag.Supplier_ID = new SelectList(supplierlist, "Supplier_ID", "Supplier");
                var nozlelist = db.Nozle_Types.ToList();
                ViewBag.Nozle_ID = new SelectList(nozlelist, "Nozle_ID", "Nozle_Type");

                List<object> lst = new List<object>();
                lst.Add(collection.Nozle_ID);
                lst.Add(collection.Spare_Store_Id);
                lst.Add(collection.Description);
                lst.Add(collection.Supplier_ID);
                lst.Add(collection.Cost);
                object[] allitems = lst.ToArray();
                int output = db.Database.ExecuteSqlCommand("insert into Nozle_Spares(Nozle_ID,Spare_Store_Id,Description,Supplier_ID,Cost) values(@p0,@p1,@p2,@p3,@p4)", allitems);
                if (output > 0)
                {
                    ViewBag.msg = "Bag is added";

                }
                // return View();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Spare/Edit/5
        public ActionResult Edit(int id)
        {
            var supplierlist = db.Nozle_Suppliers.ToList();
            ViewBag.Supplier_ID = new SelectList(supplierlist, "Supplier_ID", "Supplier");
            var nozlelist = db.Nozle_Types.ToList();
            ViewBag.Nozle_ID = new SelectList(nozlelist, "Nozle_ID", "Nozle_Type");
            var data = db.Nozle_Spares.SqlQuery("select * from Nozle_Spares where Spare_ID=@p0", id).SingleOrDefault();
            return View(data);
        }

        // POST: Spare/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Nozle_Spares collection)
        {
            try
            {
                var supplierlist = db.Nozle_Suppliers.ToList();
                ViewBag.Supplier_ID = new SelectList(supplierlist, "Supplier_ID", "Supplier");
                var nozlelist = db.Nozle_Types.ToList();
                ViewBag.Nozle_ID = new SelectList(nozlelist, "Nozle_ID", "Nozle_Type");
                List<object> list = new List<object>();
                list.Add(collection.Nozle_ID);
                list.Add(collection.Spare_Store_Id);
                list.Add(collection.Description);
                list.Add(collection.Supplier_ID);
                list.Add(collection.Cost);
                list.Add(id);
                object[] objectarray = list.ToArray();

                int output = db.Database.ExecuteSqlCommand("update Nozle_Spares set Nozle_ID=@p0,Spare_Store_Id=@p1,Description=@p2,Supplier_ID =@p3,Cost=@p4 where Spare_ID=@p5", objectarray);
                if (output > 0)
                {
                    ViewBag.Usermsg = "bag ID " + collection.Spare_ID + " is updated!";
                }
                //return View();
                return RedirectToAction("Index");
                
            }
            catch
            {
                return View();
            }
        }

        // GET: Spare/Delete/5
        public ActionResult Delete(int id)
        {
            var supplierlist = db.Nozle_Suppliers.ToList();
            ViewBag.Supplier_ID = new SelectList(supplierlist, "Supplier_ID", "Supplier");

            var nozlelist = db.Nozle_Types.ToList();
            ViewBag.Nozle_ID = new SelectList(nozlelist, "Nozle_ID", "Nozle_Type");
            var data = db.Nozle_Spares.SqlQuery("select * from Nozle_Spares where Spare_ID=@p0", id).SingleOrDefault();

            return View(data);
        }

        // POST: Spare/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var userlist = db.Database.ExecuteSqlCommand("delete from Nozle_Spares where Spare_ID=@p0", id);
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
