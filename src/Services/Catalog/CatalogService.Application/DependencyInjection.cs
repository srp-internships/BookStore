using CatalogService.Application.Behaviors;
using CatalogService.Application.Mappers;
using CatalogService.Application.UseCases;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MassTransit;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;

namespace CatalogService.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            // Adding AutoMapper
            services.AddAutoMapper(config => config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly())));

            // Adding FluentValidation and validators from all loaded assemblies in the current AppDomain
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                try
                {
                    services.AddValidatorsFromAssembly(assembly);
                }
                catch { } // Skip if any exception
            }

            // Adding Mediator
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateAuthorCommand).Assembly));
            services.AddTransient(typeof(IPipelineBehavior<,>),
                typeof(ValidationBehavior<,>));


            services.AddMassTransit(x =>
            {
                //x.UsingInMemory();
                // Configure RabbitMQ
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(configuration.GetConnectionString("RabbitMq"));
                });
            });
            services.AddMassTransitHostedService();

            return services;
        }
    }
}
