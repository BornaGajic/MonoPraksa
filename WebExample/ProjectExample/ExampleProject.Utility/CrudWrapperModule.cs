using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Integration.Web;
using Autofac.Core.Lifetime;
using ProjectExample.Utility.Common;

namespace ProjectExample.Utility
{
	public class CrudWrapperModule : Module
	{
		public string ConnectionString { get; set; }

		protected override void Load (ContainerBuilder builder)
		{
			builder.Register(c => new CrudWrapper(ConnectionString)).As<ICrudWrapper>().InstancePerMatchingLifetimeScope(LifetimeScope.RootTag);
		}
	}
}
