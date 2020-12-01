using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace newBlogprj.Models
{//開團編號 開團人 截止日期 達標人數 價格 我的訂購數 小計 團購類型 狀態
    public class vm_FollowList
    {
        public string Group_ID { get; set; }
        public string ownerMBID { get; set; }
        public string Enddate { get; set; }
        public string targetnum { get; set; }
        
        public string singleprice { get; set; }
        public string ordernum { get; set; }

        public string totalprice { get; set; }
        public string grouptype { get; set; }
        public string groupcondition { get; set; }
    }
}