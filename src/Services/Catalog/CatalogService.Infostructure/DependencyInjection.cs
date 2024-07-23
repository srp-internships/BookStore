using CatalogService.Domain.Interfaces;
using CatalogService.Infostructure.Repositories;
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

            return services;
        }
    }
}
