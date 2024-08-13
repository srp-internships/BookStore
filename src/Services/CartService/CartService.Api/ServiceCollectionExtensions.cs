using CartService.Aplication.Commons.Interfaces;
using CartService.Aplication.Services;
using CartService.Infrastructure.Repositories;

namespace CartService.Api
{
    public static class ServiceCollectionExtensions
    {
        public static void AddMyServices(this IServiceCollection service)
        {
            service.AddScoped<IUnitOfWork, UnitOfWork>();
            service.AddScoped<ICartService, CartServices>();
            service.AddScoped<ICartRepository, CartRepository>();
            service.AddScoped<IBookRepository, BookRepository>();
            service.AddScoped<IBookSellerRepositoty, BookSellerRepository>();
        }
    }
}
