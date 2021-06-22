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
    public class CoolerGraphicController : Controller
    {

  
        DataContext db = new DataContext();
        string connectionString = "Data Source=localhost; Initial Catalog = VassilikoFilters; Integrated Security = True; MultipleActiveResultSets=True;";
        // GET: CoolerGraphic

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin,Super")]
        public ActionResult ReplacementForm(string Compartment_ID)
        {
            Session["Compartment"] = Compartment_ID;

            var data = db.Compartments.SqlQuery("select * from Compartments where Compartment_ID=@p0", Compartment_ID).ToList();


            return View(data);
        }
        [HttpPost]
        public JsonResult GetReason()
        {

            var reasons = new List<string>();

            var data = db.Replacement_Reasons.SqlQuery("Select * From Replacement_Reasons where Category=4 or Category=5").ToList();


            foreach (var item in data)
            {
                reasons.Add(item.Replacement_Description);
            }



            return Json(reasons, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetNozles()
        {

            var nozles = new List<string>();

            var data = db.Nozle_Types.SqlQuery("Select * From Nozle_Types ").ToList();


            foreach (var item in data)
            {
                nozles.Add(item.Nozle_Type);
            }



            return Json(nozles, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSpares(string compartment, string supplier)
        {
            var supid = 0;
            supplier = supplier.Trim();
            var data5 = db.Nozle_Suppliers.SqlQuery("Select * From Nozle_Suppliers Where Supplier=@p0", supplier).ToList();
            foreach(var item5 in data5)
            {
                supid = item5.Supplier_ID;
            }
            Session["compartment"] = compartment;
            var spares = new List<string>();
            if (!string.IsNullOrWhiteSpace(compartment))
            {
                if (compartment == "1")
                {
                    var nozle = "Ancor";
                    Session["Nozle_Type"] = nozle;
                    var data1 = db.Nozle_Types.SqlQuery("Select * From Nozle_Types Where Nozle_Type=@p0", nozle).ToList();
                    foreach (var item1 in data1)
                    {
                        var data = db.Nozle_Spares.SqlQuery("Select * From Nozle_Spares Where Nozle_ID=@p0 and Supplier_ID=@p1"
                                , item1.Nozle_ID,supid).ToList();

                        foreach (var item in data)
                        {
                            spares.Add(item.Description);
                        }

                    }
                }
                else if (compartment == "2")
                {
                    var nozle = "Coanda for KIDS";
                    Session["Nozle_Type"] = nozle;
                    var data1 = db.Nozle_Types.SqlQuery("Select * From Nozle_Types Where Nozle_Type=@p0", nozle).ToList();
                    foreach (var item1 in data1)
                    {
                        var data = db.Nozle_Spares.SqlQuery("Select * From Nozle_Spares Where Nozle_ID=@p0 and Supplier_ID=@p1"
                                , item1.Nozle_ID,supid).ToList();

                        foreach (var item in data)
                        {
                            spares.Add(item.Description);
                        }

                    }
                }
                else
                {
                    var nozle = "Coanda for cooler";
                    Session["Nozle_Type"] = nozle;
                    var data1 = db.Nozle_Types.SqlQuery("Select * From Nozle_Types Where Nozle_Type=@p0", nozle).ToList();
                    foreach (var item1 in data1)
                    {
                        var data = db.Nozle_Spares.SqlQuery("Select * From Nozle_Spares Where Nozle_ID=@p0 and Supplier_ID=@p1"
                                , item1.Nozle_ID,supid).ToList();

                        foreach (var item in data)
                        {
                            spares.Add(item.Description);
                        }

                    }
                }
            }
            return Json(spares, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public void AddToSession(string repDate, string reason, string[] spare)
        {
            Session["Replacement_Date"] = repDate;

            Session["Reason"] = reason;
            Session["Spare"] = spare;
        }
        [Authorize(Roles = "Admin,Super")]
        public ActionResult IndexAdmin(string Compartment_ID)
        {
            string cDate = DateTime.Now.ToString("M/d/yyyy");
            

            var data = db.Compartments.SqlQuery("select * from Compartments").ToList();
            ViewBag.comp = data.Count;

            ViewBag.now = Compartment_ID;
            Session["Compartment"] = Compartment_ID;
            data = db.Compartments.SqlQuery("select * from Compartments where Compartment_ID=@p0", Compartment_ID).ToList();

            foreach (var item in data)
            {
                ViewBag.final = item.No_Of_Beams;
                int[,] x = new int[item.No_Of_Nozles + 1, item.No_Of_Beams + 1];

                for (int i = 1; i <= item.No_Of_Nozles; i++)
                {
                    for (int j = 1; j <= item.No_Of_Beams; j++)
                    {

                        var data1 = db.Replacements.SqlQuery("select * from Replacements where Compartment_ID=@p0 AND  Nozle_no=@p1  AND Beam_no=@p2 ", Compartment_ID, i, j).ToList();
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
        public ActionResult dateChange(string value)
        {
            Session["Replacement_Date"] = value;
            return RedirectToAction("IndexAdmin", "CoolerGraphic", new { Compartment = Session["Compartment"] });
        }
        public ActionResult reasonChange(string value)
        {
            Session["Reason"] = value;
            return RedirectToAction("IndexAdmin", "CoolerGraphic", new { Compartment = Session["Compartment"] });
        }
        public ActionResult NozleChange(string value)
        {
            Session["Nozle_Type"] = value;
            return RedirectToAction("IndexAdmin", "CoolerGraphic", new { Compartment = Session["Compartment"] });
        }
        public ActionResult spareChange(string[] value)
        {
            Session["Spare"] = value;
            return RedirectToAction("IndexAdmin", "CoolerGraphic", new { Compartment = Session["Compartment"] });
        }
        [Authorize(Roles = "Admin,Super")]
        public ActionResult Replacement(int[] values, string repDate, string reason, string comp, string[] spare)
        {
            if (comp != "1")
            {
                int nozleid = 0;
                string nozle = Session["Nozle_Type"].ToString();
                string cDate = DateTime.Now.ToString("M/d/yyyy");
                List<object> lst = new List<object>();
                var data1 = db.Nozle_Types.SqlQuery("select * from Nozle_Types where Nozle_Type=@p0", nozle).ToList();
                foreach (var item in data1)
                {
                    nozleid = item.Nozle_ID;
                    lst.Add(item.Nozle_ID);
                }
                var data2 = db.Replacement_Reasons.SqlQuery("select * from Replacement_Reasons where Replacement_Description=@p0", reason).ToList();
                foreach (var item in data2)
                {
                    lst.Add(item.Replacement_Reason_Code);
                }
                for (int x = 0; x < spare.Length; x++)
                {
                    var data3 = db.Nozle_Spares.SqlQuery("select * from Nozle_Spares where Description=@p0 and Nozle_ID = @p1", spare[x], nozleid).ToList();
                    foreach (var item in data3)
                    {
                        lst.Add(item.Spare_ID);
                    }

                    lst.Add(repDate);
                    lst.Add(comp);
                    lst.Add(cDate);
                    var data = db.Compartments.SqlQuery("select * from Compartments where Compartment_ID=@p0", comp).ToList();
                    foreach (var item in data)
                    {

                        int nozles = item.No_Of_Nozles;

                        for (int i = 0; i < values.Length; i++)
                        {
                            if (values[i] == 1)
                            {
                                for (int j = 1; j <= nozles; j++)
                                {

                                    lst.Add(i + 1);
                                    lst.Add(j);
                                    object[] allitems = lst.ToArray();
                                    int output = db.Database.ExecuteSqlCommand("insert into Replacements(Beam_no,Nozle_no,Spare_ID,Compartment_ID,Nozle_ID,Date,Replacement_Reason_Code,Create_Date) values(@p6,@p7,@p2,@p4,@p0,@p3,@p1,@p5)", allitems);

                                    lst.RemoveAt(7);
                                    lst.RemoveAt(6);

                                    if (output > 0)
                                    {
                                        ViewBag.msg = "Replacement is added";

                                    }
                                }
                            }
                        }
                    }
                    lst.RemoveAt(5);
                    lst.RemoveAt(4);
                    lst.RemoveAt(3);
                    lst.RemoveAt(2);
                }
            }
            else
            {
                List<object> lst = new List<object>();
                var data3 = db.Replacement_Reasons.SqlQuery("select * from Replacement_Reasons where Replacement_Description=@p0", reason).ToList();
                foreach (var item in data3)
                {
                    lst.Add(item.Replacement_Reason_Code);
                }
                lst.Add(comp);
                lst.Add(repDate);

                var nozleid = 0;

                string cDate = DateTime.Now.ToString("M/d/yyyy");
                lst.Add(cDate);
                var data = db.Compartments.SqlQuery("select * from Compartments where Compartment_ID=@p0", comp).ToList();
                foreach (var item in data)
                {
                    int nozles = item.No_Of_Nozles;
                    for (int i = 0; i < values.Length; i++)
                    {
                        if (values[i] == 1)
                        {
                            for (int j = 1; j <= nozles; j++)
                            {
                                if ((j == 2 || j == 4 || j == 6 || j == 8 || j == 10 || j == 12 || j == 14 || j == 16 || j == 18 || j == 19 || j == 20 || j == 22 || j == 24 || j == 26 || j == 28 || j == 30 || j == 32 || j == 34 || j == 36) && (i + 1 == 1))
                                {
                                    lst.Add("1");
                                    lst.Add(i + 1);
                                    lst.Add(j);
                                    nozleid = 1;
                                    var data2 = db.Nozle_Spares.SqlQuery("select * from Nozle_Spares where Nozle_ID = @p0", nozleid).ToList();
                                    foreach (var item2 in data2)
                                    {
                                        lst.Add(item2.Spare_ID);
                                        object[] allitems = lst.ToArray();
                                        int output = db.Database.ExecuteSqlCommand("insert into Replacements(Beam_no,Nozle_no,Spare_ID,Compartment_ID,Nozle_ID,Date,Replacement_Reason_Code,Create_Date) values(@p5,@p6,@p7,@p1,@p4,@p2,@p0,@p3)", allitems);
                                        lst.RemoveAt(7);
                                    }
                                    lst.RemoveAt(6);
                                    lst.RemoveAt(5);
                                    lst.RemoveAt(4);
                                }
                                else if ((j == 2 || j == 4 || j == 6 || j == 8 || j == 10 || j == 12 || j == 14 || j == 18 || j == 19 || j == 20 || j == 22 || j == 24 || j == 26 || j == 28 || j == 30 || j == 32 || j == 34 || j == 36) && (i + 1 == 2 || i + 1 == 3))
                                {
                                    lst.Add("1");
                                    lst.Add(i + 1);
                                    lst.Add(j);
                                    nozleid = 1;
                                    var data2 = db.Nozle_Spares.SqlQuery("select * from Nozle_Spares where Nozle_ID = @p0", nozleid).ToList();
                                    foreach (var item2 in data2)
                                    {
                                        lst.Add(item2.Spare_ID);
                                        object[] allitems = lst.ToArray();
                                        int output = db.Database.ExecuteSqlCommand("insert into Replacements(Beam_no,Nozle_no,Spare_ID,Compartment_ID,Nozle_ID,Date,Replacement_Reason_Code,Create_Date) values(@p5,@p6,@p7,@p1,@p4,@p2,@p0,@p3)", allitems);
                                        lst.RemoveAt(7);
                                    }
                                    lst.RemoveAt(6);
                                    lst.RemoveAt(5);
                                    lst.RemoveAt(4);
                                }
                                else if (j == 16 && (i + 1 == 2 || i + 1 == 3))
                                {
                                    lst.Add("3");
                                    lst.Add(i + 1);
                                    lst.Add(j);
                                    nozleid = 3;
                                    var data2 = db.Nozle_Spares.SqlQuery("select * from Nozle_Spares where Nozle_ID = @p0", nozleid).ToList();
                                    foreach (var item2 in data2)
                                    {
                                        lst.Add(item2.Spare_ID);
                                        object[] allitems = lst.ToArray();
                                        int output = db.Database.ExecuteSqlCommand("insert into Replacements(Beam_no,Nozle_no,Spare_ID,Compartment_ID,Nozle_ID,Date,Replacement_Reason_Code,Create_Date) values(@p5,@p6,@p7,@p1,@p4,@p2,@p0,@p3)", allitems);
                                        lst.RemoveAt(7);
                                    }
                                    lst.RemoveAt(6);
                                    lst.RemoveAt(5);
                                    lst.RemoveAt(4);
                                }
                                else
                                {
                                    lst.Add("2");
                                    lst.Add(i + 1);
                                    lst.Add(j);
                                    nozleid = 2;
                                    var data2 = db.Nozle_Spares.SqlQuery("select * from Nozle_Spares where Nozle_ID = @p0", nozleid).ToList();
                                    foreach (var item2 in data2)
                                    {
                                        lst.Add(item2.Spare_ID);
                                        object[] allitems = lst.ToArray();
                                        int output = db.Database.ExecuteSqlCommand("insert into Replacements(Beam_no,Nozle_no,Spare_ID,Compartment_ID,Nozle_ID,Date,Replacement_Reason_Code,Create_Date) values(@p5,@p6,@p7,@p1,@p4,@p2,@p0,@p3)", allitems);
                                        lst.RemoveAt(7);
                                    }
                                    lst.RemoveAt(6);
                                    lst.RemoveAt(5);
                                    lst.RemoveAt(4);
                                }
                            }
                        }
                    }
                }
            }
            return RedirectToAction("IndexAdmin", "CoolerGraphic", new { @Compartment = Session["Compartment"] });
        }
        public ActionResult Delete(string id, string button)

        {

            List<object> lst = new List<object>();
            char[] delimiterChars = { ' ', ',', '.', ':', '\t' };
            string[] words = button.Split(delimiterChars);

            Session["Compartment"] = id;

            var nozle = words[0];
            var beam = words[1];
            var repDate = Session["Replacement_Date"];

            string cDate = DateTime.Now.ToString("M/d/yyyy");
            lst.Add(beam);
            lst.Add(nozle);
            lst.Add(repDate);
            lst.Add(id);
            lst.Add(cDate);
            object[] allitems = lst.ToArray();

            var data3 = db.Replacements.SqlQuery("select  * from Replacements where Beam_no=@p0 AND Nozle_no=@p1 AND Date=@p2 AND Compartment_ID=@p3 AND Create_Date=@p4", allitems).ToList();
            int replaceID = 0;
            foreach (var item in data3)
            {
                replaceID = item.Replacement_ID;
            }

            int output = db.Database.ExecuteSqlCommand("delete from Replacements where Replacement_ID=@p0", replaceID);
            TempData["buttonval"] = id;


            return RedirectToAction("IndexAdmin", "CoolerGraphic", new { @Compartment = id });



        }
        [Authorize(Roles = "Admin,Super")]
        public ActionResult Click(string id, string button)

        {
            if (id != "1")
            {
                List<object> lst = new List<object>();
                char[] delimiterChars = { ' ', ',', '.', ':', '\t' };
                string[] words = button.Split(delimiterChars);
                int nozleid = 0;
                Session["Compartment"] = id;
                
                var nozleno = words[0];
                var beamno = words[1];
                var repDate = Session["Replacement_Date"];

                string cDate = DateTime.Now.ToString("M/d/yyyy");
                lst.Add(beamno);
                lst.Add(nozleno);
                var nozle = Session["Nozle_Type"];
                var reason = Session["Reason"];
                var spare = Session["Spare"];
                string[] spares = spare as string[];
                var data1 = db.Nozle_Types.SqlQuery("select * from Nozle_Types where Nozle_Type=@p0", nozle).ToList();
                foreach (var item in data1)
                {
                    nozleid = item.Nozle_ID;
                    lst.Add(item.Nozle_ID);
                }

                for (int x = 0; x < spares.Length; x++)
                {
                    var data3 = db.Nozle_Spares.SqlQuery("select * from Nozle_Spares where Description=@p0 AND Nozle_ID=@p1", spares[x], nozleid).ToList();
                    foreach (var item in data3)
                    {
                        lst.Add(item.Spare_ID);
                    }
                    lst.Add(id);

                    lst.Add(repDate);
                    var data2 = db.Replacement_Reasons.SqlQuery("select * from Replacement_Reasons where Replacement_Description=@p0", reason).ToList();
                    foreach (var item in data2)
                    {
                        lst.Add(item.Replacement_Reason_Code);
                    }
                    lst.Add(cDate);
                    object[] allitems = lst.ToArray();


                    int output = db.Database.ExecuteSqlCommand("insert into Replacements(Beam_no,Nozle_no,Spare_ID,Compartment_ID,Nozle_ID,Date,Replacement_Reason_Code,Create_Date) values(@p0,@p1,@p3,@p4,@p2,@p5,@p6,@p7)", allitems);
                    lst.RemoveAt(7);
                    lst.RemoveAt(6);
                    lst.RemoveAt(5);
                    lst.RemoveAt(4);
                    lst.RemoveAt(3);
                }
            }
            else
            {
                var reason = Session["Reason"];
                char[] delimiterChars = { ' ', ',', '.', ':', '\t' };
                string[] words = button.Split(delimiterChars);
                var nozleno = words[0];
                var beamno = words[1];
                List<object> lst = new List<object>();
                var data3 = db.Replacement_Reasons.SqlQuery("select * from Replacement_Reasons where Replacement_Description=@p0", reason).ToList();
                foreach (var item in data3)
                {
                    lst.Add(item.Replacement_Reason_Code);
                }
                lst.Add(id);
                var repDate = Session["Replacement_Date"];
                lst.Add(repDate);

                var nozleid = 0;

                string cDate = DateTime.Now.ToString("M/d/yyyy");
                lst.Add(cDate);



                if ((nozleno == "2" || nozleno == "4" || nozleno == "6" || nozleno == "8" || nozleno == "10" || nozleno == "12" || nozleno == "14" || nozleno == "16" || nozleno == "18" || nozleno == "19" || nozleno == "20" || nozleno == "22" || nozleno == "24" || nozleno == "26" || nozleno == "28" || nozleno == "30" || nozleno == "32" || nozleno == "34" || nozleno == "36") && (beamno == "1"))
                {
                    lst.Add("1");
                    lst.Add(beamno);
                    lst.Add(nozleno);
                    nozleid = 1;
                    var data2 = db.Nozle_Spares.SqlQuery("select * from Nozle_Spares where Nozle_ID = @p0", nozleid).ToList();
                    foreach (var item2 in data2)
                    {
                        lst.Add(item2.Spare_ID);
                        object[] allitems = lst.ToArray();
                        int output = db.Database.ExecuteSqlCommand("insert into Replacements(Beam_no,Nozle_no,Spare_ID,Compartment_ID,Nozle_ID,Date,Replacement_Reason_Code,Create_Date) values(@p5,@p6,@p7,@p1,@p4,@p2,@p0,@p3)", allitems);
                        lst.RemoveAt(7);
                    }
                    lst.RemoveAt(6);
                    lst.RemoveAt(5);
                    lst.RemoveAt(4);
                }
                else if ((nozleno == "2" || nozleno == "4" || nozleno == "6" || nozleno == "8" || nozleno == "10" || nozleno == "12" || nozleno == "14" || nozleno == "18" || nozleno == "19" || nozleno == "20" || nozleno == "22" || nozleno == "24" || nozleno == "26" || nozleno == "28" || nozleno == "30" || nozleno == "32" || nozleno == "34" || nozleno == "36") && (beamno == "2" || beamno == "3"))
                {
                    lst.Add("1");
                    lst.Add(beamno);
                    lst.Add(nozleno);
                    nozleid = 1;
                    var data2 = db.Nozle_Spares.SqlQuery("select * from Nozle_Spares where Nozle_ID = @p0", nozleid).ToList();
                    foreach (var item2 in data2)
                    {
                        lst.Add(item2.Spare_ID);
                        object[] allitems = lst.ToArray();
                        int output = db.Database.ExecuteSqlCommand("insert into Replacements(Beam_no,Nozle_no,Spare_ID,Compartment_ID,Nozle_ID,Date,Replacement_Reason_Code,Create_Date) values(@p5,@p6,@p7,@p1,@p4,@p2,@p0,@p3)", allitems);
                        lst.RemoveAt(7);
                    }
                    lst.RemoveAt(6);
                    lst.RemoveAt(5);
                    lst.RemoveAt(4);
                }
                else if (nozleno == "16" && (beamno == "2" || beamno == "3"))
                {
                    lst.Add("3");
                    lst.Add(beamno);
                    lst.Add(nozleno);
                    nozleid = 3;
                    var data2 = db.Nozle_Spares.SqlQuery("select * from Nozle_Spares where Nozle_ID = @p0", nozleid).ToList();
                    foreach (var item2 in data2)
                    {
                        lst.Add(item2.Spare_ID);
                        object[] allitems = lst.ToArray();
                        int output = db.Database.ExecuteSqlCommand("insert into Replacements(Beam_no,Nozle_no,Spare_ID,Compartment_ID,Nozle_ID,Date,Replacement_Reason_Code,Create_Date) values(@p5,@p6,@p7,@p1,@p4,@p2,@p0,@p3)", allitems);
                        lst.RemoveAt(7);
                    }
                    lst.RemoveAt(6);
                    lst.RemoveAt(5);
                    lst.RemoveAt(4);
                }
                else
                {
                    lst.Add("2");
                    lst.Add(beamno);
                    lst.Add(nozleno);
                    nozleid = 2;
                    var data2 = db.Nozle_Spares.SqlQuery("select * from Nozle_Spares where Nozle_ID = @p0", nozleid).ToList();
                    foreach (var item2 in data2)
                    {
                        lst.Add(item2.Spare_ID);
                        object[] allitems = lst.ToArray();
                        int output = db.Database.ExecuteSqlCommand("insert into Replacements(Beam_no,Nozle_no,Spare_ID,Compartment_ID,Nozle_ID,Date,Replacement_Reason_Code,Create_Date) values(@p5,@p6,@p7,@p1,@p4,@p2,@p0,@p3)", allitems);
                        lst.RemoveAt(7);
                    }
                    lst.RemoveAt(6);
                    lst.RemoveAt(5);
                    lst.RemoveAt(4);
                }


            }
            return RedirectToAction("IndexAdmin", "CoolerGraphic", new { @Compartment_ID = id });



        }
        [Authorize]
        
        public ActionResult IndexUser(string Compartment_ID)
        {
            string cDate = DateTime.Now.ToString("M/d/yyyy");

            string Compartment = Compartment_ID;
            var data = db.Compartments.SqlQuery("select * from Compartments").ToList();
            ViewBag.comp = data.Count;

            ViewBag.now = Compartment_ID;
            Session["Compartment"] = Compartment;
            data = db.Compartments.SqlQuery("select * from Compartments where Compartment_ID=@p0", Compartment).ToList();

            foreach (var item in data)
            {
                ViewBag.final = item.No_Of_Beams;
                int[,] x = new int[item.No_Of_Nozles + 1, item.No_Of_Beams + 1];
                string[,] colors = new string[item.No_Of_Nozles + 1, item.No_Of_Beams + 1];

                for (int i = 1; i <= item.No_Of_Nozles; i++)
                {
                    for (int j = 1; j <= item.No_Of_Beams; j++)
                    {


                        DataTable d = new DataTable();
                        using (SqlConnection con = new SqlConnection(connectionString))
                        {
                            con.Open();
                       
                            HashSet<string> a = new HashSet<string>();

                            SqlDataAdapter da = new SqlDataAdapter("WITH cte AS( SELECT nos.Color,ROW_NUMBER() OVER(PARTITION BY ns.Spare_Store_Id ORDER BY r.Date DESC) AS rn FROM Replacements r INNER JOIN Nozle_Spares ns ON r.Spare_ID = ns.Spare_ID INNER JOIN Nozle_Suppliers nos ON nos.Supplier_ID = ns.Supplier_ID WHERE Compartment_ID = @comp  AND Nozle_no = @nozle AND Beam_no = @beam) SELECT * FROM cte WHERE rn = 1", con);
                            da.SelectCommand.Parameters.AddWithValue("@comp", Compartment);
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
                                x[i, j] = 1;
                                colors[i, j] = "magenta";
                            }

                            else if (a.Count == 1)
                            {
                                x[i, j] = 1;
                                colors[i, j] = a.First().Trim();
                            }

                        }

                    }

                }

                ViewBag.x = x;
                ViewBag.color = colors;
            }

            return View(data);
        }
        [Authorize]
      
        public ActionResult UserClick(string id, string button)

        {
            char[] delimiterChars = { ' ', ',', '.', ':', '\t' };
            string[] words = button.Split(delimiterChars);


            var data = db.Replacements.SqlQuery("select * from Replacements where Compartment_ID=@p0  AND Nozle_no=@p1  AND Beam_no=@p2 ORDER BY Date DESC ", id, words[0], words[1]).ToList();
            if (data.Count < 1)
            {
                TempData["buttonval"] = "This nozle has no replacements";
                return RedirectToAction("IndexUser", "CoolerGraphic", new { @Compartment_ID = id });
            }
            else
            {
                TempData["buttonval"] = id;
                return View(data);
            }
        }
        public JsonResult GetSuppliers(string compartment)
        {

            var suppliers = new List<String>();

            var data = db.Nozle_Suppliers.SqlQuery("Select * From Nozle_Suppliers").ToList();

            foreach (var item in data)
            {
                suppliers.Add(item.Supplier);
            }

            return Json(suppliers, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetColors()
        {

            var colors = new List<String>();

            var data = db.Nozle_Suppliers.SqlQuery("Select * From Nozle_Suppliers").ToList();

            foreach (var item in data)
            {
                colors.Add(item.Color);
            }

            return Json(colors, JsonRequestBehavior.AllowGet);
        }

    }
}

