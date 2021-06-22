using Cooler.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cooler.Controllers
{
    [Authorize]
    public class CoolerSelectorController : Controller
    {
        DataContext db = new DataContext();
        string connectionString = "Data Source=localhost; Initial Catalog = VassilikoFilters; Integrated Security = True; MultipleActiveResultSets=True; ";

        [Authorize(Roles = "Admin,Super")]
        public ActionResult Admin()
        {
            var compartments = db.Compartments.ToList();
            ViewBag.Compartment_ID = new SelectList(compartments, "Compartment_ID", "Compartment_ID");
            return View();
        }
        public ActionResult User()
        {
            var compartments = db.Compartments.ToList();
            ViewBag.Compartment_ID = new SelectList(compartments, "Compartment_ID", "Compartment_ID");
            return View();
        }


        public JsonResult GetCoolerAdmin()
        {
            var cooler = new List<int>();
            string cDate = DateTime.Now.ToString("M/d/yyyy");
            string comp = "1";
            for (int i = 1; i <= 37; i++)
            {
                for (int j = 1; j <= 3; j++)
                {
                    var data1 = db.Replacements.SqlQuery("select * from Replacements where Compartment_ID=@p0  AND Beam_no=@p1  AND Nozle_no=@p2 AND Create_Date=@p3 ", comp, j, i, cDate).ToList();
                    if (data1.Count > 0)
                    {
                        cooler.Add(1);
                    }
                    else
                    {
                        cooler.Add(0);
                    }
                }
            }
            comp = "2";
            for (int i = 1; i <= 28; i++)
            {
                for (int j = 1; j <= 6; j++)
                {
                    var data1 = db.Replacements.SqlQuery("select * from Replacements where Compartment_ID=@p0  AND Beam_no=@p1  AND Nozle_no=@p2 AND Create_Date=@p3 ", comp, j, i, cDate).ToList();
                    if (data1.Count > 0)
                    {
                        cooler.Add(1);
                    }
                    else
                    {
                        cooler.Add(0);
                    }
                }
            }
            comp = "3";
            for (int i = 1; i <= 28; i++)
            {
                for (int j = 1; j <= 9; j++)
                {
                    var data1 = db.Replacements.SqlQuery("select * from Replacements where Compartment_ID=@p0  AND Beam_no=@p1  AND Nozle_no=@p2 AND Create_Date=@p3 ", comp, j, i, cDate).ToList();
                    if (data1.Count > 0)
                    {
                        cooler.Add(1);
                    }
                    else
                    {
                        cooler.Add(0);
                    }
                }
            }
            comp = "4";
            for (int i = 1; i <= 28; i++)
            {
                for (int j = 1; j <= 9; j++)
                {
                    var data1 = db.Replacements.SqlQuery("select * from Replacements where Compartment_ID=@p0  AND Beam_no=@p1  AND Nozle_no=@p2 AND Create_Date=@p3 ", comp, j, i, cDate).ToList();
                    if (data1.Count > 0)
                    {
                        cooler.Add(1);
                    }
                    else
                    {
                        cooler.Add(0);
                    }
                }
            }
            comp = "5";
            for (int i = 1; i <= 28; i++)
            {
                for (int j = 1; j <= 9; j++)
                {
                    var data1 = db.Replacements.SqlQuery("select * from Replacements where Compartment_ID=@p0  AND Beam_no=@p1  AND Nozle_no=@p2 AND Create_Date=@p3 ", comp, j, i, cDate).ToList();
                    if (data1.Count > 0)
                    {
                        cooler.Add(1);
                    }
                    else
                    {
                        cooler.Add(0);
                    }
                }
            }
            comp = "6";
            for (int i = 1; i <= 28; i++)
            {
                for (int j = 1; j <= 12; j++)
                {
                    var data1 = db.Replacements.SqlQuery("select * from Replacements where Compartment_ID=@p0  AND Beam_no=@p1  AND Nozle_no=@p2 AND Create_Date=@p3 ", comp, j, i, cDate).ToList();
                    if (data1.Count > 0)
                    {
                        cooler.Add(1);
                    }
                    else
                    {
                        cooler.Add(0);
                    }
                }
            }
            comp = "7";
            for (int i = 1; i <= 28; i++)
            {
                for (int j = 1; j <= 12; j++)
                {
                    var data1 = db.Replacements.SqlQuery("select * from Replacements where Compartment_ID=@p0  AND Beam_no=@p1  AND Nozle_no=@p2 AND Create_Date=@p3 ", comp, j, i, cDate).ToList();
                    if (data1.Count > 0)
                    {
                        cooler.Add(1);
                    }
                    else
                    {
                        cooler.Add(0);
                    }
                }
            }
            comp = "8";
            for (int i = 1; i <= 28; i++)
            {
                for (int j = 1; j <= 12; j++)
                {
                    var data1 = db.Replacements.SqlQuery("select * from Replacements where Compartment_ID=@p0  AND Beam_no=@p1  AND Nozle_no=@p2 AND Create_Date=@p3 ", comp, j, i, cDate).ToList();
                    if (data1.Count > 0)
                    {
                        cooler.Add(1);
                    }
                    else
                    {
                        cooler.Add(0);
                    }
                }
            }
            comp = "9";
            for (int i = 1; i <= 28; i++)
            {
                for (int j = 1; j <= 11; j++)
                {
                    var data1 = db.Replacements.SqlQuery("select * from Replacements where Compartment_ID=@p0  AND Beam_no=@p1  AND Nozle_no=@p2 AND Create_Date=@p3 ", comp, j, i, cDate).ToList();
                    if (data1.Count > 0)
                    {
                        cooler.Add(1);
                    }
                    else
                    {
                        cooler.Add(0);
                    }
                }
            }
            return Json(cooler, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCooler()
        {
            var cooler = new List<int>();
            string cDate = DateTime.Now.ToString("M/d/yyyy");
            string comp = "1";




            for (int i = 1; i <= 28; i++)
            {
                for (int j = 1; j <= 3; j++)
                {


                    DataTable d = new DataTable();
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        HashSet<string> a = new HashSet<string>();


                        SqlDataAdapter da = new SqlDataAdapter("WITH cte AS( SELECT nos.Color,ROW_NUMBER() OVER(PARTITION BY ns.Spare_Store_Id ORDER BY r.Date DESC) AS rn FROM Replacements r INNER JOIN Nozle_Spares ns ON r.Spare_ID = ns.Spare_ID INNER JOIN Nozle_Suppliers nos ON nos.Supplier_ID = ns.Supplier_ID WHERE Compartment_ID = @comp  AND Nozle_no = @nozle AND Beam_no = @beam) SELECT * FROM cte WHERE rn = 1", con);
                        da.SelectCommand.Parameters.AddWithValue("@comp", comp);
                        da.SelectCommand.Parameters.AddWithValue("@beam", j);
                        da.SelectCommand.Parameters.AddWithValue("@nozle", i);

                        da.Fill(d);

                        for (int n = 0; n < d.Rows.Count; n++)
                        {
                            DataRow row = d.Rows[n];
                            a.Add(row["Color"].ToString());
                        }

                        if (a.Count > 1)
                        {
                            cooler.Add(11);
                        }

                        else if (a.Count == 1)
                        {
                            switch (a.First().Trim())
                            {
                                case "red":
                                    cooler.Add(1);
                                    break;
                                case "blue":
                                    cooler.Add(2);
                                    break;
                                case "moccasin":
                                    cooler.Add(3);
                                    break;
                                case "yellow":
                                    cooler.Add(4);
                                    break;
                                case "purple":
                                    cooler.Add(5);
                                    break;
                                case "aqua":
                                    cooler.Add(6);
                                    break;
                                case "pink":
                                    cooler.Add(7);
                                    break;
                                case "orange":
                                    cooler.Add(8);
                                    break;
                                case "lime":
                                    cooler.Add(9);
                                    break;
                                case "black":
                                    cooler.Add(10);
                                    break;
                                default:
                                    cooler.Add(1);
                                    break;
                            }
                        }
                        else
                        {
                            cooler.Add(0);
                        }


                    }



                }
            }




            comp = "2";
            for (int i = 1; i <= 28; i++)
            {
                for (int j = 1; j <= 6; j++)
                {
                    DataTable d = new DataTable();
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        List<string> a = new List<string>();


                        SqlDataAdapter da = new SqlDataAdapter("select top 1 with ties nsp.Color from Replacements r INNER JOIN Nozle_Spares ns ON r.Spare_ID = ns.Spare_ID INNER JOIN Nozle_Suppliers nsp ON nsp.Supplier_ID = ns.Supplier_ID INNER JOIN Nozle_Colors nc ON nc.Color = nsp.Color where Compartment_ID = @comp and Beam_no = @beam and Nozle_no = @nozle ORDER BY ROW_NUMBER() OVER(PARTITION BY ns.Spare_Store_Id  ORDER BY Date desc)", con);
                        da.SelectCommand.Parameters.AddWithValue("@comp", comp);
                        da.SelectCommand.Parameters.AddWithValue("@beam", j);
                        da.SelectCommand.Parameters.AddWithValue("@nozle", i);

                        da.Fill(d);

                        for (int n = 0; n < d.Rows.Count; n++)
                        {
                            DataRow row = d.Rows[n];
                            a.Add(row["Color"].ToString());
                        }

                        if (a.Count > 1)
                        {
                            cooler.Add(11);
                        }

                        else if (a.Count == 1)
                        {
                            switch (a.First().Trim())
                            {
                                case "red":
                                    cooler.Add(1);
                                    break;
                                case "blue":
                                    cooler.Add(2);
                                    break;
                                case "moccasin":
                                    cooler.Add(3);
                                    break;
                                case "yellow":
                                    cooler.Add(4);
                                    break;
                                case "purple":
                                    cooler.Add(5);
                                    break;
                                case "aqua":
                                    cooler.Add(6);
                                    break;
                                case "pink":
                                    cooler.Add(7);
                                    break;
                                case "orange":
                                    cooler.Add(8);
                                    break;
                                case "lime":
                                    cooler.Add(9);
                                    break;
                                case "black":
                                    cooler.Add(10);
                                    break;
                                default:
                                    cooler.Add(1);
                                    break;
                            }
                        }
                        else
                        {
                            cooler.Add(0);
                        }


                    }
                }
            }
            comp = "3";
            for (int i = 1; i <= 28; i++)
            {
                for (int j = 1; j <= 9; j++)
                {
                    DataTable d = new DataTable();
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        List<string> a = new List<string>();


                        SqlDataAdapter da = new SqlDataAdapter("select top 1 with ties nsp.Color from Replacements r INNER JOIN Nozle_Spares ns ON r.Spare_ID = ns.Spare_ID INNER JOIN Nozle_Suppliers nsp ON nsp.Supplier_ID = ns.Supplier_ID INNER JOIN Nozle_Colors nc ON nc.Color = nsp.Color where Compartment_ID = @comp and Beam_no = @beam and Nozle_no = @nozle ORDER BY ROW_NUMBER() OVER(PARTITION BY ns.Spare_Store_Id  ORDER BY Date desc)", con);
                        da.SelectCommand.Parameters.AddWithValue("@comp", comp);
                        da.SelectCommand.Parameters.AddWithValue("@beam", j);
                        da.SelectCommand.Parameters.AddWithValue("@nozle", i);

                        da.Fill(d);

                        for (int n = 0; n < d.Rows.Count; n++)
                        {
                            DataRow row = d.Rows[n];
                            a.Add(row["Color"].ToString());
                        }

                        if (a.Count > 1)
                        {
                            cooler.Add(11);
                        }

                        else if (a.Count == 1)
                        {
                            switch (a.First().Trim())
                            {
                                case "red":
                                    cooler.Add(1);
                                    break;
                                case "blue":
                                    cooler.Add(2);
                                    break;
                                case "moccasin":
                                    cooler.Add(3);
                                    break;
                                case "yellow":
                                    cooler.Add(4);
                                    break;
                                case "purple":
                                    cooler.Add(5);
                                    break;
                                case "aqua":
                                    cooler.Add(6);
                                    break;
                                case "pink":
                                    cooler.Add(7);
                                    break;
                                case "orange":
                                    cooler.Add(8);
                                    break;
                                case "lime":
                                    cooler.Add(9);
                                    break;
                                case "black":
                                    cooler.Add(10);
                                    break;
                                default:
                                    cooler.Add(1);
                                    break;
                            }
                        }
                        else
                        {
                            cooler.Add(0);
                        }


                    }
                }
            }
            comp = "4";
            for (int i = 1; i <= 28; i++)
            {
                for (int j = 1; j <= 9; j++)
                {
                    DataTable d = new DataTable();
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        List<string> a = new List<string>();


                        SqlDataAdapter da = new SqlDataAdapter("select top 1 with ties nsp.Color from Replacements r INNER JOIN Nozle_Spares ns ON r.Spare_ID = ns.Spare_ID INNER JOIN Nozle_Suppliers nsp ON nsp.Supplier_ID = ns.Supplier_ID INNER JOIN Nozle_Colors nc ON nc.Color = nsp.Color where Compartment_ID = @comp and Beam_no = @beam and Nozle_no = @nozle ORDER BY ROW_NUMBER() OVER(PARTITION BY ns.Spare_Store_Id  ORDER BY Date desc)", con);
                        da.SelectCommand.Parameters.AddWithValue("@comp", comp);
                        da.SelectCommand.Parameters.AddWithValue("@beam", j);
                        da.SelectCommand.Parameters.AddWithValue("@nozle", i);

                        da.Fill(d);

                        for (int n = 0; n < d.Rows.Count; n++)
                        {
                            DataRow row = d.Rows[n];
                            a.Add(row["Color"].ToString());
                        }

                        if (a.Count > 1)
                        {
                            cooler.Add(11);
                        }

                        else if (a.Count == 1)
                        {
                            switch (a.First().Trim())
                            {
                                case "red":
                                    cooler.Add(1);
                                    break;
                                case "blue":
                                    cooler.Add(2);
                                    break;
                                case "moccasin":
                                    cooler.Add(3);
                                    break;
                                case "yellow":
                                    cooler.Add(4);
                                    break;
                                case "purple":
                                    cooler.Add(5);
                                    break;
                                case "aqua":
                                    cooler.Add(6);
                                    break;
                                case "pink":
                                    cooler.Add(7);
                                    break;
                                case "orange":
                                    cooler.Add(8);
                                    break;
                                case "lime":
                                    cooler.Add(9);
                                    break;
                                case "black":
                                    cooler.Add(10);
                                    break;
                                default:
                                    cooler.Add(1);
                                    break;
                            }
                        }
                        else
                        {
                            cooler.Add(0);
                        }


                    }
                }
            }
            comp = "5";
            for (int i = 1; i <= 28; i++)
            {
                for (int j = 1; j <= 9; j++)
                {
                    DataTable d = new DataTable();
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        List<string> a = new List<string>();


                        SqlDataAdapter da = new SqlDataAdapter("select top 1 with ties nsp.Color from Replacements r INNER JOIN Nozle_Spares ns ON r.Spare_ID = ns.Spare_ID INNER JOIN Nozle_Suppliers nsp ON nsp.Supplier_ID = ns.Supplier_ID INNER JOIN Nozle_Colors nc ON nc.Color = nsp.Color where Compartment_ID = @comp and Beam_no = @beam and Nozle_no = @nozle ORDER BY ROW_NUMBER() OVER(PARTITION BY ns.Spare_Store_Id  ORDER BY Date desc)", con);
                        da.SelectCommand.Parameters.AddWithValue("@comp", comp);
                        da.SelectCommand.Parameters.AddWithValue("@beam", j);
                        da.SelectCommand.Parameters.AddWithValue("@nozle", i);

                        da.Fill(d);

                        for (int n = 0; n < d.Rows.Count; n++)
                        {
                            DataRow row = d.Rows[n];
                            a.Add(row["Color"].ToString());
                        }

                        if (a.Count > 1)
                        {
                            cooler.Add(11);
                        }

                        else if (a.Count == 1)
                        {
                            switch (a.First().Trim())
                            {
                                case "red":
                                    cooler.Add(1);
                                    break;
                                case "blue":
                                    cooler.Add(2);
                                    break;
                                case "moccasin":
                                    cooler.Add(3);
                                    break;
                                case "yellow":
                                    cooler.Add(4);
                                    break;
                                case "purple":
                                    cooler.Add(5);
                                    break;
                                case "aqua":
                                    cooler.Add(6);
                                    break;
                                case "pink":
                                    cooler.Add(7);
                                    break;
                                case "orange":
                                    cooler.Add(8);
                                    break;
                                case "lime":
                                    cooler.Add(9);
                                    break;
                                case "black":
                                    cooler.Add(10);
                                    break;
                                default:
                                    cooler.Add(1);
                                    break;
                            }
                        }
                        else
                        {
                            cooler.Add(0);
                        }


                    }
                }
            }
            comp = "6";
            for (int i = 1; i <= 28; i++)
            {
                for (int j = 1; j <= 12; j++)
                {
                    DataTable d = new DataTable();
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        List<string> a = new List<string>();


                        SqlDataAdapter da = new SqlDataAdapter("select top 1 with ties nsp.Color from Replacements r INNER JOIN Nozle_Spares ns ON r.Spare_ID = ns.Spare_ID INNER JOIN Nozle_Suppliers nsp ON nsp.Supplier_ID = ns.Supplier_ID INNER JOIN Nozle_Colors nc ON nc.Color = nsp.Color where Compartment_ID = @comp and Beam_no = @beam and Nozle_no = @nozle ORDER BY ROW_NUMBER() OVER(PARTITION BY ns.Spare_Store_Id  ORDER BY Date desc)", con);
                        da.SelectCommand.Parameters.AddWithValue("@comp", comp);
                        da.SelectCommand.Parameters.AddWithValue("@beam", j);
                        da.SelectCommand.Parameters.AddWithValue("@nozle", i);

                        da.Fill(d);

                        for (int n = 0; n < d.Rows.Count; n++)
                        {
                            DataRow row = d.Rows[n];
                            a.Add(row["Color"].ToString());
                        }

                        if (a.Count > 1)
                        {
                            cooler.Add(11);
                        }

                        else if (a.Count == 1)
                        {
                            switch (a.First().Trim())
                            {
                                case "red":
                                    cooler.Add(1);
                                    break;
                                case "blue":
                                    cooler.Add(2);
                                    break;
                                case "moccasin":
                                    cooler.Add(3);
                                    break;
                                case "yellow":
                                    cooler.Add(4);
                                    break;
                                case "purple":
                                    cooler.Add(5);
                                    break;
                                case "aqua":
                                    cooler.Add(6);
                                    break;
                                case "pink":
                                    cooler.Add(7);
                                    break;
                                case "orange":
                                    cooler.Add(8);
                                    break;
                                case "lime":
                                    cooler.Add(9);
                                    break;
                                case "black":
                                    cooler.Add(10);
                                    break;
                                default:
                                    cooler.Add(1);
                                    break;
                            }
                        }
                        else
                        {
                            cooler.Add(0);
                        }


                    }
                }
            }
            comp = "7";
            for (int i = 1; i <= 28; i++)
            {
                for (int j = 1; j <= 12; j++)
                {
                    DataTable d = new DataTable();
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        List<string> a = new List<string>();


                        SqlDataAdapter da = new SqlDataAdapter("select top 1 with ties nsp.Color from Replacements r INNER JOIN Nozle_Spares ns ON r.Spare_ID = ns.Spare_ID INNER JOIN Nozle_Suppliers nsp ON nsp.Supplier_ID = ns.Supplier_ID INNER JOIN Nozle_Colors nc ON nc.Color = nsp.Color where Compartment_ID = @comp and Beam_no = @beam and Nozle_no = @nozle ORDER BY ROW_NUMBER() OVER(PARTITION BY ns.Spare_Store_Id  ORDER BY Date desc)", con);
                        da.SelectCommand.Parameters.AddWithValue("@comp", comp);
                        da.SelectCommand.Parameters.AddWithValue("@beam", j);
                        da.SelectCommand.Parameters.AddWithValue("@nozle", i);

                        da.Fill(d);

                        for (int n = 0; n < d.Rows.Count; n++)
                        {
                            DataRow row = d.Rows[n];
                            a.Add(row["Color"].ToString());
                        }

                        if (a.Count > 1)
                        {
                            cooler.Add(11);
                        }

                        else if (a.Count == 1)
                        {
                            switch (a.First().Trim())
                            {
                                case "red":
                                    cooler.Add(1);
                                    break;
                                case "blue":
                                    cooler.Add(2);
                                    break;
                                case "moccasin":
                                    cooler.Add(3);
                                    break;
                                case "yellow":
                                    cooler.Add(4);
                                    break;
                                case "purple":
                                    cooler.Add(5);
                                    break;
                                case "aqua":
                                    cooler.Add(6);
                                    break;
                                case "pink":
                                    cooler.Add(7);
                                    break;
                                case "orange":
                                    cooler.Add(8);
                                    break;
                                case "lime":
                                    cooler.Add(9);
                                    break;
                                case "black":
                                    cooler.Add(10);
                                    break;
                                default:
                                    cooler.Add(1);
                                    break;
                            }
                        }
                        else
                        {
                            cooler.Add(0);
                        }


                    }
                }
            }
            comp = "8";
            for (int i = 1; i <= 28; i++)
            {
                for (int j = 1; j <= 12; j++)
                {
                    DataTable d = new DataTable();
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        List<string> a = new List<string>();


                        SqlDataAdapter da = new SqlDataAdapter("select top 1 with ties nsp.Color from Replacements r INNER JOIN Nozle_Spares ns ON r.Spare_ID = ns.Spare_ID INNER JOIN Nozle_Suppliers nsp ON nsp.Supplier_ID = ns.Supplier_ID INNER JOIN Nozle_Colors nc ON nc.Color = nsp.Color where Compartment_ID = @comp and Beam_no = @beam and Nozle_no = @nozle ORDER BY ROW_NUMBER() OVER(PARTITION BY ns.Spare_Store_Id  ORDER BY Date desc)", con);
                        da.SelectCommand.Parameters.AddWithValue("@comp", comp);
                        da.SelectCommand.Parameters.AddWithValue("@beam", j);
                        da.SelectCommand.Parameters.AddWithValue("@nozle", i);

                        da.Fill(d);

                        for (int n = 0; n < d.Rows.Count; n++)
                        {
                            DataRow row = d.Rows[n];
                            a.Add(row["Color"].ToString());
                        }

                        if (a.Count > 1)
                        {
                            cooler.Add(11);
                        }

                        else if (a.Count == 1)
                        {
                            switch (a.First().Trim())
                            {
                                case "red":
                                    cooler.Add(1);
                                    break;
                                case "blue":
                                    cooler.Add(2);
                                    break;
                                case "moccasin":
                                    cooler.Add(3);
                                    break;
                                case "yellow":
                                    cooler.Add(4);
                                    break;
                                case "purple":
                                    cooler.Add(5);
                                    break;
                                case "aqua":
                                    cooler.Add(6);
                                    break;
                                case "pink":
                                    cooler.Add(7);
                                    break;
                                case "orange":
                                    cooler.Add(8);
                                    break;
                                case "lime":
                                    cooler.Add(9);
                                    break;
                                case "black":
                                    cooler.Add(10);
                                    break;
                                default:
                                    cooler.Add(1);
                                    break;
                            }
                        }
                        else
                        {
                            cooler.Add(0);
                        }


                    }
                }
            }
            comp = "9";
            for (int i = 1; i <= 28; i++)
            {
                for (int j = 1; j <= 11; j++)
                {
                    DataTable d = new DataTable();
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        List<string> a = new List<string>();


                        SqlDataAdapter da = new SqlDataAdapter("select top 1 with ties nsp.Color from Replacements r INNER JOIN Nozle_Spares ns ON r.Spare_ID = ns.Spare_ID INNER JOIN Nozle_Suppliers nsp ON nsp.Supplier_ID = ns.Supplier_ID INNER JOIN Nozle_Colors nc ON nc.Color = nsp.Color where Compartment_ID = @comp and Beam_no = @beam and Nozle_no = @nozle ORDER BY ROW_NUMBER() OVER(PARTITION BY ns.Spare_Store_Id  ORDER BY Date desc)", con);
                        da.SelectCommand.Parameters.AddWithValue("@comp", comp);
                        da.SelectCommand.Parameters.AddWithValue("@beam", j);
                        da.SelectCommand.Parameters.AddWithValue("@nozle", i);

                        da.Fill(d);

                        for (int n = 0; n < d.Rows.Count; n++)
                        {
                            DataRow row = d.Rows[n];
                            a.Add(row["Color"].ToString());
                        }

                        if (a.Count > 1)
                        {
                            cooler.Add(11);
                        }

                        else if (a.Count == 1)
                        {
                            switch (a.First().Trim())
                            {
                                case "red":
                                    cooler.Add(1);
                                    break;
                                case "blue":
                                    cooler.Add(2);
                                    break;
                                case "moccasin":
                                    cooler.Add(3);
                                    break;
                                case "yellow":
                                    cooler.Add(4);
                                    break;
                                case "purple":
                                    cooler.Add(5);
                                    break;
                                case "aqua":
                                    cooler.Add(6);
                                    break;
                                case "pink":
                                    cooler.Add(7);
                                    break;
                                case "orange":
                                    cooler.Add(8);
                                    break;
                                case "lime":
                                    cooler.Add(9);
                                    break;
                                case "black":
                                    cooler.Add(10);
                                    break;
                                default:
                                    cooler.Add(1);
                                    break;
                            }
                        }
                        else
                        {
                            cooler.Add(0);
                        }


                    }
                }
            }
            return Json(cooler, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCoolerdate(string date)
        {
            var cooler = new List<int>();
            string cDate = DateTime.Now.ToString("M/d/yyyy");
            string comp = "1";




            for (int i = 1; i <= 28; i++)
            {
                for (int j = 1; j <= 3; j++)
                {


                    DataTable d = new DataTable();
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        HashSet<string> a = new HashSet<string>();


                        SqlDataAdapter da = new SqlDataAdapter("WITH cte AS( SELECT nos.Color,ROW_NUMBER() OVER(PARTITION BY ns.Spare_Store_Id ORDER BY r.Date DESC) AS rn FROM Replacements r INNER JOIN Nozle_Spares ns ON r.Spare_ID = ns.Spare_ID INNER JOIN Nozle_Suppliers nos ON nos.Supplier_ID = ns.Supplier_ID WHERE Compartment_ID = @comp  AND Nozle_no = @nozle AND Beam_no = @beam and Date<=@date) SELECT * FROM cte WHERE rn = 1", con);
                        da.SelectCommand.Parameters.AddWithValue("@comp", comp);
                        da.SelectCommand.Parameters.AddWithValue("@beam", j);
                        da.SelectCommand.Parameters.AddWithValue("@nozle", i);
                        da.SelectCommand.Parameters.AddWithValue("@date", date);

                        da.Fill(d);

                        for (int n = 0; n < d.Rows.Count; n++)
                        {
                            DataRow row = d.Rows[n];
                            a.Add(row["Color"].ToString());
                        }

                        if (a.Count > 1)
                        {
                            cooler.Add(11);
                        }

                        else if (a.Count == 1)
                        {
                            switch (a.First().Trim())
                            {
                                case "red":
                                    cooler.Add(1);
                                    break;
                                case "blue":
                                    cooler.Add(2);
                                    break;
                                case "moccasin":
                                    cooler.Add(3);
                                    break;
                                case "yellow":
                                    cooler.Add(4);
                                    break;
                                case "purple":
                                    cooler.Add(5);
                                    break;
                                case "aqua":
                                    cooler.Add(6);
                                    break;
                                case "pink":
                                    cooler.Add(7);
                                    break;
                                case "orange":
                                    cooler.Add(8);
                                    break;
                                case "lime":
                                    cooler.Add(9);
                                    break;
                                case "black":
                                    cooler.Add(10);
                                    break;
                                default:
                                    cooler.Add(1);
                                    break;
                            }
                        }
                        else
                        {
                            cooler.Add(0);
                        }


                    }



                }
            }




            comp = "2";
            for (int i = 1; i <= 28; i++)
            {
                for (int j = 1; j <= 6; j++)
                {
                    DataTable d = new DataTable();
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        List<string> a = new List<string>();


                        SqlDataAdapter da = new SqlDataAdapter("WITH cte AS( SELECT nos.Color,ROW_NUMBER() OVER(PARTITION BY ns.Spare_Store_Id ORDER BY r.Date DESC) AS rn FROM Replacements r INNER JOIN Nozle_Spares ns ON r.Spare_ID = ns.Spare_ID INNER JOIN Nozle_Suppliers nos ON nos.Supplier_ID = ns.Supplier_ID WHERE Compartment_ID = @comp  AND Nozle_no = @nozle AND Beam_no = @beam and Date<=@date) SELECT * FROM cte WHERE rn = 1", con);
                        da.SelectCommand.Parameters.AddWithValue("@comp", comp);
                        da.SelectCommand.Parameters.AddWithValue("@beam", j);
                        da.SelectCommand.Parameters.AddWithValue("@nozle", i);
                        da.SelectCommand.Parameters.AddWithValue("@date", date);

                        da.Fill(d);

                        for (int n = 0; n < d.Rows.Count; n++)
                        {
                            DataRow row = d.Rows[n];
                            a.Add(row["Color"].ToString());
                        }

                        if (a.Count > 1)
                        {
                            cooler.Add(11);
                        }

                        else if (a.Count == 1)
                        {
                            switch (a.First().Trim())
                            {
                                case "red":
                                    cooler.Add(1);
                                    break;
                                case "blue":
                                    cooler.Add(2);
                                    break;
                                case "moccasin":
                                    cooler.Add(3);
                                    break;
                                case "yellow":
                                    cooler.Add(4);
                                    break;
                                case "purple":
                                    cooler.Add(5);
                                    break;
                                case "aqua":
                                    cooler.Add(6);
                                    break;
                                case "pink":
                                    cooler.Add(7);
                                    break;
                                case "orange":
                                    cooler.Add(8);
                                    break;
                                case "lime":
                                    cooler.Add(9);
                                    break;
                                case "black":
                                    cooler.Add(10);
                                    break;
                                default:
                                    cooler.Add(1);
                                    break;
                            }
                        }
                        else
                        {
                            cooler.Add(0);
                        }


                    }
                }
            }
            comp = "3";
            for (int i = 1; i <= 28; i++)
            {
                for (int j = 1; j <= 9; j++)
                {
                    DataTable d = new DataTable();
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        List<string> a = new List<string>();


                        SqlDataAdapter da = new SqlDataAdapter("WITH cte AS( SELECT nos.Color,ROW_NUMBER() OVER(PARTITION BY ns.Spare_Store_Id ORDER BY r.Date DESC) AS rn FROM Replacements r INNER JOIN Nozle_Spares ns ON r.Spare_ID = ns.Spare_ID INNER JOIN Nozle_Suppliers nos ON nos.Supplier_ID = ns.Supplier_ID WHERE Compartment_ID = @comp  AND Nozle_no = @nozle AND Beam_no = @beam and Date<=@date) SELECT * FROM cte WHERE rn = 1", con);
                        da.SelectCommand.Parameters.AddWithValue("@comp", comp);
                        da.SelectCommand.Parameters.AddWithValue("@beam", j);
                        da.SelectCommand.Parameters.AddWithValue("@nozle", i);
                        da.SelectCommand.Parameters.AddWithValue("@date", date);
                        da.Fill(d);

                        for (int n = 0; n < d.Rows.Count; n++)
                        {
                            DataRow row = d.Rows[n];
                            a.Add(row["Color"].ToString());
                        }

                        if (a.Count > 1)
                        {
                            cooler.Add(11);
                        }

                        else if (a.Count == 1)
                        {
                            switch (a.First().Trim())
                            {
                                case "red":
                                    cooler.Add(1);
                                    break;
                                case "blue":
                                    cooler.Add(2);
                                    break;
                                case "moccasin":
                                    cooler.Add(3);
                                    break;
                                case "yellow":
                                    cooler.Add(4);
                                    break;
                                case "purple":
                                    cooler.Add(5);
                                    break;
                                case "aqua":
                                    cooler.Add(6);
                                    break;
                                case "pink":
                                    cooler.Add(7);
                                    break;
                                case "orange":
                                    cooler.Add(8);
                                    break;
                                case "lime":
                                    cooler.Add(9);
                                    break;
                                case "black":
                                    cooler.Add(10);
                                    break;
                                default:
                                    cooler.Add(1);
                                    break;
                            }
                        }
                        else
                        {
                            cooler.Add(0);
                        }


                    }
                }
            }
            comp = "4";
            for (int i = 1; i <= 28; i++)
            {
                for (int j = 1; j <= 9; j++)
                {
                    DataTable d = new DataTable();
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        List<string> a = new List<string>();


                        SqlDataAdapter da = new SqlDataAdapter("WITH cte AS( SELECT nos.Color,ROW_NUMBER() OVER(PARTITION BY ns.Spare_Store_Id ORDER BY r.Date DESC) AS rn FROM Replacements r INNER JOIN Nozle_Spares ns ON r.Spare_ID = ns.Spare_ID INNER JOIN Nozle_Suppliers nos ON nos.Supplier_ID = ns.Supplier_ID WHERE Compartment_ID = @comp  AND Nozle_no = @nozle AND Beam_no = @beam and Date<=@date) SELECT * FROM cte WHERE rn = 1", con);
                        da.SelectCommand.Parameters.AddWithValue("@comp", comp);
                        da.SelectCommand.Parameters.AddWithValue("@beam", j);
                        da.SelectCommand.Parameters.AddWithValue("@nozle", i);
                        da.SelectCommand.Parameters.AddWithValue("@date", date);
                        da.Fill(d);

                        for (int n = 0; n < d.Rows.Count; n++)
                        {
                            DataRow row = d.Rows[n];
                            a.Add(row["Color"].ToString());
                        }

                        if (a.Count > 1)
                        {
                            cooler.Add(11);
                        }

                        else if (a.Count == 1)
                        {
                            switch (a.First().Trim())
                            {
                                case "red":
                                    cooler.Add(1);
                                    break;
                                case "blue":
                                    cooler.Add(2);
                                    break;
                                case "moccasin":
                                    cooler.Add(3);
                                    break;
                                case "yellow":
                                    cooler.Add(4);
                                    break;
                                case "purple":
                                    cooler.Add(5);
                                    break;
                                case "aqua":
                                    cooler.Add(6);
                                    break;
                                case "pink":
                                    cooler.Add(7);
                                    break;
                                case "orange":
                                    cooler.Add(8);
                                    break;
                                case "lime":
                                    cooler.Add(9);
                                    break;
                                case "black":
                                    cooler.Add(10);
                                    break;
                                default:
                                    cooler.Add(1);
                                    break;
                            }
                        }
                        else
                        {
                            cooler.Add(0);
                        }


                    }
                }
            }
            comp = "5";
            for (int i = 1; i <= 28; i++)
            {
                for (int j = 1; j <= 9; j++)
                {
                    DataTable d = new DataTable();
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        List<string> a = new List<string>();


                        SqlDataAdapter da = new SqlDataAdapter("WITH cte AS( SELECT nos.Color,ROW_NUMBER() OVER(PARTITION BY ns.Spare_Store_Id ORDER BY r.Date DESC) AS rn FROM Replacements r INNER JOIN Nozle_Spares ns ON r.Spare_ID = ns.Spare_ID INNER JOIN Nozle_Suppliers nos ON nos.Supplier_ID = ns.Supplier_ID WHERE Compartment_ID = @comp  AND Nozle_no = @nozle AND Beam_no = @beam and Date<=@date) SELECT * FROM cte WHERE rn = 1", con);
                        da.SelectCommand.Parameters.AddWithValue("@comp", comp);
                        da.SelectCommand.Parameters.AddWithValue("@beam", j);
                        da.SelectCommand.Parameters.AddWithValue("@nozle", i);
                        da.SelectCommand.Parameters.AddWithValue("@date", date);
                        da.Fill(d);

                        for (int n = 0; n < d.Rows.Count; n++)
                        {
                            DataRow row = d.Rows[n];
                            a.Add(row["Color"].ToString());
                        }

                        if (a.Count > 1)
                        {
                            cooler.Add(11);
                        }

                        else if (a.Count == 1)
                        {
                            switch (a.First().Trim())
                            {
                                case "red":
                                    cooler.Add(1);
                                    break;
                                case "blue":
                                    cooler.Add(2);
                                    break;
                                case "moccasin":
                                    cooler.Add(3);
                                    break;
                                case "yellow":
                                    cooler.Add(4);
                                    break;
                                case "purple":
                                    cooler.Add(5);
                                    break;
                                case "aqua":
                                    cooler.Add(6);
                                    break;
                                case "pink":
                                    cooler.Add(7);
                                    break;
                                case "orange":
                                    cooler.Add(8);
                                    break;
                                case "lime":
                                    cooler.Add(9);
                                    break;
                                case "black":
                                    cooler.Add(10);
                                    break;
                                default:
                                    cooler.Add(1);
                                    break;
                            }
                        }
                        else
                        {
                            cooler.Add(0);
                        }


                    }
                }
            }
            comp = "6";
            for (int i = 1; i <= 28; i++)
            {
                for (int j = 1; j <= 12; j++)
                {
                    DataTable d = new DataTable();
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        List<string> a = new List<string>();


                        SqlDataAdapter da = new SqlDataAdapter("WITH cte AS( SELECT nos.Color,ROW_NUMBER() OVER(PARTITION BY ns.Spare_Store_Id ORDER BY r.Date DESC) AS rn FROM Replacements r INNER JOIN Nozle_Spares ns ON r.Spare_ID = ns.Spare_ID INNER JOIN Nozle_Suppliers nos ON nos.Supplier_ID = ns.Supplier_ID WHERE Compartment_ID = @comp  AND Nozle_no = @nozle AND Beam_no = @beam and Date<=@date) SELECT * FROM cte WHERE rn = 1", con);
                        da.SelectCommand.Parameters.AddWithValue("@comp", comp);
                        da.SelectCommand.Parameters.AddWithValue("@beam", j);
                        da.SelectCommand.Parameters.AddWithValue("@nozle", i);
                        da.SelectCommand.Parameters.AddWithValue("@date", date);
                        da.Fill(d);

                        for (int n = 0; n < d.Rows.Count; n++)
                        {
                            DataRow row = d.Rows[n];
                            a.Add(row["Color"].ToString());
                        }

                        if (a.Count > 1)
                        {
                            cooler.Add(11);
                        }

                        else if (a.Count == 1)
                        {
                            switch (a.First().Trim())
                            {
                                case "red":
                                    cooler.Add(1);
                                    break;
                                case "blue":
                                    cooler.Add(2);
                                    break;
                                case "moccasin":
                                    cooler.Add(3);
                                    break;
                                case "yellow":
                                    cooler.Add(4);
                                    break;
                                case "purple":
                                    cooler.Add(5);
                                    break;
                                case "aqua":
                                    cooler.Add(6);
                                    break;
                                case "pink":
                                    cooler.Add(7);
                                    break;
                                case "orange":
                                    cooler.Add(8);
                                    break;
                                case "lime":
                                    cooler.Add(9);
                                    break;
                                case "black":
                                    cooler.Add(10);
                                    break;
                                default:
                                    cooler.Add(1);
                                    break;
                            }
                        }
                        else
                        {
                            cooler.Add(0);
                        }


                    }
                }
            }
            comp = "7";
            for (int i = 1; i <= 28; i++)
            {
                for (int j = 1; j <= 12; j++)
                {
                    DataTable d = new DataTable();
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        List<string> a = new List<string>();

                        SqlDataAdapter da = new SqlDataAdapter("WITH cte AS( SELECT nos.Color,ROW_NUMBER() OVER(PARTITION BY ns.Spare_Store_Id ORDER BY r.Date DESC) AS rn FROM Replacements r INNER JOIN Nozle_Spares ns ON r.Spare_ID = ns.Spare_ID INNER JOIN Nozle_Suppliers nos ON nos.Supplier_ID = ns.Supplier_ID WHERE Compartment_ID = @comp  AND Nozle_no = @nozle AND Beam_no = @beam and Date<=@date) SELECT * FROM cte WHERE rn = 1", con);
                        da.SelectCommand.Parameters.AddWithValue("@comp", comp);
                        da.SelectCommand.Parameters.AddWithValue("@beam", j);
                        da.SelectCommand.Parameters.AddWithValue("@nozle", i);
                        da.SelectCommand.Parameters.AddWithValue("@date", date);
                        da.Fill(d);

                        for (int n = 0; n < d.Rows.Count; n++)
                        {
                            DataRow row = d.Rows[n];
                            a.Add(row["Color"].ToString());
                        }

                        if (a.Count > 1)
                        {
                            cooler.Add(11);
                        }

                        else if (a.Count == 1)
                        {
                            switch (a.First().Trim())
                            {
                                case "red":
                                    cooler.Add(1);
                                    break;
                                case "blue":
                                    cooler.Add(2);
                                    break;
                                case "moccasin":
                                    cooler.Add(3);
                                    break;
                                case "yellow":
                                    cooler.Add(4);
                                    break;
                                case "purple":
                                    cooler.Add(5);
                                    break;
                                case "aqua":
                                    cooler.Add(6);
                                    break;
                                case "pink":
                                    cooler.Add(7);
                                    break;
                                case "orange":
                                    cooler.Add(8);
                                    break;
                                case "lime":
                                    cooler.Add(9);
                                    break;
                                case "black":
                                    cooler.Add(10);
                                    break;
                                default:
                                    cooler.Add(1);
                                    break;
                            }
                        }
                        else
                        {
                            cooler.Add(0);
                        }


                    }
                }
            }
            comp = "8";
            for (int i = 1; i <= 28; i++)
            {
                for (int j = 1; j <= 12; j++)
                {
                    DataTable d = new DataTable();
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        List<string> a = new List<string>();


                        SqlDataAdapter da = new SqlDataAdapter("WITH cte AS( SELECT nos.Color,ROW_NUMBER() OVER(PARTITION BY ns.Spare_Store_Id ORDER BY r.Date DESC) AS rn FROM Replacements r INNER JOIN Nozle_Spares ns ON r.Spare_ID = ns.Spare_ID INNER JOIN Nozle_Suppliers nos ON nos.Supplier_ID = ns.Supplier_ID WHERE Compartment_ID = @comp  AND Nozle_no = @nozle AND Beam_no = @beam and Date<=@date) SELECT * FROM cte WHERE rn = 1", con);
                        da.SelectCommand.Parameters.AddWithValue("@comp", comp);
                        da.SelectCommand.Parameters.AddWithValue("@beam", j);
                        da.SelectCommand.Parameters.AddWithValue("@nozle", i);
                        da.SelectCommand.Parameters.AddWithValue("@date", date);
                        da.Fill(d);

                        for (int n = 0; n < d.Rows.Count; n++)
                        {
                            DataRow row = d.Rows[n];
                            a.Add(row["Color"].ToString());
                        }

                        if (a.Count > 1)
                        {
                            cooler.Add(11);
                        }

                        else if (a.Count == 1)
                        {
                            switch (a.First().Trim())
                            {
                                case "red":
                                    cooler.Add(1);
                                    break;
                                case "blue":
                                    cooler.Add(2);
                                    break;
                                case "moccasin":
                                    cooler.Add(3);
                                    break;
                                case "yellow":
                                    cooler.Add(4);
                                    break;
                                case "purple":
                                    cooler.Add(5);
                                    break;
                                case "aqua":
                                    cooler.Add(6);
                                    break;
                                case "pink":
                                    cooler.Add(7);
                                    break;
                                case "orange":
                                    cooler.Add(8);
                                    break;
                                case "lime":
                                    cooler.Add(9);
                                    break;
                                case "black":
                                    cooler.Add(10);
                                    break;
                                default:
                                    cooler.Add(1);
                                    break;
                            }
                        }
                        else
                        {
                            cooler.Add(0);
                        }


                    }
                }
            }
            comp = "9";
            for (int i = 1; i <= 28; i++)
            {
                for (int j = 1; j <= 11; j++)
                {
                    DataTable d = new DataTable();
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        List<string> a = new List<string>();


                        SqlDataAdapter da = new SqlDataAdapter("WITH cte AS( SELECT nos.Color,ROW_NUMBER() OVER(PARTITION BY ns.Spare_Store_Id ORDER BY r.Date DESC) AS rn FROM Replacements r INNER JOIN Nozle_Spares ns ON r.Spare_ID = ns.Spare_ID INNER JOIN Nozle_Suppliers nos ON nos.Supplier_ID = ns.Supplier_ID WHERE Compartment_ID = @comp  AND Nozle_no = @nozle AND Beam_no = @beam and Date<=@date) SELECT * FROM cte WHERE rn = 1", con);
                        da.SelectCommand.Parameters.AddWithValue("@comp", comp);
                        da.SelectCommand.Parameters.AddWithValue("@beam", j);
                        da.SelectCommand.Parameters.AddWithValue("@nozle", i);
                        da.SelectCommand.Parameters.AddWithValue("@date", date);
                        da.Fill(d);

                        for (int n = 0; n < d.Rows.Count; n++)
                        {
                            DataRow row = d.Rows[n];
                            a.Add(row["Color"].ToString());
                        }

                        if (a.Count > 1)
                        {
                            cooler.Add(11);
                        }

                        else if (a.Count == 1)
                        {
                            switch (a.First().Trim())
                            {
                                case "red":
                                    cooler.Add(1);
                                    break;
                                case "blue":
                                    cooler.Add(2);
                                    break;
                                case "moccasin":
                                    cooler.Add(3);
                                    break;
                                case "yellow":
                                    cooler.Add(4);
                                    break;
                                case "purple":
                                    cooler.Add(5);
                                    break;
                                case "aqua":
                                    cooler.Add(6);
                                    break;
                                case "pink":
                                    cooler.Add(7);
                                    break;
                                case "orange":
                                    cooler.Add(8);
                                    break;
                                case "lime":
                                    cooler.Add(9);
                                    break;
                                case "black":
                                    cooler.Add(10);
                                    break;
                                default:
                                    cooler.Add(1);
                                    break;
                            }
                        }
                        else
                        {
                            cooler.Add(0);
                        }


                    }
                }
            }
            return Json(cooler, JsonRequestBehavior.AllowGet);
        }
    }
}