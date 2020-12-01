using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace newBlogprj.Models
{
    public class vm_purchaselist
    {
        public string p1img { get; set; }
        public string p2img { get; set; }

        public string c_title { get; set; }

        public string p1name { get; set; }
        public string p1price { get; set; }
        public string p1currentnumber { get; set; }
        public string p1targetnumber { get; set; }

        public string p2name { get; set; }
        public string p2price { get; set; }
        public string p2currentnumber { get; set; }
        public string p2targetnumber { get; set; }

        public string enddate { get; set; }
        public string gid { get; set; }
    }
}