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
    public class forumbackController : ApiController
    {
        finaldbEntities1 db = new finaldbEntities1();
        // GET: api/forumback
        [JwtAuthActionFilte]
        public JObject Get()
        {

            var result = new
            {

                ftable = from n in db.forumBinding
                         orderby n.forumContent.ForumContentID descending
                         where n.forumContent.ForumDelete != "delete"
                         select new
                         {
                             fID = n.forumContent.ForumContentID,
                             fName = n.forumMemberBinding.memberdb.mb_employeeName,
                             fPoNameID = n.forumMemberBinding.memberdb.mb_ID,
                             fType = n.forumMemberBinding.forumType.Type,
                             fDept = n.forumMemberBinding.memberdb.mb_employeeDeptID,
                             fTitle = n.forumContent.ForumTitle,
                             fContent = n.forumContent.ForumContent1,
                             fdate = n.forumContent.ForumContentTime,

                             picture = new
                             {
                                 pic = from t in db.forumPicture
                                       where t.ForumContentID == n.forumContent.ForumContentID
                                       select new
                                       {
                                           picID = t.ForumPictureID,
                                           pic = t.ForumPicture1
                                       }

                             },
                             message = new
                             {
                                 fmessage = from m in db.forumMessageBinding
                                            where m.ForumContentID == n.ForumContentID
                                            select new
                                            {
                                                fMID = m.FMB_ID,
                                                fMesID = m.forumMemberBinding.memberdb.mb_ID,
                                                fContent = m.ForumContentID,
                                                fMesTime = m.forummessage.ForumMessageTime,
                                                fMName = m.forumMemberBinding.memberdb.mb_employeeName,
                                                fMContent = m.forummessage.ForumMessageContent

                                            }
                             },
                             Like = new
                             {
                                 flike = from p in db.forumLikebinding
                                         where p.ForumContentID == n.ForumContentID
                                         select new
                                         {
                                             fName = p.forumMemberBinding.memberdb.mb_employeeName,
                                             fNameID = p.forumMemberBinding.memberdb.mb_ID,
                                             fLikeID = p.ForumLike_ID,
                                             fContent = p.ForumContentID,
                                             fLike = p.Like_ID,

                                         }
                             },

                             Likecount = new
                             {
                                 flikecount = (from p in db.forumLikebinding
                                               where p.ForumContentID == n.ForumContentID && p.Like_ID == 1
                                               select p).LongCount()
                             }
                         }

            };

            string strJson = JsonConvert.SerializeObject(result, Formatting.Indented);
            JObject o = JObject.Parse(strJson);

            //JObject o = new JObject();
            //o["MyArray"] = array;
            return o;
        }



        // GET: api/forumback/5
        public JObject Get(int srchPoster_ID)
        {

            var result = new
            {
                ftable = from n in db.forumBinding
                         orderby n.forumContent.ForumContentID descending
                         where n.forumMemberBinding.memberdb.mb_ID == srchPoster_ID
                         select new
                         {
                             fID = n.forumContent.ForumContentID,
                             fName = n.forumMemberBinding.memberdb.mb_employeeName,
                             fPoNameID = n.forumMemberBinding.memberdb.mb_ID,
                             fType = n.forumMemberBinding.forumType.Type,
                             fDept = n.forumMemberBinding.memberdb.mb_employeeDeptID,
                             fTitle = n.forumContent.ForumTitle,
                             fContent = n.forumContent.ForumContent1,
                             fdate = n.forumContent.ForumContentTime,

                             picture = new
                             {
                                 pic = from t in db.forumPicture
                                       where t.ForumContentID == n.forumContent.ForumContentID
                                       select new
                                       {
                                           picID = t.ForumPictureID,
                                           pic = t.ForumPicture1
                                       }

                             },
                             message = new
                             {
                                 fmessage = from m in db.forumMessageBinding
                                            where m.ForumContentID == n.ForumContentID
                                            select new
                                            {
                                                fMID = m.FMB_ID,
                                                fMesID = m.forumMemberBinding.memberdb.mb_ID,
                                                fContent = m.ForumContentID,
                                                fMesTime = m.forummessage.ForumMessageTime,
                                                fMName = m.forumMemberBinding.memberdb.mb_employeeName,
                                                fMContent = m.forummessage.ForumMessageContent

                                            }
                             },
                             Like = new
                             {
                                 flike = from p in db.forumLikebinding
                                         where p.ForumContentID == n.ForumContentID
                                         select new
                                         {
                                             fName = p.forumMemberBinding.memberdb.mb_employeeName,
                                             fNameID = p.forumMemberBinding.memberdb.mb_ID,
                                             fLikeID = p.ForumLike_ID,
                                             fContent = p.ForumContentID,
                                             fLike = p.Like_ID,

                                         }
                             },
                             Likecount = new
                             {
                                 flikecount = (from p in db.forumLikebinding
                                               where p.ForumContentID == n.ForumContentID && p.Like_ID == 1
                                               select p).LongCount()
                             }
                         }
            };
            string strJson = JsonConvert.SerializeObject(result, Formatting.Indented);
            JObject o = JObject.Parse(strJson);

            //JObject o = new JObject();
            //o["MyArray"] = array;
            return o;
        }

        // POST: api/forumback
        [HttpPost]
        //[EnableCors(origins: "http://mywebclient.azurewebsites.net", headers: "*", methods: "*")]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage Post([FromBody] JObject value)
        {
            try
            {
                string controllerName = ControllerContext.RouteData.Values["controller"].ToString();                forumContent q = new forumContent();
                forumBinding p = new forumBinding();
                forumMemberBinding m = new forumMemberBinding();
                
                //JObject jo = JObject.Parse(value);

                m.ForumTypeID = int.Parse(value["type"].ToString());
                m.mb_ID = int.Parse(value["id"].ToString());
                db.forumMemberBinding.Add(m);
                db.SaveChanges();

                q.ForumTitle = value["title"].ToString();
                q.ForumContent1 = value["content"].ToString();
                q.ForumContentTime = DateTime.Now.ToString("G");
                db.forumContent.Add(q);
                db.SaveChanges();

               
                p.fmb_ID = m.ForumMemberBinding_ID;
                p.ForumContentID = q.ForumContentID;
                db.forumBinding.Add(p);
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

        // PUT: api/forumback/5
        [HttpPut]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage Put(int putID, [FromBody] JObject value)
        {
            string controllerName = ControllerContext.RouteData.Values["controller"].ToString();
            var PutForum = db.forumContent.FirstOrDefault(p => p.ForumContentID == putID);
            //var type = value["type"].ToString();
            var title = value["title"].ToString();
            var content = value["content"].ToString();
            var time = DateTime.Now.ToString("G");


            PutForum.ForumTitle = title;
            PutForum.ForumContent1 = content;
            PutForum.ForumContentTime = time;


            db.SaveChanges();
            var result = new
            {
                STATUS = true,
                MSG = "成功",
            };

            return Request.CreateResponse(HttpStatusCode.OK, result);


        }

        // DELETE: api/forumback/5
        public HttpResponseMessage Delete(int id)
        {
            forumContent deleteForum = db.forumContent.FirstOrDefault(p => p.ForumContentID == id);

            if (deleteForum != null)
            {
                deleteForum.ForumDelete = "delete";
            }
            db.SaveChanges();
            var result = new
            {
                STATUS = true,
                MSG = "刪除成功",
            };

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
