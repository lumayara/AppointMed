using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppointMed.Models;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace AppointMed.Controllers
{
    public class SchedulleController : Controller
    {
        // GET: Schedulle
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Schedule()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Schedule(Schedule schedule, string datePick)
        {
            //var name = datePick;
            string[] times = { "9:00:00", "10:00:00", "11:00:00", "12:00:00", "13:00:00", "14:00:00", "15:00:00", "16:00:00", "17:00:00" };

            var dates = datePick.Split(',');
            DateTime selectedDate;
            string dateComplete;
            foreach (string date in dates)
            {
                for (int i =0; i< times.Length; i++)
                {
                    dateComplete = date + " " + times[i];
                    selectedDate = Convert.ToDateTime(dateComplete);

                    schedule.startDate = selectedDate;

                    schedule.TDoctor_Id = Convert.ToInt32(Session["DoctorId"]);
                    if (ModelState.IsValid)
                    {
                        using (Entities db = new Entities())
                        {
                            try
                            {
                                db.Schedules.Add(schedule);
                                db.SaveChanges();
                            }catch (Exception e)
                            {
                                System.Diagnostics.Debug.WriteLine("Errorrrrrrrrrr: "+ e);
                            }
                        }
                        ModelState.Clear();
                    }

                   // System.Diagnostics.Debug.WriteLine("DATES: " + selectedDate);
                }
                
               
            }
            
            return View();
        }

        public ActionResult List()
        {
            var doctorId = Session["DoctorId"];
            List<Schedule> schedules = new List<Models.Schedule>();
            int id = Convert.ToInt32(Session["DoctorId"]);
            
            using (Entities db = new Entities())
            {
                //TDoctor doctor = db.TDoctors.Find(id);
                schedules = db.Schedules.Where(s => s.TDoctor.Id == id).ToList();

            }

            return View(schedules);
        }

        [HttpPost]
        public JsonResult List(int id)
        {
            //list doctors with selected id
            int doctorId = id;
            //List<Schedule> schedules = new List<Models.Schedule>();
            //int id = Convert.ToInt32(Session["DoctorId"]);
            var schedules = new List<Models.Availability>();
            //string json;
            int patientID = Convert.ToInt32(Session["PatientId"]);
            using (Entities db = new Entities())
            {
                //TDoctor doctor = db.TDoctors.Find(id);
                schedules = db.Schedules.Where(s => s.TDoctor.Id == doctorId && !db.Appointments.Any(a => a.scheduleID == s.Id))
                                         .Select(s => new Availability
                                         {
                                             startDate = s.startDate,
                                             Id = s.Id
                                         }).ToList();
                                       

                    //json =  JsonConvert.SerializeObject(schedules, Formatting.Indented,
                    //new JsonSerializerSettings()
                    //{
                    //    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                    //}
                //);
                //JsonConvert.SerializeObject(schedules);
              //  System.Diagnostics.Debug.WriteLine("schedules ooiooioi: "+ schedules);
               // schedules.ForEach(Console.WriteLine);

            }

            return Json(new { Schedules = schedules });
        }




        public ActionResult Delete(int id)
        {
            using (Entities db = new Entities())
            {
                Schedule schedule = db.Schedules.Find(id);
                db.Schedules.Remove(schedule);
                db.SaveChanges();

            }
            return RedirectToAction("List", "Schedulle");
        }

    }
}