using ReviewService.Application.Services;
using ReviewService.Domain.Repositories;
using ReviewService.Infrastructure.Repositories;
using ReviewService.Infrastructure.Services;

namespace ReviewService.WebApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddMyServices(this IServiceCollection service)
        {
            service.AddScoped<IReviewService, ReviewServices>();
            service.AddScoped<IReviewRepository, ReviewRepository>();
            service.AddScoped<IBookService, BookService>();
            service.AddScoped<IBookRepository, BookRepository>();
            service.AddScoped<IUnitOfWork, UnitOfWork>();  

        }
    }
}
