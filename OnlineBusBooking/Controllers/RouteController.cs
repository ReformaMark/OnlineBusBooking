
using OnlineBusBooking.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusBookingSystem.Controllers
{
    
    public class RouteController : Controller
    {
        OnlineBusBookingEntities context = new OnlineBusBookingEntities();
        // GET: Route
        public ActionResult Details()
        {
            if (Session["employeeRole"] != null)
            {
                if (Session["employeeRole"].ToString() == "User")
                {
                    return HttpNotFound();
                }
                var routes = context.Routes.ToList();
                return View(routes);
            }

            return HttpNotFound();
        }

        public ActionResult AddRoute()
        {
            if (Session["employeeRole"] != null)
            {
                if (Session["employeeRole"].ToString() == "User")
                {
                    return HttpNotFound();
                }
                Route route = new Route();
                route.destinationCollection = context.Destinations.ToList();
                route.originCollection = context.Origintbls.ToList();
                route.busCollection = context.BusMasters.ToList();

                return View(route);
            }
            return HttpNotFound();

        }


        [HttpPost]
        public ActionResult AddRoute(Route route)
        {
            Route r = new Route();
            r.destinationCollection = context.Destinations.ToList();
            r.originCollection = context.Origintbls.ToList();
            r.busCollection = context.BusMasters.ToList();
            if (ModelState.IsValid)
            {
                bool hasRoute = context.Routes.Any(x => x.Date == route.Date && x.BusId == route.BusId);
                if (hasRoute)
                {


                    TempData["errAlert"] = "The Bus you are trying to add had already scheduled on Date and time you selected.";
                    return View(r);
                }
                context.Routes.Add(route);
                context.SaveChanges();
                TempData["addRouteAlert"] = "Added Bus Route...";
                ModelState.Clear();
            }
            else
            {
                ModelState.AddModelError("", "Error");
            }
            return View(r);
        }

        public ActionResult AddOrigin()
        {
            if (Session["employeeRole"] != null)
            {
                if (Session["employeeRole"].ToString() == "User")
                {
                    return HttpNotFound();
                }
                return View();
            }

            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult AddOrigin(Origintbl origintbl)
        {

            context.Origintbls.Add(origintbl);
            context.SaveChanges();
            return View(origintbl);
        }

        public ActionResult AddDestination()
        {
            if (Session["employeeRole"] != null)
            {
                if (Session["employeeRole"].ToString() == "User")
                {
                    return HttpNotFound();
                }
                Destination destination = new Destination();
                destination.originCollection = context.Origintbls.ToList();
                return View(destination);
            }
            return HttpNotFound();
        }


        [HttpPost]
        public ActionResult AddDestination(Destination destination)
        {
            if (Session["employeeRole"] != null)
            {
                if (Session["employeeRole"].ToString() == "User")
                {
                    return HttpNotFound();
                }
                context.Destinations.Add(destination);
                context.SaveChanges();
                return View(destination);
            }
            return HttpNotFound();
        }
        public ActionResult Edit(int id)
        {
            if (Session["employeeRole"] != null)
            {
                if (Session["employeeRole"].ToString() == "User")
                {
                    return HttpNotFound();
                }
                Route route = context.Routes.Single(x => x.RouteId == id);
                return View(route);
            }
            return HttpNotFound();
        }
        //POST: Edit/id
        [HttpPost]
        public ActionResult Edit(Route route)
        {
            context.Entry(route).State = EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("Details", "Route");
        }
    }
}