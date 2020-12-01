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
    public class gpproductController : ApiController
    {
        // GET: api/gpproduct
        final_pEntities db = new final_pEntities();
        public JObject Get()
        {
            var result = new
            {
                gptable = from n in db.Group_Product_Binding.AsEnumerable()
                          orderby n.pGroupdb.Group_StartDate 
                          select new
                          {
                              groupid = n.Groupid,
                              productid = n.pProductdb.Product_ID,
                              productname = n.pProductdb.Product_name,
                              productprice = n.pProductdb.Product_Price,
                              productcurrentnum = n.pProductdb.Product_currentnum,
                              productrestnumber = n.pProductdb.Product_restnumber,
                              productdescription = n.pProductdb.Product_description,
                              productpictureurl = n.pProductdb.Picturebyte,
                              countdown = Math.Floor(((DateTime.Parse(n.pGroupdb.Group_EndDate) - DateTime.Now).TotalDays))
                          }

            };
            string strJson = JsonConvert.SerializeObject(result, Formatting.Indented);
            JObject o = JObject.Parse(strJson);
            return o;
        }

        // GET: api/gpproduct/5
        public JObject Get(int id)
        {
            var result = new
            {
                gptable = from n in db.Group_Product_Binding.AsEnumerable()
                          where n.Groupid == id
                          select new
                          {
                              groupid = n.Groupid,
                              productid = n.pProductdb.Product_ID,
                              productname = n.pProductdb.Product_name,
                              productprice = n.pProductdb.Product_Price,
                              productcurrentnum = n.pProductdb.Product_currentnum,
                              productrestnumber = n.pProductdb.Product_restnumber,
                              productdescription = n.pProductdb.Product_description,
                              productpictureurl = n.pProductdb.Picturebyte,
                              countdown = Math.Floor(((DateTime.Parse(n.pGroupdb.Group_EndDate) - DateTime.Now).TotalDays))
                          }

            };
            string strJson = JsonConvert.SerializeObject(result, Formatting.Indented);
            JObject o = JObject.Parse(strJson);
            return o;
        }
        [HttpPost]
        [EnableCors("*", "*", "*")]
        // POST: api/gpproduct
        public HttpResponseMessage Post([FromBody] JObject value)
        {
            string controllerName = ControllerContext.RouteData.Values["controller"].ToString();
            Group_Product_Binding group_Product_Binding = new Group_Product_Binding();
            pProductdb pProductdb = new pProductdb();
            var newproductname = value["newproductname"].ToString();
            var newproductprice = value["newproductprice"].ToString();
            var newproductcurrentnum = value["newproductcurrentnum"].ToString();
            var newproductrestnumber = value["newproductrestnumber"].ToString();
            var newproductdescription = value["newproductdescription"].ToString();
            var newgroupid = value["newgroupid"].ToString();


            pProductdb.Product_name = newproductname;
            pProductdb.Product_Price = Convert.ToInt32(newproductprice);
            pProductdb.Product_currentnum = Convert.ToInt32(newproductcurrentnum);
            pProductdb.Product_restnumber = Convert.ToInt32(newproductrestnumber);
            pProductdb.Product_description = newproductdescription;
            group_Product_Binding.Groupid = Convert.ToInt32(newgroupid);
            group_Product_Binding.Productid = pProductdb.Product_ID;
            group_Product_Binding.pProductdb = pProductdb;
            db.Group_Product_Binding.Add(group_Product_Binding);
         //   pProductdb.PictureURL = value["newproductpictureurl"].ToString();
            pProductdb.Picturebyte = value["newproductpictureurl"].ToString();

            db.pProductdb.Add(group_Product_Binding.pProductdb);
            db.SaveChanges();


            var result = new
            {
                STATUS = true,
                MSG = "成功",
            };

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // PUT: api/gpproduct/5
        [HttpPut]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage Put(int id, [FromBody] JObject value)
        {
            pProductdb pProductdb = db.pProductdb.FirstOrDefault(p => p.Product_ID == id);
            if (pProductdb != null)
            {
                pProductdb.Product_name = value["productname"].ToString();
                pProductdb.Product_Price = Convert.ToInt32(value["productprice"].ToString());
                pProductdb.Product_restnumber = Convert.ToInt32(value["productcurrentnum"].ToString());
                pProductdb.Product_currentnum = Convert.ToInt32(value["productrestnumber"].ToString());
                pProductdb.Product_description = value["productdescription"].ToString();
                //pProductdb.PictureURL = value["productpictureurl"].ToString();
                pProductdb.Picturebyte = value["productpictureurl"].ToString();
                db.SaveChanges();
            }
            var result = new
            {
                STATUS = true,
                MSG = "成功",
            };

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // DELETE: api/gpproduct/5
        [HttpDelete]
        [EnableCors("*", "*", "*")]
        public HttpResponseMessage Delete(int id)
        {
            pProductdb pProductdb = db.pProductdb.FirstOrDefault(p => p.Product_ID == id);
            Group_Product_Binding group_Product_Binding = db.Group_Product_Binding.FirstOrDefault(p => p.Productid == id);
            if (pProductdb != null)
            {
                db.Group_Product_Binding.Remove(group_Product_Binding);
                db.SaveChanges();
                db.pProductdb.Remove(pProductdb);
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
