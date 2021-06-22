using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Cooler.Models;
using Filter = Cooler.Models.Filter;
using Bag_Replacement = Cooler.Models.Bag_Replacement;
using System.Data.SqlClient;
using System.Web.Routing;
using System.Data;

namespace Cooler.Controllers
{
    [Authorize(Roles = "Admin,Super")]
    public class CagesMaintenanceController : Controller
    {
       
        DataContext db = new DataContext();
        // GET: CagesMaintenance
        public ActionResult Index()
        {
            var data = db.Cages_Maintenance.SqlQuery("select * from Cages_Maintenance").ToList();
            return View(data);
        }


        // GET: CagesMaintenance/Create
        public ActionResult Create()
        {
            var filterlist = db.Filters.ToList();
            ViewBag.Filter_Code = new SelectList(filterlist, "Filter_Code", "Filter_Code");
            var reasonlist = db.Replacement_Reasons.ToList();
            ViewBag.Replacement_Reason_Code = new SelectList(reasonlist, "Replacement_Reason_Code", "Replacement_Description");
            return View();
        }

        // POST: CagesMaintenance/Create
        [HttpPost]
        public ActionResult Create(Cages_Maintenance collection)
        {
            try
            {
                var filterlist = db.Filters.ToList();
                ViewBag.Filter_Code = new SelectList(filterlist, "Filter_Code", "Filter_Code");
                var reasonlist = db.Replacement_Reasons.ToList();
                ViewBag.Replacement_Reason_Code = new SelectList(reasonlist, "Replacement_Reason_Code", "Replacement_Description");
                List<object> lst = new List<object>();
                lst.Add(collection.Date);
                lst.Add(collection.Filter_Code);
                lst.Add(collection.Replacement_Reason_Code);
                lst.Add(collection.Sector_No);
                lst.Add(collection.Valve_No);
                lst.Add(collection.Bag_No);
                object[] allitems = lst.ToArray();
                int output = db.Database.ExecuteSqlCommand("insert into Cages_Maintenance(Date,Filter_Code,Replacement_Reason_Code,Sector_No,Valve_No,Bag_No) values(@p0,@p1,@p2,@p3,@p4,@p5)", allitems);
                if (output > 0)
                {
                    ViewBag.msg = "Maintenance replacement is added";

                }
                // return View();
                return RedirectToAction("Index");
              
            }
            catch(Exception ex)
            {
                ViewBag.msg = ex.Message;
                return View();
            }
        }

        // GET: CagesMaintenance/Edit/5
        public ActionResult Edit(int id)
        {
            var filterlist = db.Filters.ToList();
            ViewBag.Filter_Code = new SelectList(filterlist, "Filter_Code", "Filter_Code");
            var reasonlist = db.Replacement_Reasons.ToList();
            ViewBag.Replacement_Reason_Code = new SelectList(reasonlist, "Replacement_Reason_Code", "Replacement_Description");
            var data = db.Cages_Maintenance.SqlQuery("select * from Cages_Maintenance where Cage_Code=@p0", id).SingleOrDefault();
            return View(data);
        }

