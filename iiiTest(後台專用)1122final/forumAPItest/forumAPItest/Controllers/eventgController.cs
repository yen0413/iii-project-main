using forumAPItest.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace forumAPItest.Controllers
{
    public class eventgController : ApiController
    {
        finaldbEntities1 db = new finaldbEntities1();
        [JwtAuthActionFilte]
        public JObject Get()
        {
            var result = new
            {
                etable = from n in db.Event
                         select new
                         {
                             eventid = n.Event_ID,
                             eventname = n.EventName,
                             eventstartdate = n.EventStartDate,
                             eventenddate = n.EventEndDate,
                             eventlocation = n.EventLocation,
                             eventcontent = n.EventContent,
                             eventmaxpeople = n.EventMaxPeople,
                             eventminpeople = n.EventMinPeople,
                             eventcreatpeople = n.EventCreateEmployeeID,
                             booking = new
                             {
                                 ebtable = from m in db.EventBooking
                                           where m.Event_ID == n.Event_ID
                                           select new
                                           {
                                               eventid = m.Event_ID,
                                               bookingdate = m.BookingDate,
                                               bookingpeopleid = m.mb_ID,
                                               employeestatus = m.EmployeeJoinStatus,
                                           }
                             }
                         }
            };



            string strJson = JsonConvert.SerializeObject(result, Formatting.Indented);
            JObject o = JObject.Parse(strJson);
            return o;

        }

        // GET: api/gp/5
        public JObject Get(int id)
        {
            var result = new
            {
                etable = from n in db.Event
                         where n.Event_ID == id
                         select new
                         {
                             eventid = n.Event_ID,
                             eventname = n.EventName,
                             eventstartdate = n.EventStartDate,
                             eventenddate = n.EventEndDate,
                             eventlocation = n.EventLocation,
                             eventcontent = n.EventContent,
                             eventmaxpeople = n.EventMaxPeople,
                             eventminpeople = n.EventMinPeople,
                             eventcreatpeople = n.EventCreateEmployeeID,
                             booking = new
                             {
                                 ebtable = from m in db.EventBooking
                                           where m.Event_ID == n.Event_ID
                                           select new
                                           {
                                               bookingdate = m.BookingDate,
                                               bookingpeopleid = m.mb_ID,
                                               employeestatus = m.EmployeeJoinStatus,
                                           }
                             }
                         }
            };

            string strJson = JsonConvert.SerializeObject(result, Formatting.Indented);
            JObject o = JObject.Parse(strJson);
            return o;
        }

        // POST: api/event
        public HttpResponseMessage Post([FromBody] JObject value)
        {

            string controllerName = ControllerContext.RouteData.Values["controller"].ToString();
            //string eventg= ControllerContext.RouteData.Values["eventg"].ToString();
            Event editevent = new Event();
            //Event editevent = new Event();
            //var title = value["title"].ToString();
            //var newEventname = value["newEventname"].ToString();
            //var newEventstartdate = value["newEventstartdate"].ToString();
            //var newEventenddate = value["newEventenddate"].ToString();
            //var newEventlocation = value["newEventlocation"].ToString();
            //var newEventcontent = value["newEventcontent"].ToString();
            //var newEventmaxpeople = value["newEventmaxpeople"].ToString();
            //var newEventminpeople = value["newEventminpeople"].ToString();
            //var newEventcreatpeople = value["newEventcreatpeople"].ToString();

            //var time = DateTime.Now.ToString("G");
            if (editevent != null)
            {

                editevent.EventName = value["newEventname"].ToString();
                editevent.EventStartDate = DateTime.Parse(value["newEventstartdate"].ToString());
                editevent.EventEndDate = DateTime.Parse(value["newEventenddate"].ToString());
                editevent.EventLocation = value["newEventlocation"].ToString();
                editevent.EventContent = value["newEventcontent"].ToString();
                editevent.EventMaxPeople = Convert.ToInt32(value["newEventmaxpeople"].ToString());
                editevent.EventMinPeople = Convert.ToInt32(value["newEventminpeople"].ToString());
                editevent.EventCreateEmployeeID = Convert.ToInt32(value["newEventcreatpeople"].ToString());
                db.Event.Add(editevent);
                db.SaveChanges();
            }
            var result = new
            {
                STATUS = true,
                MSG = "成功",
            };
            return Request.CreateResponse(HttpStatusCode.OK, result);


        }

        // PUT: api/event/5
        [HttpPut]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage Put(int id, [FromBody] JObject value)
        {
            //string eventg= ControllerContext.RouteData.Values["eventg"].ToString();
            var editevent = db.Event.FirstOrDefault(p => p.Event_ID == id);
            //Event editevent = new Event();
            //var title = value["title"].ToString();
            var Eventname = value["Eventname"].ToString();
            var Eventstartdate = value["Eventstartdate"].ToString();
            var Eventenddate = value["Eventenddate"].ToString();
            var Eventlocation = value["Eventlocation"].ToString();
            var Eventcontent = value["Eventcontent"].ToString();
            var Eventmaxpeople = value["Eventmaxpeople"].ToString();
            var Eventminpeople = value["Eventminpeople"].ToString();
            var Eventcreatpeople = value["Eventcreatpeople"].ToString();

            //var time = DateTime.Now.ToString("G");
            if (editevent != null)
            {

                editevent.EventName = Eventname;
                editevent.EventStartDate = DateTime.Parse(Eventstartdate);
                editevent.EventEndDate = DateTime.Parse(Eventenddate);
                editevent.EventLocation = Eventlocation;
                editevent.EventContent = Eventcontent;
                editevent.EventMaxPeople = Convert.ToInt32(Eventmaxpeople);
                editevent.EventMinPeople = Convert.ToInt32(Eventminpeople);
                editevent.EventCreateEmployeeID = Convert.ToInt32(Eventcreatpeople);
                db.SaveChanges();
            }
            var result = new
            {
                STATUS = true,
                MSG = "成功",
            };

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // DELETE: api/event/5
        [HttpDelete]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage Delete(int id)
        {
            Event deletevent = db.Event.FirstOrDefault(p => p.Event_ID == id);
            EventBooking eventBooking = db.EventBooking.FirstOrDefault(p => p.Event_ID == id);


            if (deletevent != null)
            {
                db.EventBooking.Remove(eventBooking);
                db.SaveChanges();
                db.Event.Remove(deletevent);
                db.SaveChanges();

            }
            var result = new
            {
                STATUS = true,
                MSG = "刪除成功",
            };

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

    }
}
