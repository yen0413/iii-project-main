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
    public class gpController : ApiController
    {
        // GET: api/gp
        final_pEntities db = new final_pEntities();
        public JObject Get()
        {
            var result = new
            {

                gtable = from n in db.pGroupdb.AsEnumerable()
                         orderby n.Group_EndDate 
                         where DateTime.Compare(DateTime.Parse(n.Group_EndDate), DateTime.Now) > 0
                         select new
                         {
                             groupid = n.Group_ID,
                             grouptitle = n.Group_Title,
                             groupstartdate = n.Group_StartDate,
                             groupenddate = n.Group_EndDate,
                             grouptargetnumber = n.Group_TartgetNumber1,
                             grouptargetnumbertwo = n.Group_TartgetNumber2,
                             grouptype = n.Group_type,
                             groupdescription = n.Group_description,
                             ownermemberid = n.OwnerMember_ID,
                             countdown = Math.Floor(((DateTime.Parse(n.Group_EndDate) - DateTime.Now).TotalDays))


                         }
            };
            string strJson = JsonConvert.SerializeObject(result, Formatting.Indented);
            JObject o = JObject.Parse(strJson);
            return o;

        }

        // GET: api/gp/5
        public JObject Get(string grouptitle)
        {
            var result = new
            {

                gtable = from n in db.pGroupdb.AsEnumerable()
                         where n.Group_Title.Contains(grouptitle.ToString())
                         select new
                         {
                             groupid = n.Group_ID,
                             grouptitle = n.Group_Title,
                             groupstartdate = n.Group_StartDate,
                             groupenddate = n.Group_EndDate,
                             grouptargetnumber = n.Group_TartgetNumber1,
                             grouptargetnumbertwo = n.Group_TartgetNumber2,
                             grouptype = n.Group_type,
                             groupdescription = n.Group_description,
                             ownermemberid = n.OwnerMember_ID,
                             countdown = Math.Floor((DateTime.Parse(n.Group_EndDate) - DateTime.Now).TotalDays),


                         }


            };
            string strJson = JsonConvert.SerializeObject(result, Formatting.Indented);
            JObject o = JObject.Parse(strJson);
            return o;
        }

        //POST: api/gp
        public HttpResponseMessage Post([FromBody] JObject value)
        {
            string controllerName = ControllerContext.RouteData.Values["controller"].ToString();
            pGroupdb pGroupdb = new pGroupdb();
            if (pGroupdb != null)
            {
                pGroupdb.Group_Title = value["newGroup_Title"].ToString();
                pGroupdb.Group_StartDate = value["newGroup_StartDate"].ToString();
                pGroupdb.Group_EndDate = value["newGroup_EndDate"].ToString();
                pGroupdb.Group_TartgetNumber1 = Convert.ToInt32(value["newGroup_TartgetNumber1"].ToString());
                pGroupdb.Group_TartgetNumber2 = Convert.ToInt32(value["newGroup_TartgetNumber2"].ToString());
                pGroupdb.Group_type = Convert.ToInt32(value["newGroup_type"].ToString());
                pGroupdb.Group_description = value["newGroup_description"].ToString();
                pGroupdb.OwnerMember_ID = Convert.ToInt32(value["newOwnerMember_ID"].ToString());
                db.pGroupdb.Add(pGroupdb);
                db.SaveChanges();
            }
            var result = new
            {
                STATUS = true,
                MSG = "成功",
            };
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // PUT: api/gp/5
        [HttpPut]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage Put(int id, [FromBody] JObject value)
        {
            string controllerName = ControllerContext.RouteData.Values["controller"].ToString();
            var pGroupdb = db.pGroupdb.FirstOrDefault(p => p.Group_ID == id);

            if (pGroupdb != null)
            {
                pGroupdb.Group_Title = value["Group_Title"].ToString();
                pGroupdb.Group_StartDate = value["Group_StartDate"].ToString();
                pGroupdb.Group_EndDate = value["Group_EndDate"].ToString();
                pGroupdb.Group_TartgetNumber1 = Convert.ToInt32(value["Group_TartgetNumber1"].ToString());
                pGroupdb.Group_TartgetNumber2 = Convert.ToInt32(value["Group_TartgetNumber2"].ToString());
                pGroupdb.Group_type = Convert.ToInt32(value["Group_type"].ToString());
                pGroupdb.Group_description = value["Group_description"].ToString();
                pGroupdb.OwnerMember_ID = Convert.ToInt32(value["OwnerMember_ID"].ToString());
                db.SaveChanges();
            }
            var result = new
            {
                STATUS = true,
                MSG = "成功",
            };
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // DELETE: api/gp/5
        [HttpDelete]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage Delete(int id)
        {
            pGroupdb pGroupdb = db.pGroupdb.FirstOrDefault(p => p.Group_ID == id);
            if (pGroupdb != null)
            {
                db.pGroupdb.Remove(pGroupdb);
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
