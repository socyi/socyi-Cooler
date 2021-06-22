using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cooler.Models;

namespace Cooler.Controllers
{
    [Authorize(Roles = "Admin,Super")]
    public class NozleReplacementController : Controller
    {
        DataContext db = new DataContext();
        // GET: NozleReplacement
        public ActionResult Index()
        {
            var data = db.Replacements.SqlQuery("select * from Replacements").ToList();
            return View(data);
        }

       

        // GET: NozleReplacement/Create
        public ActionResult Create()
        {
            var compList = db.Compartments.ToList();
            ViewBag.Compartment_ID = new SelectList(compList, "Compartment_ID", "Compartment_ID");
            var nozList = db.Nozle_Types.ToList();
            ViewBag.Nozle_ID = new SelectList(nozList, "Nozle_ID", "Nozle_Type");
            var spareList = db.Nozle_Spares.ToList();
            ViewBag.Spare_ID = new SelectList(spareList, "Spare_ID", "Spare_Store_Id");
            var reasonList = db.Replacement_Reasons.ToList();
            ViewBag.Replacement_Reason_Code = new SelectList(reasonList, "Replacement_Reason_Code", "Replacement_Description");
            return View();
        }

        // POST: NozleReplacement/Create
        [HttpPost]
        public ActionResult Create(Replacement collection)
        {
            try
            {
                var compList = db.Compartments.ToList();
                ViewBag.Compartment_ID = new SelectList(compList, "Compartment_ID", "Compartment_ID");
                var nozList = db.Nozle_Types.ToList();
                ViewBag.Nozle_ID = new SelectList(nozList, "Nozle_ID", "Nozle_Type");
                var spareList = db.Nozle_Spares.ToList();
                ViewBag.Spare_ID = new SelectList(spareList, "Spare_ID", "Spare_Store_Id");
                var reasonList = db.Replacement_Reasons.ToList();
                ViewBag.Replacement_Reason_Code = new SelectList(reasonList, "Replacement_Reason_Code", "Replacement_Description");

                List<object> lst = new List<object>();
                lst.Add(collection.Beam_no);
                lst.Add(collection.Nozle_no);
                lst.Add(collection.Spare_ID);
                lst.Add(collection.Compartment_ID);
                lst.Add(collection.Nozle_ID);
                lst.Add(collection.Date);
                lst.Add(collection.Replacement_Reason_Code);
                lst.Add(DateTime.Now.ToString("M/d/yyyy"));

                object[] allitems = lst.ToArray();



                int output = db.Database.ExecuteSqlCommand("insert into Replacements (Beam_no, Nozle_no," +
                    "Spare_ID,Compartment_ID,Nozle_ID,Date,Replacement_Reason_Code,Create_Date) values(@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7)", allitems);
                if (output > 0)
                {
                    ViewBag.msg = "Nozle Replacement added!";


                }
                return View();

            }
            catch(Exception e)
            {
                ViewBag.msg =e.ToString();
                return View();
            }
        }

        // GET: NozleReplacement/Edit/5
        public ActionResult Edit(int id)
        {
            var compList = db.Compartments.ToList();
            ViewBag.Compartment_ID = new SelectList(compList, "Compartment_ID", "Compartment_ID");
            var nozList = db.Nozle_Types.ToList();
            ViewBag.Nozle_ID = new SelectList(nozList, "Nozle_ID", "Nozle_Type");
            var spareList = db.Nozle_Spares.ToList();
            ViewBag.Spare_ID = new SelectList(spareList, "Spare_ID", "Spare_Store_Id");
            var data = db.Replacements.SqlQuery("select * from Replacements where Replacement_ID=@p0", id).SingleOrDefault();
            return View(data);
        }

        // POST: NozleReplacement/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Replacement collection)
        {
            try
            {
                var compList = db.Compartments.ToList();
                ViewBag.Compartment_ID = new SelectList(compList, "Compartment_ID", "Compartment_ID");
                var nozList = db.Nozle_Types.ToList();
                ViewBag.Nozle_ID = new SelectList(nozList, "Nozle_ID", "Nozle_Type");
                var spareList = db.Nozle_Spares.ToList();
                ViewBag.Spare_ID = new SelectList(spareList, "Spare_ID", "Spare_Store_Id");
                List<object> lst = new List<object>();

                lst.Add(collection.Beam_no);
                lst.Add(collection.Nozle_no);
                lst.Add(collection.Spare_ID);
                lst.Add(collection.Compartment_ID);
                lst.Add(collection.Nozle_ID);
                lst.Add(collection.Date);
                lst.Add(id);
                object[] objectarray = lst.ToArray();

                int output = db.Database.ExecuteSqlCommand("update Replacements set Beam_no=@p0,Nozle_no=@p1,Spare_ID=@p2" +
                    ",Compartment_ID=@p3,Nozle_ID=@p4,Date=@p5 where Replacement_ID=@p6", objectarray);
                if (output > 0)
                {
                    ViewBag.msg = "Replacement ID " + collection.Replacement_ID + " updated!";
                }
                //return View();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: NozleReplacement/Delete/5
        public ActionResult Delete(int id)
        {
            var compList = db.Compartments.ToList();
            ViewBag.Compartment_ID = new SelectList(compList, "Compartment_ID", "Compartment_ID");
            var nozList = db.Nozle_Types.ToList();
            ViewBag.Nozle_ID = new SelectList(nozList, "Nozle_ID", "Nozle_Type");
            var spareList = db.Nozle_Spares.ToList();
            ViewBag.Spare_ID = new SelectList(spareList, "Spare_ID", "Spare_Store_Id");
            var data = db.Replacements.SqlQuery("select * from Replacements where Replacement_ID=@p0", id).SingleOrDefault();
            return View(data);
        }

        // POST: NozleReplacement/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var list = db.Database.ExecuteSqlCommand("delete from Replacements where Replacement_ID=@p0", id);
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