        // POST: CagesMaintenance/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Cages_Maintenance collection)
        {
            try
            {
                var filterlist = db.Filters.ToList();
                ViewBag.Filter_Code = new SelectList(filterlist, "Filter_Code", "Filter_Code");
                var reasonlist = db.Replacement_Reasons.ToList();
                ViewBag.Replacement_Reason_Code = new SelectList(reasonlist, "Replacement_Reason_Code", "Replacement_Description");
                var baglist = db.Bag_Types.ToList();

                List<object> list = new List<object>();
                list.Add(collection.Date);
                list.Add(Session["Filter"]);
                list.Add(collection.Replacement_Reason_Code);
                list.Add(Session["sector"]);
                list.Add(Session["valve"]);
                list.Add(Session["bag"]);
                list.Add(id);
                object[] objectarray = list.ToArray();
                var filter = "";
                var b = "";
                int s = 0;
                var data = db.Cages_Maintenance.SqlQuery("select * from Cages_Maintenance where Cage_Code=@p0", id).ToList();
                foreach (var item in data)
                {
                    filter = item.Filter_Code;
                    b = item.Bag_No + " " + item.Valve_No;
                    s = item.Sector_No;
                }
                int output = db.Database.ExecuteSqlCommand("update Cages_Maintenance set Date=@p0,Filter_Code=@p1,Replacement_Reason_Code=@p2,Sector_No=@p3,Valve_No=@p4,Bag_No=@p5 where Cage_Code=@p6", objectarray);
                if (output > 0)
                {
                    ViewBag.Usermsg = "Cage " + collection.Cage_Code + " is updated!";
                }
                //return View();
                return RedirectToAction("CageClick", "FilterGraphic", new { id = filter, button = b, sec = s });
            }
            catch(Exception ex)
            {
                ViewBag.msg = ex.Message;
                return View();
            }
        }

        // GET: CagesMaintenance/Delete/5
        public ActionResult Delete(int id)
        {
            var filterlist = db.Filters.ToList();
            ViewBag.Filter_Code = new SelectList(filterlist, "Filter_Code", "Filter_Code");
            var reasonlist = db.Replacement_Reasons.ToList();
            ViewBag.Replacement_Reason_Code = new SelectList(reasonlist, "Replacement_Reason_Code", "Replacement_Description");
            var data = db.Cages_Maintenance.SqlQuery("select * from Cages_Maintenance where Cage_Code=@p0", id).SingleOrDefault();
            return View(data);
            
        }

        // POST: CagesMaintenance/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var filter = "";
            var b = "";
            int s = 0;
            var data = db.Bag_Replacement.SqlQuery("select * from Bag_Replacement where Replacement_Code=@p0", id).ToList();
            foreach (var item in data)
            {
                filter = item.Filter_Code;
                b = item.Bag_No + " " + item.Valve_No;
                s = item.Sector_No;
            }
            try
            {
                var userlist = db.Database.ExecuteSqlCommand("delete from Cages_Maintenance where Cage_Code=@p0", id);
                if (userlist != 0)
                {
                    return RedirectToAction("Cage", "FilterGraphic", new { Filter_Code = filter, dd2 = s });
                }
                return View();

            }
            catch
            {
                return View();
            }
        }
        public ActionResult CreateSingle()
        {
            var filterlist = db.Filters.ToList();
            ViewBag.Filter_Code = new SelectList(filterlist, "Filter_Code", "Filter_Code");
            var reasonlist = db.Replacement_Reasons.ToList();
            ViewBag.Replacement_Reason_Code = new SelectList(reasonlist, "Replacement_Reason_Code", "Replacement_Description");
           
            return View();
        }

        // POST: BagReplacement/Create
        [HttpPost]
        public ActionResult CreateSingle(Cages_Maintenance collection)
        {
            try
            {

                var reasonlist = db.Replacement_Reasons.ToList();
                ViewBag.Replacement_Reason_Code = new SelectList(reasonlist, "Replacement_Reason_Code", "Replacement_Description");
               

                List<object> lst = new List<object>();
                lst.Add(collection.Date);
                lst.Add(Session["Filter"]);
                lst.Add(Session["sector"]);
                lst.Add(Session["valve"]);
                lst.Add(Session["bag"]);
               
                lst.Add(collection.Replacement_Reason_Code);
                object[] allitems = lst.ToArray();
                int output = db.Database.ExecuteSqlCommand("insert into Cages_Maintenance(Date,Filter_Code,Replacement_Reason_Code,Sector_No,Valve_No,Bag_No) values(@p0,@p1,@p5,@p2,@p3,@p4)", allitems);
                if (output > 0)
                {
                    ViewBag.msg = "Bag replacement is added";

                }
                // return View();
                return RedirectToAction("Cage", "FilterGraphic", new { Filter_Code = Session["Filter"], @dd2 = Session["sector"] });
            }
            catch
            {
                return View();
            }
        }
    }
}
