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
    public class MesLikeController : ApiController
    {
        // GET: api/MesLike

        finaldbEntities2 db = new finaldbEntities2();
        int memberdb = 14;
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/MesLike/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/MesLike
        [HttpPost]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage Post([FromBody] JObject value)
        {

            try
            {

                int contentID = int.Parse(value["contentID"].ToString());
                forumLikebinding L = new forumLikebinding();
                forumMemberBinding m = new forumMemberBinding();

                LoginMethod lm = new LoginMethod();
                memberdb = lm.getmid(lm.getuserac());//使用loginmethod裡面的拿到登入使用者的ID


                var q = (from p in db.forumLikebinding
                         where p.forumMemberBinding.mb_ID == memberdb && p.ForumContentID == contentID
                         select p).Count();

                if (q < 1)
                {
                    m.mb_ID = memberdb;
                    m.ForumTypeID = 5; //like
                    db.forumMemberBinding.Add(m);
                    db.SaveChanges();


                    L.fmb_ID = m.ForumMemberBinding_ID;
                    L.ForumContentID = contentID;
                    L.Like_ID = 1;
                    db.forumLikebinding.Add(L);
                    db.SaveChanges();

                    var result = new
                    {
                        STATUS = true,
                        MSG = "成功",
                    };

                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else
                {
                    var likeIDed = (from p in db.forumLikebinding
                                    where p.forumMemberBinding.mb_ID == memberdb && p.ForumContentID == contentID
                                    select p).FirstOrDefault();

                    int likeIDed2 = likeIDed.ForumLike_ID;

                    var result2 = new
                    {
                        likeID = likeIDed2,
                        STATUS = false,
                        MSG = "按過讚了",
                    };

                    return Request.CreateResponse(HttpStatusCode.OK, result2);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // PUT: api/MesLike/5
        [HttpPut]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage Put(int id)
        {
            forumLikebinding forumLikebinding = db.forumLikebinding.FirstOrDefault(p => p.ForumLike_ID == id);
            if (forumLikebinding != null && forumLikebinding.Like_ID == 1)
            {
                forumLikebinding.Like_ID = 2;
                db.SaveChanges();

            }
            else if (forumLikebinding != null && forumLikebinding.Like_ID == 2)
            {
                forumLikebinding.Like_ID = 1;
                db.SaveChanges();
            }
            var result = new
            {
                STATUS = true,
                MSG = "收回讚",
            };

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // DELETE: api/MesLike/5
        public void Delete(int id)
        {
        }
    }
}
