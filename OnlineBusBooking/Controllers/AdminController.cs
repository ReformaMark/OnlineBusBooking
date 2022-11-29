using OnlineBusBooking.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusBookingSystem.Controllers
    {

      
        public class AdminController : Controller
        {
            OnlineBusBookingEntities context = new OnlineBusBookingEntities();
            // GET: Employee
            public ActionResult Employee()
            {
            if (Session["userUsername"] != null && Session["employeeRole"] != null)
            {
                if (Session["employeeRole"].ToString() == "User")
                {
                     return HttpNotFound();
                }
                var admin = "Admin";
                var staff = "Staff";

                var employees = from e in context.Users select e;

                employees = employees.Where(e => e.Role.Contains(admin) || e.Role.Contains(staff));

                return View(employees.ToList());
            } else
            {
                return HttpNotFound();
            }
                
        }



            public ActionResult Add([Bind(Include = "EmployeeId,Name,Username,Email,Password,Image,Role,ImageFile")] User employee)
            {


                bool isUsername = context.Users.Any(x => x.Username == employee.Username || x.Email == employee.Email);
                if (isUsername)
                {
                    ModelState.AddModelError("", "Username or Email already exists");

                }
                if (employee.ImageFile != null)
                {
                    string fiename = Path.GetFileName(employee.ImageFile.FileName);
                    string _filename = DateTime.Now.ToString("hhmmssfff") + fiename;
                    string path = Path.Combine(Server.MapPath("~/Images/"), _filename);
                    employee.Image = "~/Images/" + _filename;
                    employee.ConfirmPassword = employee.Password;
                    context.Users.Add(employee);
                    if (employee.ImageFile.ContentLength < 2000000)
                    {
                        if (context.SaveChanges() > 0)
                        {
                            employee.ImageFile.SaveAs(path);
                        }
                        TempData["alertMessage"] = "added employee!";
                        return RedirectToAction("Employee", "Admin");
                    }
                    else
                    {
                        ViewBag.msg = "File must less than or equal to 5MB";
                    }
                }
                employee.ConfirmPassword = employee.Password;
                context.Users.Add(employee);
                context.SaveChanges();


                return RedirectToAction("Employee", "Admin");

            }

            [HttpGet]
            public ActionResult Edit(int? id)
            {

                if (id == null)
                {
                    return HttpNotFound();
                }
                User u = context.Users.Where(x => x.EmployeeId == id).FirstOrDefault();
                if (u == null)
                {
                    return HttpNotFound();
                }
                return PartialView("_EmployeeEditPartialView", u);
            }

            [HttpPost]

            public ActionResult Edit(HttpPostedFileBase file, User employee)
            {
                User emp = context.Users.Single(c => c.EmployeeId == employee.EmployeeId);
                bool isUsername = context.Users.Any(x => x.Username == employee.Username || x.Email == employee.Email);
                if (isUsername)
                {
                    ModelState.AddModelError("", "Username or Email already exists");

                }
                if (file != null)
                {
                    string filename = Path.GetFileName(file.FileName);
                    string _filename = DateTime.Now.ToString("hhmmssfff") + filename;
                    string extention = Path.GetExtension(file.FileName).ToString();
                    string path = Path.Combine(Server.MapPath("~/Images/"), _filename);
                    employee.Image = "~/Images/" + _filename;

                    Session["imgPath"] = emp.Image;


                    if (file.ContentLength < 1000000)
                    {
                        emp.Name = employee.Name;
                        emp.Username = employee.Username;
                        emp.Email = employee.Email;
                        emp.Password = employee.Password;
                        emp.ConfirmPassword = employee.Password;
                        emp.Role = employee.Role;
                        if (Session["imgPath"] != null)
                        {
                            string oldImage = Request.MapPath(Session["imgPath"].ToString());


                            if (System.IO.File.Exists(oldImage))
                            {
                                System.IO.File.Delete(oldImage);
                            }

                        }
                        file.SaveAs(path);
                        TempData["alertMessage"] = "updated employee!";
                        emp.Image = employee.Image;
                        context.SaveChanges();
                        return RedirectToAction("Employee");
                    }
                    else
                    {
                        ViewBag.msg = "File must less than or equal to 1MB";
                    }
                }
                emp.Name = employee.Name;
                emp.Username = employee.Username;
                emp.Email = employee.Email;
                emp.Password = employee.Password;
                emp.ConfirmPassword = employee.Password;
                emp.Role = employee.Role;
                TempData["alertMessage"] = "updated employee!";
                context.SaveChanges();

                TempData["msg"] = "Employee updated";
                return RedirectToAction("Employee");
            }

            public ActionResult Details(int id)
            {
                User u = context.Users.Where(x => x.EmployeeId == id).FirstOrDefault();
                if (u == null)
                {
                    return HttpNotFound();
                }
                return PartialView("_EmployeeDeletePartialView", u);
            }

            [HttpPost]
            public ActionResult Delete(User employee)
            {
                User emp = context.Users.Single(c => c.EmployeeId == employee.EmployeeId);
                if (emp != null)
                {
                    Session["imgPath"] = emp.Image;
                    if (Session["imgPath"] != null)
                    {
                        string Image = Request.MapPath(Session["imgPath"].ToString());
                        if (System.IO.File.Exists(Image))
                        {
                            System.IO.File.Delete(Image);
                        }
                    }

                    context.Users.Remove(emp);
                    context.SaveChanges();
                    TempData["alertMessage"] = "deleted employee!";
                }
                return RedirectToAction("Employee");
            }
        }
    }
