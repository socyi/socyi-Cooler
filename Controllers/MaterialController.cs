using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cooler.Models;
using System.Web.Security;

namespace Cooler.Controllers
{
    [Authorize(Roles = "Admin,Super")]
    public class MaterialController : Controller
    {
        // GET: Material
        DataContext db = new DataContext();
        public ActionResult Index()
        {
            var data = db.Material_Types.SqlQuery("select * from Material_Types").ToList();
            return View(data);
            //return View();
        }
        public ActionResult Create()
        {

            return View();
        }

        // POST: Data/Create
        [HttpPost]
        public ActionResult Create(Material_Types collection)
        {
            try
            {

                List<object> lst = new List<object>();
                lst.Add(collection.Material_Description);
                object[] allitems = lst.ToArray();
                int output = db.Database.ExecuteSqlCommand("insert into Material_Types(Material_Description) values(@p0)", allitems);
                if (output > 0)
                {
                    ViewBag.msg = "Material Type is added";

                }
                //return View();
                 return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Edit(int id)
        {
            var data = db.Material_Types.SqlQuery("select * from Material_Types where Material_Code=@p0", id).SingleOrDefault();
            return View(data);
        }

        // POST: Data/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Material_Types collection)
        {
            try
            {
                List<object> list = new List<object>();
                list.Add(collection.Material_Description);
                list.Add(id);
                object[] objectarray = list.ToArray();

                int output = db.Database.ExecuteSqlCommand("update Material_Types set Material_Description=@p0 where Material_Code=@p1", objectarray);
                if (output > 0)
                {
                    ViewBag.Usermsg = "Material_Code " + collection.Material_Code + " is updated!";
                }
                //return View();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Delete(int id)
        {
            var data = db.Material_Types.SqlQuery("select * from Material_Types where Material_Code=@p0", id).SingleOrDefault();
            return View(data);
        }

        // POST: material/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var userlist = db.Database.ExecuteSqlCommand("delete from Material_Types where Material_Code=@p0", id);
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