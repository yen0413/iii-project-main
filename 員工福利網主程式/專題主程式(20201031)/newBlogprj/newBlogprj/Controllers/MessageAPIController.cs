using newBlogprj.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace newBlogprj.Controllers
{
    public class MessageAPIController : ApiController
    {
        LoginMethod lm = new LoginMethod();
        finaldbEntities2 db = new finaldbEntities2();
        int member = 14;
        
        // GET: api/MessageAPI
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/MessageAPI/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/MessageAPI
        [HttpPost]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage Post([FromBody] JObject value)
        {
            try
            {
                string controllerName = ControllerContext.RouteData.Values["controller"].ToString();

                forummessage q = new forummessage();
                forumMessageBinding p = new forumMessageBinding();
                forumMemberBinding m = new forumMemberBinding();

              member=  lm.getmid(lm.getuserac()); //使用loginmethod改寫member的數值

                m.ForumTypeID = int.Parse(value["forumType"].ToString());
                m.mb_ID = member;
                db.forumMemberBinding.Add(m);
                db.SaveChanges();

                q.ForumMessageContent = value["content"].ToString();
                q.ForumMessageTime = DateTime.Now.ToString("G");
                db.forummessage.Add(q);
                db.SaveChanges();

                p.ForumContentID = int.Parse(value["ForumID"].ToString());
                p.ForumMessage_ID = q.ForumMessage_ID;
                p.mb_ID = m.ForumMemberBinding_ID;
                db.forumMessageBinding.Add(p);
                db.SaveChanges();

                var result = new
                {
                    STATUS = true,
                    MSG = "成功",
                };

                return Request.CreateResponse(HttpStatusCode.OK, result);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        // PUT: api/MessageAPI/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/MessageAPI/5
        [HttpDelete]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage Delete(int id)
        {

            forummessage deleteforumMessage = db.forummessage.FirstOrDefault(p => p.ForumMessage_ID == id);
            forumMessageBinding deleteforumMesBind = db.forumMessageBinding.FirstOrDefault(p => p.ForumMessage_ID == id);

            if (deleteforumMessage != null)
            {
                db.forummessage.Remove(deleteforumMessage);

                db.forumMessageBinding.Remove(deleteforumMesBind);
                db.SaveChanges();

            }
            var result = new
            {
                STATUS = true,
                MSG = "刪除成功",
            };

            return Request.CreateResponse(HttpStatusCode.OK, result);

        }

        // DELETE: api/MessageAPI/5
       
    }
}
