using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace newBlogprj.Models
{
    public class Mmemberdb
    {
        public string mb_empName { get; set; }
        public string mb_empAc { get; set; }
        public string mb_empPsw { get; set; }
        public int mb_empDeptID { get; set; }
        public string mb_empPhone { get; set; }
        public string mb_empEmail { get; set; }
        public string mb_empAddress { get; set; }
        public string mb_empGender { get; set; }
        public string mb_empHobby { get; set; }
        public string mb_empPicture { get; set; }
        public string mb_empBirthday { get; set; }
        public string mb_empLiveCity { get; set; }
        public string mb_empHireDate { get; set; }
        public string empTransport { get; set; }
        public int mb_empState { get; set; }

        public HttpPostedFileBase XXX { get; set; }
    }
}