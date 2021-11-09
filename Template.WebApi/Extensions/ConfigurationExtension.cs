using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Template.WebApi.Extensions
{
    public static partial class ConfigureServices
    {
        public static IServiceCollection AddAppConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            //Add app settings configuration here
            return services;
        }
    }
}
