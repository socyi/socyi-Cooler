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
    public class Replacement_ReasonController : Controller
    {
        DataContext db = new DataContext();
        // GET: Replacement_Reason
        public ActionResult Index()
        {
            var data = db.Replacement_Reasons.SqlQuery("select * from Replacement_Reasons").ToList();
            return View(data);
        }

       
        // GET: Replacement_Reason/Create
        public ActionResult Create()
        {
            var for_list = db.Replacement_Reasons.ToList();
            ViewBag.Category = new SelectList(for_list, "Category", "Category");
            return View();
        }

        // POST: Replacement_Reason/Create
        [HttpPost]
        public ActionResult Create(Replacement_Reasons collection)
        {
            var for_list = db.Replacement_Reasons.ToList();
            var category = Session["For"];
            
           
            try
            {
                List<object> lst = new List<object>();
                lst.Add(collection.Replacement_Description);
                lst.Add(category);
                
                object[] allitems = lst.ToArray();
                int output = db.Database.ExecuteSqlCommand("insert into Replacement_Reasons(Replacement_Description,Category) values(@p0,@p1)", allitems);
                if (output > 0)
                {
                    ViewBag.msg = "Reason is added";

                }
                // return View();
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Replacement_Reason/Edit/5
        public ActionResult Edit(int id)
        {
            var data = db.Replacement_Reasons.SqlQuery("select * from Replacement_Reasons where Replacement_Reason_Code=@p0", id).SingleOrDefault();
            return View(data);
        }

        // POST: Replacement_Reason/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Replacement_Reasons collection)
        {
            try
            {
                List<object> list = new List<object>();
               
                list.Add(collection.Replacement_Description);
                list.Add(id);
                object[] objectarray = list.ToArray();

                int output = db.Database.ExecuteSqlCommand("update Replacement_Reasons set Replacement_Description=@p0 where Replacement_Reason_Code=@p1", objectarray);
                if (output > 0)
                {
                    ViewBag.Usermsg = "bag ID " + collection.Replacement_Reason_Code + " is updated!";
                }
                //return View();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Replacement_Reason/Delete/5
        public ActionResult Delete(int id)
        {
            var data = db.Replacement_Reasons.SqlQuery("select * from Replacement_Reasons where Replacement_Reason_Code=@p0", id).SingleOrDefault();

            return View(data);
        }

        // POST: Replacement_Reason/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var userlist = db.Database.ExecuteSqlCommand("delete from Replacement_Reasons where Replacement_Reason_Code=@p0", id);
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
        [HttpPost]
        public ActionResult forChange(string value)
        {
            Session["For"] = value;
            return RedirectToAction("Create");
        }
    }
}
