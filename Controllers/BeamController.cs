using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cooler.Models;
namespace Cooler.Controllers
{
    [Authorize(Roles = "Admin,Super")]
    public class BeamController : Controller
    {
        
        DataContext db = new DataContext();
        // GET: Beam
        public ActionResult Index()
        {
            var data = db.Moveable_Beams.SqlQuery("select * from Moveable_Beams").ToList();
            return View(data);
        }

  
        // GET: Beam/Create
        public ActionResult Create()
        {
            var compList = db.Compartments.ToList();
            ViewBag.Compartment_ID = new SelectList(compList, "Compartment_ID", "Compartment_ID");
            return View();
        }

        // POST: Beam/Create
        [HttpPost]
        public ActionResult Create(Moveable_Beams collection)
        {
            try
            {
                var compList = db.Compartments.ToList();
                ViewBag.Compartment_ID = new SelectList(compList, "Compartment_ID", "Compartment_ID");

                List<object> lst = new List<object>();
                lst.Add(collection.Compartment_ID);
                lst.Add(collection.Beam_no);
                lst.Add(collection.Moveable);
                object[] allitems = lst.ToArray();

                int output = db.Database.ExecuteSqlCommand("insert into Moveable_Beams(Compartment_ID," +
                  "Beam_no, Moveable) values(@p0,@p1,@p2 )", allitems);
                if (output > 0)
                {
                    return RedirectToAction("Index");
                    


                }
                return View();

            }
            catch
            {
                ViewBag.msg = "Something went wrong, Moveable Beam not added.";
                return View();
            }
        }
        // GET: Filter/Edit/5

        public ActionResult Edit(int id, int id2)
        {
            var compList = db.Compartments.ToList();
            ViewBag.Compartment_ID = new SelectList(compList, "Compartment_ID", "Compartment_ID");
            var data = db.Moveable_Beams.SqlQuery("select * from Moveable_Beams where Compartment_ID=@p0 and Beam_no=@p1", id,id2).SingleOrDefault();
            return View(data);
        }

        // POST: Bag/Edit/5
        [HttpPost]
        public ActionResult Edit(int id,int id2, Moveable_Beams collection)
        {
            try
            {
                var compList = db.Compartments.ToList();
                ViewBag.Compartment_ID = new SelectList(compList, "Compartment_ID", "Compartment_ID");
                List<object> lst = new List<object>();

                lst.Add(collection.Moveable);
                lst.Add(id);
                lst.Add(id2);
                object[] objectarray = lst.ToArray();

                int output = db.Database.ExecuteSqlCommand("update Moveable_Beams set Moveable=@p0" +
                    " where Compartment_ID=@p1 and Beam_no=@p2", objectarray);
                if (output > 0)
                {
                    ViewBag.msg = "Beam " + collection.Beam_no+"from Compartment "+collection.Compartment_ID+ " updated!";
                    
                    return RedirectToAction("Index");

                }
                else {
                    ViewBag.msg = "done";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.msg = ex.Message;
                return View();

            }
           
        }

        // GET: Beam/Delete/5
        public ActionResult Delete(int id,int id2)
        {
            var compList = db.Compartments.ToList();
            ViewBag.Compartment_ID = new SelectList(compList, "Compartment_ID", "Compartment_ID");
            var data = db.Moveable_Beams.SqlQuery("select * from Moveable_Beams where Compartment_ID=@p0 and Beam_no=@p1", id,id2).SingleOrDefault();
            return View(data);
           
        }

        // POST: Beam/Delete/5
        [HttpPost]
        public ActionResult Delete(int id,int id2, Moveable_Beams collection)
        {
            try
            {
                var list = db.Database.ExecuteSqlCommand("delete from Moveable_Beams where Compartment_ID=@p0 and Beam_no=@p1", id,id2);
                // TODO: Add delete logic here
                if (list != 0)
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
