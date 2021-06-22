using Cooler.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Security.Cryptography.Pkcs;
using System.Globalization;

namespace Cooler.Controllers
{
    [Authorize]
    public class ReportsController : Controller
    {
        string connectionString = "Data Source=localhost; Initial Catalog = VassilikoFilters; Integrated Security = True; MultipleActiveResultSets=True; ";
        DataContext db = new DataContext();
        // GET: Reports

        public ActionResult CoolerReports()
        {
            return View();
        }
        public ActionResult IndexAdmin()
        {
            return View();
        }
        public ActionResult Query1()
        {
            DataTable d = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter("select * from Bag_Replacement ", con);

                da.Fill(d);
            }
            return View(d);
        }
        public ActionResult Query2()
        {
            return View();
        }
        public ActionResult Query3()
        {
            return View();
        }
        public ActionResult Query4()
        {
            return View();
        }
        public ActionResult Query5()
        {
            DataTable d = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter("select * from Filters ", con);

                da.Fill(d);
            }
            return View(d);
        }
        public ActionResult Query6()
        {
            return View();
        }
        public ActionResult Query7()
        {
            return View();
        }
        public ActionResult Query8()
        {
            return View();
        }
        public ActionResult Query9()
        {
            return View();
        }
        public ActionResult Query10()
        {
            return View();
        }
        public JsonResult GetDepartments()
        {

            var departments = new List<String>();

            var data = db.Departments.SqlQuery("Select * From Departments").ToList();

            foreach (var item in data)
            {
                departments.Add(item.Department_Code);
            }

            return Json(departments, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetFilters(string department)
        {

            var filters = new List<string>();
            if (!string.IsNullOrWhiteSpace(department))
            {
                var data = db.Filters.SqlQuery("Select * From Filters Where Department_Code=@p0"
                        , department).ToList();

                foreach (var item in data)
                {
                    filters.Add(item.Filter_Code);
                }


            }
            return Json(filters, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSuppliers()
        {

            var suppliers = new List<String>();

            var data = db.Bag_Suppliers.SqlQuery("Select * From Bag_Suppliers").ToList();

            foreach (var item in data)
            {
                suppliers.Add(item.Supplier_Name);
            }

            return Json(suppliers, JsonRequestBehavior.AllowGet);
        }
        public JsonResult q1(string department, string filter, string supplier, string from, string to)
        {
            bool all = filter.Equals("All Filters");
            bool alls = supplier.Equals("All Suppliers");
            DataTable d = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                List<string> a = new List<string>();

                if (!all && !alls)
                {
                    SqlDataAdapter da = new SqlDataAdapter("select br.Filter_Code,br.Repl_Date,mt.Material_Description,bt.Bag_Dimensions,bs.Supplier_Name,COUNT(*) AS 'Bags Replaced',SUM(br.Cost) AS 'Cost' from Bag_Replacement br INNER JOIN Bag_Types bt ON br.Bag_Code = bt.Bag_Code INNER JOIN Bag_Suppliers bs ON bs.Supplier_Code = bt.Supplier_Code INNER JOIN Filters f ON f.Filter_Code = br.Filter_Code INNER JOIN Material_Types mt ON mt.Material_Code= bt.Material_Code WHERE f.Department_Code = @department AND br.Repl_Date BETWEEN @from AND @to AND br.Filter_Code = @filter AND bs.Supplier_Name=@supplier GROUP BY br.Filter_Code,br.Repl_Date,Supplier_Name,mt.Material_Description,bt.Bag_Dimensions ORDER BY br.Repl_Date DESC", con);

                    da.SelectCommand.Parameters.AddWithValue("@department", department);
                    da.SelectCommand.Parameters.AddWithValue("@from", from);
                    da.SelectCommand.Parameters.AddWithValue("@to", to);
                    da.SelectCommand.Parameters.AddWithValue("@filter", filter);
                    da.SelectCommand.Parameters.AddWithValue("@supplier", supplier);
                    da.Fill(d);

                    for (int i = 0; i < d.Rows.Count; i++)
                    {
                        DataRow row = d.Rows[i];
                        a.Add(row["Filter_Code"].ToString());
                        a.Add(row["Repl_Date"].ToString());
                        a.Add(row["Material_Description"].ToString());
                        a.Add(row["Bag_Dimensions"].ToString());
                        a.Add(row["Supplier_Name"].ToString());
                        a.Add(row["Bags Replaced"].ToString());
                        a.Add(row["Cost"].ToString());

                    }
                }
                else if (all && !alls)
                {
                    var data = db.Filters.SqlQuery("Select * From Filters Where Department_Code=@p0", department).ToList();
                    SqlDataAdapter da;
                    foreach (var item in data)
                    {
                        da = new SqlDataAdapter("select br.Filter_Code,br.Repl_Date,mt.Material_Description,bt.Bag_Dimensions,bs.Supplier_Name,COUNT(*) AS 'Bags Replaced',SUM(bt.Bag_Cost) AS 'Cost' from Bag_Replacement br INNER JOIN Bag_Types bt ON br.Bag_Code = bt.Bag_Code INNER JOIN Bag_Suppliers bs ON bs.Supplier_Code = bt.Supplier_Code INNER JOIN Filters f ON f.Filter_Code = br.Filter_Code INNER JOIN Material_Types mt ON mt.Material_Code= bt.Material_Code WHERE f.Department_Code = @department AND br.Repl_Date BETWEEN @from AND @to AND br.Filter_Code = @filter AND bs.Supplier_Name=@supplier GROUP BY br.Filter_Code,br.Repl_Date,Supplier_Name,mt.Material_Description,bt.Bag_Dimensions ORDER BY br.Repl_Date DESC", con);
                        da.SelectCommand.Parameters.AddWithValue("@department", department);
                        da.SelectCommand.Parameters.AddWithValue("@from", from);
                        da.SelectCommand.Parameters.AddWithValue("@to", to);
                        da.SelectCommand.Parameters.AddWithValue("@filter", item.Filter_Code);
                        da.SelectCommand.Parameters.AddWithValue("@supplier", supplier);
                        da.Fill(d);
                    }


                    for (int i = 0; i < d.Rows.Count; i++)
                    {
                        DataRow row = d.Rows[i];
                        a.Add(row["Filter_Code"].ToString());
                        a.Add(row["Repl_Date"].ToString());
                        a.Add(row["Material_Description"].ToString());
                        a.Add(row["Bag_Dimensions"].ToString());
                        a.Add(row["Supplier_Name"].ToString());
                        a.Add(row["Bags Replaced"].ToString());
                        a.Add(row["Cost"].ToString());

                    }


                }
                else if (!all && alls)
                {
                    var data = db.Bag_Suppliers.SqlQuery("Select * From Bag_Suppliers").ToList();
                    SqlDataAdapter da;
                    foreach (var item in data)
                    {
                        da = new SqlDataAdapter("select br.Filter_Code,br.Repl_Date,mt.Material_Description,bt.Bag_Dimensions,bs.Supplier_Name,COUNT(*) AS 'Bags Replaced',SUM(bt.Bag_Cost) AS 'Cost' from Bag_Replacement br INNER JOIN Bag_Types bt ON br.Bag_Code = bt.Bag_Code INNER JOIN Bag_Suppliers bs ON bs.Supplier_Code = bt.Supplier_Code INNER JOIN Filters f ON f.Filter_Code = br.Filter_Code INNER JOIN Material_Types mt ON mt.Material_Code= bt.Material_Code WHERE f.Department_Code = @department AND br.Repl_Date BETWEEN @from AND @to AND br.Filter_Code = @filter AND bs.Supplier_Name=@supplier GROUP BY br.Filter_Code,br.Repl_Date,Supplier_Name,mt.Material_Description,bt.Bag_Dimensions ORDER BY br.Repl_Date DESC", con);
                        da.SelectCommand.Parameters.AddWithValue("@department", department);
                        da.SelectCommand.Parameters.AddWithValue("@from", from);
                        da.SelectCommand.Parameters.AddWithValue("@to", to);
                        da.SelectCommand.Parameters.AddWithValue("@filter", filter);
                        da.SelectCommand.Parameters.AddWithValue("@supplier", item.Supplier_Name);
                        da.Fill(d);
                    }


                    for (int i = 0; i < d.Rows.Count; i++)
                    {
                        DataRow row = d.Rows[i];
                        a.Add(row["Filter_Code"].ToString());
                        a.Add(row["Repl_Date"].ToString());
                        a.Add(row["Material_Description"].ToString());
                        a.Add(row["Bag_Dimensions"].ToString());
                        a.Add(row["Supplier_Name"].ToString());
                        a.Add(row["Bags Replaced"].ToString());
                        a.Add(row["Cost"].ToString());

                    }


                }
                else
                {
                    var data = db.Filters.SqlQuery("Select  * From Filters Where Department_Code=@p0", department).ToList();
                    SqlDataAdapter da;
                    foreach (var item in data)
                    {
                        var data1 = db.Bag_Suppliers.SqlQuery("Select * From Bag_Suppliers").ToList();
                        foreach (var item1 in data1)
                        {
                            da = new SqlDataAdapter("select br.Filter_Code,br.Repl_Date,mt.Material_Description,bt.Bag_Dimensions,bs.Supplier_Name,COUNT(*) AS 'Bags Replaced',SUM(bt.Bag_Cost) AS 'Cost' from Bag_Replacement br INNER JOIN Bag_Types bt ON br.Bag_Code = bt.Bag_Code INNER JOIN Bag_Suppliers bs ON bs.Supplier_Code = bt.Supplier_Code INNER JOIN Filters f ON f.Filter_Code = br.Filter_Code INNER JOIN Material_Types mt ON mt.Material_Code= bt.Material_Code WHERE f.Department_Code = @department AND br.Repl_Date BETWEEN @from AND @to AND br.Filter_Code = @filter AND bs.Supplier_Name=@supplier GROUP BY br.Filter_Code,br.Repl_Date,Supplier_Name,mt.Material_Description,bt.Bag_Dimensions ORDER BY br.Repl_Date DESC", con);
                            da.SelectCommand.Parameters.AddWithValue("@department", department);
                            da.SelectCommand.Parameters.AddWithValue("@from", from);
                            da.SelectCommand.Parameters.AddWithValue("@to", to);
                            da.SelectCommand.Parameters.AddWithValue("@filter", item.Filter_Code);
                            da.SelectCommand.Parameters.AddWithValue("@supplier", item1.Supplier_Name);
                            da.Fill(d);
                        }

                    }
                    for (int i = 0; i < d.Rows.Count; i++)
                    {
                        DataRow row = d.Rows[i];
                        a.Add(row["Filter_Code"].ToString());
                        a.Add(row["Repl_Date"].ToString());
                        a.Add(row["Material_Description"].ToString());
                        a.Add(row["Bag_Dimensions"].ToString());
                        a.Add(row["Supplier_Name"].ToString());
                        a.Add(row["Bags Replaced"].ToString());
                        a.Add(row["Cost"].ToString());
                    }


                }



                return Json(a, JsonRequestBehavior.AllowGet);
            }
        }



        public JsonResult q5(int top, string from, string to)
        {
            DataTable d = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                List<string> a = new List<string>();

                SqlDataAdapter da = new SqlDataAdapter("select TOP " + top + "f.Department_Code, f.Filter_Code ,COUNT(DISTINCT br.Repl_Date) AS 'Times Replaced',SUM(br.Cost) AS 'Cost' from Bag_Replacement br INNER JOIN Bag_Types bt ON br.Bag_Code = bt.Bag_Code INNER JOIN Bag_Suppliers bs ON bs.Supplier_Code = bt.Supplier_Code INNER JOIN Filters f ON f.Filter_Code = br.Filter_Code WHERE br.Repl_Date BETWEEN @from AND @to GROUP BY f.Department_Code,f.Filter_Code ORDER BY[Times Replaced] DESC", con);
                da.SelectCommand.Parameters.AddWithValue("@from", from);
                da.SelectCommand.Parameters.AddWithValue("@to", to);
                da.Fill(d);

                for (int i = 0; i < d.Rows.Count; i++)
                {
                    DataRow row = d.Rows[i];
                    a.Add(row["Department_Code"].ToString());
                    a.Add(row["Filter_Code"].ToString());
                    a.Add(row["Times Replaced"].ToString());
                    a.Add(row["Cost"].ToString());
                }


                return Json(a, JsonRequestBehavior.AllowGet);

            }
        }

        public JsonResult q3(string supplier, string from, string to)
        {
            bool all = supplier.Equals("All Suppliers");
            DataTable d = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                List<string> a = new List<string>();
                if (!all)
                {
                    SqlDataAdapter da = new SqlDataAdapter("select bs.Supplier_Name, br.Repl_Date,br.Filter_Code,COUNT(*) AS 'Bags Replaced' from Bag_Replacement br INNER JOIN Bag_Types bt ON br.Bag_Code = bt.Bag_Code INNER JOIN Bag_Suppliers bs ON bs.Supplier_Code = bt.Supplier_Code WHERE bs.Supplier_Name=@supplier AND br.Repl_Date BETWEEN @from AND @to GROUP BY bs.Supplier_Name,br.Repl_Date,br.Filter_Code", con);
                    da.SelectCommand.Parameters.AddWithValue("@supplier", supplier);
                    da.SelectCommand.Parameters.AddWithValue("@from", from);
                    da.SelectCommand.Parameters.AddWithValue("@to", to);

                    da.Fill(d);

                    for (int i = 0; i < d.Rows.Count; i++)
                    {
                        DataRow row = d.Rows[i];
                        a.Add(row["Supplier_Name"].ToString());
                        a.Add(row["Repl_Date"].ToString());
                        a.Add(row["Filter_Code"].ToString());
                        a.Add(row["Bags Replaced"].ToString());
                    }
                }
                else
                {
                    var data = db.Bag_Suppliers.SqlQuery("Select * From Bag_Suppliers").ToList();
                    SqlDataAdapter da;
                    foreach (var item in data)
                    {
                        da = new SqlDataAdapter("select bs.Supplier_Name, br.Repl_Date,br.Filter_Code,COUNT(*) AS 'Bags Replaced' from Bag_Replacement br INNER JOIN Bag_Types bt ON br.Bag_Code = bt.Bag_Code INNER JOIN Bag_Suppliers bs ON bs.Supplier_Code = bt.Supplier_Code WHERE bs.Supplier_Name=@supplier AND br.Repl_Date BETWEEN @from AND @to GROUP BY bs.Supplier_Name,br.Repl_Date,br.Filter_Code", con);
                        da.SelectCommand.Parameters.AddWithValue("@supplier", item.Supplier_Name);
                        da.SelectCommand.Parameters.AddWithValue("@from", from);
                        da.SelectCommand.Parameters.AddWithValue("@to", to);
                        da.Fill(d);
                    }


                    for (int i = 0; i < d.Rows.Count; i++)
                    {
                        DataRow row = d.Rows[i];
                        a.Add(row["Supplier_Name"].ToString());
                        a.Add(row["Repl_Date"].ToString());
                        a.Add(row["Filter_Code"].ToString());
                        a.Add(row["Bags Replaced"].ToString());

                    }


                }

                return Json(a, JsonRequestBehavior.AllowGet);

            }
        }
        public JsonResult cq2( string from, string to)
        {
            DataTable d = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                List<string> a = new List<string>();
                var data = db.Compartments.SqlQuery("Select * From Compartments").ToList();

                foreach (var item in data)
                {

                        SqlDataAdapter da = new SqlDataAdapter("SELECT r.Compartment_ID,r.Nozle_no,r.Beam_no , COUNT(*)  AS 'Times Replaced' FROM Replacements r  WHERE r.Date BETWEEN @from AND @to AND r.Compartment_ID=@comp  GROUP BY r.Compartment_ID,r.Nozle_no,r.Beam_no order by  r.Compartment_ID,r.Nozle_no,r.Beam_no ", con);

                        da.SelectCommand.Parameters.AddWithValue("@from", from);
                        da.SelectCommand.Parameters.AddWithValue("@to", to);
                        da.SelectCommand.Parameters.AddWithValue("@comp", item.Compartment_ID);
                        da.Fill(d);
                    
                }
                for (int i = 0; i < d.Rows.Count; i++)
                {
                    DataRow row = d.Rows[i];
                    a.Add(row["Compartment_ID"].ToString());
                    a.Add(row["Nozle_no"].ToString());
                    a.Add(row["Beam_no"].ToString());
                    a.Add(row["Times Replaced"].ToString());
                }


                return Json(a, JsonRequestBehavior.AllowGet);

            }
        }
        public JsonResult q6(string department, string filter, string from, string to)
        {
            DataTable d = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                List<string> a = new List<string>();
                var data = db.Filters.SqlQuery("Select * From Filters Where Filter_Code=@p0", filter).ToList();

                foreach (var item in data)
                {
                    a.Add(item.Sector_No_Valves.ToString());
                    a.Add(item.Bags_Per_Valve.ToString());
                    for (int i = 1; i <= item.No_Of_Sectors; i++)
                    {
                        SqlDataAdapter da = new SqlDataAdapter("SELECT  br.Sector_No,br.Bag_No,br.Valve_No, COUNT(*)  AS 'Times Replaced' FROM Bag_Replacement br INNER JOIN Replacement_Reasons rr ON rr.Replacement_Reason_Code=br.Replacement_Reason_Code INNER JOIN Bag_Types bt ON bt.Bag_Code=br.Bag_Code INNER JOIN Bag_Suppliers bs ON bs.Supplier_Code=bt.Supplier_Code WHERE br.Repl_Date BETWEEN @from AND @to AND br.Filter_Code = @filter AND BR.Sector_No = @sector GROUP BY br.Sector_No,br.Bag_No,br.Valve_No order by Sector_No,Bag_No,Valve_No", con);

                        da.SelectCommand.Parameters.AddWithValue("@from", from);
                        da.SelectCommand.Parameters.AddWithValue("@to", to);
                        da.SelectCommand.Parameters.AddWithValue("@filter", filter);
                        da.SelectCommand.Parameters.AddWithValue("@sector", i);
                        da.Fill(d);
                    }
                }
                for (int i = 0; i < d.Rows.Count; i++)
                {
                    DataRow row = d.Rows[i];
                    a.Add(row["Sector_No"].ToString());
                    a.Add(row["Bag_No"].ToString());
                    a.Add(row["Valve_No"].ToString());
                    a.Add(row["Times Replaced"].ToString());
                  

                }


                return Json(a, JsonRequestBehavior.AllowGet);

            }
        }
        public JsonResult q8(string filter, string order)
        {
            DataTable d = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                List<string> a = new List<string>();
                HashSet<string> sectors = new HashSet<string>();
               
                var data = db.Filters.SqlQuery("Select * From Filters Where Filter_Code=@p0", filter).ToList();
                foreach (var items in data)
                {
                    SqlCommand com1 = new SqlCommand("select Sector_No, Repl_Date from Bag_Replacement where Filter_Code = @filter group by Sector_No,Repl_Date order by Repl_Date " + order, con);
                    com1.Parameters.AddWithValue("@filter", items.Filter_Code);
                    using (SqlDataReader reader = com1.ExecuteReader())
                    {
                        while (reader.Read())
                        {


                            sectors.Add(String.Format("{0}", reader["Sector_No"]));
                        }
                    }
                }
                String[] sectorsArray = sectors.ToArray();
                foreach (var item in data)
                {
                    a.Add(item.Sector_No_Valves.ToString());
                    a.Add(item.Bags_Per_Valve.ToString());


                    for (int i = 0; i < sectors.Count(); i++)
                    {
                        SqlCommand com = new SqlCommand("select top 1 Repl_Date from Bag_Replacement where Filter_Code=@filter and Sector_No=@sector order by Repl_Date " + order, con);
                        com.Parameters.AddWithValue("@filter", item.Filter_Code);
                        com.Parameters.AddWithValue("@sector", sectorsArray[i]);
                        var date = "";
                        string stopAt = " ";
                        using (SqlDataReader reader = com.ExecuteReader())
                        {
                            if (reader.Read())
                            {


                                date = String.Format("{0}", reader["Repl_Date"]);
                                if (!String.IsNullOrWhiteSpace(date))
                                {
                                    int charLocation = date.IndexOf(stopAt, StringComparison.Ordinal);

                                    if (charLocation > 0)
                                    {
                                        date = date.Substring(0, charLocation);
                                        date = date.Replace("/", "-");
                                        date = DateTime.ParseExact(date, "dd'-'MM'-'yyyy", CultureInfo.InvariantCulture).ToString("yyyy'-'MM'-'dd");
                                    }
                                }
                            }
                        }

                        SqlDataAdapter da = new SqlDataAdapter("SELECT br.Sector_No,br.Bag_No,br.Valve_No,br.Repl_Date,bs.Supplier_Name,mt.Material_Description FROM Bag_Replacement br INNER JOIN Bag_Types bt ON bt.Bag_Code = br.Bag_Code INNER JOIN Bag_Suppliers bs ON bt.Supplier_Code = bs.Supplier_Code INNER JOIN Material_Types mt ON mt.Material_Code=bt.Material_Code WHERE br.Filter_Code =@filter AND Sector_No =@sector AND Repl_Date =@date GROUP BY br.Sector_No,br.Bag_No,br.Valve_No,br.Repl_Date,bs.Supplier_Name,mt.Material_Description ORDER BY Repl_Date " + order, con);
                        da.SelectCommand.Parameters.AddWithValue("@filter", filter);
                        da.SelectCommand.Parameters.AddWithValue("@sector", sectorsArray[i]);
                        da.SelectCommand.Parameters.AddWithValue("@date", date);
                        da.Fill(d);
                    }
                }
                for (int i = 0; i < d.Rows.Count; i++)
                {
                    DataRow row = d.Rows[i];
                    a.Add(row["Sector_No"].ToString());
                    a.Add(row["Bag_No"].ToString());
                    a.Add(row["Valve_No"].ToString());
                    a.Add(row["Repl_Date"].ToString());
                    a.Add(row["Supplier_Name"].ToString());
                    a.Add(row["Material_Description"].ToString());

                }


                return Json(a, JsonRequestBehavior.AllowGet);

            }
        }
        public JsonResult q9(string department, string filter, int top)
        {
            DataTable d = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                List<string> a = new List<string>();

                SqlDataAdapter da = new SqlDataAdapter("SELECT TOP " + top + "br.Sector_No,br.Repl_Date FROM Bag_Replacement br WHERE br.Filter_Code = @filter GROUP BY Sector_No, Repl_Date ORDER BY Repl_Date", con);
                da.SelectCommand.Parameters.AddWithValue("@filter", filter);

                da.Fill(d);

                for (int i = 0; i < d.Rows.Count; i++)
                {
                    DataRow row = d.Rows[i];
                    a.Add(row["Sector_No"].ToString());
                    a.Add(row["Repl_Date"].ToString());

                }


                return Json(a, JsonRequestBehavior.AllowGet);

            }
        }


        public JsonResult q7(string department, string filter, int top, string from, string to)
        {
            DataTable d = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                List<string> a = new List<string>();
                int x = 0;
                int y = 0;
                int bags = 0;
                int valves = 0;
                var data = db.Filters.SqlQuery("Select * from filters where Filter_Code =@p0", filter);
                foreach (var item in data)
                {
                    x = item.X;
                    y = item.Y;
                    bags = item.Bags_Per_Valve;
                    valves = item.Sector_No_Valves;
                }
                a.Add(x.ToString());
                a.Add(y.ToString());
                a.Add(bags.ToString());
                a.Add(valves.ToString());


                con.Open();

                SqlDataAdapter da = new SqlDataAdapter("SELECT TOP " + top + " br.Bag_No,br.Valve_No ,br.Sector_No,COUNT(*) AS 'Times Replaced' FROM Bag_Replacement br WHERE br.Repl_Date BETWEEN @from AND @to AND br.Filter_Code = @filter GROUP BY br.Sector_No,br.Valve_No ,br.Bag_No ORDER BY[Times Replaced] DESC", con);
                da.SelectCommand.Parameters.AddWithValue("@filter", filter);
                da.SelectCommand.Parameters.AddWithValue("@from", from);
                da.SelectCommand.Parameters.AddWithValue("@to", to);

                da.Fill(d);

                for (int i = 0; i < d.Rows.Count; i++)
                {
                    DataRow row = d.Rows[i];
                    a.Add(row["Bag_No"].ToString());
                    a.Add(row["Valve_No"].ToString());
                    a.Add(row["Sector_No"].ToString());
                    a.Add(row["Repl_Date"].ToString());

                }

                return Json(a, JsonRequestBehavior.AllowGet);

            }
        }
        public JsonResult GetReasons()
        {

            var reasons = new List<String>();

            var data = db.Replacement_Reasons.SqlQuery("Select * From Replacement_Reasons Where Category=1 or Category=5").ToList();

            foreach (var item in data)
            {
                reasons.Add(item.Replacement_Description);
            }

            return Json(reasons, JsonRequestBehavior.AllowGet);
        }
        public JsonResult q2(string reason, string from, string to)
        {

            {
                bool all = reason.Equals("All Reasons");
                DataTable d = new DataTable();
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    List<string> a = new List<string>();

                    if (!all)
                    {
                        SqlDataAdapter da = new SqlDataAdapter("select rr.Replacement_Description,f.Department_Code, f.Filter_Code,br.Repl_Date,bs.Supplier_Name,mt.Material_Description,bt.Bag_Dimensions,COUNT(*) AS 'Bags Replaced',SUM(br.Cost) AS 'COST' from Bag_Replacement br INNER JOIN Bag_Types bt ON br.Bag_Code = bt.Bag_Code INNER JOIN Material_Types mt ON mt.Material_Code = bt.Material_Code INNER JOIN Bag_Suppliers bs ON bs.Supplier_Code = bt.Supplier_Code INNER JOIN Filters f ON f.Filter_Code = br.Filter_Code INNER JOIN Replacement_Reasons rr ON rr.Replacement_Reason_Code = br.Replacement_Reason_Code WHERE Replacement_Description = @reason AND br.Repl_Date BETWEEN @from AND @to GROUP BY rr.Replacement_Description,f.Department_Code, f.Filter_Code,br.Repl_Date,bs.Supplier_Name,mt.Material_Description,bt.Bag_Dimensions ORDER BY f.Department_Code,br.Repl_Date,bs.Supplier_Name", con);
                        da.SelectCommand.Parameters.AddWithValue("@reason", reason);
                        da.SelectCommand.Parameters.AddWithValue("@from", from);
                        da.SelectCommand.Parameters.AddWithValue("@to", to);

                        da.Fill(d);

                        for (int i = 0; i < d.Rows.Count; i++)
                        {
                            DataRow row = d.Rows[i];
                            a.Add(row["Replacement_Description"].ToString());
                            a.Add(row["Department_Code"].ToString());
                            a.Add(row["Filter_Code"].ToString());
                            a.Add(row["Repl_Date"].ToString());
                            a.Add(row["Supplier_Name"].ToString());
                            a.Add(row["Material_Description"].ToString());
                            a.Add(row["Bag_Dimensions"].ToString());
                            a.Add(row["Bags Replaced"].ToString());
                            a.Add(row["Cost"].ToString());

                        }
                    }
                    else
                    {
                        var data = db.Replacement_Reasons.SqlQuery("Select * From Replacement_Reasons", reason).ToList();
                        SqlDataAdapter da;
                        foreach (var item in data)
                        {
                            da = new SqlDataAdapter("select rr.Replacement_Description,f.Department_Code, f.Filter_Code,br.Repl_Date,bs.Supplier_Name,mt.Material_Description,bt.Bag_Dimensions,COUNT(*) AS 'Bags Replaced',SUM(bt.Bag_Cost) AS 'COST' from Bag_Replacement br INNER JOIN Bag_Types bt ON br.Bag_Code = bt.Bag_Code INNER JOIN Material_Types mt ON mt.Material_Code = bt.Material_Code INNER JOIN Bag_Suppliers bs ON bs.Supplier_Code = bt.Supplier_Code INNER JOIN Filters f ON f.Filter_Code = br.Filter_Code INNER JOIN Replacement_Reasons rr ON rr.Replacement_Reason_Code = br.Replacement_Reason_Code WHERE Replacement_Description = @reason AND br.Repl_Date BETWEEN @from AND @to GROUP BY rr.Replacement_Description,f.Department_Code, f.Filter_Code,br.Repl_Date,bs.Supplier_Name,mt.Material_Description,bt.Bag_Dimensions ORDER BY f.Department_Code,br.Repl_Date,bs.Supplier_Name", con);
                            da.SelectCommand.Parameters.AddWithValue("@reason", item.Replacement_Description);
                            da.SelectCommand.Parameters.AddWithValue("@from", from);
                            da.SelectCommand.Parameters.AddWithValue("@to", to);
                            da.Fill(d);
                        }


                        for (int i = 0; i < d.Rows.Count; i++)
                        {
                            DataRow row = d.Rows[i];
                            a.Add(row["Replacement_Description"].ToString());
                            a.Add(row["Department_Code"].ToString());
                            a.Add(row["Filter_Code"].ToString());
                            a.Add(row["Repl_Date"].ToString());
                            a.Add(row["Supplier_Name"].ToString());
                            a.Add(row["Material_Description"].ToString());
                            a.Add(row["Bag_Dimensions"].ToString());
                            a.Add(row["Bags Replaced"].ToString());
                            a.Add(row["Cost"].ToString());

                        }


                    }
                    return Json(a, JsonRequestBehavior.AllowGet);

                }


            }

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
        public ActionResult Graphic(string filter, int top)
        {


            DataTable d = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();


                SqlDataAdapter da = new SqlDataAdapter("SELECT DISTINCT top " + top + " br.Bag_No,br.Valve_No ,br.Sector_No,br.Repl_Date FROM Bag_Replacement br WHERE br.Filter_Code = @filter GROUP BY Sector_No,Valve_No,Bag_No, Repl_Date ORDER BY Repl_Date", con);
                da.SelectCommand.Parameters.AddWithValue("@filter", filter);

                da.Fill(d);

                ViewBag.topCount = d.Rows.Count;

                List<int> sectorList = new List<int>();
                List<int> valveList = new List<int>();
                List<int> bagList = new List<int>();

                for (int i = 0; i < d.Rows.Count; i++)
                {
                    DataRow row = d.Rows[i];
                    sectorList.Add((int)row["sector_no"]);
                    valveList.Add((int)row["valve_no"]);
                    bagList.Add((int)row["bag_no"]);


                }
                ViewBag.sectorList = sectorList;
                ViewBag.valveList = valveList;
                ViewBag.bagList = bagList;
            }
            ViewBag.fil = filter;

            return View();
        }

        public ActionResult Graphic2(string filter, int top)
        {


            DataTable d = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();


                SqlDataAdapter da = new SqlDataAdapter("SELECT  top " + top + " br.Bag_No,br.Valve_No,br.Sector_No,COUNT(*) AS 'Times Replaced' FROM Bag_Replacement br WHERE br.Filter_Code = @filter GROUP BY Sector_No,Valve_No,Bag_No ORDER BY [Times Replaced] DESC", con);
                da.SelectCommand.Parameters.AddWithValue("@filter", filter);


                da.Fill(d);

                ViewBag.topCount = d.Rows.Count;

                List<int> sectorList = new List<int>();
                List<int> valveList = new List<int>();
                List<int> bagList = new List<int>();

                for (int i = 0; i < d.Rows.Count; i++)
                {
                    DataRow row = d.Rows[i];
                    sectorList.Add((int)row["sector_no"]);
                    valveList.Add((int)row["valve_no"]);
                    bagList.Add((int)row["bag_no"]);


                }
                ViewBag.sectorList = sectorList;
                ViewBag.valveList = valveList;
                ViewBag.bagList = bagList;
            }
            ViewBag.fil = filter;

            return View();
        }
        public JsonResult q4(string filter, string from, string to)
        {

            DataTable d = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                List<string> a = new List<string>();

                SqlDataAdapter da = new SqlDataAdapter("  select f.Department_Code,f.Filter_Code,SUM(bt.Bag_Cost) AS 'Cost' from Bag_Replacement br INNER JOIN Bag_Types bt ON br.Bag_Code = bt.Bag_Code INNER JOIN Bag_Suppliers bs ON bs.Supplier_Code = bt.Supplier_Code INNER JOIN Filters f ON f.Filter_Code = br.Filter_Code WHERE br.Repl_Date BETWEEN @from AND @to GROUP BY f.Department_Code, f.Filter_Code ORDER BY Cost  DESC", con);
                da.SelectCommand.Parameters.AddWithValue("@from", from);
                da.SelectCommand.Parameters.AddWithValue("@to", to);

                da.Fill(d);

                for (int i = 0; i < d.Rows.Count; i++)
                {
                    DataRow row = d.Rows[i];
                    a.Add(row["Department_Code"].ToString());
                    a.Add(row["Filter_Code"].ToString());
                    a.Add(row["Cost"].ToString());
                }

                return Json(a, JsonRequestBehavior.AllowGet);

            }


        }

        public ActionResult CQuery1()
        {
            var complist = db.Compartments.ToList();
            ViewBag.Compartment_ID = new SelectList(complist, "Compartment_ID", "Compartment_ID");
            return View();
        }
        public ActionResult CQuery2()
        {
            var complist = db.Compartments.ToList();
            ViewBag.Compartment_ID = new SelectList(complist, "Compartment_ID", "Compartment_ID");
            return View();
        }
        public ActionResult CQuery3()
        {
            var complist = db.Compartments.ToList();
            ViewBag.Compartment_ID = new SelectList(complist, "Compartment_ID", "Compartment_ID");
            return View();
        }

        public JsonResult cq1(string comp, string from, string to)
        {

            DataTable d = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                List<string> a = new List<string>();

                SqlDataAdapter da = new SqlDataAdapter("select DISTINCT R.Compartment_ID,r.Nozle_no,r.Beam_no,r.Date,ns.Description,ns.Cost,nos.Supplier from Replacements r INNER JOIN Nozle_Spares ns ON ns.Spare_ID = r.Spare_ID INNER JOIN Nozle_Suppliers nos ON ns.Supplier_ID = nos.Supplier_ID where r.Compartment_ID =@comp AND r.Date BETWEEN @from AND @to  GROUP BY R.Compartment_ID,R.Nozle_no,R.Beam_no,r.Date,ns.Description,ns.Cost,nos.Supplier Order By r.Nozle_no,r.Beam_no,r.Date desc", con);
                da.SelectCommand.Parameters.AddWithValue("@from", from);
                da.SelectCommand.Parameters.AddWithValue("@to", to);
                da.SelectCommand.Parameters.AddWithValue("@comp", comp);
                da.Fill(d);

                for (int i = 0; i < d.Rows.Count; i++)
                {
                    DataRow row = d.Rows[i];
                    a.Add(row["Compartment_ID"].ToString());
                    a.Add(row["Nozle_no"].ToString());
                    a.Add(row["Beam_no"].ToString());
                    a.Add(row["Date"].ToString());
                    a.Add(row["Description"].ToString());
                    a.Add(row["Cost"].ToString());
                    a.Add(row["Supplier"].ToString());
                }

                return Json(a, JsonRequestBehavior.AllowGet);

            }


        }





    }
}