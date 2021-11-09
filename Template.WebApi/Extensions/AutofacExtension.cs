using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Linq;

namespace Template.WebApi.Extensions
{
	public static partial class ConfigureServices
	{
		public static IContainer AddAutofacDependencyInjection(this IServiceCollection services)
		{
			Logger.Information("Configure Services - Add AutoFac Modules");
			var autofacBuilder = new ContainerBuilder();
			autofacBuilder.Populate(services);
			IContainer autofacContainer = AddAutofacContatinerServices(autofacBuilder);
			return autofacContainer;
		}

        public static IContainer AddAutofacContatinerServices(ContainerBuilder autofacBuilder)
        {
            RegisterModules(autofacBuilder);

            IContainer autofacContainer = autofacBuilder.Build();
            var reg = autofacContainer.ComponentRegistry.Registrations.Select(r => r.GetType().Name);
            return autofacContainer;
        }


        public static void RegisterModules(ContainerBuilder autofacBuilder)
        {

        }
    }
}
