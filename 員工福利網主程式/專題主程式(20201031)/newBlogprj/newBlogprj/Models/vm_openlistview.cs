using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace newBlogprj.Models
{
    public class vm_openlistview
    {
        public string title { get; set; }
        public string startdate { get; set; }
        public string enddate { get; set; }

        public string product1ID{get;set;}
        public string product1targetnumber { get; set; }
        public int p1currentnumber { get; set; }
      
        public string product2ID { get; set; }
        public string product2targetnumber { get; set; }
        public int p2currentnumber { get; set; }
       
        public string group_type { get; set; }
        public string group_description { get; set; }
        public string ownerid { get; set; }

        public string groupid { get; set; }
    }
}