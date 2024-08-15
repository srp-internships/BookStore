using Azure.Storage.Blobs;
using CatalogService.Application.BlobServices;
using CatalogService.Application.Interfaces.BlobServices;
using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Application.Interfaces.UnitOfWork;
using CatalogService.Infostructure.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
                    cfg.Host(configuration.GetValue<string>("RabbitMq:Host"), h =>
                    {
                        h.Username(configuration.GetValue<string>("RabbitMq:Username"));
                        h.Password(configuration.GetValue<string>("RabbitMq:Password"));
                    });
                });
            });
            services.AddMassTransitHostedService();


            services.AddDbContext<CatalogDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DbConnection"));
                options.LogTo(Console.WriteLine);
            });

            services.AddSingleton<IBlobService, BlobService>();
            services.AddSingleton(_ => new BlobServiceClient(configuration.GetValue<string>("BlobStorage")));

            return services;
        }
    }
}
