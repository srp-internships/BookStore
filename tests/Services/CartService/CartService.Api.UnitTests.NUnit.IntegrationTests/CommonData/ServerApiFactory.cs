using CartService.Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;


namespace CartService.Api.UnitTests.NUnit.IntegrationTests.CommonData
{
    public class ServerApiFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Test");

            builder.ConfigureServices(services =>
            {
                // Remove the existing context configuration
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<CartDbContext>));
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // Add an in-memory database for testing
                services.AddDbContext<CartDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryBankTest");
                });
            });
        }
    }
}
