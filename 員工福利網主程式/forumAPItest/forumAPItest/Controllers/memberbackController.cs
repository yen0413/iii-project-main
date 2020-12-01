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
    public class memberbackController : ApiController
    {
        finaldbEntities1 db = new finaldbEntities1();
        // GET: api/memberback
        [JwtAuthActionFilte]
        public JObject Get()
        {
            var ftable = from n in db.memberdb
                         orderby n.mb_ID
                         select new
                         {
                             id = n.mb_ID,
                             name = n.mb_employeeName,
                             empAcc = n.mb_employeeAccount,
                             Pw = n.mb_employeePassword,
                             dept = n.mb_employeeDeptID,
                             phone = n.mb_employeePhone,
                             Email = n.mb_employeeEmail,
                             address = n.mb_employeeAddress,
                             hireDate = n.mb_employeeHireDate,
                             transport = n.mb_employeeTransport,
                             state = n.mb_employeeState,
                         };
            var result = new
            {
                ftable
            };
            string strJson = JsonConvert.SerializeObject(result, Formatting.Indented);
            JObject o = JObject.Parse(strJson);
            return o;

        }

        // GET: api/memberback/5
        public JObject Get(int searchID)
        {
            var result = new
            {
                ftable = from n in db.memberdb
                         where n.mb_ID == searchID
                         select new
                         {
                             id = n.mb_ID,
                             name = n.mb_employeeName,
                             empAcc = n.mb_employeeAccount,
                             Pw = n.mb_employeePassword,
                             dept = n.mb_employeeDeptID,
                             phone = n.mb_employeePhone,
                             Email = n.mb_employeeEmail,
                             address = n.mb_employeeAddress,
                             hireDate = n.mb_employeeHireDate,
                             transport = n.mb_employeeTransport,
                             state = n.mb_employeeState,
                         }
            };
            string strJson = JsonConvert.SerializeObject(result, Formatting.Indented);
            JObject o = JObject.Parse(strJson);
            return o;
        }

        // POST: api/memberback
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/memberback/5
        [HttpPut]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage Put(int putID, [FromBody] JObject value)
        {
            string controllerName = ControllerContext.RouteData.Values["controller"].ToString();
            var PutMember = db.memberdb.FirstOrDefault(p => p.mb_ID == putID);
            var name = value["name"].ToString();
            var empAcc = value["empAcc"].ToString();
            var Pw = value["Pw"].ToString();
            var dept = value["dept"].ToString();
            var phone = value["phone"].ToString();
            var Email = value["Email"].ToString();
            var address = value["address"].ToString();
            var hireDate = DateTime.Parse(value["hireDate"].ToString());
            var transport = value["transport"].ToString();
            var state = value["state"].ToString();

            PutMember.mb_employeeName = name;
            PutMember.mb_employeeAccount = empAcc;
            PutMember.mb_employeePassword = Pw;
            PutMember.mb_employeeDeptID = int.Parse(dept);
            PutMember.mb_employeePhone = phone;
            PutMember.mb_employeeEmail = Email;
            PutMember.mb_employeeAddress = address;
            PutMember.mb_employeeHireDate = hireDate;
            PutMember.mb_employeeTransport = transport;
            PutMember.mb_employeeState = int.Parse(state);

            db.SaveChanges();
            var result = new
            {
                STATUS = true,
                MSG = "成功",
            };

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // DELETE: api/memberback/5
        //public HttpResponseMessage Delete(int id)
        //{

        //}
    }
}
