﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class OnlineBusBookingEntities : DbContext
    {
        public OnlineBusBookingEntities()
            : base("name=OnlineBusBookingEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BookingMaster> BookingMasters { get; set; }
        public virtual DbSet<BusMaster> BusMasters { get; set; }
        public virtual DbSet<CardDetail> CardDetails { get; set; }
        public virtual DbSet<CityDetail> CityDetails { get; set; }
        public virtual DbSet<Destination> Destinations { get; set; }
        public virtual DbSet<Origintbl> Origintbls { get; set; }
        public virtual DbSet<PickUpStand> PickUpStands { get; set; }
        public virtual DbSet<PNRDetail> PNRDetails { get; set; }
        public virtual DbSet<Registration> Registrations { get; set; }
        public virtual DbSet<Route> Routes { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<ScheduleMaster> ScheduleMasters { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}