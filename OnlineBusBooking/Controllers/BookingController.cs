
using OnlineBusBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusBookingSystem.Controllers
{
    public class BookingController : Controller
    {
        OnlineBusBookingEntities context = new OnlineBusBookingEntities();
        // GET: Booking
        public ActionResult Report()
        {

            var bookingReport = context.BookingMasters.ToList();
            return View(bookingReport);
        }
    }
}