using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Cooler.Models;
using Filter = Cooler.Models.Filter;
using System.Data.SqlClient;

namespace Cooler.Controllers
{
    [Authorize(Roles = "Admin,Super")]
    public class FilterController : Controller
    {
        // GET: Filter
        DataContext db = new DataContext();
        public ActionResult Index()
        {
            var data = db.Filters.SqlQuery("select * from Filters").ToList();
            return View(data);
        }

    
        
        // GET: Filter/Create
        public ActionResult Create()
        {
            var dep_list = db.Departments.ToList();
            ViewBag.Department_Code = new SelectList(dep_list, "Department_Code", "Department_Code");
            var man_list = db.Manufacturers.ToList();
            ViewBag.Manufacturer_Code = new SelectList(man_list, "Manufacturer_Code", "Manufacturer_Name");

            return View();
        }
        
        // POST: Filter/Create

        [HttpPost]
        public ActionResult Create(Filter collection)
        {
            try
            {
                var dep_list = db.Departments.ToList();
                ViewBag.Department_Code = new SelectList(dep_list, "Department_Code", "Department_Code");
                var man_list = db.Manufacturers.ToList();
                ViewBag.Manufacturer_Code = new SelectList(man_list, "Manufacturer_Code", "Manufacturer_Name");

                List<object> lst = new List<object>();
                lst.Add(collection.Filter_Code);
                lst.Add(collection.Filter_Description);
                lst.Add(collection.Department_Code);
                lst.Add(collection.Manufacturer_Code);
                lst.Add(collection.No_Of_Sectors);
                lst.Add(collection.Sector_No_Valves);
                lst.Add(collection.Bags_Per_Valve);
                lst.Add(collection.No_Of_Vibrators);
               
                lst.Add(collection.X);
                lst.Add(collection.Y);
                lst.Add(collection.Dimensions);

                object[] allitems = lst.ToArray();

                

                int output = db.Database.ExecuteSqlCommand("insert into Filters(Filter_Code," +
                    "Filter_Description,Department_Code,Manufacturer_Code,No_Of_Sectors,Sector_No_Valves," +
                    "Bags_Per_Valve,No_Of_Vibrators,X,Y,Dimensions) values(@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10 )", allitems);
                if (output > 0)
                {
                    ViewBag.msg =  "Filter added!";
                    

                }
                return View();
       
            }
            catch
            {
                ViewBag.msg = "Something went wrong, Filter not added.";
                return View();
            }
        }

        // GET: Filter/Edit/5

        public ActionResult Edit(string id)
        {
            var deplist = db.Departments.ToList();
            ViewBag.Department_Code = new SelectList(deplist, "Department_Code", "Department_Code");
            var manlist = db.Manufacturers.ToList();
            ViewBag.Manufacturer_Code = new SelectList(manlist, "Manufacturer_Code", "Manufacturer_Name");
            var data = db.Filters.SqlQuery("select * from Filters where Filter_Code=@p0", id).SingleOrDefault();
            return View(data);
        }

        // POST: Bag/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Filter collection)
        {
            try
            {
                var deplist = db.Departments.ToList();
                ViewBag.Department_Code = new SelectList(deplist, "Department_Code", "Department_Code");
                var manlist = db.Manufacturers.ToList();
                ViewBag.Manufacturer_Code = new SelectList(manlist, "Manufacturer_Code", "Manufacturer_Name");
                List<object> lst = new List<object>();
                
                lst.Add(collection.Filter_Description);
                lst.Add(collection.Department_Code);
                lst.Add(collection.Manufacturer_Code);
                lst.Add(collection.No_Of_Sectors);
                lst.Add(collection.Sector_No_Valves);
                lst.Add(collection.Bags_Per_Valve);
                lst.Add(collection.No_Of_Vibrators);
                lst.Add(collection.X);
                lst.Add(collection.Y);
                lst.Add(collection.Dimensions);
                lst.Add(id);
                object[] objectarray = lst.ToArray();

                int output = db.Database.ExecuteSqlCommand("update Filters set Filter_Description=@p0,Department_Code=@p1,Manufacturer_Code=@p2" +
                    ",No_Of_Sectors=@p3,Sector_No_Valves=@p4,Bags_Per_Valve=@p5,No_Of_Vibrators=@p6,X=@p7,Y=@p8,Dimensions=@p9 where Filter_Code=@p10", objectarray);
                if (output > 0)
                {
                    ViewBag.msg = "Filter ID " + collection.Filter_Code + " updated!";
                }
                //return View();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        // GET: Filter/Delete/5
        public ActionResult Delete(string id)
        {
            var deplist = db.Departments.ToList();
            ViewBag.Department_Code = new SelectList(deplist, "Department_Code", "Department_Code");
            var manlist = db.Manufacturers.ToList();
            ViewBag.Manufacturer_Code = new SelectList(manlist, "Manufacturer_Code", "Manufacturer_Name");
            var data = db.Filters.SqlQuery("select * from Filters where Filter_Code=@p0", id).SingleOrDefault();
            return View(data);
        }

        // POST: Filter/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, Filter collection)
        {
            try
            {
                var list = db.Database.ExecuteSqlCommand("delete from Filters where Filter_Code=@p0", id); 
                // TODO: Add delete logic here
                if (list != 0)
                {
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch(Exception e)
            {
                ViewBag.msg = e.ToString();
                return View();
            }
        }
        
    }
}
