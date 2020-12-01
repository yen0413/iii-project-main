using forumAPItest.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web.Http;

namespace forumAPItest.Controllers
{
    public class CountbackController : ApiController
    {
        // GET: api/Countback
        finaldbEntities1 db = new finaldbEntities1();
        final_pEntities pdb = new final_pEntities();
        //[JwtAuthActionFilte]
        public JObject Get()
        {
            var fblogmemberCount = from p in db.blogBinding
                             group p by p.memberdb.mb_employeeName into g
                             select new
                             {
                                 member = g.Key,
                                 Count = g.Count()
                             };
          
                                 
            var fmemberCount = from q in db.memberdb
                               group q by q.mb_employeeGender into g
                               select new
                               {
                                   gender = g.Key ,
                                   Count = g.Count()
                               };

            var ffourmTypeCount = from q in db.forumMemberBinding
                                  group q by q.forumType.Type into g
                                  select new
                                  {
                                      Type = g.Key,
                                      Count = g.Count()
                                  };


            var fforumTimeCount = from q in db.forumContent
                                  group q by q.ForumContentTime.Substring(0,10) into g
                                  select new
                                  {
                                      time = g.Key,
                                      Count = g.Count()
                                  };





            var fpgroupCount = (from q in pdb.pGroupdb
                                select q).Count();

            var fvotetitleCount = (from q in db.voteTitle
                                   where q.title != "delete"
                                   select q).Count();

            var empCount = (from q in db.memberdb
                            select q).Count();

            var result = new
            {
                fblogmemberCount,
                fmemberCount,
                ffourmTypeCount,
                fforumTimeCount,
                fpgroupCount,
                fvotetitleCount,
                empCount
            };

            string strJson = JsonConvert.SerializeObject(result, Formatting.Indented);
            JObject o = JObject.Parse(strJson);
            return o;
        }

        // GET: api/Countback/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Countback
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Countback/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Countback/5
        public void Delete(int id)
        {
        }
    }
}
