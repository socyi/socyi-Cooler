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
    public class BagReplacementController : Controller
    {
        DataContext db = new DataContext();
        // GET: BagReplacement
        public ActionResult Index()
        {
            var data = db.Bag_Replacement.SqlQuery("select * from Bag_Replacement").ToList();
            return View(data);
        }

        // GET: BagReplacement/Create
        
        public ActionResult Create()
        {
            var filterlist = db.Filters.ToList();
            ViewBag.Filter_Code = new SelectList(filterlist, "Filter_Code", "Filter_Code");
            var reasonlist = db.Replacement_Reasons.ToList();
            ViewBag.Replacement_Reason_Code = new SelectList(reasonlist, "Replacement_Reason_Code", "Replacement_Description");
            var baglist = db.Bag_Types.ToList();
            ViewBag.Bag_Code = new SelectList(baglist, "Bag_Code", "Bag_Description");
            return View();
        }

        // POST: BagReplacement/Create
        [HttpPost]
        public ActionResult Create(Bag_Replacement collection)
        {
            try
            {
                var filterlist = db.Filters.ToList();
                ViewBag.Filter_Code = new SelectList(filterlist, "Filter_Code", "Filter_Code");
                var reasonlist = db.Replacement_Reasons.ToList();
                ViewBag.Replacement_Reason_Code = new SelectList(reasonlist, "Replacement_Reason_Code", "Replacement_Description");
                var baglist = db.Bag_Types.ToList();
                ViewBag.Bag_Code = new SelectList(baglist, "Bag_Code", "Bag_Code");

                List<object> lst = new List<object>();
                lst.Add(collection.Repl_Date);
                lst.Add(collection.Filter_Code);
                lst.Add(collection.Sector_No);
                lst.Add(collection.Valve_No);
                lst.Add(collection.Bag_No);
                lst.Add(collection.Bag_Code);
                lst.Add(collection.Replacement_Reason_Code);
                object[] allitems = lst.ToArray();
                int output = db.Database.ExecuteSqlCommand("insert into Bag_Replacement(Repl_Date,Filter_Code,Sector_No,Valve_No,Bag_No,Bag_Code,Replacement_Reason_Code) values(@p0,@p1,@p2,@p3,@p4,@p5,@p6)", allitems);
                if (output > 0)
                {
                    ViewBag.msg = "Bag replacement is added";

                }
                // return View();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: BagReplacement/Edit/5
        public ActionResult Edit(int id)
        {
            var filterlist = db.Filters.ToList();
            ViewBag.Filter_Code = new SelectList(filterlist, "Filter_Code", "Filter_Code");
            var reasonlist = db.Replacement_Reasons.ToList();
            ViewBag.Replacement_Reason_Code = new SelectList(reasonlist, "Replacement_Reason_Code", "Replacement_Description");
            var baglist = db.Bag_Types.ToList();
            ViewBag.Bag_Code = new SelectList(baglist, "Bag_Code", "Bag_Description");
            var data = db.Bag_Replacement.SqlQuery("select * from Bag_Replacement where Replacement_Code=@p0", id).SingleOrDefault();
            return View(data);
        }

        // POST: BagReplacement/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Bag_Replacement collection)
        {
            try
            {
                var filterlist = db.Filters.ToList();
                ViewBag.Filter_Code = new SelectList(filterlist, "Filter_Code", "Filter_Code");
                var reasonlist = db.Replacement_Reasons.ToList();
                ViewBag.Replacement_Reason_Code = new SelectList(reasonlist, "Replacement_Reason_Code", "Replacement_Description");
                var baglist = db.Bag_Types.ToList();
                ViewBag.Bag_Code = new SelectList(baglist, "Bag_Code", "Bag_Code");

                List<object> list = new List<object>();
                list.Add(collection.Repl_Date);
                list.Add(collection.Filter_Code);
                list.Add(collection.Sector_No);
                list.Add(collection.Valve_No);
                list.Add(collection.Bag_No);
                list.Add(collection.Bag_Code);
                list.Add(collection.Replacement_Reason_Code);
                list.Add(id);
                object[] objectarray = list.ToArray();
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
                int output = db.Database.ExecuteSqlCommand("update Bag_Replacement set Repl_Date=@p0,Filter_Code=@p1,Sector_No=@p2,Valve_No=@p3,Bag_No=@p4,Bag_Code=@p5,Replacement_Reason_Code=@p6 where Replacement_Code=@p7", objectarray);
                if (output > 0)
                {
                    return RedirectToAction("Click", "FilterGraphic", new { id = filter, button = b, sec = s });
                }
                //return View();
                return View();
            }
            catch
            {

                ViewBag.Usermsg = "Replacement_Code " + collection.Replacement_Code + " is not updated!";
                return View();
            }
        }

        // GET: BagReplacement/Delete/5
        public ActionResult Delete(int id)
        {
            var filterlist = db.Filters.ToList();
            ViewBag.Filter_Code = new SelectList(filterlist, "Filter_Code", "Filter_Code");
            var reasonlist = db.Replacement_Reasons.ToList();
            ViewBag.Replacement_Reason_Code = new SelectList(reasonlist, "Replacement_Reason_Code", "Replacement_Description");
            var baglist = db.Bag_Types.ToList();
            ViewBag.Bag_Code = new SelectList(baglist, "Bag_Code", "Bag_Code");

            var data = db.Bag_Replacement.SqlQuery("select * from Bag_Replacement where Replacement_Code=@p0", id).SingleOrDefault();
            return View(data);
        }

        // POST: BagReplacement/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var filter = "";
            var b = "";
            int s = 0;
            var data = db.Bag_Replacement.SqlQuery("select * from Bag_Replacement where Replacement_Code=@p0",id).ToList();
            foreach(var item in data)
            {
                filter = item.Filter_Code;
                b = item.Bag_No + " " + item.Valve_No;
                s = item.Sector_No;
            }
            try
            {
                var userlist = db.Database.ExecuteSqlCommand("delete from Bag_Replacement where Replacement_Code=@p0", id);
                if (userlist != 0)
                {
                    return RedirectToAction("Click", "FilterGraphic", new { id = filter,button = b , sec = s });
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
            var baglist = db.Bag_Types.ToList();
            ViewBag.Bag_Code = new SelectList(baglist, "Bag_Code", "Bag_Description");
            return View();
        }

        // POST: BagReplacement/Create
        [HttpPost]
        public ActionResult CreateSingle(Bag_Replacement collection)
        {
            try
            {
               
                var reasonlist = db.Replacement_Reasons.ToList();
                ViewBag.Replacement_Reason_Code = new SelectList(reasonlist, "Replacement_Reason_Code", "Replacement_Description");
                var baglist = db.Bag_Types.ToList();
                ViewBag.Bag_Code = new SelectList(baglist, "Bag_Code", "Bag_Code");

                List<object> lst = new List<object>();
                lst.Add(collection.Repl_Date);
                lst.Add(Session["Filter"]);
                lst.Add(Session["sector"]);
                lst.Add(Session["valve"]);
                lst.Add(Session["bag"]);
                lst.Add(collection.Bag_Code);
                lst.Add(collection.Replacement_Reason_Code);
                object[] allitems = lst.ToArray();
                int output = db.Database.ExecuteSqlCommand("insert into Bag_Replacement(Repl_Date,Filter_Code,Sector_No,Valve_No,Bag_No,Bag_Code,Replacement_Reason_Code) values(@p0,@p1,@p2,@p3,@p4,@p5,@p6)", allitems);
                if (output > 0)
                {
                    ViewBag.msg = "Bag replacement is added";

                }
                // return View();
                return RedirectToAction("IndexAdmin", "FilterGraphic", new { Filter_Code = Session["Filter"], @dd2 = Session["sector"] });
            }
            catch
            {
                return View();
            }
        }

    }
}
