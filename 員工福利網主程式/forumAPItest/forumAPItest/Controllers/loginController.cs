using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;

namespace forumAPItest.Controllers
{
    public class loginController : ApiController
    {
        // GET: api/login


        finaldbEntities1 db = new finaldbEntities1();
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/login/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/login
        [HttpPost]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage Post([FromBody] JObject value)
        {

            try
            {
                string account = value["account"].ToString();
                string password = value["password"].ToString();

                string accountbase64Decoded;
                byte[] accountdata = Convert.FromBase64String(account);
                accountbase64Decoded = Encoding.UTF8.GetString(accountdata);

                string passwordbase64Decoded;
                byte[] passworddata = Convert.FromBase64String(password);
                passwordbase64Decoded = Encoding.UTF8.GetString(passworddata);

                var q = (from p in db.memberdb
                         where p.mb_employeeName == accountbase64Decoded
                         select p).FirstOrDefault();

               if (passwordbase64Decoded == "test" && accountbase64Decoded==q.mb_employeeName)
                {
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
                        STATUS = false,
                        MSG = "失敗",
                    };

                    return Request.CreateResponse(HttpStatusCode.OK, result2);
                }
                    
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
           
                     

        

        // PUT: api/login/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/login/5
        public void Delete(int id)
        {
        }
    }
}
