using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppointMed.Models;
using System.Data.Entity;

namespace AppointMed.Controllers
{
    public class AppointmentController : Controller
    {
        // GET: Appointment
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MakeAppointment()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MakeAppointment(Appointment appointment, string availability)
        {
            //System.Diagnostics.Debug.WriteLine("availability ooiooioi: " + availability);
            try
            {
                appointment.patientID = Convert.ToInt32(Session["PatientId"]);
                //System.Diagnostics.Debug.WriteLine("id id id ooiooioi: " + appointment.patientID);

                appointment.scheduleID = Convert.ToInt32(availability);
                if (ModelState.IsValid)
                {
                    using (Entities db = new Entities())
                    {
                        db.Appointments.Add(appointment);
                        db.SaveChanges();
                        TempData["AddedAppointment"] = "Your appointment was made Successfully!";
                    }
                    ModelState.Clear();

                    return RedirectToAction("LoggedIn", "Patient");
                }
            }
            catch(Exception e)
            {
                TempData["AddedAppointment"] = "Your appointment was NOT registered! Error: "+e.Message;
                return RedirectToAction("LoggedIn", "Patient");
            }
            return View();
        }

        [HttpPost]
        public JsonResult Exists(int schedule)
        {

            using (Entities db = new Entities())
            {
                //System.Diagnostics.Debug.WriteLine("availability ooiooioi: " + schedule);
                //int patientID = Convert.ToInt32(Session["PatientId"]);
                bool appointmentExists = db.Appointments.Any(appointment => appointment.scheduleID == schedule);
                if (appointmentExists)
                {
                    return Json(new { exists = "true" });
                }
                else
                {

                    return Json(new { exists = "false" });
                }
            }

        }

        //public ActionResult List()
        //{
        //    int patientID = Convert.ToInt32(Session["PatientId"]);

        //    List<Agendamento> appointments = new List<Models.Agendamento>();

        //    using (Entities db = new Entities())
        //    {
        //        appointments = db.Appointments.Where(a => a.Patient.Id == patientID)
        //                                      .Select(a => new Agendamento
        //                                      {
        //                                          a.Id,
        //                                          a.prescription,
        //                                          a.patientID,
        //                                          a.Schedule.startDate
        //                                      })
        //                                      .ToList();
        //    }

        //    return View(appointments);
        //}


        public ActionResult List()
        {
            int patientID = Convert.ToInt32(Session["PatientId"]);

            var appointments = new List<Appointment>();

            using (Entities db = new Entities())
            {
                appointments = db.Appointments.Where(a => a.Patient.Id == patientID)
                                              .Include(a=> a.Schedule)
                                              .Include(a=> a.Schedule.TDoctor)
                                              .ToList();
            }

            return View(appointments);
        }

        public ActionResult Delete(string appointment)
        {
            try
            {
              
                //System.Diagnostics.Debug.WriteLine("id id id ooiooioi: " + appointment.patientID);

                int id = Convert.ToInt32(appointment);
                if (ModelState.IsValid)
                {
                    using (Entities db = new Entities())
                    {
                        Appointment app = db.Appointments.Find(id);
                        db.Appointments.Remove(app);
                        db.SaveChanges();
                        TempData["AddedAppointment"] = "Your appointment was canceled!";
                    }
                    ModelState.Clear();

                    return RedirectToAction("LoggedIn", "Patient");
                }
            }
            catch (Exception e)
            {
                TempData["AddedAppointment"] = "Your appointment was NOT canceled! Error: " + e.Message;
                return RedirectToAction("LoggedIn", "Patient");
            }
            return View();
        }

        //[HttpPost]
        //public ActionResult List()
        //{
        //    var appointments = new List<Models.Appointment>();
        //    int patientID = Convert.ToInt32(Session["PatientId"]);
        //    using (Entities db = new Entities())
        //    {
        //        appointments = db.Appointments.Where(a => a.Patient.Id == patientID )
        //                                 .Select(s => new Availability
        //                                 {
        //                                     startDate = s.startDate,
        //                                     Id = s.Id
        //                                 }).ToList();

        //    }

        //    return RedirectToAction("LoggedIn", "Patient");
        //    }


    }
}