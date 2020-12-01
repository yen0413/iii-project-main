﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;


namespace forumAPItest


{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //config.EnableCors();
            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));

            
            // Web API 設定和服務

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
