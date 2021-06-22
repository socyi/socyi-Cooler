using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cooler.Models;

namespace Cooler.Controllers
{
    [Authorize(Roles = "Admin,Super")]
    public class DepartmentController : Controller
    {
        DataContext db = new DataContext();
        // GET: Department
        public ActionResult Index()
        {
            var data = db.Departments.SqlQuery("select * from Departments").ToList();
            return View(data);
        }


        // GET: Department/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Department/Create
        [HttpPost]
        public ActionResult Create(Department collection)
        {
            try
            {

                List<object> lst = new List<object>();
                lst.Add(collection.Department_Code);
                lst.Add(collection.Department_Description);
                lst.Add(collection.No_Of_Filters);

                object[] allitems = lst.ToArray();

                int output = db.Database.ExecuteSqlCommand("insert into Departments" +
                    "(Department_Code,Department_Description,No_Of_Filters) values(@p0,@p1,@p2)", allitems);
                if (output > 0)
                {
                    ViewBag.msg = "Department added!";


                }
                return View();

            }
            catch(Exception e)
            {
                ViewBag.msg = e.Message;
                return View();
            }
        }

        // GET: Department/Edit/5
        public ActionResult Edit(string id)
        {
            var data = db.Departments.SqlQuery("select * from Departments where Department_Code=@p0", id).SingleOrDefault();
            return View(data);
        }

        // POST: Department/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Department collection)
        {
            try
            {
                List<object> list = new List<object>();
                list.Add(collection.No_Of_Filters);
                list.Add(collection.Department_Description);
                list.Add(id);

                object[] objectarray = list.ToArray();

                int output = db.Database.ExecuteSqlCommand("update Departments set No_Of_Filters=@p0,Department_Description=@p1" +
                    " where Department_Code=@p2", objectarray);
                if (output > 0)
                {
                    ViewBag.msg = "Department with Code " + collection.Department_Code + " updated!";
                }
                return View();
                //return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Department/Delete/5
        public ActionResult Delete(string id)
        {
            var data = db.Departments.SqlQuery("select * from Departments where Department_Code=@p0", id).SingleOrDefault();
            return View(data);
        }

        // POST: Department/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, Department collection)
        {
            try
            {
                var list = db.Database.ExecuteSqlCommand("delete from Departments where Department_Code=@p0", id);
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
