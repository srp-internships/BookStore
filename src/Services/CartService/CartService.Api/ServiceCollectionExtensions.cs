using CartService.Aplication.Interfaces;
using CartService.Aplication.Services;
using CartService.Infrastructure.Repositories;

namespace CartService.Api
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
