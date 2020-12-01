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
    public class APIController : ApiController
    {
        // GET: api/API

        finaldbEntities1 db = new finaldbEntities1();
        int member =13;

        public string like_count(int? contentID)
        {
            var p = (from n in db.forumLikebinding
                     where n.Like_ID == 1 && n.ForumContentID==contentID
                     select n).Count();
            return p.ToString();
        } 
       
        
        public JObject Get()
        {

            var result = new
            {

                ftable = from n in db.forumBinding
                         orderby n.forumContent.ForumContentID descending
                         where n.forumContent.ForumDelete != "delete"
                         select new
                         {
                             chkPoPerson = n.forumMemberBinding.memberdb.mb_ID == member ? true : false,
                             fID = n.forumContent.ForumContentID,
                             fName = n.forumMemberBinding.memberdb.mb_employeeName,
                             fPoNameID = n.forumMemberBinding.memberdb.mb_ID,
                             fempPic = n.forumMemberBinding.memberdb.mb_employeePicture,
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
                                                chkMesPerson = m.forumMemberBinding.memberdb.mb_ID == member ? true : false,
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
                                             chkLikePerson = p.forumMemberBinding.memberdb.mb_ID == member ? true : false,
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
                                              where p.ForumContentID == n.ForumContentID && p.Like_ID==1
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

        

        // GET: api/API/5
        public JObject Get(int id)
        {
            var result = new
            {

                ftable = from n in db.forumBinding
                         orderby n.forumContent.ForumContentID descending
                         where n.ForumContentID==id
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
                             
                            
                         }

            };

            string strJson = JsonConvert.SerializeObject(result, Formatting.Indented);
            JObject o = JObject.Parse(strJson);

            //JObject o = new JObject();
            //o["MyArray"] = array;
            return o;
        }

        // POST: api/API
        [HttpPost]
        //[EnableCors(origins: "http://mywebclient.azurewebsites.net", headers: "*", methods: "*")]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage Post([FromBody] JObject value)
        {
            try
            {
                string controllerName = ControllerContext.RouteData.Values["controller"].ToString();
                cJsonModels model = new cJsonModels();
                forumContent q = new forumContent();
                forumBinding p = new forumBinding();
                forumMemberBinding m = new forumMemberBinding();
                forumPicture pic = new forumPicture();
                //JObject jo = JObject.Parse(value);

                m.ForumTypeID = 2;
                m.mb_ID = member;
                db.forumMemberBinding.Add(m);
                db.SaveChanges();

                q.ForumTitle = value["title"].ToString();
                q.ForumContent1 = value["content"].ToString();
                q.ForumContentTime = DateTime.Now.ToString("G");           
                db.forumContent.Add(q);
                db.SaveChanges();

                for (var i = 1; i <= value.Count; i++)
                {
                    if (i <= value.Count - 2)
                    {
                        pic.ForumPicture1 = value["pic"+i].ToString();
                        pic.ForumContentID = q.ForumContentID;
                        db.forumPicture.Add(pic);
                        db.SaveChanges();
                    }
                }
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
            catch(Exception ex)
            {
                throw ex;
            }
        }

        // PUT: api/API/5
        [HttpPut]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage Put(int id,[FromBody]JObject value)
        {
            string controllerName = ControllerContext.RouteData.Values["controller"].ToString();
            var deleteforum = db.forumContent.FirstOrDefault(p => p.ForumContentID == id);
            
            var title = value["title"].ToString();
            var content = value["content"].ToString();
            var time = DateTime.Now.ToString("G");
            if (deleteforum != null)
            {
                
                
                deleteforum.ForumTitle = title;
                deleteforum.ForumContent1 = content;
                deleteforum.ForumContentTime = time;
                db.SaveChanges();
            }
            var result = new
            {
                STATUS = true,
                MSG = "成功",
            };

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // DELETE: api/API/5
        [HttpDelete]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage Delete(int id,[FromBody]JObject value)
        {
            string controllerName = ControllerContext.RouteData.Values["controller"].ToString();
            forumContent deleteforum = db.forumContent.FirstOrDefault(p => p.ForumContentID == id);

            if (deleteforum != null)
            {               
                deleteforum.ForumDelete = value["delete"].ToString();
                db.SaveChanges();
            }
            var result = new
            {
                STATUS = true,
                MSG = "成功",
            };

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
