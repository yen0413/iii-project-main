using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace newBlogprj.Models
{
    public class vmp_openlist
    {
        public string group_title { get; set; }
        public string group_startdate { get; set; }
        public string group_enddate { get; set; }
        public int targetnumber1 { get; set; }
        public int targetnumber2 { get; set; }
        public int currentnum1 { get; set; }
        public int currentnum2 { get; set; }
        public int group_type { get; set; }
        public string group_description { get; set; }
        public int owndermemberid { get; set; }

        public string pic1url { get; set; }
        public string pic2url { get; set; }
        
        public HttpPostedFileBase XXX { get; set; }
        public HttpPostedFileBase XXX2 { get; set; }
    }
}