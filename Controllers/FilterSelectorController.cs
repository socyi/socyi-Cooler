using Cooler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Web;
using System.Web.Mvc;
using Filter = Cooler.Models.Filter;

namespace Cooler.Controllers
{
    [Authorize]
    public class FilterSelectorController : Controller
    {
        DataContext db = new DataContext();
        [Authorize(Roles = "Admin,Super")]
        public ActionResult Admin()
        {
            Session["Role"] = "Admin";
            var filterList = db.Filters.ToList();
            ViewBag.Filter_Code = new SelectList(filterList, "Filter_Code", "Filter_Code");

            return View();
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

        public ActionResult User()

        {
           
            var filterList = db.Filters.ToList();
            ViewBag.Filter_Code = new SelectList(filterList, "Filter_Code", "Filter_Code");

            return View();
        }
        [HttpPost]
        public JsonResult GetSectors(string filter)
        {

            var sectors = new List<int>();
            if (!string.IsNullOrWhiteSpace(filter))
            {
                var data = db.Filters.SqlQuery("Select * From Filters Where Filter_Code=@p0"
                        , filter).ToList();
                int num = 0;
                foreach (var item in data)
                {
                    num = item.No_Of_Sectors;
                }

                for (int i = 0; i < num; i++)
                {
                    sectors.Add(i + 1);
                }
            }
            return Json(sectors, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetFilter(string filter)
        {
            var filterxy = new List<int>();
            if (!string.IsNullOrWhiteSpace(filter))
            {
                var data = db.Filters.SqlQuery("Select * From Filters Where Filter_Code=@p0"
                       , filter).ToList();
                int x = 0;
                int y = 0;
                int i = 0;
                int j = 0;
                foreach (var item in data)
                {
                    i = item.Bags_Per_Valve;
                    j = item.Sector_No_Valves;
                    x = item.X;
                    y = item.Y;
                }

                filterxy.Add(x);
                filterxy.Add(y);
                filterxy.Add(i);
                filterxy.Add(j);

                int sec = 0;
                String[,] colors = new String[i + 1, j + 1];
                for (int q = 1; q <= x; q++)
                {
                    for (int r = 1; r <= y; r++)
                    {
                        sec++;
                        for (int w = 1; w <= i; w++)
                        {
                            for (int e = 1; e <= j; e++)
                            {
                                var data1 = db.Bag_Replacement.SqlQuery("select * from Bag_Replacement where Filter_Code=@p0 AND Sector_No=@p1 AND Bag_No=@p2  AND Valve_no=@p3 ", filter, sec, w, e).ToList();
                                int bcode = 0;

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
                                        string tempColor = item3.Color.Trim();

                                        int tempCode = 0;
                                        switch (tempColor)
                                        {
                                            case "red":
                                                tempCode = 1;
                                                filterxy.Add(1);
                                                break;
                                            case "blue":
                                                filterxy.Add(2);
                                                break;
                                            case "moccasin":
                                                filterxy.Add(3);
                                                break;
                                            case "yellow":
                                                filterxy.Add(4);
                                                break;
                                            case "purple":
                                                filterxy.Add(5);
                                                break;
                                            case "aqua":
                                                filterxy.Add(6);
                                                break;
                                            case "pink":
                                                filterxy.Add(7);
                                                break;
                                            case "orange":
                                                filterxy.Add(8);
                                                break;
                                            case "lime":
                                                filterxy.Add(9);
                                                break;
                                            case "black":
                                                filterxy.Add(10);
                                                break;
                                            default:
                                                filterxy.Add(1);
                                                break;
                                        }
                                    }


                                }
                                else
                                {
                                    filterxy.Add(0);
                                }
                                data1.Clear();
                            }
                        }

                    }
                }

            }

            return Json(filterxy, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetFilterDate(string filter,string date)
        {
            var filterxy = new List<int>();
            if (!string.IsNullOrWhiteSpace(filter))
            {
                var data = db.Filters.SqlQuery("Select * From Filters Where Filter_Code=@p0"
                       , filter).ToList();
                int x = 0;
                int y = 0;
                int i = 0;
                int j = 0;
                foreach (var item in data)
                {
                    i = item.Bags_Per_Valve;
                    j = item.Sector_No_Valves;
                    x = item.X;
                    y = item.Y;
                }

                filterxy.Add(x);
                filterxy.Add(y);
                filterxy.Add(i);
                filterxy.Add(j);

                int sec = 0;
                String[,] colors = new String[i + 1, j + 1];
                for (int q = 1; q <= x; q++)
                {
                    for (int r = 1; r <= y; r++)
                    {
                        sec++;
                        for (int w = 1; w <= i; w++)
                        {
                            for (int e = 1; e <= j; e++)
                            {
                                var data1 = db.Bag_Replacement.SqlQuery("select * from Bag_Replacement where Filter_Code=@p0 AND Sector_No=@p1 AND Bag_No=@p2  AND Valve_no=@p3 AND Repl_Date<=@p4", filter, sec, w, e,date).ToList();
                                int bcode = 0;

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
                                        string tempColor = item3.Color.Trim();

                                        int tempCode = 0;
                                        switch (tempColor)
                                        {
                                            case "red":
                                                tempCode = 1;
                                                filterxy.Add(1);
                                                break;
                                            case "blue":
                                                filterxy.Add(2);
                                                break;
                                            case "moccasin":
                                                filterxy.Add(3);
                                                break;
                                            case "yellow":
                                                filterxy.Add(4);
                                                break;
                                            case "purple":
                                                filterxy.Add(5);
                                                break;
                                            case "aqua":
                                                filterxy.Add(6);
                                                break;
                                            case "pink":
                                                filterxy.Add(7);
                                                break;
                                            case "orange":
                                                filterxy.Add(8);
                                                break;
                                            case "lime":
                                                filterxy.Add(9);
                                                break;
                                            case "black":
                                                filterxy.Add(10);
                                                break;
                                            default:
                                                filterxy.Add(1);
                                                break;
                                        }
                                    }


                                }
                                else
                                {
                                    filterxy.Add(0);
                                }
                                data1.Clear();
                            }
                        }

                    }
                }

            }

            return Json(filterxy, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetFilterAdmin(string filter)
        {
            var filterxy = new List<int>();
            if (!string.IsNullOrWhiteSpace(filter))
            {
                var data = db.Filters.SqlQuery("Select * From Filters Where Filter_Code=@p0", filter).ToList();
                int x = 0;
                int y = 0;
                int i = 0;
                int j = 0;
               
                foreach (var item in data)
                {
                    i = item.Bags_Per_Valve;
                    j = item.Sector_No_Valves;
                    x = item.X;
                    y = item.Y;
                }
                string cDate = DateTime.Now.ToString("M/d/yyyy");
                filterxy.Add(x);
                filterxy.Add(y);
                filterxy.Add(i);
                filterxy.Add(j);

                int sec = 0;
                
                for (int q = 1; q <= x; q++)
                {
                    for (int r = 1; r <= y; r++)
                    {
                        sec++;
                        for (int w = 1; w <= i; w++)
                        {
                            for (int e = 1; e <= j; e++)
                            {
                                var data1 = db.Bag_Replacement.SqlQuery("select * from Bag_Replacement where Filter_Code=@p0 AND Sector_No=@p1 AND Bag_No=@p2  AND Valve_no=@p3 AND Create_Date=@p4 ", filter, sec, w, e, cDate).ToList();
                               
                              
                               
                               

                                if (data1.Count != 0)
                                {

                                    filterxy.Add(1);


                                }
                                else
                                {
                                    filterxy.Add(0);
                                }
                                data1.Clear();
                            }
                        }

                    }
                }

            }

            return Json(filterxy, JsonRequestBehavior.AllowGet);
        }


    }
}