using CartService.Aplication.Interfaces;
using CartService.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartService.Infrastructure.Services
{
    public static class ServiceCollectionExtensions
    {
        public static void AddMyServices(this IServiceCollection service)
        {
            service.AddScoped<IBookService, BookService>();
            service.AddScoped<IBookRepository, BookRepository>();
        }
    }
}
