using CartService.Aplication.Interfaces;
using CartService.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CartService.Infrastructure.Services
{
    public static class ServiceCollectionExtensions
    {
        public static void AddMyServices(this IServiceCollection service)
        {
            service.AddScoped<IBookService, BookService>();
            service.AddScoped<IBookRepository, BookRepository>();
            service.AddScoped<ICartService, CartServices>();
            service.AddScoped<ICartRepository, CartRepository>();
        }
    }
}
