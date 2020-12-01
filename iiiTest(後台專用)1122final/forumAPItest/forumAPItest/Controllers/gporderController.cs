using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace forumAPItest.Controllers
{
    public class gporderController : ApiController
    {
        // GET: api/gporder
        final_pEntities db = new final_pEntities();
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/gporder/5
        public JObject Get(int id)
        {
            var result = new
            {
                gotable = from n in db.Group_Order_Binding
                          where n.GroupdID == id
                          select new
                          {
                              orderid = n.Order.Orderid,
                              orderproductid = n.Order.productID,
                              ordergroupid = n.Order.GroupID,
                              orderquantity = n.Order.quantity,
                              orderdate = n.Order.orderdate,
                              orderstate = n.Order.orderstate,
                              ordermemberid = n.Order.followmember_id

                          }
            };
            string strJson = JsonConvert.SerializeObject(result, Formatting.Indented);
            JObject o = JObject.Parse(strJson);
            return o;
        }

        // POST: api/gporder
        [HttpPost]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage Post([FromBody] JObject value)
        {
            int orderpid = 0;
           int qnum = 0;
            Order order = new Order();
            Group_Order_Binding group_Order_Binding = new Group_Order_Binding();
            if (order != null)
            {
                group_Order_Binding.GroupdID = Convert.ToInt32(value["newordergroupid"].ToString());
                group_Order_Binding.Order = order;
                order.GroupID = group_Order_Binding.GroupdID;
                order.productID = Convert.ToInt32(value["neworderproductid"].ToString());

                orderpid =Convert.ToInt32( order.productID);

                order.quantity = Convert.ToInt32(value["neworderquantity"].ToString());

                qnum = Convert.ToInt32(order.quantity);

                order.orderdate = value["neworderdate"].ToString();
                order.orderstate = Convert.ToInt32(value["neworderstate"].ToString());
                order.followmember_id = Convert.ToInt32(value["newordermemberid"].ToString());
                db.Group_Order_Binding.Add(group_Order_Binding);
                db.Order.Add(order);
                db.SaveChanges();
            }
            //-----------------------------------------更改currrentnum
            pProductdb ppdb = db.pProductdb.FirstOrDefault(p => p.Product_ID == orderpid);
            ppdb.Product_currentnum += qnum;
            db.SaveChanges();
            //-----------------------------------
            
            //var q = from f in db.pProductdb
            //        where f.Product_ID == orderpid
            //        select f;
            //foreach(var item in q)
            //{
            //    item.Product_currentnum += qnum;
            //    db.SaveChanges();
            //}
            //--------------------------------------------
            var result = new
            {
                STATUS = true,
                MSG = "成功",
            };

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // PUT: api/gporder/5
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage Put(int id, [FromBody] JObject value)
        {
            Order order = db.Order.FirstOrDefault(p => p.Orderid == id);
            if (order != null)
            {
                order.quantity = Convert.ToInt32(value["orderquantity"].ToString());
                order.orderdate = value["orderdate"].ToString();
                order.orderstate = Convert.ToInt32(value["orderstate"].ToString());
                db.SaveChanges();
            }
            var result = new
            {
                STATUS = true,
                MSG = "成功",
            };

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // DELETE: api/gporder/5
        [HttpDelete]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage Delete(int id)
        {
            Order order = db.Order.FirstOrDefault(p => p.Orderid == id);
            Group_Order_Binding group_Order_Binding = db.Group_Order_Binding.FirstOrDefault(p => p.Orderid == id);
            if (order != null)
            {
                db.Order.Remove(order);
                db.Group_Order_Binding.Remove(group_Order_Binding);
                db.SaveChanges();
            }
            var result = new
            {
                STATUS = true,
                MSG = "刪除成功",
            };

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
