using newBlogprj.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace newBlogprj.Controllers
{
    public class cBlogController : Controller
    {
        // GET: cBlog

        int h = 12;
        public ActionResult BlogList(string searchString)
        {
            finaldbEntities3 db = new finaldbEntities3();

            var table = from bloginner in db.blogBinding                        
                        orderby bloginner.Blog_ID descending                       
                        select new
                        {
                            //memberName = bloginner.memberdb.mb_employeeName,
                            blogContent = bloginner.blog.BlogContent,
                            blogTitle = bloginner.blog.BlogTitle,
                            blogID = bloginner.Blog_ID,  
                            blogdate=bloginner.blog.Blogdate,
                        };
            if (!String.IsNullOrEmpty(searchString)){
                table = table.Where(s => s.blogTitle.Contains(searchString));
            }

            var table2 = from blogemp in db.memberdb
                         where blogemp.mb_ID==h
                         select blogemp;

            List<mBlogViewmodels> blogList = new List<mBlogViewmodels>();
            
            foreach (var items in table)
            {
                mBlogViewmodels blogView = new mBlogViewmodels();
                //blogView.blog_member = items.memberName;
                blogView.blog_title = items.blogTitle;
                blogView.blog_content = items.blogContent;
                blogView.blog_ID = (int)items.blogID;
                blogView.blog_date = items.blogdate;
                blogList.Add(blogView);
            }

            foreach (var item in table2)
            {
                mBlogViewmodels blogView = new mBlogViewmodels();
                blogView.Memberdb = item;
                blogList.Add(blogView);
            }

            return View(blogList);

        }

        public ActionResult Create()
        {
           
            return View();
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(blog p,blogBinding q)
        {
            try
            {
                finaldbEntities3 db = new finaldbEntities3();
                p.Blogdate = DateTime.Now.ToString("g");
                db.blog.Add(p);
                db.SaveChanges();
                int blogID = p.Blog_ID;

                q.Blog_ID = blogID;
                q.Memberdb_ID = h;
                db.blogBinding.Add(q);
                db.SaveChanges();
                
            }
            catch(Exception ex)
            {
                throw;
            }
            return RedirectToAction("BlogList");

        }

        public ActionResult Delete(int id)
        {
            finaldbEntities3 db = new finaldbEntities3();
            blog deleteblog = db.blog.FirstOrDefault(p => p.Blog_ID == id);
            blogBinding deleteblogBinding = db.blogBinding.FirstOrDefault(p => p.Blog_ID == id);
            if (deleteblog != null)
            {
                db.blog.Remove(deleteblog);
                db.SaveChanges();
                db.blogBinding.Remove(deleteblogBinding);
                db.SaveChanges();
            }
            return RedirectToAction("BlogList");
        }

        public ActionResult Edit(int id)
        {
            blog blog = (new finaldbEntities3()).blog.FirstOrDefault(p => p.Blog_ID == id);
            if (blog == null)
                return RedirectToAction("BlogList");
            return View(blog);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(blog blog)
        {
            finaldbEntities3 db = new finaldbEntities3();
            blog editblog = db.blog.FirstOrDefault(p => p.Blog_ID == blog.Blog_ID);
            if (blog != null)
            {
                editblog.Blogdate= DateTime.Now.ToString("g");
                editblog.BlogTitle = blog.BlogTitle;
                editblog.BlogContent = blog.BlogContent;              
                db.SaveChanges();
            }
            return RedirectToAction("BlogList");
        }
    }
}