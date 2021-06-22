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
    public class VibratorController : Controller
    {
        DataContext db = new DataContext();
        // GET: Vibrator
        public ActionResult Index()
        {
            var data = db.Vibrators.SqlQuery("select * from Vibrators").ToList();
            return View(data);
        }


        // GET: Vibrator/Create
        public ActionResult Create()
        {
            var vibratorlist = db.Filters.ToList();
            ViewBag.Filter_Code = new SelectList(vibratorlist, "Filter_Code", "Filter_Code");
            return View();
        }

        // POST: Vibrator/Create
        [HttpPost]
        public ActionResult Create(Vibrator collection)
        {
            try
            {
                var vibratorlist = db.Filters.ToList();
                ViewBag.Filter_Code = new SelectList(vibratorlist, "Filter_Code", "Filter_Code");
                List<object> lst = new List<object>();
                lst.Add(collection.Vibrator_Type);
                lst.Add(collection.Filter_Code);
                object[] allitems = lst.ToArray();
                int output = db.Database.ExecuteSqlCommand("insert into Vibrators(Vibrator_Type,Filter_Code) values(@p0,@p1)", allitems);
                if (output > 0)
                {
                    ViewBag.msg = "Vibrator is added";

                }
                // return View();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Vibrator/Edit/5
        public ActionResult Edit(int id)
        {
            var vibratorlist = db.Filters.ToList();
            ViewBag.Filter_Code = new SelectList(vibratorlist, "Filter_Code", "Filter_Code");
            var data = db.Vibrators.SqlQuery("select * from Vibrators where Vibrator_Code=@p0", id).SingleOrDefault();
            return View();
        }

        // POST: Vibrator/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Vibrator collection)
        {
            try
            {
                var vibratorlist = db.Filters.ToList();
                ViewBag.Filter_Code = new SelectList(vibratorlist, "Filter_Code", "Filter_Code");
                List<object> list = new List<object>();
                List<object> lst = new List<object>();
                lst.Add(collection.Vibrator_Type);
                lst.Add(collection.Filter_Code);
                lst.Add(id);
                object[] objectarray = lst.ToArray();

                int output = db.Database.ExecuteSqlCommand("update Vibrators set Vibrator_Type=@p0,Filter_Code=@p1 where Vibrator_Code=@p2", objectarray);
                if (output > 0)
                {
                    ViewBag.Usermsg = "Vibrator ID " + collection.Vibrator_Code + " is updated!";
                }
                //return View();
                return RedirectToAction("Index");

             
            }
            catch
            {
                return View();
            }
        }

        // GET: Vibrator/Delete/5
        public ActionResult Delete(int id)
        {
            var vibratorlist = db.Filters.ToList();
            ViewBag.Filter_Code = new SelectList(vibratorlist, "Filter_Code", "Filter_Code"); 
            var data = db.Vibrators.SqlQuery("select * from Vibrators where Vibrator_Code=@p0", id).SingleOrDefault();
            return View(data);
        }

        // POST: Vibrator/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var userlist = db.Database.ExecuteSqlCommand("delete from Vibrators where Vibrator_Code=@p0", id);
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
