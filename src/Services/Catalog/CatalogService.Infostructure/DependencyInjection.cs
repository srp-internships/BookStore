using CatalogService.Domain.Interfaces;
using CatalogService.Infostructure.Repositories;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Infostructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            // Adding DataBase
            services.AddDbContext<CatalogDbContext>((serviceProvider, options) =>
            {
                string path = Directory.GetCurrentDirectory();
                
            });

            // Adding Repository and Unit of work with Repository patterns implementations
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IPublisherRepository, PublisherRepository>();
            services.AddScoped<IBookSellerRepository, BookSellerRepository>();

            services.AddMassTransit(x =>
            {
                // Configure RabbitMQ
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("rabbitmq://localhost");
                });
            });/*
            services.AddMassTransit(busConfigurator =>
            {
                busConfigurator.SetKebabCaseEndpointNameFormatter();
                busConfigurator.UsingRabbitMq((context, configurator) =>
                {
                    var host = "http://localhost:15672";
                    var username = "guest";
                    var password = "guest";

                    if (string.IsNullOrEmpty(host))
                    {
                        throw new ArgumentNullException(nameof(host), "RabbitMQ host cannot be null or empty.");
                    }

                    configurator.Host(new Uri(host), h =>
                    {
                        h.Username(username);
                        h.Password(password);
                    });

                    configurator.ConfigureEndpoints(context);
                });
            });*/

            return services;
        }
    }
}
