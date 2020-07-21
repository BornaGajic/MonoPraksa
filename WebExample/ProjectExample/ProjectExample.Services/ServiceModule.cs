using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace ProjectExample.Service
{
	public class ServiceModule : Module
	{
		protected override void Load (ContainerBuilder builder)
		{
			builder.RegisterType<Service>().As<Common.IService>();
		}
	}
}
