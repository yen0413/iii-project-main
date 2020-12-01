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
    public class mbPackageController : ApiController
    {
        // GET: api/mbPackage
        finaldbEntities1 db = new finaldbEntities1();
       
        [JwtAuthPackageFilte]
        public JObject Get()
        {
            var ftable = from n in db.memberdb
                         where n.mb_package >0
                         orderby n.mb_ID
                         select new
                         {
                             id = n.mb_ID,
                             name = n.mb_employeeName,
                             package = n.mb_package
                         };
            var result = new
            {
                ftable
            };
            string strJson = JsonConvert.SerializeObject(result, Formatting.Indented);
            JObject o = JObject.Parse(strJson);
            return o;

        }

        // GET: api/mbPackage/5
        public JObject Get(string name)
        {
            var result = new
            {
                ftable = from n in db.memberdb
                         where n.mb_employeeName == name
                         select new
                         {
                             id = n.mb_ID,
                             name = n.mb_employeeName,
                             package = n.mb_package
                         }
            };
            string strJson = JsonConvert.SerializeObject(result, Formatting.Indented);
            JObject o = JObject.Parse(strJson);
            return o;
        }


        // POST: api/mbPackage
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/mbPackage/5
        [HttpPut]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage Put(int putID, [FromBody] JObject value)
        {
            string controllerName = ControllerContext.RouteData.Values["controller"].ToString();
            var PutMemberPackage = db.memberdb.FirstOrDefault(p => p.mb_ID == putID);
            var package = value["packageNumber"].ToString();
              PutMemberPackage.mb_package = int.Parse(package);
           // PutMemberPackage.mb_package = 13;
            db.SaveChanges();
            //db.SaveChanges();
            var result = new
            {
                STATUS = true,
                MSG = "成功",
            };

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // DELETE: api/mbPackage/5
        public void Delete(int id)
        {
        }
    }
}
