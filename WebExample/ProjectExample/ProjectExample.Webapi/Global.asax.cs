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
using ProjectExample;
using ProjectExample.Service;
using ProjectExample.Repository;
using ProjectExample.Utility;

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
            const string connectionString = "Data Source=BORNA-PC\\SQLEXPRESS;" +
                                            "Initial Catalog=MonoPraksa;" +
                                            "Integrated Security=True;";

            GlobalConfiguration.Configure(WebApiConfig.Register);

		    var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;
               
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterModule(new ServiceModule());
            builder.RegisterModule(new RepositoryModule());
            builder.RegisterModule(new CrudWrapperModule{ConnectionString = connectionString});

            Container = builder.Build();  

            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(Container);
        }
    }
}
