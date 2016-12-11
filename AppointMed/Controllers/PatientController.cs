using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppointMed.Models;

namespace AppointMed.Controllers
{
    public class PatientController : Controller
    {
        // GET: Patient
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Patient p)
        {
            if (ModelState.IsValid)
            {
                using(Entities db = new Entities())
                {
                    bool contactExists = db.Patients.Any(patient => patient.Username.Equals(p.Username));
                    if (contactExists)
                    {
                        return Json(new { result = "This username already exists" });
                    }
                    else
                    {
                        db.Patients.Add(p);
                        db.SaveChanges();

                    }
                }
                ModelState.Clear();
                return Json(new { result = "Redirect", url = Url.Action("Index", "Home") });
            }
            return View();
        }

        public ActionResult Login()
        {
            return PartialView("Login");
        }

        [HttpPost]
        public ActionResult Login(Patient patient)
        {
            bool success = false;
            string message = "Username or password is incorrect";
            using (Entities db = new Entities())
            {
                try
                {
                    var pat = db.Patients.Single(p => p.Username == patient.Username && p.Password == patient.Password);
                    if (pat!= null)
                    {
                        Session["PatientId"] = pat.Id.ToString();
                        Session["Username"] = pat.Username.ToString();
                        Session["FirstName"] = pat.FirstName.ToString();
                        success = true;
                        //return Json(Url.Action("LoggedIn", "Patient"));
                        return Json(new { result = "Redirect", url = Url.Action("LoggedIn", "Patient") });
                    }
                   
                }
                catch (Exception ex)
                {
                    return Json(new { success = success, message = message });
                }
             }
            return Json(new { success = success, message = message });
        }

        //[HttpPost]
        //public ActionResult Login(Patient patient)
        //{
        //    using (DataModel db = new DataModel())
        //    {
        //        try
        //        {
        //            var pat = db.Patients.Single(p => p.Username == patient.Username && p.Password == patient.Password);
        //            if (pat != null)
        //            {
        //                Session["PatientId"] = pat.Id.ToString();
        //                Session["Username"] = pat.Username.ToString();
        //                return RedirectToAction("LoggedIn");
        //            }
        //        }
        //        catch (Exception e)
        //        { 

        //                ViewBag.Error = "* Don't have such user! " + e.Message;


        //        }

        //        }
        //    return RedirectToAction("Index");
        //}



        public ActionResult LoggedIn()
        {
            if (Session["PatientId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}