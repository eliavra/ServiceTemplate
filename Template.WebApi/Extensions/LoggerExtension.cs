using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Template.Domain.external.Formatter;

namespace Template.WebApi.Extensions
{
    public static partial class ConfigureServices
    {
        private static readonly ILogger Logger = Log.ForContext(typeof(ConfigureServices));

        public static IServiceCollection AddLogger(this IServiceCollection services)
        {
            Logger.Information("Configure Services - Add Logger");

            services.AddScoped<ILogger>(ctx =>
            {
                var conf = new LoggerConfiguration();
                conf.Enrich.FromLogContext();
                conf.WriteTo.Console(new DatadogFormatter());
                return conf.CreateLogger();
            });

            return services;
        }
    }
}
