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

    public class MessageAPIController : ApiController
    {
        finaldbEntities1 db = new finaldbEntities1();
        int member = 13;

        public JObject Get(int id)
        {
            var result = new
            {

                ftable = from n in db.forumBinding
                         orderby n.forumContent.ForumContentID descending
                         where n.ForumContentID == id
                         select new
                         {
                             chkPoPerson = n.forumMemberBinding.memberdb.mb_ID == member ? true : false,
                             fID = n.forumContent.ForumContentID,
                             fName = n.forumMemberBinding.memberdb.mb_employeeName,
                             fPoNameID = n.forumMemberBinding.memberdb.mb_ID,
                             fType = n.forumMemberBinding.forumType.Type,
                             fDept = n.forumMemberBinding.memberdb.mb_employeeDeptID,
                             fTitle = n.forumContent.ForumTitle,
                             fContent = n.forumContent.ForumContent1,
                             fdate = n.forumContent.ForumContentTime,

                             message = new
                             {
                                 fmessage = from m in db.forumMessageBinding
                                            where m.ForumContentID == n.ForumContentID
                                            select new
                                            {
                                                chkMesPerson = m.forumMemberBinding.memberdb.mb_ID == member ? true : false,
                                                fMID = m.ForumMessage_ID,
                                                fMesID = m.forumMemberBinding.memberdb.mb_ID,
                                                fContent = m.ForumContentID,
                                                fMesTime = m.forummessage.ForumMessageTime,
                                                fMName = m.forumMemberBinding.memberdb.mb_employeeName,
                                                fMContent = m.forummessage.ForumMessageContent

                                            }
                             },

                         }

            };

            string strJson = JsonConvert.SerializeObject(result, Formatting.Indented);
            JObject o = JObject.Parse(strJson);

            //JObject o = new JObject();
            //o["MyArray"] = array;
            return o;
        }

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
    }
}

