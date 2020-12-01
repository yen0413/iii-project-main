using newBlogprj;
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

namespace forumAPItest.Controllers
{
    public class VoteController : ApiController
    {
        // GET: api/Vote
        finaldbEntities2 db = new finaldbEntities2();
        int member = 59;
        public JObject Get()
        {
            LoginMethod lm = new LoginMethod();
            member = lm.getmid(lm.getuserac());
            var result = new
            {
                
                ftitle = from p in db.voteTitle.AsEnumerable()
                         where p.memberID == p.voteMember.memberID && p.title != "delete"
                         orderby p.startTime descending
                         select new
                         {
                             nameChke = p.voteMember.memberNAME == member ? true : false,
                             dateline = DateTime.Compare(DateTime.Now, DateTime.Parse(p.endTime)) > 0 ? true : false,
                             titleID = p.titleID,
                             title = p.title,
                             name = p.voteMember.memberNAME,
                             startTime = p.startTime,
                             endTime = p.endTime,
                             countdown = Math.Floor(((DateTime.Parse(p.endTime) - DateTime.Now).TotalDays)) <0 ? 0: Math.Floor(((DateTime.Parse(p.endTime) - DateTime.Now).TotalDays)),

                             fitems = new
                             {
                                 items = from q in db.memberVoteitem
                                         where q.voteitem.titleID == p.titleID
                                         group q by q.itemsID into g
                                         orderby g.Count() descending
                                         select new
                                         {
                                             items = g.Key,
                                             name = g.FirstOrDefault(q => q.voteitem.itemsID == g.Key).voteitem.items,
                                             count = g.Count() - 1,
                                         }
                             }
                         }

            };

            string strJson = JsonConvert.SerializeObject(result, Formatting.Indented);
            JObject o = JObject.Parse(strJson);
            return o;
        }

        // GET: api/Vote/5


        public JObject Get(int id)
        {
            LoginMethod lm = new LoginMethod();
            member = lm.getmid(lm.getuserac());
            var result = new
            {
                ftitle = from p in db.voteTitle.AsEnumerable()
                         where p.memberID == p.voteMember.memberID && p.title != "delete" && p.titleID == id
                         orderby p.startTime descending
                         select new
                         {
                             nameChke = p.voteMember.memberNAME == member ? true : false,
                             dateline = DateTime.Compare(DateTime.Now, DateTime.Parse(p.endTime)) > 0 ? true : false,
                             titleID = p.titleID,
                             title = p.title,
                             name = p.voteMember.memberNAME,
                             startTime = p.startTime,
                             endTime = p.endTime,

                             fitems = new
                             {
                                 items = from q in db.memberVoteitem
                                         where q.voteitem.titleID == p.titleID
                                         group q by q.itemsID into g
                                         orderby g.Count() descending
                                         select new
                                         {
                                             items = g.Key,
                                             name = g.FirstOrDefault(q => q.voteitem.itemsID == g.Key).voteitem.items,
                                             count = g.Count() - 1,
                                         }
                             }
                         }

            };

            string strJson = JsonConvert.SerializeObject(result, Formatting.Indented);
            JObject o = JObject.Parse(strJson);
            return o;
        }

        // GET: api/Vote/5
        [HttpPost]
        //[EnableCors(origins: "http://mywebclient.azurewebsites.net", headers: "*", methods: "*")]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage Post([FromBody] JObject value)
        {
            LoginMethod lm = new LoginMethod();
            member = lm.getmid(lm.getuserac());
            if (value["title"].ToString() != "delete")
            {

                voteTitle votetitle = new voteTitle();

                voteMember votemember = new voteMember();


                votemember.memberNAME = Convert.ToInt32(member.ToString());
                db.voteMember.Add(votemember);
                db.SaveChanges();


                votetitle.title = value["title"].ToString();
                votetitle.startTime = DateTime.Now.ToString("G");
                votetitle.endTime = value["endtime"].ToString();
                votetitle.memberID = votemember.memberID;
                db.voteTitle.Add(votetitle);
                db.SaveChanges();

                for (var i = 1; i <= value.Count; i++)
                {
                    voteitem voteitem = new voteitem();
                    if (i <= value.Count - 2)
                    {

                        voteitem.items = value["item" + i].ToString();
                        voteitem.titleID = votetitle.titleID;
                        db.voteitem.Add(voteitem);
                        db.SaveChanges();

                    }
                    else
                    {
                        break;
                    }
                    memberVoteitem membervoteitem = new memberVoteitem();
                    membervoteitem.memberID = 37;//超級權限
                    membervoteitem.itemsID = voteitem.itemsID;
                    db.memberVoteitem.Add(membervoteitem);
                    db.SaveChanges();
                }
                var result = new
                {
                    STATUS = true,
                    MSG = "成功",
                };

                return Request.CreateResponse(HttpStatusCode.OK, result);


            }
            else
            {
                var result2 = new
                {
                    STATUS = true,
                    MSG = "不可取名為delete",
                };

                return Request.CreateResponse(HttpStatusCode.OK, result2);
            }
        }

        // POST: api/Vote


        [HttpPut]
        //[EnableCors(origins: "http://mywebclient.azurewebsites.net", headers: "*", methods: "*")]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage Put(int id, [FromBody] JObject value)
        {

            var votetitle = db.voteTitle.FirstOrDefault(p => p.titleID == id);
            votetitle.title = value["title"].ToString();
            votetitle.endTime = value["endtime"].ToString();
            db.SaveChanges();


            var result = new
            {
                STATUS = true,
                MSG = "成功",
            };

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpDelete]
        // DELETE: api/Vote/5
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage Delete(int id, [FromBody] JObject value)
        {

            var votetitle = db.voteTitle.FirstOrDefault(p => p.titleID == id);
            votetitle.title = "delete";

            db.SaveChanges();


            var result = new
            {
                STATUS = true,
                MSG = "成功",
            };

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
