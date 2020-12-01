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
    public class blogbackController : ApiController
    {
        // GET: api/blogback
        finaldbEntities1 db = new finaldbEntities1();

        [JwtAuthActionFilte]
        public JObject Get()
        {

            var ftable = from n in db.blogBinding
                         orderby n.blog.Blog_ID descending
                         select new
                         {
                             id = n.Blog_ID,
                             title = n.blog.blogTitle,
                             Content = n.blog.BlogContent,
                             time = n.blog.Blogdate,
                         };

            var allName = from m in db.memberdb
                          select new
                          {
                              name = m.mb_employeeName
                          };


            var result = new
            {
                ftable,
                allName,
            };

            string strJson = JsonConvert.SerializeObject(result, Formatting.Indented);
            JObject o = JObject.Parse(strJson);
            return o;

        }

        public JObject Get(string titletxt, string contenttxt)
        {


            var ftable = from n in db.blogBinding
                         orderby n.blog.Blog_ID descending
                         where n.blog.blogTitle.Contains(titletxt) || n.blog.BlogContent.Contains(contenttxt)
                         select new
                         {
                             id = n.Blog_ID,
                             title = n.blog.blogTitle,
                             Content = n.blog.BlogContent,
                             time = n.blog.Blogdate,
                             name = n.memberdb.mb_employeeName
                         };
            //if (!String.IsNullOrEmpty(titletxt))
            //{
            //    ftable = ftable.Where(s => s.title.Contains(titletxt));
            //}

            var result = new
            {
                ftable
            };

            string strJson = JsonConvert.SerializeObject(result, Formatting.Indented);
            JObject o = JObject.Parse(strJson);
            return o;

        }

        // POST: api/blogback
        [HttpPost]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage Post([FromBody] JObject value)
        {
            blog blogadd = new blog();
            blogBinding blogBinding = new blogBinding();

            blogadd.blogTitle= value["title"].ToString();
            blogadd.BlogContent= value["content"].ToString();
            blogadd.Blogdate = DateTime.Now.ToString("G");
            db.blog.Add(blogadd);
            db.SaveChanges();


            string name = value["member"].ToString();
            var nameID = db.memberdb.FirstOrDefault(p => p.mb_employeeName == name);
                     
            blogBinding.Memberdb_ID= nameID.mb_ID;
            blogBinding.Blog_ID = blogadd.Blog_ID;
            db.blogBinding.Add(blogBinding);
            db.SaveChanges();


            var result = new
            {
                STATUS = true,
                MSG = "成功",
            };

            return Request.CreateResponse(HttpStatusCode.OK, result);


        }

        // PUT: api/blogback/5
        [HttpPut]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage Put(int id, [FromBody] JObject value)
        {

            string controllerName = ControllerContext.RouteData.Values["controller"].ToString();
            var Putblog = db.blog.FirstOrDefault(p => p.Blog_ID == id);
            var title = value["title"].ToString();
            var content = value["content"].ToString();
            var time = DateTime.Now.ToString("G");

            Putblog.blogTitle = title;
            Putblog.BlogContent = content;
            Putblog.Blogdate = time;
            db.SaveChanges();
            var result = new
            {
                STATUS = true,
                MSG = "成功",
            };

            return Request.CreateResponse(HttpStatusCode.OK, result);




        }

        // DELETE: api/blogback/5
        public HttpResponseMessage Delete(int id)
        {
            blog deleteblog = db.blog.FirstOrDefault(p => p.Blog_ID == id);
            blogBinding deleteblogBinding = db.blogBinding.FirstOrDefault(p => p.Blog_ID == id);
            if (deleteblog != null)
            {
                db.blog.Remove(deleteblog);
                db.SaveChanges();
                db.blogBinding.Remove(deleteblogBinding);
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
