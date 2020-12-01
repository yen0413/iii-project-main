using Action.Models;
using newBlogprj.Models;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Activities.Statements;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace newBlogprj.Controllers
{
    public class cBlogController : Controller
    {
        public static string evv = "";
        public static string ecmsg = "";
        public static string rgmsg = "";
         public  static  string acc = "";
     //   public  const  string  acc ;

        //public static int testacc = 0;

        // public static ArrayList acc = new ArrayList();
        //public static int ecmsgcount = 0;
        public static bool logincheck = false;
        public ActionResult Start()
        {
            
            ViewBag.emails = evv;
            ViewBag.registerstatus = rgmsg;
            if (rgmsg == "success")
            {
                ecmsg = "";
            }
           //if(ecmsg=="true" && ecmsgcount == 0)
           // {
           //     ecmsgcount++;
           // }
           // if (ecmsg == "true" && ecmsgcount != 0)
           // {
           //     ecmsg = "";
           // }
            ViewBag.emailconfirm = ecmsg;
            ecmsg = "";
            rgmsg = "fail";
            return View();
            
        }
        [HttpPost]
        public ActionResult Start(MemberACandPSW macpsw)
        {
            macpsw.mac = Request.Form["txtac"];
            macpsw.mpsw = Request.Form["txtpsw"];
           
            
             acc = Request.Form["txtac"];
            
            //  Session[""+list_acc_key+""] = acc;
            //if(acc != Session["" + testacc + ""].ToString())
            //{
            //    foreach(string item in Session)
            //    testacc++;

            //}
            
         //   Session[""+testacc+""] = acc;
            

            LoginMethod lm = new LoginMethod();
            //lm.setuserac(acc);
            bool isacpswok = lm.AcPswCheck(macpsw.mac, macpsw.mpsw);

            HttpCookie cookie = new HttpCookie("test");
            //產生一個cookie
            cookie.Value = Server.UrlEncode(acc);
            //設訂單值
            cookie.Expires = DateTime.Now.AddDays(2);
            //設定過期日
            Response.Cookies.Add(cookie);
            //寫到用戶端

            if (isacpswok)
            {
             //   list_acc.Add(lm.getusername(acc));
            
                ViewBag.Title = "find";
                // ViewBag.LoginName ="歡迎 "+list_acc[list_acc.IndexOf(lm.getusername(acc))];
                //  ViewBag.LoginName = "歡迎" + Session[acc];
                Session[Request.Form["txtac"]] = Request.Form["txtac"];
                Response.Cookies.Add(cookie);
                HttpCookie cookies = Request.Cookies[Request.Form["txtac"]];
                ViewBag.LoginName = "歡迎" + cookies;
                //ViewBag.LoginName = "歡迎" + lm.getusername(acc);
                //lm.setuserac(cookies.Value);
                logincheck = true;
                return RedirectToAction("SecondStart");
            }
            else
            {
                ViewBag.Title = "fail";
                ViewBag.LoginName = "";
                 logincheck = false;
               // logincheck = true;
            }
            return View();
          //  return RedirectToAction("SecondStart");
        }

        public ActionResult SecondStart()
        {
            //彥程新增
            //--------------------------------------------
            HttpCookie cookies = Request.Cookies["test"];
            int mb_id = lm2.getmid(cookies.Value);

            //  int mb_id = lm2.getmid(acc);
            ViewBag.mb_id = mb_id;
            return View();
        }

        public ActionResult MemberRegister()
        {
            //Mmemberdb mbdb = new Mmemberdb();
            //LoginMethod lm = new LoginMethod();
            //mbdb.mb_empAc = "test1";
            //mbdb.mb_empPsw = "123";
            //lm.Register(mbdb);
            
            return View();
        }
        [HttpPost]
        public ActionResult MemberRegister(Mmemberdb mbdb)
        {
            mbdb.mb_empAc = Request.Form["txtac"];
            bool accheck = lm2.AcCheck(mbdb.mb_empAc);
            if (accheck == false)
            {
                ViewBag.rmsg = "此帳號已經註冊過";
                return View();
            }
            mbdb.mb_empPsw = Request.Form["txtpsw"];
            mbdb.mb_empName = Request.Form["txtname"];
            mbdb.mb_empGender = Request.Form["txtgender"];
            mbdb.mb_empAddress = Request.Form["txtaddress"];
            mbdb.mb_empDeptID = Convert.ToInt32(Request.Form["txtdept"]);
            mbdb.mb_empPhone = Request.Form["txtphone"];
            mbdb.mb_empEmail = Request.Form["txtemail"];
            mbdb.mb_empHobby = Request.Form["txthobby"];
            mbdb.mb_empLiveCity = Request.Form["txtlivecity"];
            mbdb.mb_empHireDate = Request.Form["txthireday"];
            mbdb.mb_empState = 1;
            if (mbdb.mb_empHireDate == "")
            {
                mbdb.mb_empHireDate = DateTime.Now.ToString();
            }
            if (mbdb.mb_empDeptID == 0)
            {
                mbdb.mb_empDeptID = 4;
            }
            mbdb.mb_empBirthday = Request.Form["txtbirthday"];
            mbdb.empTransport = Request.Form["txttransport"];
            //-------------------------------------------------
            //拿照片的base64資料
            FileInfo file = new FileInfo(mbdb.XXX.FileName);
            
            string photName =file.Name ;
            mbdb.XXX.SaveAs(Server.MapPath("../Content/" + photName));

           // string filename3 = @"C:\專案重要備份檔\專案備份\iiiTest\專題主程式(20201031)\newBlogprj\newBlogprj\Content\"+photName;
          
            string getpath = AppDomain.CurrentDomain.BaseDirectory;
            string filename3 = getpath + @"\Content\" + photName;

            Bitmap bit3 = new Bitmap(filename3);
            MemoryStream ms3 = new MemoryStream();
            bit3.Save(ms3, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] arr3 = new byte[ms3.Length];
            ms3.Position = 0;
            ms3.Read(arr3, 0, (int)ms3.Length);
            string accountbase64Decoded3 = Convert.ToBase64String(arr3);
            //--------------------------------------------
            // mbdb.mb_empPicture = "../Content/" + photName;
            mbdb.mb_empPicture = "data:image/jpg;base64,"+ accountbase64Decoded3;
            // mbdb.mb_empPicture = 
            //  ViewBag.img =".."+mbdb.mb_empPicture;
            

            LoginMethod lm = new LoginMethod();
            lm.Register(mbdb);
            rgmsg = "success";
            return RedirectToAction("Start");
        }
        public ActionResult memberregister2()
        {
            return View();
        }

        public ActionResult MemberDataEdit(int id)
        {
            finaldbEntities2 db = new finaldbEntities2();
            Mmemberdb mdb = new Mmemberdb();
            var q = from f in db.memberdb
                    where f.mb_ID == id
                    select f;
            foreach(var item in q)
            {
                mdb.mb_empName = item.mb_employeeName;
                mdb.mb_empAc = item.mb_employeeAccount;
                mdb.mb_empPsw = item.mb_employeePassword;
                mdb.mb_empDeptID =Convert.ToInt32( item.mb_employeeDeptID);
                mdb.mb_empPhone = item.mb_employeePhone;
                mdb.mb_empEmail = item.mb_employeeEmail;
                mdb.mb_empAddress = item.mb_employeeAddress;
                mdb.mb_empGender = item.mb_employeeGender;
                mdb.mb_empHobby = item.mb_employeeHobby;
                mdb.mb_empPicture = item.mb_employeePicture;
                mdb.mb_empBirthday = item.mb_employeeBirthday;
                mdb.mb_empLiveCity = item.mb_employeeLiveCity;
                 mdb.mb_empHireDate = item.mb_employeeHireDate.ToString();
                DateTime dt = new DateTime();
                string sy = "";
                string sm = "";
                string sd = "";
                sy = item.mb_employeeHireDate.Value.Year.ToString();
                sm = item.mb_employeeHireDate.Value.Month.ToString();
                sd = item.mb_employeeHireDate.Value.Day.ToString();
                if (sm.Length == 1)
                {
                    sm = "0" + sm;
                }
                if (sd.Length == 1)
                {
                    sd = "0" + sd;
                }
                mdb.mb_empHireDate = sy+"-"+sm + "-" + sd;
                mdb.empTransport = item.mb_employeeTransport;
                mdb.mb_empState = Convert.ToInt32( item.mb_employeeState);
                
            }
            return View(mdb);
        }
        [HttpPost]
        public ActionResult MemberDataEdit(Mmemberdb mbd)
        {
            return RedirectToAction("Start");
        }
        public ActionResult SendEmail(string id)
        {
            string tid = id;
            evv = id;
            string ttid = "";
            string se = "";
            finaldbEntities2 db = new finaldbEntities2();
            var q = from f in db.memberdb
                    where f.mb_employeeEmail == id
                    select f;
            foreach(var item in q)
            {
                se = item.mb_employeePassword;
            }
            //--------先做email的驗證 並回傳電子郵件驗證結果
            if (se == "")
            {
                ecmsg = "false";
                return RedirectToAction("Start");
            }
            //--------------------------
            SmtpClient Client = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential()
                {
                    UserName = "hungyeelin@gmail.com",
                    Password = "0976204711"
                }
            };
            MailAddress FromEmail = new MailAddress("hungyeelin@gmail.com", "HY LIN");
            //    MailAddress ToEmail = new MailAddress("hongyeelin5@gmail.com", "HY LIN2");
            //MailAddress ToEmail = new MailAddress(id, "HY LIN2");
            MailAddress ToEmail = new MailAddress("hongyeelin5@gmail.com", "HY LIN2");
            MailMessage Message = new MailMessage()
            {
                From = FromEmail,
                Subject = "This is your password number",
                Body ="這是您的註冊密碼: " +se
                //  IsBodyHtml = true
            };
            Message.To.Add(ToEmail);

            Client.Send(Message);
            ecmsg = "true";
            return RedirectToAction("Start");
        }
        public ActionResult LogOut()
        {
            logincheck = false;
            return RedirectToAction("Start");         
        }
        // GET: cBlog
        //----------------------------------------------------------
        // int h = 12;
        int h = 58;
        LoginMethod lm2 = new LoginMethod();
        
        
        public ActionResult BlogList(string searchString)
        {
           // string testnum = Session[testacc.ToString()].ToString();
            finaldbEntities2 db = new finaldbEntities2();
            HttpCookie cookies = Request.Cookies["test"];
            //int hh = lm2.getmid(acc);
            int hh = lm2.getmid(cookies.Value);
                ViewBag.LoginName ="歡迎 "+ lm2.getusername(cookies.Value);
         //   ViewBag.LoginName = "歡迎 " + ;

            //  ViewBag.enumber2 = lm2.getmid(acc);
            ViewBag.enumber2 = lm2.getmid(cookies.Value);

            var table = from bloginner in db.blogBinding
                        where bloginner.Memberdb_ID ==hh                
                        orderby bloginner.Blog_ID descending  
                       
                        select new
                        {
                            //memberName = bloginner.memberdb.mb_employeeName,
                            blogContent = bloginner.blog.BlogContent,
                            blogTitle = bloginner.blog.blogTitle,
                            blogID = bloginner.Blog_ID,  
                            blogdate=bloginner.blog.Blogdate,
                        };
            if (!String.IsNullOrEmpty(searchString)){
                table = table.Where(s => s.blogTitle.Contains(searchString));
            }

            //     h =lm2.getmid(acc);
            h = lm2.getmid(cookies.Value);

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
            ViewBag.enumber3 = hh;
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
            HttpCookie cookies = Request.Cookies["test"];
            try
            {
                finaldbEntities2 db = new finaldbEntities2();
                p.Blogdate = DateTime.Now.ToString("g");
                db.blog.Add(p);
                db.SaveChanges();
                int blogID = p.Blog_ID;

                q.Blog_ID = blogID;
                //   q.Memberdb_ID = lm2.getmid(acc);
                q.Memberdb_ID = lm2.getmid(cookies.Value);
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
            finaldbEntities2 db = new finaldbEntities2();
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
            blog blog = (new finaldbEntities2()).blog.FirstOrDefault(p => p.Blog_ID == id);
            if (blog == null)
                return RedirectToAction("BlogList");
            return View(blog);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(blog blog)
        {
            finaldbEntities2 db = new finaldbEntities2();
            blog editblog = db.blog.FirstOrDefault(p => p.Blog_ID == blog.Blog_ID);
            if (blog != null)
            {
                editblog.Blogdate= DateTime.Now.ToString("g");
                editblog.blogTitle = blog.blogTitle;
                editblog.BlogContent = blog.BlogContent;              
                db.SaveChanges();
            }
            return RedirectToAction("BlogList");
        }

        //--------------------------------------------------------------------活動區
        finaldbEntities2 db = new finaldbEntities2();
        EventdbEntities1 edb = new EventdbEntities1();
        public ActionResult Action()
        {
            HttpCookie cookies = Request.Cookies["test"];
            ViewBag.username = lm2.getusername(cookies.Value);
            ViewBag.enumber2 = lm2.getmid(cookies.Value);
            string eventFilePath = System.Web.Configuration.WebConfigurationManager.AppSettings["EventFilePath"];

            List<ActionViewModel> list = (from p in edb.Event2
                                          where p.EventStartDate > DateTime.Now
                                          select new ActionViewModel
                                          {
                                              Event_ID = p.Event_ID,
                                              EventName = p.EventName,
                                              EventLocation = p.EventLocation,
                                              EventStartDate = p.EventStartDate,
                                              EventEndDate = p.EventEndDate,
                                              EventImage = eventFilePath + p.Event_ID + "/" + p.EventImage,
                                              LikeCount = (from i in edb.EventComment2
                                                           where i.Event_ID == p.Event_ID
                                                           select i).Count(),
                                              JoinCount = (from i in edb.EventBooking2
                                                           where i.Event_ID == p.Event_ID
                                                           select i).Count(),
                                              RemainCount = p.EventMaxPeople - (from i in edb.EventBooking2
                                                                                where i.Event_ID == p.Event_ID
                                                                                select i).Count()
                                          }).ToList();
            ViewBag.Top5 = (from p in list
                            orderby p.LikeCount descending
                            select p).Take(5).ToList();
            ViewBag.keyword = "";
            ViewBag.startdate = "";
            ViewBag.enddate = "";
            return View(list);
        }
        [HttpPost]
        public ActionResult Action(int a = 0)
        {
            HttpCookie cookies = Request.Cookies["test"];
            ViewBag.username = lm2.getusername(cookies.Value);
            ViewBag.enumber2 = lm2.getmid(cookies.Value);
            string eventFilePath = System.Web.Configuration.WebConfigurationManager.AppSettings["EventFilePath"];
            string keyword = Request.Form["keyword"].ToString();
            bool haskeyword = false;
            if (string.IsNullOrWhiteSpace(keyword))
            {
                haskeyword = true;
            }
            DateTime startdate = string.IsNullOrWhiteSpace(Request.Form["startdate"]) ? DateTime.MinValue : Convert.ToDateTime(Request.Form["startdate"].ToString());
            DateTime enddate = string.IsNullOrWhiteSpace(Request.Form["enddate"]) ? DateTime.MaxValue : Convert.ToDateTime(Request.Form["enddate"].ToString());

            List<ActionViewModel> list = (from p in edb.Event2
                                          where (p.EventName.Contains(keyword) || haskeyword) &&
                                                p.EventStartDate >= startdate &&
                                                p.EventEndDate <= enddate &&
                                                p.EventStartDate > DateTime.Now
                                          select new ActionViewModel
                                          {
                                              Event_ID = p.Event_ID,
                                              EventName = p.EventName,
                                              EventLocation = p.EventLocation,
                                              EventStartDate = p.EventStartDate,
                                              EventEndDate = p.EventEndDate,
                                              EventImage = eventFilePath + p.Event_ID + "/" + p.EventImage,
                                              LikeCount = (from i in edb.EventComment2
                                                           where i.Event_ID == p.Event_ID
                                                           select i).Count(),
                                              JoinCount = (from i in edb.EventBooking2
                                                           where i.Event_ID == p.Event_ID
                                                           select i).Count(),
                                              RemainCount = p.EventMaxPeople - (from i in edb.EventBooking2
                                                                                where i.Event_ID == p.Event_ID
                                                                                select i).Count()
                                          }).ToList();
            ViewBag.Top5 = (from p in list
                            orderby p.LikeCount descending
                            select p).Take(5).ToList();

            ViewBag.keyword = keyword;
            ViewBag.startdate = "";
            ViewBag.enddate = "";
            if (startdate != DateTime.MinValue)
            {
                ViewBag.startdate = startdate.ToString("yyyy/MM/dd");
            }
            if (enddate != DateTime.MaxValue)
            {
                ViewBag.enddate = enddate.ToString("yyyy/MM/dd");
            }

            return View(list);
        }

        public ActionResult ActionList()
        {
            HttpCookie cookies = Request.Cookies["test"];
            bool boolManager = false;
            int mb_ID = lm2.getmid(cookies.Value);
            if (mb_ID == 2059)
            {
                boolManager = true;
            }
            ViewBag.username = lm2.getusername(cookies.Value);
            ViewBag.enumber2 = mb_ID;

            string eventFilePath = System.Web.Configuration.WebConfigurationManager.AppSettings["EventFilePath"];
            var table = from p in edb.Event2
                        where p.EventCreateEmployeeID == mb_ID || boolManager
                        select new ActionViewModel { Event_ID = p.Event_ID, EventName = p.EventName, EventContent = p.EventContent, EventLocation = p.EventLocation, EventStartDate = p.EventStartDate, EventEndDate = p.EventEndDate, EventImage = eventFilePath + p.Event_ID + "/" + p.EventImage };
            ViewBag.EventContent = (from p in table
                                    where p.Event_ID == p.Event_ID
                                    select p).Take(1).ToList();
            ViewBag.keyword = "";
            ViewBag.startdate = "";
            ViewBag.enddate = "";

            return View(table);
        }

        [HttpPost]
        public ActionResult ActionList(int a = 0)    //給管理者查詢用的
        {
            HttpCookie cookies = Request.Cookies["test"];
            bool boolManager = false;
            int mb_ID = lm2.getmid(cookies.Value);
            if (mb_ID == 2059)
            {
                boolManager = true;
            }
            ViewBag.username = lm2.getusername(cookies.Value);
            ViewBag.enumber2 = mb_ID;

            string eventFilePath = System.Web.Configuration.WebConfigurationManager.AppSettings["EventFilePath"];
            string keyword = Request.Form["keyword"].ToString();
            bool haskeyword = false;
            if (string.IsNullOrWhiteSpace(keyword))
            {
                haskeyword = true;
            }
            DateTime startdate = string.IsNullOrWhiteSpace(Request.Form["startdate"]) ? DateTime.MinValue : Convert.ToDateTime(Request.Form["startdate"].ToString());
            DateTime enddate = string.IsNullOrWhiteSpace(Request.Form["enddate"]) ? DateTime.MaxValue : Convert.ToDateTime(Request.Form["enddate"].ToString());

            var table = from p in edb.Event2
                        where (p.EventCreateEmployeeID == mb_ID || boolManager)
                        where (p.EventName.Contains(keyword) || haskeyword) &&
                              p.EventStartDate >= startdate &&
                              p.EventEndDate <= enddate &&
                              p.EventStartDate > DateTime.Now
                        select new ActionViewModel { Event_ID = p.Event_ID, EventName = p.EventName, EventContent = p.EventContent, EventLocation = p.EventLocation, EventStartDate = p.EventStartDate, EventEndDate = p.EventEndDate, EventImage = eventFilePath + p.Event_ID + "/" + p.EventImage };
            ViewBag.EventContent = (from p in table
                                    where p.Event_ID == p.Event_ID
                                    select p).Take(1).ToList();

            ViewBag.keyword = keyword;
            ViewBag.startdate = "";
            ViewBag.enddate = "";
            if (startdate != DateTime.MinValue)
            {
                ViewBag.startdate = startdate.ToString("yyyy/MM/dd");
            }
            if (enddate != DateTime.MaxValue)
            {
                ViewBag.enddate = enddate.ToString("yyyy/MM/dd");
            }

            return View(table);
        }


        public FileResult ExportActionList(string keyword, string sd, string ed)
        {
            bool haskeyword = false;
            if (string.IsNullOrWhiteSpace(keyword))
            {
                haskeyword = true;
            }
            DateTime startdate = string.IsNullOrWhiteSpace(sd) ? DateTime.MinValue : Convert.ToDateTime(sd);
            DateTime enddate = string.IsNullOrWhiteSpace(ed) ? DateTime.MaxValue : Convert.ToDateTime(ed);

            var acttionLst = (from p in edb.Event2
                              where (p.EventName.Contains(keyword) || haskeyword) &&
                                    p.EventStartDate >= startdate &&
                                    p.EventEndDate <= enddate &&
                                    p.EventStartDate > DateTime.Now
                              select new ActionViewModel
                              {
                                  Event_ID = p.Event_ID,
                                  EventName = p.EventName,
                                  EventContent = p.EventContent,
                                  EventLocation = p.EventLocation,
                                  EventStartDate = p.EventStartDate,
                                  EventEndDate = p.EventEndDate
                              }).ToList();
            if (acttionLst != null)
            {
                XSSFWorkbook workbook;
                using (FileStream files = new FileStream(Server.MapPath("~/spforaction/Action.xlsx"), FileMode.Open, FileAccess.Read))
                {
                    workbook = new XSSFWorkbook(files);

                    ISheet sheet = workbook.GetSheetAt(0);
                    //sheet.GetRow(2).GetCell(1).SetCellValue(DateTime.Now.ToString("yyyy/MM/dd")); //製表日期

                    int intDataSeqNo = 1;
                    int intExportStartRowIndex = 4;
                    XSSFFont font = (XSSFFont)workbook.CreateFont();
                    font.FontName = "微軟正黑體";
                    font.FontHeightInPoints = 12;

                    XSSFCellStyle borderedCellStyle = (XSSFCellStyle)workbook.CreateCellStyle();
                    borderedCellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    borderedCellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    borderedCellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    borderedCellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    borderedCellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
                    borderedCellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                    borderedCellStyle.WrapText = true;
                    borderedCellStyle.SetFont(font);


                    XSSFCellStyle borderedCellStyle2 = (XSSFCellStyle)workbook.CreateCellStyle();
                    borderedCellStyle2.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    borderedCellStyle2.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    borderedCellStyle2.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    borderedCellStyle2.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    borderedCellStyle2.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
                    borderedCellStyle2.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Left;
                    borderedCellStyle2.WrapText = true;
                    borderedCellStyle2.SetFont(font);


                    foreach (var item in acttionLst)
                    {
                        IRow tmpExportRow = sheet.CreateRow(intExportStartRowIndex);
                        ICell tmpExportCell_0 = tmpExportRow.CreateCell(0); //序號
                        tmpExportCell_0.SetCellValue(intDataSeqNo);
                        tmpExportCell_0.CellStyle = borderedCellStyle;

                        ICell tmpExportCell_1 = tmpExportRow.CreateCell(1); //活動名稱
                        tmpExportCell_1.SetCellValue(item.EventName);
                        tmpExportCell_1.CellStyle = borderedCellStyle2;

                        ICell tmpExportCell_2 = tmpExportRow.CreateCell(2); //活動起日
                        tmpExportCell_2.SetCellValue(item.EventStartDate.Value.ToString("yyyy/MM/dd"));
                        tmpExportCell_2.CellStyle = borderedCellStyle;

                        ICell tmpExportCell_3 = tmpExportRow.CreateCell(3); //活動迄日
                        tmpExportCell_3.SetCellValue(item.EventEndDate.Value.ToString("yyyy/MM/dd"));
                        tmpExportCell_3.CellStyle = borderedCellStyle;

                        ICell tmpExportCell_4 = tmpExportRow.CreateCell(4); //活動地點
                        tmpExportCell_4.SetCellValue(item.EventLocation);
                        tmpExportCell_4.CellStyle = borderedCellStyle2;

                        ICell tmpExportCell_5 = tmpExportRow.CreateCell(5); //活動內容
                        tmpExportCell_5.SetCellValue(item.EventContent);
                        tmpExportCell_5.CellStyle = borderedCellStyle2;

                        intDataSeqNo += 1;
                        intExportStartRowIndex += 1;
                    }

                    MemoryStream ms = new MemoryStream();
                    workbook.Write(ms);
                    ms.Close();
                    return File(ms.ToArray(), "application/vnd.ms-excel", "員工福利網-活動清單一覽表.xlsx");
                }
            }
            else
            {
                return null;
            }
        }



        public ActionResult aCreate()
        {
            HttpCookie cookies = Request.Cookies["test"];
            ViewBag.username = lm2.getusername(cookies.Value);
            return View();
        }
        [HttpPost]
        public ActionResult aCreate(Event2 p/*,int EventCreateEmployeeID = 58*/)
        {
            HttpCookie cookies = Request.Cookies["test"];
            //p.EventCreateEmployeeID = 58;

            //using (TransactionScope ts = new TransactionScope())
            // {
            p.EventCreateEmployeeID = lm2.getmid(cookies.Value);
            edb.Event2.Add(p);
            edb.SaveChanges();

            //  if (Request.Files != null)
            // {
            p.EventImage = SaveFile(Request.Files, p.Event_ID);
            edb.Entry(p).Property(x => x.EventImage).IsModified = true;
            edb.SaveChanges();
            //   }
            //  ts.Complete();
            // }
            return RedirectToAction("ActionList");
            //===================================================================================
            //if (EventCreateEmployeeID == 58)
            //{
            //    return RedirectToAction("personalActionList");
            //}
            //else
            //{
            //    return RedirectToAction("ActionList");
            //}
        }
        public string SaveFile(HttpFileCollectionBase files, int Event_ID)
        {
            string eventFilePath = System.Web.Configuration.WebConfigurationManager.AppSettings["EventFilePath"];
            foreach (string file in files)
            {
                HttpPostedFileBase uploadFile = Request.Files[file] as HttpPostedFileBase;
                if (uploadFile != null && uploadFile.ContentLength > 0)
                {
                    string path = Server.MapPath(eventFilePath) + Event_ID.ToString();
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    path = Path.Combine(path, uploadFile.FileName);
                    uploadFile.SaveAs(path);

                    return uploadFile.FileName;
                }
                else
                {
                    return "";
                }
            }
            return "";
        }
        //[HttpPost]
        //public ActionResult aCreate(Event p)
        //{
        //    db.Event.Add(p);
        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //    return RedirectToAction("ActionList");
        //}
        public ActionResult Copy(int id)
        {
            Event2 product = edb.Event2.FirstOrDefault(p => p.Event_ID == id);
            if (product != null)
            {
                product.EventName += "(複製)";
                edb.Event2.Add(product);
                edb.SaveChanges();
            }
            return RedirectToAction("ActionList");
        }
        //public ActionResult Copy(int id)
        //{
        //    Event product = db.Event.FirstOrDefault(p => p.Event_ID == id);
        //    if (product != null)
        //    {
        //        product.EventName += "(複製)";
        //        db.Event.Add(product);
        //        db.SaveChanges();
        //    }
        //    return RedirectToAction("ActionList");
        //}
        public ActionResult aEdit(int id)
        {
            string eventFilePath = System.Web.Configuration.WebConfigurationManager.AppSettings["EventFilePath"];
            string _Event_ID = id.ToString();
            ActionViewModel product = (from p in edb.Event2
                                       where p.Event_ID == id
                                       select new ActionViewModel
                                       {
                                           Event_ID = p.Event_ID,
                                           EventName = p.EventName,
                                           EventContent = p.EventContent,
                                           EventLocation = p.EventLocation,
                                           EventStartDate = p.EventStartDate,
                                           EventEndDate = p.EventEndDate,
                                           EventMaxPeople = p.EventMaxPeople,
                                           EventImage = eventFilePath + _Event_ID + "/" + p.EventImage
                                       }).FirstOrDefault();
            if (product == null)
            {
                return RedirectToAction("ActionList");
            }
            return View(product);
        }
        [HttpPost]
        public ActionResult aEdit(ActionViewModel prod/*,int EventCreateEmployeeID = 58*/)
        {
            Event2 product = edb.Event2.FirstOrDefault(p => p.Event_ID == prod.Event_ID);
            if (product != null)
            {
                product.EventName = prod.EventName;
                product.EventContent = prod.EventContent;
                product.EventLocation = prod.EventLocation;
                product.EventStartDate = prod.EventStartDate;
                product.EventEndDate = prod.EventEndDate;
                product.EventMaxPeople = (int)prod.EventMaxPeople;
                string newfileName = SaveFile(Request.Files, product.Event_ID);
                edb.SaveChanges();
                product.EventImage = string.IsNullOrWhiteSpace(newfileName) ? product.EventImage : newfileName;
            }
            return RedirectToAction("ActionList");
            //===================================================================================
            //if (EventCreateEmployeeID == 58)
            //{
            //    return RedirectToAction("personalActionList");
            //}
            //else
            //{
            //    return RedirectToAction("ActionList");
            //}
        }
        //public ActionResult aEdit(int id)
        //{
        //    ActionViewModel product = (from p in db.Event
        //                               where p.Event_ID == id
        //                               select new ActionViewModel
        //                               {
        //                                   Event_ID = p.Event_ID,
        //                                   EventName = p.EventName,
        //                                   EventLocation = p.EventLocation,
        //                                   EventStartDate = p.EventStartDate,
        //                                   EventEndDate = p.EventEndDate
        //                               }).FirstOrDefault();
        //    if (product == null)
        //    {
        //        return RedirectToAction("ActionList");
        //    }
        //    return View(product);
        //}
        //[HttpPost]
        //public ActionResult aEdit(ActionViewModel prod)
        //{
        //    Event product = db.Event.FirstOrDefault(p => p.Event_ID == prod.Event_ID);
        //    if (product != null)
        //    {
        //        product.EventName = prod.EventName;
        //        product.EventLocation = prod.EventLocation;
        //        product.EventStartDate = prod.EventStartDate;
        //        product.EventEndDate = prod.EventEndDate;
        //        db.SaveChanges();
        //    }
        //    return RedirectToAction("ActionList");
        //}
        public ActionResult Index()
        {
            return RedirectToAction("ActionList");
        }
        public ActionResult aDelete(int id)
        {
            Event2 _Event = edb.Event2.FirstOrDefault(p => p.Event_ID == id);
            EventBooking2 _EventBooking = edb.EventBooking2.FirstOrDefault(p => p.Event_ID == id);
            EventComment2 _EventComment = edb.EventComment2.FirstOrDefault(p => p.Event_ID == id);

            // using (TransactionScope ts = new TransactionScope())
            // {
            while (_EventComment != null)
            {
                edb.EventComment2.Remove(_EventComment);
                edb.SaveChanges();
                _EventComment = edb.EventComment2.FirstOrDefault(p => p.Event_ID == id);
            }

            /* if (_EventComment != null)
                 {
                     edb.EventComment2.Remove(_EventComment);
                 }*/
            while (_EventBooking != null)
            {
                edb.EventBooking2.Remove(_EventBooking);
                edb.SaveChanges();
                _EventBooking = edb.EventBooking2.FirstOrDefault(p => p.Event_ID == id);
            }
            if (_EventBooking != null)
            {
                edb.EventBooking2.Remove(_EventBooking);
            }
            if (_Event != null)
            {
                edb.Event2.Remove(_Event);
            }
            edb.SaveChanges();
            // ts.Complete();
            //}

            return RedirectToAction("ActionList");
            //===================================================================================
            //if (EventCreateEmployeeID == 58)
            //{
            //    return RedirectToAction("personalActionList");
            //}
            //else
            //{
            //    return RedirectToAction("ActionList");
            //}
        }
        //public ActionResult aDelete(int id)
        //{
        //    Event product = db.Event.FirstOrDefault(p => p.Event_ID == id);
        //    if (product != null)
        //    {
        //        db.Event.Remove(product);
        //        db.SaveChanges();
        //    }

        //    return RedirectToAction("ActionList");
        //}
        public ActionResult EventReading(int id)
        {
            HttpCookie cookies = Request.Cookies["test"];
            //ViewBag.enumber2 = lm2.getmid(acc);
            //ViewBag.username = lm2.getusername(acc);
            int mb_ID = lm2.getmid(cookies.Value);
            ViewBag.username = lm2.getusername(cookies.Value);
            ViewBag.enumber2 = mb_ID;


            string eventFilePath = System.Web.Configuration.WebConfigurationManager.AppSettings["EventFilePath"];
            string _Event_ID = id.ToString();
            ActionViewModel product = (from p in edb.Event2
                                       where p.Event_ID == id
                                       select new ActionViewModel
                                       {
                                           Event_ID = p.Event_ID,
                                           EventName = p.EventName,
                                           EventContent = p.EventContent,
                                           EventLocation = p.EventLocation,
                                           EventStartDate = p.EventStartDate,
                                           EventEndDate = p.EventEndDate,
                                           EventImage = eventFilePath + _Event_ID + "/" + p.EventImage,
                                           LikeCheck = (from i in edb.EventComment2
                                                        where i.Event_ID == p.Event_ID && i.mb_ID == mb_ID
                                                        select i).Any(),
                                           JoinCheck = (from i in edb.EventBooking2
                                                        where i.Event_ID == p.Event_ID && i.mb_ID == mb_ID
                                                        select i).Any(),
                                           JoinCount = (from i in edb.EventBooking2
                                                        where i.Event_ID == p.Event_ID
                                                        select i).Count(),
                                           RemainCount = p.EventMaxPeople - (from i in edb.EventBooking2
                                                                             where i.Event_ID == p.Event_ID
                                                                             select i).Count()
                                       }).FirstOrDefault();
            if (product == null)
            {
                return RedirectToAction("ActionList");
            }


            return View(product);
        }
        //public ActionResult EventReading(int id)
        //{
        //    ActionViewModel product = (from p in db.Event
        //                               where p.Event_ID == id
        //                               select new ActionViewModel
        //                               {
        //                                   Event_ID = p.Event_ID,
        //                                   EventName = p.EventName,
        //                                   EventLocation = p.EventLocation,
        //                                   EventStartDate = p.EventStartDate,
        //                                   EventEndDate = p.EventEndDate
        //                               }).FirstOrDefault();
        //    if (product == null)
        //    {
        //        return RedirectToAction("ActionList");
        //    }
        //    return View(product);
        //}
        public ActionResult BookingListView()
        {
            HttpCookie cookies = Request.Cookies["test"];
            //ViewBag.username = lm2.getusername(acc);
            //ViewBag.enumber2 = lm2.getmid(acc);
            int mb_ID = lm2.getmid(cookies.Value);
            ViewBag.username = lm2.getusername(cookies.Value);
            ViewBag.enumber2 = mb_ID;

            string eventFilePath = System.Web.Configuration.WebConfigurationManager.AppSettings["EventFilePath"];
            //string _Event_ID = id.ToString();
            List<ActionViewModel> table = (from p in edb.EventBooking2
                                           join c in edb.Event2 on p.Event_ID equals c.Event_ID
                                           where p.mb_ID == mb_ID
                                           select new ActionViewModel
                                           {
                                               Event_ID = p.Event_ID,
                                               BookingDate = p.BookingDate,
                                               EventName = c.EventName,
                                               EventLocation = c.EventLocation,
                                               EventStartDate = c.EventStartDate,
                                               EventEndDate = c.EventEndDate,
                                               EventImage = eventFilePath + c.Event_ID + "/" + c.EventImage
                                           }).ToList();
            ViewBag.CommingEvent = (from p in table
                                    where p.EventStartDate > DateTime.Now
                                    orderby p.EventStartDate ascending
                                    select p).ToList();
            ViewBag.historyEvent = (from p in table
                                    where p.EventStartDate < DateTime.Now
                                    orderby p.EventStartDate descending
                                    select p).ToList();
            if (table == null)
            {
                return RedirectToAction("Action");
            }
            else
            {
                return View(table);
            }
        }
        //public ActionResult BookingListView()
        //{
        //    //List<ActionViewModel> list = Session[CDictionary.SK_Event_Booking_into_list] as List<ActionViewModel>;
        //    //if (list==null)
        //    //    return RedirectToAction("Action");
        //    return View(/*list*/);
        //}
        public ActionResult BookingDelete(int id)
        {
            EventBooking2 product2 = edb.EventBooking2.FirstOrDefault(p => p.Event_ID == id);
            if (product2 != null)
            {
                edb.EventBooking2.Remove(product2);
                edb.SaveChanges();
            }
            //else if (product2.Event_ID < 0)
            //{
            //    return BookingZero();
            //}

            return RedirectToAction("BookingListView");
        }
        public ActionResult EventList()
        {
            var table = from p in edb.Event2
                        select p;
            return View(table);
        }
        //public ActionResult AddtoSession(ActionViewModel booking)
        //{
        //    Event product = db.Event.FirstOrDefault(p => p.Event_ID == booking.Event_ID);
        //    if (product != null)
        //    {
        //        product.EventName = booking.EventName;
        //        product.EventLocation = booking.EventLocation;
        //        product.EventStartDate = booking.EventStartDate;
        //        product.EventEndDate = booking.EventEndDate;
        //        db.SaveChanges();
        //    }
        //    return RedirectToAction("ActionList");
        //}
        // public string JoinEvent(int Event_ID)
        public string JoinEvent(int Event_ID)
        {
            HttpCookie cookies = Request.Cookies["test"];
            int remainCount = (from p in edb.Event2
                               where p.Event_ID == Event_ID
                               select p.EventMaxPeople).FirstOrDefault() -
                                   (from i in edb.EventBooking2
                                    where i.Event_ID == Event_ID
                                    select i).Count();
            if (remainCount == 0)
            {
                return "報名人數已達上限";
            }

            int mb_ID = lm2.getmid(cookies.Value);
            ViewBag.username = lm2.getusername(cookies.Value);
            ViewBag.enumber2 = mb_ID;

            bool hasJoin = (from p in edb.EventBooking2
                                //where p.Event_ID == Event_ID && p.mb_ID == 1
                            where p.Event_ID == Event_ID && p.mb_ID == mb_ID
                            select p).Any();
            if (!hasJoin)
            {
                //EmployeeJoinStatus， 1:參加成功，2:參加失敗(人數過多、過少)，3:退出活動
                EventBooking2 e = new EventBooking2();
                e.Event_ID = Event_ID;
                //e.mb_ID = 1;
                e.mb_ID = mb_ID;
                e.BookingDate = DateTime.Now;
                e.EmployeeJoinStatus = 1;

                edb.EventBooking2.Add(e);
                edb.SaveChanges();
            }
            //else
            //{
            //    return "已報名該活動";
            //}
            return "";
        }
        public string EventComment(int Event_ID)
        {
            HttpCookie cookies = Request.Cookies["test"];
            int mb_ID = lm2.getmid(cookies.Value);
            ViewBag.username = lm2.getusername(cookies.Value);
            ViewBag.enumber2 = mb_ID;
            bool hasComment = (from p in edb.EventComment2
                                   //where p.Event_ID == Event_ID && p.mb_ID == 1
                               where p.Event_ID == Event_ID && p.mb_ID == mb_ID
                               select p).Any();
            if (!hasComment)
            {
                EventComment2 c = new EventComment2();
                c.Event_ID = Event_ID;
                //c.mb_ID = 1;
                c.mb_ID = mb_ID;
                c.CommentStatus = 1;
                edb.EventComment2.Add(c);
                edb.SaveChanges();
            }
            else
            {
                EventComment2 c = (from p in edb.EventComment2
                                       //where p.Event_ID == Event_ID && p.mb_ID == 1
                                   where p.Event_ID == Event_ID && p.mb_ID == mb_ID
                                   select p).FirstOrDefault();
                if (c != null)
                {
                    edb.EventComment2.Remove(c);
                    edb.SaveChanges();
                }
            }
            return "";
        }
        //-----------------------------------------------------------------------------------forum
        public ActionResult ForumList()
        {
           
            if (logincheck == false)
            {
                return RedirectToAction("Start");
            }
            ViewBag.enumber = lm2.getmid(acc);
            ViewBag.LoginName = lm2.getusername(acc);
            return View();
        }
        public string updateForum(string gt,string gc)
        {
            return 
                           
                            "<span>[標題]</span><input type = \"text\" value = "+gt+" class=\"form-control\" id=\"recipient-name\">"+


                            "<span>推文</span><input type = \"text\" value = " + gc + " class=\"form-control\" id=\"recipient-name\">" 
                        ;
        }
        //---------------------------揪團go-----------------------------
        //final_pEntities dbp = new final_pEntities();
        public int opengroupd_productnumber = 300;
        public ActionResult PurchaseList()
        {

            if (logincheck == false)
            {
                return RedirectToAction("Start");
            }

            ViewBag.enumber2 = lm2.getmid(acc);
            ViewBag.LoginName = lm2.getusername(acc);
            
        /*    List<vm_purchaselist> pmlist = new List<vm_purchaselist>();
            PurchaseMethod pm = new PurchaseMethod();

            int[] bb = new int[pm.GetProductList().Length];
            bb = pm.GetProductList();
            foreach (int item in bb)
            {
                pmlist.Add(pm.GetPurchaseModel(item));
            }*/
            return View();
          //  return View(pmlist);
        }
        public ActionResult Purchase_Open()
        {
            ViewBag.enumber5 = lm2.getmid(acc);
            ViewBag.LoginName = lm2.getusername(acc);
            return View();
        }
        string p1name = "";
        string p2name = "";
        [HttpPost]
        public ActionResult Purchase_Open(vmp_openlist vmpol)
        {
            vmpol.group_title = Request.Form["txtptitle"];
            vmpol.group_startdate = DateTime.Now.ToString("yyyyMMdd");
            vmpol.group_enddate = Request.Form["txtpenddate"];

            //vmpol.targetnumber1 = Convert.ToInt32(Request.Form["txttargetnumber1"]);
            //vmpol.targetnumber2 = Convert.ToInt32(Request.Form["txttargetnumber2"]);

            //vmpol.currentnum1 = 0;
            //vmpol.currentnum2 = 0;

            vmpol.group_type = Convert.ToInt32(Request.Form["group_type"]);
            vmpol.group_description = Request.Form["group_description"];
            // vmpol.owndermemberid = Convert.ToInt32(Request.Form["opnepeopleid"]);
            vmpol.owndermemberid = lm2.getmid(acc);

            //-------------------------------嘗試拿到照片本身
            //  string filename = @"C:\Users\ia958\Desktop\iiiTest\專題主程式(20201031)\newBlogprj\newBlogprj\pimg\"+vmpol.XXX.FileName;


            //string getpath = AppDomain.CurrentDomain.BaseDirectory;
            //string filename = getpath + @"\pimg\" + vmpol.XXX.FileName;
            //Bitmap bit = new Bitmap(filename);
            //MemoryStream ms = new MemoryStream();
            //bit.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            //byte[] arr = new byte[ms.Length];
            //ms.Position = 0;
            //ms.Read(arr, 0, (int)ms.Length);
            
          //  Convert.ToBase64String(arr);
            //   string accountbase64Decoded2 = Encoding.UTF8.GetString();
            //string accountbase64Decoded1 = Convert.ToBase64String(arr);

            //  string filename2 = @"C:\Users\ia958\Desktop\iiiTest\專題主程式(20201031)\newBlogprj\newBlogprj\pimg\" + vmpol.XXX2.FileName;
            //string filename2 = getpath + @"\pimg\" + vmpol.XXX2.FileName;
            //Bitmap bit2 = new Bitmap(filename2);
            //MemoryStream ms2 = new MemoryStream();
            //bit2.Save(ms2, System.Drawing.Imaging.ImageFormat.Jpeg);
            //byte[] arr2 = new byte[ms2.Length];
            //ms2.Position = 0;
            //ms2.Read(arr2, 0, (int)ms2.Length);

            //  Convert.ToBase64String(arr);
            //   string accountbase64Decoded2 = Encoding.UTF8.GetString();
            //string accountbase64Decoded2 = Convert.ToBase64String(arr2);
            //----------------------------------------------------------------------------
            pGroupdb gdb = new pGroupdb();
            gdb.Group_Title = vmpol.group_title;
            gdb.Group_StartDate = vmpol.group_startdate;
            gdb.Group_EndDate = vmpol.group_enddate;

            //gdb.Group_TartgetNumber1 = vmpol.targetnumber1;
            //gdb.Group_TartgetNumber2 = vmpol.targetnumber2;



            gdb.Group_type = vmpol.group_type;
            gdb.Group_description = vmpol.group_description;
            gdb.OwnerMember_ID = vmpol.owndermemberid;
            final_pEntities2 dbp = new final_pEntities2();
            dbp.pGroupdb.Add(gdb);
            dbp.SaveChanges();

            //p1name = Request.Form["ponename"];
            //int p1n = Convert.ToInt32(Request.Form["ponenum"]);
            //int p1price = Convert.ToInt32(Request.Form["poneprice"]);
            //vmpol.pic1url = Request.Form["txtpic1"];

            //p2name = Request.Form["ptwoname"];
            //int p2n = Convert.ToInt32(Request.Form["ptwonum"]);
            //int p2price = Convert.ToInt32(Request.Form["ptwoprice"]);
            //vmpol.pic2url = Request.Form["txtpic2"];

            //pProductdb pproduct = new pProductdb();
            //pproduct.Product_name = p1name;
            //pproduct.Product_restnumber = p1n;
            //pproduct.Product_currentnum = vmpol.currentnum1;
            //pproduct.Product_Price = p1price;
            // pproduct.PictureURL = vmpol.pic1url;
            //pproduct.PictureURL = vmpol.XXX.FileName;
            //加入測試照片的地方---------------
            //pproduct.Picturebyte = accountbase64Decoded1.ToString();
            //---------------------------------------------
            //dbp.pProductdb.Add(pproduct);
            //dbp.SaveChanges();

            //pProductdb pproduct2 = new pProductdb();
            //pproduct2.Product_name = p2name;
            //pproduct2.Product_restnumber = p2n;
            //pproduct2.Product_currentnum = vmpol.currentnum1;
            //pproduct2.Product_Price = p2price;
            //pproduct2.PictureURL = vmpol.XXX2.FileName;
            //pproduct2.Picturebyte = accountbase64Decoded2.ToString();

            //dbp.pProductdb.Add(pproduct2);
            //dbp.SaveChanges();
            int b1_gid = 0;
            var q1 = from f in dbp.pGroupdb
                     where f.Group_Title == vmpol.group_title
                     select f.Group_ID;
            foreach (var item in q1)
            {
                b1_gid = item;
            }
            //int b1_pid1 = 0;
            //var q2 = from f2 in dbp.pProductdb
            //         where f2.Product_name == p1name
            //         select f2.Product_ID;
            //foreach (var item in q2)
            //{
            //    b1_pid1 = item;
            //}
            //int b1_pid2 = 0;
            //var q3 = from f3 in dbp.pProductdb
            //         where f3.Product_name == p2name
            //         select f3.Product_ID;
            //foreach (var item in q3)
            //{
            //    b1_pid2 = item;
            //}
            //Group_Product_Binding pgpb = new Group_Product_Binding();
            //pgpb.Groupid = b1_gid;
            //pgpb.Productid = b1_pid1;
            //dbp.Group_Product_Binding.Add(pgpb);
            //dbp.SaveChanges();
            //pgpb.Groupid = b1_gid;
            //pgpb.Productid = b1_pid2;
            //dbp.Group_Product_Binding.Add(pgpb);
            //dbp.SaveChanges();






            return RedirectToAction("Purchase_OpenList");
        }
        public ActionResult Purchase_OpenList()
        {
            final_pEntities2 dbp = new final_pEntities2();
            ViewBag.enumber6 = lm2.getmid(acc);
            ViewBag.username = lm2.getusername(acc);
            int mid = lm2.getmid(acc);

            int p1 = 0;
            int p2 = 0;

            var q = from f in dbp.pGroupdb
                    where f.OwnerMember_ID == mid
                    select f;

            var q2 = from f2 in dbp.Group_Product_Binding
                     where f2.pGroupdb.OwnerMember_ID == mid && f2.pGroupdb.Group_ID == f2.Groupid && f2.Productid == f2.pProductdb.Product_ID
                     select f2.pProductdb;
            string[] pid = new string[opengroupd_productnumber];
            string[] pc = new string[opengroupd_productnumber];
            string[] prn = new string[opengroupd_productnumber];
            int pp = 0;
            foreach (var item in q2)
            {
                pid[pp] = item.Product_ID.ToString();
                pc[pp] = item.Product_currentnum.ToString();
                prn[pp] = item.Product_restnumber.ToString();
                pp++;
            }


            List<vm_openlistview> lpg = new List<vm_openlistview>();

            pp = 0;
            foreach (var item in q)
            {
                vm_openlistview pg = new vm_openlistview();
                pg.title = item.Group_Title;
                pg.startdate = item.Group_StartDate;
                pg.enddate = item.Group_EndDate;
                pg.product1targetnumber = item.Group_TartgetNumber1.ToString();
                pg.product2targetnumber = item.Group_TartgetNumber2.ToString();
                pg.group_type = item.Group_type.ToString();
                pg.group_description = item.Group_description;
                pg.ownerid = item.OwnerMember_ID.ToString();
                //pp = 0;
                pg.product1ID = pid[pp];

                pg.p1currentnumber = Convert.ToInt32(pc[pp]);
                pp++;
                pg.product2ID = pid[pp];
                pg.p2currentnumber = Convert.ToInt32(pc[pp]);
                pp++;
                //加入開團的id方便抓資料
                pg.groupid = item.Group_ID.ToString();

                lpg.Add(pg);
            }
            return View(lpg);
        }
        public ActionResult Purchase_FollowList()
        {
            ViewBag.enumber7 = lm2.getmid(acc);
            ViewBag.username = lm2.getusername(acc);
            final_pEntities2 pdb = new final_pEntities2();
            PurchaseMethod pm = new PurchaseMethod();
            vm_FollowList vmff = new vm_FollowList();
            int user = lm2.getmid(acc);
            var q = from f in pdb.Order
                    where f.followmember_id == user
                    select f.Orderid;
            List<vm_FollowList> flist = new List<vm_FollowList>();
            foreach (var item in q)
            {
                vmff = pm.GetvmfollowModel(item);
                flist.Add(vmff);
            }





            return View(flist);
        }
        [HttpPost]
        public ActionResult order()
        {
            string n1 = Request.Form["q1"];
            string n2 = Request.Form["q2"];
            string gid = Request.Form["gid"];
            PurchaseMethod pm = new PurchaseMethod();
            pm.InsertOrderModel(gid, n1, n2, acc);
            pm.ChangeCurrentnum(gid, n1, n2);
            return RedirectToAction("Purchase_FollowList");
        }

        public ActionResult CheckList(int chkgid)
        {
            
            LoginMethod lm = new LoginMethod();
            ViewBag.enumber6 = lm.getmid(acc);
            ViewBag.loginname = lm.getusername(lm.getuserac());
            final_pEntities2 pdb = new final_pEntities2();
            List<vm_ChekcOrderOwnderList> clist = new List<vm_ChekcOrderOwnderList>();
            var q = from f in pdb.Group_Order_Binding
                    where f.GroupdID == chkgid &&
                    f.Orderid == f.Order.Orderid
                    select f.Order;
            foreach(var item in q)
            {
                vm_ChekcOrderOwnderList vmcoo = new vm_ChekcOrderOwnderList();
                vmcoo.OrderOwnderID = item.followmember_id.ToString();
                vmcoo.OrderOwnderName = lm.getuesrname2(Convert.ToInt32( item.followmember_id)).ToString();
                vmcoo.OrderOwnderDeptname = lm.getuserdept(Convert.ToInt32(item.followmember_id));
                vmcoo.productid = item.productID.ToString();
                vmcoo.pname = lm.getproductname(item.productID.ToString());
                vmcoo.orderquantity = item.quantity.ToString();
                vmcoo.pprice = lm.getproductprice(item.productID.ToString());
                clist.Add(vmcoo);
            }
            return View(clist);
        }
        public ActionResult vote()
        {
            HttpCookie cookies = Request.Cookies["test"];
            lm2.setuserac(cookies.Value);
            ViewBag.LoginName = lm2.getusername(cookies.Value);
            ViewBag.enumber2 = lm2.getmid(cookies.Value);
            return View();
        }
      
    }
    }