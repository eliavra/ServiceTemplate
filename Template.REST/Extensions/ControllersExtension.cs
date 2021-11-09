using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.REST.Extensions
{
    public static class ControllersExtension
    {
        public static void AddRestControllers(this IServiceCollection services)
        {
            services.AddControllers();
        }
    }
}
