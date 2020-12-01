using Jose;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace forumAPItest.Models
{
    public class JwtAuthPackageFilte : ActionFilterAttribute
    {

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            // TODO: key應該移至config
            var secret = "wellwindJtwDemo";

            if (actionContext.Request.Headers.Authorization == null || actionContext.Request.Headers.Authorization.Scheme != "Bearer")
            {
                setErrorResponse(actionContext, "驗證錯誤");
            }
            else
            {
                try
                {
                    var jwtObject = Jose.JWT.Decode<JwtAuthPackgeObject>(
                        actionContext.Request.Headers.Authorization.Parameter,
                        Encoding.UTF8.GetBytes(secret),
                        JwsAlgorithm.HS256);
                }
                catch (Exception ex)
                {
                    setErrorResponse(actionContext, ex.Message);
                }
            }

            base.OnActionExecuting(actionContext);
        }

        private static void setErrorResponse(HttpActionContext actionContext, string message)
        {
            var response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, message);
            actionContext.Response = response;
        }
    }
}