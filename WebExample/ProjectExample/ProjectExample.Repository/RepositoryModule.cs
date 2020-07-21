using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace ProjectExample.Repository
{
	public class RepositoryModule : Module
	{
		protected override void Load (ContainerBuilder builder)
		{
			builder.RegisterType<Repository>().As<Common.IRepository>();
		}
	}
}
