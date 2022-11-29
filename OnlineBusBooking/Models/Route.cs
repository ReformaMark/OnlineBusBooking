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
    
    public partial class Route
    {
        public int RouteId { get; set; }
        public int OriginId { get; set; }
        public int DesinationId { get; set; }
        public string RouteName { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public int Fare { get; set; }
        public int AvailableSeats { get; set; }
        public int BusId { get; set; }
    
        public virtual BusMaster BusMaster { get; set; }
        public virtual Destination Destination { get; set; }
        public virtual Origintbl Origintbl { get; set; }

        public List <BusMaster> busCollection { get; set; }
        public List<Origintbl> originCollection { get; set; }
        public List<Destination> destinationCollection { get; set; }
    }
}
