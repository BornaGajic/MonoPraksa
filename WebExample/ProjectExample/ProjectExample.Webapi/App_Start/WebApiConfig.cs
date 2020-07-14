﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ProjectExample.Webapi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();
                
            config.Routes.MapHttpRoute
            (
                name: "name",
                routeTemplate: "api/name/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}