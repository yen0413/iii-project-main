using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace newBlogprj.Models
{
    public class mBlogViewmodels
    {
     
        private blog blog;
        public blog Blog
        {
            get
            {
                if (blog == null)
                {
                    blog = new blog();
                }
                return blog;
            }
            set => blog = value;
        }

        public int blog_ID { get { return this.Blog.Blog_ID; } set { Blog.Blog_ID = value; } }
        public string blog_title { get { return this.Blog.BlogTitle; } set { Blog.BlogTitle = value; } }
        public string blog_content { get { return this.Blog.BlogContent; } set { Blog.BlogContent = value; } }
        public string blog_date { get { return this.Blog.Blogdate; } set { Blog.Blogdate = value; } }
        public string blog_member { get; set; }


        private memberdb memberdb;
        public memberdb Memberdb
        {
            get
            {
                if (memberdb == null)
                {
                    memberdb = new memberdb();
                }
                return memberdb;
            }
            set => memberdb = value;
        }

        public int mb_ID { get { return this.Memberdb.mb_ID; } set { Memberdb.mb_ID = value; } }
        public string mb_employeeName { get { return this.Memberdb.mb_employeeName; } set { Memberdb.mb_employeeName = value; } }
        public string mb_employeeAccount { get { return this.Memberdb.mb_employeeAccount; } set { Memberdb.mb_employeeAccount = value; } }
        public string mb_employeePassword { get { return this.Memberdb.mb_employeePassword; } set { Memberdb.mb_employeePassword = value; } }
        public Nullable<int> mb_employeeDeptID { get { return this.Memberdb.mb_employeeDeptID; } set { Memberdb.mb_employeeDeptID = value; } }
        public string mb_employeePhone { get { return this.Memberdb.mb_employeePhone; } set { Memberdb.mb_employeePhone = value; } }
        public string mb_employeeEmail { get { return this.Memberdb.mb_employeeEmail; } set { Memberdb.mb_employeeEmail = value; } }
        public string mb_employeeAddress { get { return this.Memberdb.mb_employeeAddress; } set { Memberdb.mb_employeeAddress = value; } }
        public string mb_employeeGender { get { return this.Memberdb.mb_employeeGender; } set { Memberdb.mb_employeeGender = value; } }
        public string mb_employeeHobby { get { return this.Memberdb.mb_employeeHobby; } set { Memberdb.mb_employeeHobby = value; } }
        public string mb_employeePicture { get { return this.Memberdb.mb_employeePicture; } set { Memberdb.mb_employeePicture = value; } }
        public string mb_employeeBirthday { get { return this.Memberdb.mb_employeeBirthday; } set { Memberdb.mb_employeeBirthday = value; } }
        public string mb_employeeLiveCity { get { return this.Memberdb.mb_employeeLiveCity; } set { Memberdb.mb_employeeLiveCity = value; } }
        public Nullable<System.DateTime> mb_employeeHireDate { get { return this.Memberdb.mb_employeeHireDate; } set { Memberdb.mb_employeeHireDate = value; } }
        public string mb_employeeTransport { get { return this.Memberdb.mb_employeeTransport; } set { Memberdb.mb_employeeTransport = value; } }
        public Nullable<int> mb_employeeState { get { return this.Memberdb.mb_employeeState; } set { Memberdb.mb_employeeState = value; } }
    }
}
