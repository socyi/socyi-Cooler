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
    public class BagController : Controller
    {
        DataContext db = new DataContext();
        public ActionResult Index()
        {
            var data = db.Bag_Types.SqlQuery("select * from Bag_Types").ToList();
            return View(data);
        }

        // GET: Bag/Create
        [Authorize(Roles = "Admin,Super")]
        public ActionResult Create()
        {
            var materiallist = db.Material_Types.ToList();
            ViewBag.Material_Code = new SelectList(materiallist, "Material_Code", "Material_Description");
            var supplierlist = db.Bag_Suppliers.ToList();
            ViewBag.Supplier_Code = new SelectList(supplierlist, "Supplier_Code", "Supplier_Name");
            var fiberlist = db.Fiber_Brand.ToList();
            ViewBag.Fiber_Code = new SelectList(fiberlist, "Fiber_Code", "Fiber_Supplier");
            return View();
        }

        // POST: Bag/Create
        [Authorize(Roles = "Admin,Super")]
        [HttpPost]
        public ActionResult Create(Bag_Types collection)
        {
            try
            {
                var materiallist = db.Material_Types.ToList();
                ViewBag.Material_Code = new SelectList(materiallist, "Material_Code", "Material_Description");
                var supplierlist = db.Bag_Suppliers.ToList();
                ViewBag.Supplier_Code = new SelectList(supplierlist, "Supplier_Code", "Supplier_Name");
                var fiberlist = db.Fiber_Brand.ToList();
                ViewBag.Fiber_Code = new SelectList(fiberlist, "Fiber_Code", "Fiber_Supplier");

                List<object> lst = new List<object>();
                lst.Add(collection.Bag_Dimensions);
                lst.Add(collection.Bag_Description);
                lst.Add(collection.Material_Code);
                lst.Add(collection.Supplier_Code);
                lst.Add(collection.Bag_Cost);
                lst.Add(collection.Fiber_Code);
                object[] allitems = lst.ToArray();
                int output = db.Database.ExecuteSqlCommand("insert into Bag_Types(Bag_Dimensions,Bag_Description,Material_Code,Supplier_Code,Bag_Cost,Fiber_Code) values(@p0,@p1,@p2,@p3,@p4,@p5)", allitems);
                if (output > 0)
                {
                    ViewBag.msg = "Bag is added";

                }
                // return View();
                return View( );
            }
            catch
            {
                return View();
            }
        }

        // GET: Bag/Edit/5
        public ActionResult Edit(int id)
        {
            var materiallist = db.Material_Types.ToList();
            ViewBag.Material_Code = new SelectList(materiallist, "Material_Code", "Material_Description");
            var supplierlist = db.Bag_Suppliers.ToList();
            ViewBag.Supplier_Code = new SelectList(supplierlist, "Supplier_Code", "Supplier_Name");
            var data = db.Bag_Types.SqlQuery("select * from Bag_Types where Bag_Code=@p0", id).SingleOrDefault();
            var fiberlist = db.Fiber_Brand.ToList();
            ViewBag.Fiber_Code = new SelectList(fiberlist, "Fiber_Code", "Fiber_Supplier");
            return View(data);
        }

        // POST: Bag/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Bag_Types collection)
        {
            try
            {
                var materiallist = db.Material_Types.ToList();
                ViewBag.Material_Code = new SelectList(materiallist, "Material_Code", "Material_Description");
                var supplierlist = db.Bag_Suppliers.ToList();
                ViewBag.Supplier_Code = new SelectList(supplierlist, "Supplier_Code", "Supplier_Name");
                var fiberlist = db.Fiber_Brand.ToList();
                ViewBag.Fiber_Code = new SelectList(fiberlist, "Fiber_Code", "Fiber_Supplier");

                List<object> lst = new List<object>();
                lst.Add(collection.Bag_Dimensions);
                lst.Add(collection.Bag_Description);
                lst.Add(collection.Material_Code);
                lst.Add(collection.Supplier_Code);
                lst.Add(collection.Bag_Cost);
                lst.Add(collection.Fiber_Code);
                lst.Add(id);
                object[] allitems = lst.ToArray();

                int output = db.Database.ExecuteSqlCommand("update Bag_Types set Bag_Dimensions=@p0,Bag_Description=@p1,Material_Code=@p2,Supplier_Code=@p3,Bag_Cost=@p4,Fiber_Code=@p5 where Bag_Code=@p6", allitems);
                if (output > 0)
                {
                    ViewBag.Usermsg = "bag ID " + collection.Bag_Code + " is updated!";
                }
                //return View();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.msg = ex.Message;
                return View();

            }
        }

        // GET: Bag/Delete/5
        public ActionResult Delete(int id)
        {
            var materiallist = db.Material_Types.ToList();
            ViewBag.Material_Code = new SelectList(materiallist, "Material_Description", "Material_Description");
            var supplierlist = db.Bag_Suppliers.ToList();
            ViewBag.Supplier_Code = new SelectList(supplierlist, "Supplier_Name", "Supplier_Name");
            var fiberlist = db.Fiber_Brand.ToList();
            ViewBag.Fiber_Code = new SelectList(fiberlist, "Fiber_Code", "Fiber_Supplier");

            var data = db.Bag_Types.SqlQuery("select * from Bag_Types where Bag_Code=@p0", id).SingleOrDefault();
            return View(data);
        }

        // POST: Bag/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var userlist = db.Database.ExecuteSqlCommand("delete from Bag_Types where Bag_Code=@p0", id);
                if (userlist != 0)
                {
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch(Exception ex)
            {
                ViewBag.msg = ex.Message;
                return View();
               
            }
        }
    }
}
