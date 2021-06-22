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
    public class ValvesMaintenanceController : Controller
    {
        DataContext db = new DataContext();
        // GET: ValvesMaintenance
        public ActionResult Index()
        {
            var data = db.Valves_Maintenance.SqlQuery("select * from Valves_Maintenance").ToList();
            return View(data);
        }

      
        // GET: ValvesMaintenance/Create
        public ActionResult Create()
        {
            var filterlist = db.Filters.ToList();
            ViewBag.Filter_Code = new SelectList(filterlist, "Filter_Code", "Filter_Code");
            return View();
        }

        // POST: ValvesMaintenance/Create
        [HttpPost]
        public ActionResult Create(Valves_Maintenance collection)
        {
            try
            {
                var filterlist = db.Filters.ToList();
                ViewBag.Filter_Code = new SelectList(filterlist, "Filter_Code", "Filter_Code");

                List<object> lst = new List<object>();
                lst.Add(collection.Maint_Date);
                lst.Add(collection.Filter_Code);
                lst.Add(collection.Sector_No);
                lst.Add(collection.Valve_No);
                object[] allitems = lst.ToArray();
                int output = db.Database.ExecuteSqlCommand("insert into Valves_Maintenance(Maint_Date,Filter_Code,Sector_No,Valve_No) values(@p0,@p1,@p2,@p3)", allitems);
                if (output > 0)
                {
                    ViewBag.msg = "Valve maintenance is added";

                }
                // return View();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ValvesMaintenance/Edit/5
        public ActionResult Edit(int id)
        {
            var filterlist = db.Filters.ToList();
            ViewBag.Filter_Code = new SelectList(filterlist, "Filter_Code", "Filter_Code");
            var data = db.Valves_Maintenance.SqlQuery("select * from Valves_Maintenance where Maintenance_Code=@p0", id).SingleOrDefault();

            return View(data);
        }

        // POST: ValvesMaintenance/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Valves_Maintenance collection)
        {
            try
            {
                var filterlist = db.Filters.ToList();
                ViewBag.Filter_Code = new SelectList(filterlist, "Filter_Code", "Filter_Code"); List<object> list = new List<object>();
                list.Add(collection.Maint_Date);
                list.Add(collection.Filter_Code);
                list.Add(collection.Sector_No);
                list.Add(collection.Valve_No);
                list.Add(id);
                object[] objectarray = list.ToArray();

                int output = db.Database.ExecuteSqlCommand("update Valves_Maintenance set Maint_Date=@p0,Filter_Code=@p1,Sector_No=@p2,Valve_No=@p3 where Maintenance_Code=@p4", objectarray);
                if (output > 0)
                {
                    ViewBag.Usermsg = "maintenance ID " + collection.Maintenance_Code + " is updated!";
                }
                //return View();
                return RedirectToAction("Index");

            }
            catch
            {
                return View();
            }
        }

        // GET: ValvesMaintenance/Delete/5
        public ActionResult Delete(int id)
        {
            var filterlist = db.Filters.ToList();
            ViewBag.Filter_Code = new SelectList(filterlist, "Filter_Code", "Filter_Code"); List<object> list = new List<object>();
            var data = db.Valves_Maintenance.SqlQuery("select * from Valves_Maintenance where Maintenance_Code=@p0", id).SingleOrDefault();
            return View(data);
        }

        // POST: ValvesMaintenance/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var userlist = db.Database.ExecuteSqlCommand("delete from Valves_Maintenance where Maintenance_Code=@p0", id);
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
