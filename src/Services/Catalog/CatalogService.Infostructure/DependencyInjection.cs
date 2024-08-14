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

            


            services.AddDbContext<CatalogDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DbConnection"));
                options.LogTo(Console.WriteLine);
            });

            services.AddSingleton<IBlobService, BlobService>();
            services.AddSingleton(_ => new BlobServiceClient(configuration.GetConnectionString("BlobStorage")));

            return services;
        }
    }
}
