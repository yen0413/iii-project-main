using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net.Http.Formatting;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using forumAPItest.Models;
using Jose;
using Newtonsoft.Json.Linq;

namespace forumAPItest.Controllers
{
    public class PackageTokenController  : ApiController
    {
        // GET: api/PackageToken
        finaldbEntities1 db = new finaldbEntities1();
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Token/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Token
      

        // PUT: api/Token/5

        [HttpPost]
        [EnableCors("*", "*", "*")]
        public object Post([FromBody] JObject value)
        {
            // TODO: key應該移至config
            var secret = "wellwindJtwDemo";

            // TODO: 真實世界檢查帳號密碼

            var account = value["account"].ToString();
            var password = value["password"].ToString();

            

            
                if(account == "email" && password == "code")
                {
                   
                        //var payload = new JwtAuthObject()
                        var payload = new JwtAuthPackgeObject()
                        {
                            accId = account,
                            pswId = "code"
                        };

                        return new
                        {
                            Result = true,
                            token = Jose.JWT.Encode(payload, Encoding.UTF8.GetBytes(secret), JwsAlgorithm.HS256)
                        };
           
                }
                else
                {
                    return new
                    {
                        Result = false,
                    };
                }

            }
           
        }
    }

    // DELETE: api/Token/5
    
    

