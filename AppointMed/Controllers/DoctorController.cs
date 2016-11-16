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
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(TDoctor doctor)
        {
            bool success = false;
            string message = "Username or password is incorrect";
            using (DataModel db = new DataModel())
            {
                try
                {
                    var doc = db.TDoctor.Single(u => u.Username == doctor.Username && u.Password == doctor.Password);
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

        public ActionResult LoggedIn()
        {
            return View();
        }
    }
}