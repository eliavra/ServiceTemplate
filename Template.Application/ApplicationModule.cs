using AutoMapper;
using FluentValidation;
using MediatR;
using Template.Application.Behaviours;
using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Template.Application.Mappings;
using MediatR.Extensions.Autofac.DependencyInjection;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Template.Application
{
    public  class ApplicationModule : Autofac.Module
    {


        protected override void Load(ContainerBuilder builder)
        {
            // Register all the Command classes (they implement IRequestHandler) in assembly holding the Commands
            builder.RegisterMediatR(typeof(ApplicationModule).Assembly);

            // Register all the Command classes (they implement IRequestHandler) in assembly holding the Commands
            builder.RegisterAssemblyTypes(typeof(ApplicationModule).Assembly)
                    .AsClosedTypesOf(typeof(IRequestHandler<,>));

            builder.RegisterAssemblyTypes(typeof(ValidationBehavior<,>).Assembly)
            .AsClosedTypesOf(typeof(IPipelineBehavior<,>));

            RegisterAutoMapper(builder);
        }

        private void RegisterAutoMapper(ContainerBuilder builder)
        {
            var profile = new MessagesMappingProfile();

            builder.Register(ctx => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(profile);
            }));

            builder.Register(ctx => ctx.Resolve<MapperConfiguration>().CreateMapper()).As<IMapper>().InstancePerLifetimeScope();
        }
    }
}
