﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Cors;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http;

namespace newBlogprj
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
           

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "cBlog", action = "Start", id = UrlParameter.Optional }
            );

            
        }
    }
}
