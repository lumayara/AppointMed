using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppointMed.Models;


namespace AppointMed.Controllers
{
    public class DoctorController : Controller
    {
        // GET: Doctor
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(TDoctor doctor)
        {
            bool success = false;
            string message = "Username or password is incorrect";
            using (Entities db = new Entities())
            {
                try
                {
                    var doc = db.TDoctors.Single(u => u.Username == doctor.Username && u.Password == doctor.Password);
                    if (doc != null)
                    {
                        Session["DoctorId"] = doc.Id.ToString();
                        Session["FirstName"] = doc.firstName.ToString();
                        success = true;
                        //return Json(Url.Action("LoggedIn", "Patient"));
                        return RedirectToAction("LoggedIn");
                    }

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", message);
                }
            }
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoggedIn()
        {
            return View();
        }
    }
}