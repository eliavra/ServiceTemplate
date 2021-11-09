using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Template.Application;

namespace Template.WebApi.Extensions
{
    public static class FluentValidatorExtension
    {
        public static IServiceCollection AddFluentValidator(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining(typeof(ApplicationModule));
            return services;
        }
    }
}
