using newBlogprj;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace forumAPItest.Models
{
    public class cJsonModels
    {
        private forumContent forumContent;
        public forumContent ForumContent
        {
            get
            {
                if (forumContent == null)
                {
                    forumContent = new forumContent();
                }
                return forumContent;
            }
            set => forumContent = value;
        }

        public int fourm_ID { get { return this.ForumContent.ForumContentID; } set { ForumContent.ForumContentID = value; } }
        public string fourm_title { get { return this.ForumContent.ForumTitle; } set { ForumContent.ForumTitle = value; } }
        public string fourm_content { get { return this.ForumContent.ForumContent1; } set { ForumContent.ForumContent1 = value; } }
        public string fourm_time { get { return this.ForumContent.ForumContentTime; } set { ForumContent.ForumContentTime = value; } }
    }
}