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
    [Authorize]
    public class FilterGraphicController : Controller

    {
        string connectionString = "Data Source=localhost; Initial Catalog = VassilikoFilters; Integrated Security = True; MultipleActiveResultSets=True; ";
        DataContext db = new DataContext();
        // GET: FilterGraphic
        public ActionResult Index(string Filter_Code, int dd2)
        {

            Session["Filter"] = Filter_Code;
            Session["Sector"] = dd2;

            ViewBag.sec = dd2;
            ViewBag.fil = Filter_Code;

            var data = db.Filters.SqlQuery("select * from Filters where Filter_Code=@p0", Filter_Code).ToList();

            foreach (var item in data)
            {
                ViewBag.allSec = item.No_Of_Sectors;
                String[,] colors = new String[item.Bags_Per_Valve + 1, item.Sector_No_Valves + 1];
                int[,] x = new int[item.Bags_Per_Valve + 1, item.Sector_No_Valves + 1];
                for (int i = 1; i <= item.Bags_Per_Valve; i++)
                {
                    for (int j = 1; j <= item.Sector_No_Valves; j++)
                    {
                        int bcode = 0;
                        var data1 = db.Bag_Replacement.SqlQuery("select * from Bag_Replacement where Filter_Code=@p0 AND Sector_No=@p1 AND Bag_No=@p2  AND Valve_no=@p3 ", Filter_Code, dd2, i, j).ToList();
                        foreach (var item2 in data1)
                        {
                            bcode = item2.Bag_Code;
                        }
                        int scode = 0;
                        var data3 = db.Bag_Types.SqlQuery("select * from Bag_Types where Bag_Code=@p0", bcode).ToList();
                        foreach (var item2 in data3)
                        {
                            scode = item2.Supplier_Code;
                        }
                        var data4 = db.Bag_Suppliers.SqlQuery("select * from Bag_Suppliers where Supplier_Code=@p0", scode).ToList();

                        if (data1.Count != 0)
                        {
                            foreach (var item3 in data4)
                            {
                                colors[i, j] = item3.Color;
                            }
                            x[i, j] = 1;
                        }
                        data1.Clear();
                    }
                }
                ViewBag.color = colors;
                ViewBag.x = x;

            }

            return View(data);
        }
        public ActionResult ReplacementForm(string Filter_Code, int dd2)
        {
            Session["Filter"] = Filter_Code;
            Session["Sector"] = dd2;
            var data = db.Filters.SqlQuery("select * from Filters where Filter_Code=@p0", Filter_Code).ToList();

            foreach (var item in data)
            {
                ViewBag.allSec = item.No_Of_Sectors;


            }
            return View();
        }
        [Authorize(Roles = "Admin,Super")]
        public ActionResult IndexAdmin(string Filter_Code, int dd2)
        {
            string cDate = DateTime.Now.ToString("M/d/yyyy");
            ViewBag.sec = dd2;
            ViewBag.fil = Filter_Code;
            Session["sec"] = dd2;
            Session["fil"] = Filter_Code;
            var data = db.Filters.SqlQuery("select * from Filters where Filter_Code=@p0", Filter_Code).ToList();

            foreach (var item in data)
            {
                ViewBag.allSec = item.No_Of_Sectors;
                int[,] x = new int[item.Bags_Per_Valve + 1, item.Sector_No_Valves + 1];

                for (int i = 1; i <= item.Bags_Per_Valve; i++)
                {
                    for (int j = 1; j <= item.Sector_No_Valves; j++)
                    {

                        var data1 = db.Bag_Replacement.SqlQuery("select * from Bag_Replacement where Filter_Code=@p0 AND Sector_No=@p1 AND Bag_No=@p2  AND Valve_no=@p3 ", Filter_Code, dd2, i, j).ToList();
                        foreach (var item2 in data1)
                        {
                            if (item2.Create_Date.ToString("M/d/yyyy") == DateTime.Now.ToString("M/d/yyyy"))
                            {
                                x[i, j] = 1;
                            }
                        }
                        data1.Clear();
                    }
                }
                ViewBag.x = x;

            }

            return View(data);
        }
        [Authorize(Roles = "Admin,Super")]
        public ActionResult Click(string id, int sec, string button)

        {
            var dimensions = Session["Dimensions"];
            var supplier = Session["Supplier"];
            var material = Session["Bag_Type"];
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand com = new SqlCommand("Select Material_Code from Material_Types Where Material_Description=@mat", con);
            com.Parameters.AddWithValue("@mat", material);
            string mat = "";
            string sup = "";
            using (SqlDataReader reader = com.ExecuteReader())
            {
                if (reader.Read())
                {

                    mat = String.Format("{0}", reader["Material_Code"]);
                }
            }
            com = new SqlCommand("Select Supplier_Code from Bag_Suppliers Where Supplier_Name=@sup", con);
            com.Parameters.AddWithValue("@sup", supplier);
            using (SqlDataReader reader = com.ExecuteReader())
            {
                if (reader.Read())
                {

                    sup = String.Format("{0}", reader["Supplier_Code"]);
                }
            }
            con.Close();

            List<object> lst = new List<object>();
            char[] delimiterChars = { ' ', ',', '.', ':', '\t' };
            string[] words = button.Split(delimiterChars);

            Session["Filter"] = id;
            Session["sector"] = sec;
            var bagno = words[0];
            var valveno = words[1];
            var repDate = Session["Replacement_Date"];

            string cDate = DateTime.Now.ToString("M/d/yyyy");
            lst.Add(repDate);
            lst.Add(id);
            lst.Add(sec);
            lst.Add(valveno);
            lst.Add(bagno);

            var reason = Session["Reason"];

            var data1 = db.Bag_Types.SqlQuery("select * from Bag_Types where Material_Code=@p0 and Supplier_Code=@p1 and Bag_Dimensions=@p2", mat, sup, dimensions).ToList();
            foreach (var item in data1)
            {
                lst.Add(item.Bag_Code);
                lst.Add(item.Bag_Cost);
            }

            var data2 = db.Replacement_Reasons.SqlQuery("select * from Replacement_Reasons where Replacement_Description=@p0", reason).ToList();
            foreach (var item in data2)
            {
                lst.Add(item.Replacement_Reason_Code);
            }
            lst.Add(cDate);
            object[] allitems = lst.ToArray();


            int output = db.Database.ExecuteSqlCommand("insert into Bag_Replacement(Repl_Date,Filter_Code,Sector_No,Valve_No,Bag_No,Bag_Code,Replacement_Reason_Code,Create_Date,Cost) values(@p0,@p1,@p2,@p3,@p4,@p5,@p7,@p8,@p6)", allitems);
            TempData["buttonval"] = id;
            TempData["sector"] = sec;

            return RedirectToAction("IndexAdmin", "FilterGraphic", new { @Filter_Code = id, @dd2 = sec });



        }

        public ActionResult UserClick(string id, string button, int sec)

        {
            char[] delimiterChars = { ' ', ',', '.', ':', '\t' };
            string[] words = button.Split(delimiterChars);


            var data = db.Bag_Replacement.SqlQuery("select * from Bag_Replacement where Filter_Code=@p0 AND Sector_No=@p1 AND Bag_No=@p2  AND Valve_no=@p3 ", id, sec, words[0], words[1]).ToList();
            if (data.Count < 1)
            {
                TempData["buttonval"] = "This bag has no replacements";
                return RedirectToAction("Index", "FilterGraphic", new { @Filter_Code = id, @dd2 = sec });
            }
            else
            {
                TempData["buttonval"] = id;
                return View(data);
            }
        }
        [Authorize(Roles = "Admin,Super")]
        public ActionResult Create()
        {
            Session["Role"] = "Admin";
            var materiallist = db.Material_Types.ToList();
            ViewBag.Material_Code = new SelectList(materiallist, "Material_Code", "Material_Description");
            var supplierlist = db.Bag_Suppliers.ToList();
            ViewBag.Supplier_Code = new SelectList(supplierlist, "Supplier_Code", "Supplier_Name");
            var fiberlist = db.Fiber_Brand.ToList();
            ViewBag.Fiber_Code = new SelectList(fiberlist, "Fiber_Code", "Fiber_Supplier");
            return View();
        }
        [HttpPost]
        public JsonResult GetBags()
        {

            var bags = new List<string>();

            var data = db.Bag_Types.SqlQuery("Select * From Bag_Types ").ToList();


            foreach (var item in data)
            {
                bags.Add(item.Material_Types.Material_Description);
            }



            return Json(bags, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetBags1()
        {

            List<string> bags = new List<string>();
            var data = db.Material_Types.SqlQuery("Select * From Material_Types ").ToList();

            foreach (var item2 in data)
            {
                bags.Add(item2.Material_Description);
            }




            return Json(bags, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetDimensions()
        {

            List<string> dimensions = new List<string>();
            var filter = Session["Filter"];


            var data = db.Filters.SqlQuery("Select * From Filters where Filter_Code=@p0",filter).ToList();

            foreach (var item2 in data)
            {
                dimensions.Add(item2.Dimensions);
            }




            return Json(dimensions, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetReason()
        {

            var reasons = new List<string>();

            var data = db.Replacement_Reasons.SqlQuery("Select * From Replacement_Reasons Where Category=1 or Category=5").ToList();


            foreach (var item in data)
            {
                reasons.Add(item.Replacement_Description);
            }



            return Json(reasons, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetReasonCage()
        {

            var reasons = new List<string>();

            var data = db.Replacement_Reasons.SqlQuery("Select * From Replacement_Reasons Where Category=2 or Category=5").ToList();


            foreach (var item in data)
            {
                reasons.Add(item.Replacement_Description);
            }



            return Json(reasons, JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "Admin,Super")]
        [HttpPost]
        public ActionResult Replacement(int[] values, string repDate, string bagType, string reason, string id, int sec, string supplier, string dimensions)
        {

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand com = new SqlCommand("Select Material_Code from Material_Types Where Material_Description=@mat", con);
            com.Parameters.AddWithValue("@mat", bagType);
            string mat = "";
            string sup = "";
            using (SqlDataReader reader = com.ExecuteReader())
            {
                if (reader.Read())
                {

                    mat = String.Format("{0}", reader["Material_Code"]);
                }
            }
            com = new SqlCommand("Select Supplier_Code from Bag_Suppliers Where Supplier_Name=@sup", con);
            com.Parameters.AddWithValue("@sup", supplier);
            using (SqlDataReader reader = com.ExecuteReader())
            {
                if (reader.Read())
                {

                    sup = String.Format("{0}", reader["Supplier_Code"]);
                }
            }
            con.Close();


           // Session["Role"] = "Admin";
            string cDate = DateTime.Now.ToString("M/d/yyyy");
            List<object> lst = new List<object>();


            var data1 = db.Bag_Types.SqlQuery("select * from Bag_Types where Material_Code=@p0 and Supplier_Code=@p1 and Bag_Dimensions=@p2", mat, sup, dimensions).ToList();
            foreach (var item in data1)
            {
                lst.Add(item.Bag_Cost);
                lst.Add(item.Bag_Code);
                
            }


            var data2 = db.Replacement_Reasons.SqlQuery("select * from Replacement_Reasons where Replacement_Description=@p0", reason).ToList();
            foreach (var item in data2)
            {
                lst.Add(item.Replacement_Reason_Code);
            }
            lst.Add(repDate);
            lst.Add(id);
            lst.Add(sec);
            lst.Add(cDate);
            var data = db.Filters.SqlQuery("select * from Filters where Filter_Code=@p0", id).ToList();
            foreach (var item in data)
            {

                int bags = item.Bags_Per_Valve;

                for (int i = 0; i < values.Length; i++)
                {
                    if (values[i] == 1)
                    {
                        for (int j = 1; j <= bags; j++)
                        {

                            lst.Add(i + 1);
                            lst.Add(j);
                            object[] allitems = lst.ToArray();
                            int output = db.Database.ExecuteSqlCommand("insert into Bag_Replacement(Repl_Date,Filter_Code,Sector_No,Valve_No,Bag_No,Bag_Code,Replacement_Reason_Code,Create_Date,Cost) values(@p3,@p4,@p5,@p7,@p8,@p1,@p2,@p6,@p0)", allitems);

                            lst.RemoveAt(8);
                            lst.RemoveAt(7);

                            if (output > 0)
                            {
                                ViewBag.msg = "Bag replacement is added";

                            }
                        }
                    }
                }
            }
            return RedirectToAction("IndexAdmin", "FilterGraphic", new { @Filter_Code = id, @dd2 = sec + 1 });
        }




        public ActionResult Cage(string Filter_Code, int dd2)
        {
            string cDate = DateTime.Now.ToString("M/d/yyyy");
            ViewBag.sec = dd2;
            ViewBag.fil = Filter_Code;
            var data = db.Filters.SqlQuery("select * from Filters where Filter_Code=@p0", Filter_Code).ToList();

            foreach (var item in data)
            {
                ViewBag.allSec = item.No_Of_Sectors;
                int[,] x = new int[item.Bags_Per_Valve + 1, item.Sector_No_Valves + 1];
                for (int i = 1; i <= item.Bags_Per_Valve; i++)
                {
                    for (int j = 1; j <= item.Sector_No_Valves; j++)
                    {

                        var data1 = db.Cages_Maintenance.SqlQuery("select * from Cages_Maintenance where Filter_Code=@p0 AND Sector_No=@p1 AND Bag_No=@p2  AND Valve_no=@p3 ", Filter_Code, dd2, i, j).ToList();
                        foreach (var item2 in data1)
                        {
                            if (item2.Create_Date.ToString("M/d/yyyy") == DateTime.Now.ToString("M/d/yyyy"))
                            {
                                x[i, j] = 1;
                            }
                        }
                        data1.Clear();
                    }
                }
                ViewBag.x = x;

            }

            return View(data);
        }

        public ActionResult CageReplacement(int[] values, string repDate, string reason, string id, int sec)
        {
            List<object> lst = new List<object>();
            string cDate = DateTime.Now.ToString("M/d/yyyy");
            var data2 = db.Replacement_Reasons.SqlQuery("select * from Replacement_Reasons where Replacement_Description=@p0", reason).ToList();
            foreach (var item in data2)
            {
                lst.Add(item.Replacement_Reason_Code);
            }
            lst.Add(repDate);
            lst.Add(id);
            lst.Add(sec);
            lst.Add(cDate);

            var data = db.Filters.SqlQuery("select * from Filters where Filter_Code=@p0", id).ToList();
            foreach (var item in data)
            {

                int bags = item.Bags_Per_Valve;

                for (int i = 0; i < values.Length; i++)
                {
                    if (values[i] == 1)
                    {
                        for (int j = 1; j <= bags; j++)
                        {

                            lst.Add(i + 1);
                            lst.Add(j);
                            object[] allitems = lst.ToArray();
                            int output = db.Database.ExecuteSqlCommand("insert into Cages_Maintenance(Date,Filter_Code,Replacement_Reason_Code,Sector_No,Valve_No,Bag_No,Create_Date) values(@p1,@p2,@p0,@p3,@p5,@p6,@p4)", allitems);
                            lst.RemoveAt(6);
                            lst.RemoveAt(5);

                            if (output > 0)
                            {
                                ViewBag.msg = "Bag replacement is added";

                            }
                        }
                    }
                }
            }
            return RedirectToAction("Cage", "FilterGraphic", new { @Filter_Code = id, @dd2 = sec + 1 });
        }
        public ActionResult CageClick(string id, string button, int sec)

        {

            List<object> lst = new List<object>();
            char[] delimiterChars = { ' ', ',', '.', ':', '\t' };
            string[] words = button.Split(delimiterChars);

            Session["Filter"] = id;
            Session["sector"] = sec;
            var bagno = words[0];
            var valveno = words[1];
            var repDate = Session["Replacement_Date"];

            string cDate = DateTime.Now.ToString("M/d/yyyy");
            lst.Add(repDate);
            lst.Add(id);
            lst.Add(sec);
            lst.Add(valveno);
            lst.Add(bagno);

            var reason = Session["Reason"];

            var data2 = db.Replacement_Reasons.SqlQuery("select * from Replacement_Reasons where Replacement_Description=@p0", reason).ToList();
            foreach (var item in data2)
            {
                lst.Add(item.Replacement_Reason_Code);
            }
            lst.Add(cDate);
            object[] allitems = lst.ToArray();


            int output = db.Database.ExecuteSqlCommand("insert into Cages_Maintenance(Date,Filter_Code,Replacement_Reason_Code,Sector_No,Valve_No,Bag_No,Create_Date) values(@p0,@p1,@p5,@p2,@p3,@p4,@p6)", allitems);
            TempData["buttonval"] = id;
            TempData["sector"] = sec;

            return RedirectToAction("Cage", "FilterGraphic", new { @Filter_Code = id, @dd2 = sec });




        }
        [HttpPost]
        public void AddToSession(string repDate, string bagType, string reason, string supplier, string dimensions)
        {
            Session["Replacement_Date"] = repDate;
            Session["Bag_Type"] = bagType;
            Session["Reason"] = reason;
            Session["Supplier"] = supplier;
            Session["dimensions"] = dimensions;
        }
        [HttpPost]
        public ActionResult dateChange(string value)
        {
            Session["Replacement_Date"] = value;
            return RedirectToAction("IndexAdmin", "FilterGraphic", new { @Filter_Code = Session["Filter"], @dd2 = Session["sector"] });
        }
        public ActionResult bagChange(string value)
        {
            Session["Bag_Type"] = value;
            return RedirectToAction("IndexAdmin", "FilterGraphic", new { @Filter_Code = Session["Filter"], @dd2 = Session["sector"] });
        }
        public ActionResult reasonChange(string value)
        {
            Session["Reason"] = value;
            return RedirectToAction("IndexAdmin", "FilterGraphic", new { @Filter_Code = Session["Filter"], @dd2 = Session["sector"] });
        }
        public ActionResult supplierChange(string value)
        {
            Session["Supplier"] = value;
            return RedirectToAction("IndexAdmin", "FilterGraphic", new { @Filter_Code = Session["Filter"], @dd2 = Session["sector"] });
        }
        public ActionResult dimensionsChange(string value)
        {
            Session["Dimensions"] = value;
            return RedirectToAction("IndexAdmin", "FilterGraphic", new { @Filter_Code = Session["Filter"], @dd2 = Session["sector"] });
        }
        public JsonResult GetSuppliers(string dimensions,string material)
        {

           
           
            var baglst = new List<int>();
            var data = db.Material_Types.SqlQuery("Select * From Material_Types Where Material_Description=@p0", material).ToList();
            foreach(var item in data)
            {
               var data2 = db.Bag_Types.SqlQuery("Select * From Bag_Types Where Bag_Dimensions=@p0 AND Material_Code=@p1", dimensions,item.Material_Code).ToList();
               foreach(var item2 in data2)
                {
                baglst.Add(item2.Supplier_Code);
                }
            }
            var suppliers = new HashSet<string>();
            for (int i = 0; i < baglst.Count; i++)
            {
                var data3 = db.Bag_Suppliers.SqlQuery("Select * From Bag_Suppliers Where Supplier_Code=@p0", baglst[i]).ToList();
                foreach(var item3 in data3)
                {
                    suppliers.Add(item3.Supplier_Name);
                }
            }
            
            return Json(suppliers, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSuppliers2()
        {
            var suppliers = new HashSet<string>();
           
                var data3 = db.Bag_Suppliers.SqlQuery("Select * From Bag_Suppliers").ToList();
                foreach (var item3 in data3)
                {
                    suppliers.Add(item3.Supplier_Name);
                }
            

            return Json(suppliers, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetColors()
        {

            var colors = new List<String>();

            var data = db.Bag_Suppliers.SqlQuery("Select * From Bag_Suppliers").ToList();

            foreach (var item in data)
            {
                colors.Add(item.Color);
            }

            return Json(colors, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(string id, int sec, string button)

        {

            List<object> lst = new List<object>();
            char[] delimiterChars = { ' ', ',', '.', ':', '\t' };
            string[] words = button.Split(delimiterChars);

            Session["Filter"] = id;
            Session["sector"] = sec;
            var bagno = words[0];
            var valveno = words[1];
            var repDate = Session["Replacement_Date"];

            string cDate = DateTime.Now.ToString("M/d/yyyy");
            lst.Add(repDate);
            lst.Add(id);
            lst.Add(sec);
            lst.Add(valveno);
            lst.Add(bagno);
            var bag = Session["Bag_Type"];
            var reason = Session["Reason"];

            lst.Add(cDate);
            object[] allitems = lst.ToArray();

            var data3 = db.Bag_Replacement.SqlQuery("select top 1 * from Bag_Replacement where Repl_Date=@p0 AND Filter_Code=@p1 AND Sector_No=@p2 AND Valve_No=@p3 AND Bag_No=@p4 AND Create_Date=@p5", allitems).ToList();
            int replaceID = 0;
            foreach (var item in data3)
            {
                replaceID = item.Replacement_Code;
            }

            int output = db.Database.ExecuteSqlCommand("delete from Bag_Replacement where Replacement_Code=@p0", replaceID);
            TempData["buttonval"] = id;
            TempData["sector"] = sec;

            return RedirectToAction("IndexAdmin", "FilterGraphic", new { @Filter_Code = id, @dd2 = sec });



        }
        public ActionResult CageDelete(string id, int sec, string button)

        {

            List<object> lst = new List<object>();
            char[] delimiterChars = { ' ', ',', '.', ':', '\t' };
            string[] words = button.Split(delimiterChars);

            Session["Filter"] = id;
            Session["sector"] = sec;
            var bagno = words[0];
            var valveno = words[1];
            var repDate = Session["Replacement_Date"];

            string cDate = DateTime.Now.ToString("M/d/yyyy");
            lst.Add(repDate);
            lst.Add(id);
            lst.Add(sec);
            lst.Add(valveno);
            lst.Add(bagno);


            lst.Add(cDate);
            object[] allitems = lst.ToArray();

            var data3 = db.Cages_Maintenance.SqlQuery("select top 1 * from Cages_Maintenance where Date=@p0 AND Filter_Code=@p1 AND Sector_No=@p2 AND Valve_No=@p3 AND Bag_No=@p4 AND Create_Date=@p5", allitems).ToList();
            int replaceID = 0;
            foreach (var item in data3)
            {
                replaceID = item.Cage_Code;
            }

            int output = db.Database.ExecuteSqlCommand("delete from Cages_Maintenance where Cage_Code=@p0", replaceID);
            TempData["buttonval"] = id;
            TempData["sector"] = sec;

            return RedirectToAction("Cage", "FilterGraphic", new { @Filter_Code = id, @dd2 = sec });



        }
    }
}
