
using OnlineBusBooking.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusBookingSystem.Controllers
{
    
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
        public ActionResult EditBus(int id)
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
        public ActionResult EditBus(BusMaster busDetails, HttpPostedFileBase file)
        { 
            BusMaster bus = context.BusMasters.Single(c => c.BusId == busDetails.BusId);
            
            if (file != null)
            {
                string filename = Path.GetFileName(file.FileName);
                string _filename = DateTime.Now.ToString("hhmmssfff") + filename;
                string extention = Path.GetExtension(file.FileName).ToString();
                string path = Path.Combine(Server.MapPath("~/Images/"), _filename);
                busDetails.Image = "~/Images/" + _filename;

                Session["imgPath"] = bus.Image;


                if (file.ContentLength< 10000000)
                {
                    bus.BusNo = busDetails.BusNo;
                    bus.BustType = busDetails.BustType;
                    bus.TotalSeat = busDetails.TotalSeat;
                    bus.SeatRow = busDetails.SeatRow;
                    bus.SeatColumn = busDetails.SeatColumn;
                    bus.BookedSeat = busDetails.BookedSeat;
                    bus.AvailableSeats = busDetails.AvailableSeats;
                    bus.BusName = busDetails.BusName;
                    if (Session["imgPath"] != null)
                    {
                        string oldImage = Request.MapPath(Session["imgPath"].ToString());


                        if (System.IO.File.Exists(oldImage))
                        {
                            System.IO.File.Delete(oldImage);
                        }

                    }
                    file.SaveAs(path);
                    TempData["alertMsgEdit"] = "updated bus details!";
                    bus.Image = busDetails.Image;
                    context.SaveChanges();
                    return RedirectToAction("BusDetails", "Bus");
                }
                else
                {
                    ViewBag.msg = "File must less than or equal to 1MB";
                }
            }
            bus.BusNo = busDetails.BusNo;
            bus.BustType = busDetails.BustType;
            bus.TotalSeat = busDetails.TotalSeat;
            bus.SeatRow = busDetails.SeatRow;
            bus.SeatColumn = busDetails.SeatColumn;
            bus.BookedSeat = busDetails.BookedSeat;
            bus.AvailableSeats = busDetails.AvailableSeats;
            bus.BusName = busDetails.BusName;
            TempData["alertMsgEdit"] = "updated bus details!";
            context.SaveChanges();
            return RedirectToAction("BusDetails", "Bus");
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
        public ActionResult AddBus(BusMaster busDetails, HttpPostedFileBase image)
        {
            bool isExisting = context.BusMasters.Any(x => x.BusNo == busDetails.BusNo);
            if (isExisting)
            {
                ModelState.AddModelError("", "The Bus you are trying to add already existing");

            }
            if (Session["employeeRole"] != null)
            {
                if (Session["employeeRole"].ToString() == "User")
                {
                    return HttpNotFound();
                }              
                if (image != null)
                {
                    string filename = Path.GetFileName(image.FileName);
                    string _filename = DateTime.Now.ToString("hhmmssfff") + filename;
                    string extention = Path.GetExtension(image.FileName).ToString();
                    string path = Path.Combine(Server.MapPath("~/Images/"), _filename);
                    busDetails.Image = "~/Images/" + _filename;

                    context.BusMasters.Add(busDetails);


                    if (image.ContentLength < 5000000)
                    {
                        if (context.SaveChanges() > 0)
                        {
                           image.SaveAs(path);
                        }
                        TempData["alertMessage"] = "added bus!";
                        return View();
                    }
                    else
                    {
                        ViewBag.msg = "File must less than or equal to 5MB";
                    }
                }
                TempData["alertMessage"] = "added bus!";
                context.BusMasters.Add(busDetails);
                context.SaveChanges();
                ModelState.Clear();

                return RedirectToAction("BusDetails", "Bus");
            }

            return HttpNotFound();
        }

        public ActionResult DeleteBus(int id)
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

        [HttpPost]
        public ActionResult DeleteBUs(BusMaster busDetails)
        {
            try
            {
                BusMaster bus = context.BusMasters.Single(c => c.BusId == busDetails.BusId);
                if (bus != null)
                {
                    Session["imgPath"] = bus.Image;
                    if (Session["imgPath"] != null)
                    {
                        string Image = Request.MapPath(Session["imgPath"].ToString());
                        if (System.IO.File.Exists(Image))
                        {
                            System.IO.File.Delete(Image);
                        }
                    }

                    context.BusMasters.Remove(bus);
                    context.SaveChanges();
                    TempData["alertMessage"] = "deleted employee!";
                }
            } catch
            {
                TempData["busfk"] = "Error! The bus you are trying to delete has remaining booking or routes!";
                return RedirectToAction("BusDetails","Bus");
            }


            TempData["busfk"] = "Deleted successfully!";
            return RedirectToAction("BusDetails");
        }
    }
}
