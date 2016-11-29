using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using DHTMLX.Scheduler;
using DHTMLX.Common;
using DHTMLX.Scheduler.Data;
using DHTMLX.Scheduler.Controls;

using AppointMed.Models;
namespace AppointMed.Controllers
{
    public class CalendarController : Controller
    {
        public ActionResult Index()
        {
            try
            {
                //Being initialized in that way, scheduler will use CalendarController.Data as a the datasource and CalendarController.Save to process changes
                var scheduler = new DHXScheduler(this);
                scheduler.Skin = DHXScheduler.Skins.Terrace;
                scheduler.InitialDate = new DateTime(2012, 09, 03);

                scheduler.Config.multi_day = true;

                scheduler.LoadData = true;
                scheduler.EnableDataprocessor = true;

                return View(scheduler);
            }
            catch(Exception ex)
            {
                return View(ex.Message, new HandleErrorInfo(ex, "Calendar", "Index"));
            }
        }

        public ContentResult Data()
        {
            var data = new SchedulerAjaxData(new ModelData().Schedule);                    
            return (ContentResult)data;
        }

        public ContentResult Save(int? id, FormCollection actionValues)
        {
            var action = new DataAction(actionValues);
            var changedEvent = (Schedule)DHXEventsHelper.Bind(typeof(Schedule), actionValues);
            var data = new ModelData();
            Schedule Nschedule;
            TDoctor doctor = new TDoctor();
           
           
            try
            {
                switch (action.Type)
                {
                    case DataActionTypes.Insert:
                        //do insert
                        //action.TargetId = changedEvent.id;//assign postoperational id
                        Nschedule = new Schedule();
                        Nschedule.startDate = DateTime.Parse(actionValues["startDate"]);
                        Nschedule.endDate = DateTime.Parse(actionValues["endDate"]);
                        Nschedule.text = actionValues["text"];
                        Nschedule.TDoctor = data.TDoctor.SingleOrDefault(x => x.Id == 1);

                        data.Schedule.Add(changedEvent);
                        break;
                    case DataActionTypes.Delete:
                        //do delete
                        changedEvent = data.Schedule.SingleOrDefault(sched => sched.Id == action.SourceId);
                        data.Schedule.Remove(changedEvent);
                        break;
                    default:
                        //do update
                        var eventToUpdate = data.Schedule.SingleOrDefault(sched => sched.Id == action.SourceId);
                        DHXEventsHelper.Update(eventToUpdate, changedEvent, new List<string>() { "id" });
                        break;
                }
                data.SaveChanges();
                action.TargetId = changedEvent.Id;
            }
            catch
            {
                action.Type = DataActionTypes.Error;
            }
            return (ContentResult)new AjaxSaveResponse(action);
        }
    }
}

