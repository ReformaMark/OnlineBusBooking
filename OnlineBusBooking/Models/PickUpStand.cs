//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnlineBusBooking.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PickUpStand
    {
        public int StandId { get; set; }
        public Nullable<int> RouteId { get; set; }
        public string PlaceName { get; set; }
        public string PlaceTime { get; set; }
        public Nullable<int> BusID { get; set; }
    }
}
