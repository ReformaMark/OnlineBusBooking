
using OnlineBusBooking.Models;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace BusBookingSystem.Controllers
{
    public class UserController : Controller
    {
        OnlineBusBookingEntities context = new OnlineBusBookingEntities();
        // GET: Register View
        public ActionResult Register()
        {
            return View();
        }
        // POST: Register
        [HttpPost]
        public ActionResult Register(User employee)
        {
            if (ModelState.IsValid)
            {
                bool isUsername = context.Users.Any(x => x.Username == employee.Username || x.Email == employee.Email);
                if (isUsername)
                {
                    ModelState.AddModelError("", "Username or Email already exists");
                    return View(employee);
                }
                context.Users.Add(employee);
                context.SaveChanges();
                ModelState.Clear();
                TempData["register"] = "Registered";
                return RedirectToAction("Login", "User");
            }
            return View(employee);
        }

        //Get: Login View
        public ActionResult Login()
        {

            return View();
        }

        //POST: Login
        [HttpPost]
        public ActionResult Login(User employee)
        {

            bool employeeExist = context.Users.Any(x => x.Username == employee.Username && x.Password == employee.Password);

            User emp = context.Users.FirstOrDefault(c => c.Username == employee.Username && c.Password == employee.Password);
            if (employeeExist)
            {
                if (employeeExist && emp.Role != null)
                {
                    Session["employeeRole"] = emp.Role;
                    Session["userUsername"] = emp.Username;
                    FormsAuthentication.SetAuthCookie(employee.Username, false);
                    return RedirectToAction("Employee", "Admin");
                }
                else
                {
                    Session["employeeRole"] = "User";
                    Session["userUsername"] = emp.Username;
                    FormsAuthentication.SetAuthCookie(employee.Username, false);
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("", "Please enter a valid credentials.");
            return View(employee);
        }
        //Logout
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.RemoveAll();
            return RedirectToAction("Login", "User");
        }

    }
}