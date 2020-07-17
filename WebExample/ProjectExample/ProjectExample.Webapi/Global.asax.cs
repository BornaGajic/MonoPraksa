using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using ProjectExample.Model;
using AutoMapper;
using AutoMapper.Configuration;

namespace ProjectExample.Webapi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        static public MapperConfiguration config = new MapperConfiguration(cfg =>          
        {
            cfg.CreateMap<ProjectExample.Webapi.Controllers.PersonRestModel, Person>();
        });

        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
