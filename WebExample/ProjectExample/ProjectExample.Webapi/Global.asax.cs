using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using System.Reflection;
using ProjectExample.Model;
using AutoMapper;
using AutoMapper.Configuration;
using Autofac.Integration.WebApi;
using Autofac.Builder;
using Autofac;


namespace ProjectExample.Webapi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private static IContainer Container { get; set; }

        public static MapperConfiguration config = new MapperConfiguration(cfg =>          
        {
            cfg.CreateMap<Controllers.PersonRestModel, Person>();
        });

        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

		    var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;
               
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<Controllers.ExampleController>();

            builder.RegisterType<Service.Service>().As<Service.Common.IService>();
            builder.RegisterType<Repository.Repository>().As<Repository.Common.IRepository>();
            builder.RegisterType<Utility.CrudWrapper>().As<Utility.Common.ICrudWrapper>();
            
            Container = builder.Build();  

            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(Container);
        }
    }
}
