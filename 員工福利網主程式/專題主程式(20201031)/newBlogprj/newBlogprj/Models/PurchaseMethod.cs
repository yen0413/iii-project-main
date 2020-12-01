using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace newBlogprj.Models
{
    public class PurchaseMethod
    {
        public vm_purchaselist GetPurchaseModel(int Group_idn)
        {
            final_pEntities2 pdb = new final_pEntities2();
            vm_purchaselist vmp = new vm_purchaselist();
            //開始拿兩張相片
            var qimg = from f in pdb.Group_Product_Binding
                       where f.pProductdb.Product_ID == f.Productid &&
                                     f.Groupid == f.pGroupdb.Group_ID &&
                                     f.pGroupdb.Group_ID == Group_idn
                       select f.pProductdb.PictureURL;
            string[] pimgurl = new string[2];
            int pimgn = 0;
            foreach(var item in qimg)
            {
                pimgurl[pimgn] = item;
                pimgn++;
            }
            vmp.p1img = pimgurl[0];
            vmp.p2img = pimgurl[1];

            //開始拿開團標題
            var qtitle = from f in pdb.Group_Product_Binding
                         where f.pProductdb.Product_ID == f.Productid &&
                         f.Groupid == f.pGroupdb.Group_ID &&
                         f.pGroupdb.Group_ID == Group_idn
                         select f.pGroupdb.Group_Title;
            string G_Title = "";
            foreach(var item in qtitle)
            {
                G_Title = item;
            }//拿到title

            vmp.c_title = G_Title;

            //開始拿兩個產品的價格
            var qprice = from f in pdb.Group_Product_Binding
                         where f.pProductdb.Product_ID == f.Productid &&
                         f.Groupid == f.pGroupdb.Group_ID &&
                         f.pGroupdb.Group_ID == Group_idn
                         select f.pProductdb.Product_Price;
            string[] Product_price = new string[2];
            int pprice_num = 0;

            foreach(var item in qprice)
            {
                Product_price[pprice_num] = item.ToString();
                pprice_num++;
            }//拿到兩個產品的價格

            vmp.p1price = Product_price[0];
            vmp.p2price = Product_price[1];

            //開始設定兩個產品的目前訂購人數
            int[] Product_current_num = new int[2];
            int product_current_n = 0;
            Product_current_num[0] = 0;
            Product_current_num[1] = 0;
            //兩個產品的目前訂購人數設定完成

            vmp.p1currentnumber = Product_current_num[0].ToString();
            vmp.p2currentnumber = Product_current_num[1].ToString();

            //開始拿兩個產品的達標人數
            var qtargetnum = from f in pdb.Group_Product_Binding
                             where f.pProductdb.Product_ID == f.Productid &&
                         f.Groupid == f.pGroupdb.Group_ID &&
                         f.pGroupdb.Group_ID == Group_idn
                             select new { f.pGroupdb.Group_TartgetNumber1, f.pGroupdb.Group_TartgetNumber2 };

            string[] product_target_num = new string[2];
            
            foreach(var item in qtargetnum)
            {
                product_target_num[0] = item.Group_TartgetNumber1.ToString();
                product_target_num[1] = item.Group_TartgetNumber2.ToString();
            }//拿到兩個產品的目標數值

            vmp.p1targetnumber = product_target_num[0];
            vmp.p2targetnumber = product_target_num[1];

            //拿開團的截止日期

            var qenddate = from f in pdb.Group_Product_Binding
                           where f.pProductdb.Product_ID == f.Productid &&
                        f.Groupid == f.pGroupdb.Group_ID &&
                        f.pGroupdb.Group_ID == Group_idn
                           select f.pGroupdb.Group_EndDate;
            string Groupd_Enddate = "";
            foreach(var item in qenddate)
            {
                Groupd_Enddate = item;
            }//拿到開團的截止日期

            vmp.enddate = Groupd_Enddate;
            //拿兩個產品的名稱
            var qname = from f in pdb.Group_Product_Binding
                        where f.pProductdb.Product_ID == f.Productid &&
                        f.Groupid == f.pGroupdb.Group_ID &&
                        f.pGroupdb.Group_ID == Group_idn
                        select f.pProductdb.Product_name;
            string[] pname = new string[2];
            int pnum = 0;
            foreach(var item in qname)
            {
                pname[pnum] = item;
                pnum++;
            }
            vmp.p1name = pname[0];
            vmp.p2name = pname[1];

            //拿到該團的id
            vmp.gid = Group_idn.ToString();

            return vmp;
        }

        public int[] GetProductList()
        {
            ArrayList a = new ArrayList();
            final_pEntities2 pdb = new final_pEntities2();
            var q = from f in pdb.Group_Product_Binding
                    select f.Groupid;
            int qn = 0;
            foreach(var item in q)
            {
                a.Add(item);
                qn++;
            }
            int an = 0;
            
            int[] aa = new int[qn/2];
            for(int i = 0; i < qn; i++)
            {
                if (i == 0 || i % 2 == 0)
                {
                    aa[an] = Convert.ToInt32( a[i]);
                    an++;
                }
                
            }
            return aa;
        }
        //public vm_FollowList GetFollowModel(string gid, string q1,string q2)
        //{
        //    final_pEntities pdb = new final_pEntities();
        //    vm_FollowList vmf = new vm_FollowList();
            
            
          
            
        //    return vmf;
        //}
        public void InsertOrderModel(string gid,string q1,string q2,string acc)
        {
            final_pEntities2 pdb = new final_pEntities2();
            Order order = new Order();
            int ngid = Convert.ToInt32(gid);
            //拿兩個產品的id
            string[] pid = new string[2];
            int pn = 0;
            var qp1 = from f in pdb.Group_Product_Binding
                      where f.Groupid == ngid &&
                      f.Productid == f.pProductdb.Product_ID
                      select f.pProductdb.Product_ID;
            foreach(var item in qp1)
            {
                pid[pn] = item.ToString();
                pn++;
            }//取得兩個產品的id

            //拿兩個產品的訂購數量
            //也就是n1,n2

            //拿這筆訂單的訂購日期
            //就是今天

            //訂購狀態就是1 = 正常

            //跟團人id
            //就是登入者

            //------開始寫入第一筆order資料
            LoginMethod lm = new LoginMethod();
          
            order.GroupID = Convert.ToInt32(gid);
            order.productID =Convert.ToInt32( pid[0]);
            order.quantity = Convert.ToInt32(q1);
            order.orderdate = DateTime.Now.ToString("yyyyMMdd");
            order.orderstate = 1;
            order.followmember_id =   lm.getmid(acc);
            pdb.Order.Add(order);
            pdb.SaveChanges();
            //--------第一筆資料完成
            //---------寫入第二筆資料
            order.GroupID = Convert.ToInt32(gid);
            order.productID = Convert.ToInt32(pid[1]);
            order.quantity = Convert.ToInt32(q2);
            order.orderdate = DateTime.Now.ToString("yyyyMMdd");
            order.orderstate = 1;
            order.followmember_id = lm.getmid(acc);
            pdb.Order.Add(order);
            pdb.SaveChanges();
            //第二筆資料輸入完成


            //----開始寫入group_order_binding的資料
            Group_Order_Binding gob = new Group_Order_Binding();
            gob.GroupdID = Convert.ToInt32(gid);

            var qg = from f in pdb.Order
                     where f.GroupID == ngid
                     
                     select f.Orderid;
            int qgnum = 0;

            int oid = 0;
            
            foreach(var item in qg)
            {
                oid=Convert.ToInt32( item);
                
            }//取得兩個訂單的id
            
                gob.Orderid =oid-1;
                gob.GroupdID = Convert.ToInt32(gid);
            pdb.Group_Order_Binding.Add(gob);
                pdb.SaveChanges();

            gob.Orderid = oid;
            gob.GroupdID = Convert.ToInt32(gid);
            pdb.Group_Order_Binding.Add(gob);
            pdb.SaveChanges();
            //gob.Orderid = oid[0];
            //pdb.Group_Order_Binding.Add(gob);
            //pdb.SaveChanges();
            //寫玩group_order_binding的第一筆資料

            //開始寫入groupdorderbinding的第二筆資料
            //gob.GroupdID = Convert.ToInt32(gid);
            //gob.Orderid = oid[1];
            //pdb.Group_Order_Binding.Add(gob);
            //pdb.SaveChanges();
        }

        public vm_FollowList GetvmfollowModel (int orderid)
        {
            vm_FollowList vmf = new vm_FollowList();
            final_pEntities2 pdb = new final_pEntities2();
            //拿groupid
            var qgid = from f in pdb.Order
                       where f.Orderid == orderid
                       select f;
            foreach(var item in qgid)
            {
                vmf.Group_ID = item.GroupID.ToString();                
            }
            //拿ownermbid
            var qoid = from f in pdb.Group_Order_Binding
                       where f.Orderid == orderid
                       select f.pGroupdb.OwnerMember_ID;
            foreach(var item in qoid)
            {
                vmf.ownerMBID = item.ToString();
            }
            //拿enddate
            var qed = from f in pdb.Group_Order_Binding
                      where f.Orderid == orderid
                      select f.pGroupdb.Group_EndDate;
            foreach(var item in qed)
            {
                vmf.Enddate = item;
            }
            //拿targetnumber //另外還要做判斷
            var qtn = from f in pdb.Group_Order_Binding
                      where f.Orderid == orderid
                      select f.pGroupdb.Group_TartgetNumber1;
            foreach(var item in qtn)
            {
                vmf.targetnum = item.ToString();
            }
            //拿singleprice
            var qsingleprice1 = from f in pdb.Order
                               where f.Orderid == orderid
                               select f.productID;
            int fpid = 0;
            foreach(var item in qsingleprice1)
            {
                fpid =Convert.ToInt32( item);
            }
            var qsingleprice2 = from f in pdb.pProductdb
                                where f.Product_ID == fpid
                                select f.Product_Price;
            foreach(var item in qsingleprice2)
            {
                vmf.singleprice = item.ToString();
            }

            //拿ordernum
            var qordernum = from f in pdb.Order
                            where f.Orderid == orderid
                            select f.quantity;
            foreach(var item in qordernum)
            {
                vmf.ordernum = item.ToString();
            }
            //拿totalprice
            vmf.totalprice = (Convert.ToInt32(vmf.ordernum) * Convert.ToInt32(vmf.singleprice)).ToString();
            //拿grouptype
            var qgtype1 = from f in pdb.Order
                         where f.Orderid == orderid
                         select f.GroupID;
            int ggid = 0;
            foreach(var item in qgtype1)
            {
                ggid = Convert.ToInt32( item);
            }
            var qgtype2 = from f in pdb.pGroupdb
                          where f.Group_ID == ggid
                          select f.Group_type;
            foreach(var item in qgtype2)
            {
                vmf.grouptype = item.ToString();
            }
            vmf.groupcondition = "1";
            return vmf;
        }

        public void ChangeCurrentnum(string gid,string q1,string q2)
        {
            int ngid = Convert.ToInt32(gid);
            final_pEntities2 pdb = new final_pEntities2();
            var q = from f in pdb.Group_Product_Binding
                    where f.Groupid == ngid
                    select f.Productid;
            int pidnum = 0;
            int[] pid = new int[2];
            foreach(var item in q)
            {
                pid[pidnum] = Convert.ToInt32( item);
                pidnum++;
            }
            int npidnum1 = pid[0];
            int nupidnum2 = pid[1];
            var qc1 = from f in pdb.pProductdb
                      where f.Product_ID == npidnum1 
                      select f;
            foreach(var item in qc1)
            {
                item.Product_currentnum = Convert.ToInt32(q1);
                
            }
            pdb.SaveChanges();
            var qc2 = from f in pdb.pProductdb
                      where f.Product_ID == nupidnum2
                      select f;
            foreach(var item in qc2)
            {
                item.Product_currentnum = Convert.ToInt32(q2);
                
            }
pdb.SaveChanges();
        }


    }
}