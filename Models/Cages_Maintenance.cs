//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Cooler.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Cages_Maintenance
    {
        public int Cage_Code { get; set; }
        public System.DateTime Date { get; set; }
        public string Filter_Code { get; set; }
        public int Replacement_Reason_Code { get; set; }
        public int Sector_No { get; set; }
        public int Valve_No { get; set; }
        public int Bag_No { get; set; }
    
        public virtual Filter Filter { get; set; }
        public virtual Replacement_Reasons Replacement_Reasons { get; set; }
    }
}