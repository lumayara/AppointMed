using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppointMed.Models;

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

                    schedule.TDoctor_Id = 1;
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
            var query = new Object();
            var id = Session["DoctorId"];
            using (Entities db = new Entities())
            {

                    query = from s in db.Schedules
                            where s.TDoctor.Equals(id)
                            select s;
                   
            }

            return View();
        }



    }
}