using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Cooler.Models
{
    public class DataContext :DbContext
    {
        public DataContext(): base("CoolerContext") {}
        public DbSet<User> Users { get; set; }

        public DbSet<Material_Types> Material_Types { get; set; }
        public DbSet<Bag_Suppliers> Bag_Suppliers { get; set; }
        public DbSet<Bag_Types> Bag_Types { get; set; }
        public DbSet<Nozle_Types> Nozle_Types { get; set; }
        public DbSet<Nozle_Spares> Nozle_Spares { get; set; }
        public DbSet<Replacement_Reasons> Replacement_Reasons { get; set; }
        public DbSet<Vibrator> Vibrators { get; set; }
        public DbSet<Filter> Filters { get; set; }

        public DbSet<Compartment> Compartments { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Valves_Maintenance> Valves_Maintenance { get; set; }
        public DbSet<Bag_Replacement> Bag_Replacement { get; set; }
        public DbSet<Moveable_Beams> Moveable_Beams { get; set; }
        public DbSet<Replacement> Replacements { get; set; }
        public DbSet<Fiber_Brand> Fiber_Brand { get; set; }
        public DbSet<Cages_Maintenance> Cages_Maintenance { get; set; }
        public DbSet<Colors> Colors { get; set; }
        public DbSet<Nozle_Colors> Nozle_Colors { get; set; }
        public DbSet<Nozle_Suppliers> Nozle_Suppliers { get; set; }
       
    }
}