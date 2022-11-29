
using OnlineBusBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusBookingSystem.Controllers
{
    public class HomeController : Controller
    {
        OnlineBusBookingEntities context = new OnlineBusBookingEntities();

        public ActionResult Index()
        {
            if (Session["employeeRole"] != null)
            {
                if (Session["employeeRole"].ToString() == "Admin" || Session["employeeRole"].ToString() == "Staff")
                {
                    return RedirectToAction("Employee", "Admin");
                }
            }

            List<Origintbl> origintList = context.Origintbls.ToList();
            ViewBag.OriginList = new SelectList(origintList, "OriginID", "Origin");

            return View();
        }
        public JsonResult GetDestinationList(int OriginID)
        {
            context.Configuration.ProxyCreationEnabled = false;
            List<Destination> destinations = context.Destinations.Where(x => x.OriginID == OriginID).ToList();
            return Json(destinations, JsonRequestBehavior.AllowGet);
        }


        public ActionResult SearchRoute(OriginDestinationViewModel searchRoute)
        {
            var routes = from r in context.Routes select r;

            routes = routes.Where(e => e.OriginId == searchRoute.OriginID && e.DesinationId == searchRoute.DestinationID && e.AvailableSeats != 0);

            return View(routes.ToList());
        }


        public ActionResult About()
        {
            if (User.Identity.Name == "Admin")
            {
                return RedirectToAction("Details", "Route");
            }
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            if (User.Identity.Name == "Admin")
            {
                return RedirectToAction("Details", "Route");
            }
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}