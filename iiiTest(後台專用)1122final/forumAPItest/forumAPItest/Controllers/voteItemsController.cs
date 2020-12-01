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
    public class voteItemsController : ApiController
    {
        finaldbEntities1 db = new finaldbEntities1();
        int member = 59;
        // GET: api/voteItems
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/voteItems/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/voteItems
        [HttpPost]
        //[EnableCors(origins: "http://mywebclient.azurewebsites.net", headers: "*", methods: "*")]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage Post([FromBody] JObject value)
        {
            int title = int.Parse(value["title"].ToString());
            var items = value["items"].ToString();

            var votememberchk = from q in db.voteMember
                                where q.memberNAME == member
                                select new
                                {
                                    ID = q.memberID
                                };
           
            var voteItems = (from p in db.voteitem
                             where p.titleID == title && p.items == items
                             select p).FirstOrDefault();

            var voteItemsmemberchk = from r in db.memberVoteitem
                                     select new
                                     {
                                         itemsID = r.itemsID,
                                         itemsmember = r.memberID
                                     };

            foreach(var item in votememberchk)
            {
                foreach(var item2 in voteItemsmemberchk)
                {
                    if (item.ID == item2.itemsmember && voteItems.itemsID == item2.itemsID)
                    {
                        var result = new
                        {
                            STATUS = true,
                            MSG = "重複投票囉",
                        };

                        return Request.CreateResponse(HttpStatusCode.OK, result);

                    }
                                 
                }
               
            }

            {
                voteMember votemember = new voteMember();
                votemember.memberNAME = member;
                db.voteMember.Add(votemember);
                db.SaveChanges();

                memberVoteitem membervoteitem = new memberVoteitem();
                membervoteitem.memberID = votemember.memberID;
                membervoteitem.itemsID = voteItems.itemsID;
                db.memberVoteitem.Add(membervoteitem);
                db.SaveChanges();
                var result2 = new
                {
                    STATUS = true,
                    MSG = "成功",
                };

                return Request.CreateResponse(HttpStatusCode.OK, result2);
            }

        }
            // PUT: api/voteItems/5
            public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/voteItems/5
        public void Delete(int id)
        {
        }
    }
}
