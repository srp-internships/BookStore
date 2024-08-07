using CatalogService.Domain.Interfaces;
using CatalogService.Infostructure.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {


            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IPublisherRepository, PublisherRepository>();
            services.AddScoped<IBookSellerRepository, BookSellerRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddMassTransit(x =>
            {
                //x.UsingInMemory();
                // Configure RabbitMQ
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("rabbitmq://localhost");
                });
            });
            services.AddMassTransitHostedService();


            services.AddDbContext<CatalogDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DbConnection"));
                options.LogTo(Console.WriteLine);
            });

            return services;
        }
    }
}
