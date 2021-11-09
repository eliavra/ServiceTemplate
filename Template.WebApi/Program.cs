using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Template.WebApi
{
    public class Program
    {

        public static readonly string Namespace = typeof(Program).Namespace;
        public static readonly string AppName = Namespace.Substring(Namespace.LastIndexOf('.', Namespace.LastIndexOf('.') - 1) + 1);

        public async static Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information).Enrich
                .FromLogContext().WriteTo
                .Console()
                .CreateLogger();

            try
            {
                var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                       .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                                       .AddJsonFile("serilogSettings.json", optional: false, reloadOnChange: true)
                                                       .AddEnvironmentVariables()
                                                       .Build();
                var host = CreateHostBuilder(args).Build();
                using (var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                    try
                    {
                        Log.Information("Finished Seeding Default Data");
                        Log.Information("Application Starting");
                    }
                    catch (Exception ex)
                    {
                        Log.Warning(ex, "An error occurred seeding the DB");
                    }
                    finally
                    {
                        Log.CloseAndFlush();
                    }
                }
                host.Run();
            }
            catch (Exception ex)
            {
                if (Log.Logger != null)
                {
                    Log.Fatal(ex, "terminated unexpectedly {AppName}", AppName);
                }
                else
                {
                    Log.Fatal(ex, "Host terminated unexpectedly {AppName}", AppName);
                }
            }
            finally
            {
                Log.CloseAndFlush();

                // For datadog will able to catch up the error
                Thread.Sleep((int)TimeSpan.FromSeconds(10).TotalMilliseconds);
            }



            //Read Configuration from appSettings

        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseSerilog() //Uses Serilog instead of default .NET Logger
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
