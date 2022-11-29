
using OnlineBusBooking.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusBookingSystem.Controllers
{
    [Authorize]
    public class BusController : Controller
    {
        OnlineBusBookingEntities context = new OnlineBusBookingEntities();

        // GET: Bus View list
        public ActionResult BusDetails()
        {
            if (Session["employeeRole"] != null)
            {
                if (Session["employeeRole"].ToString() == "User")
                {
                    return HttpNotFound();
                }
                var busItem = context.BusMasters.ToList();
                return View(busItem);
            }
            return HttpNotFound();

        }
        //Get: Edit/id
        public ActionResult Edit(int id)
        {
            if (Session["employeeRole"] != null)
            {
                if (Session["employeeRole"].ToString() == "User")
                {
                    return HttpNotFound();
                }
                BusMaster bus = context.BusMasters.Single(x => x.BusId == id);
                return View(bus);
            }

            return HttpNotFound();
        }

        //POST: Edit/id
        [HttpPost]
        public ActionResult Edit(BusMaster busDetails)
        {
            try
            {
                context.Entry(busDetails).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("BusDetails", "Bus");
            }
            catch
            {
                throw;
            }
        }

        //GET: AddBus
        public ActionResult AddBus()
        {
            if (Session["employeeRole"] != null)
            {
                if (Session["employeeRole"].ToString() == "User")
                {
                    return HttpNotFound();
                }
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
        //POST: AddBus
        [HttpPost]
        public ActionResult AddBus(BusMaster busDetails)
        {
            if (Session["employeeRole"] != null)
            {
                if (Session["employeeRole"].ToString() == "User")
                {
                    return HttpNotFound();
                }
                var hasTheSame = context.BusMasters.Any(x => x.BusNo == busDetails.BusNo);
                if (hasTheSame)
                {
                    ModelState.AddModelError("BusNo", "Bus number already exists");
                    return View(busDetails);
                }
                context.BusMasters.Add(busDetails);
                context.SaveChanges();
                ModelState.Clear();

                return RedirectToAction("BusDetails", "Bus");
            }

            return HttpNotFound();
        }

        public ActionResult DeleteBus(int? BusId)
        {
            if (Session["employeeRole"] != null)
            {
                if (Session["employeeRole"].ToString() == "User")
                {
                    return HttpNotFound();
                }
                BusMaster bus = context.BusMasters.Find(BusId);

                return View(bus);


            }
            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult DeleteBus(int BusId)
        {
            if (User.Identity.Name == "Admin")
            {
                BusMaster bus = context.BusMasters.Find(BusId);
                context.BusMasters.Remove(bus);
                context.SaveChanges();
                return RedirectToAction("BusDetais", "Bus");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}