using MassTransit;
using Microsoft.EntityFrameworkCore;
using ReviewService.Infrastructure.Consumers;
using ReviewService.Infrastructure.Persistence.Contexts;
using ReviewService.WebApi.Extensions;
using ReviewService.WebApi.Middlewares;
using Serilog;
using System.Text.Json.Serialization;
namespace ReviewService.WebApi;

public class Program
{
    public static string AppKey => "Test";

    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .CreateLogger();

        builder.Host.UseSerilog();
        builder.Services.AddDbContext<ReviewDbContext>(con => con.UseSqlServer(builder.Configuration["ConnectionString"])
            .LogTo(Console.Write, LogLevel.Error)
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
        #region AddMassTransit
        builder.Services.AddMassTransit(x =>
        {
            x.AddConsumer<BookCreatedConsumer>();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.ConfigureEndpoints(context);
            });
        });
        #endregion

        builder.Services.AddControllers()
            .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddMyServices();

        #region AddCors
        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
        });
        #endregion

        try
        {
            Log.Information("Starting web host");
            var app = builder.Build();

            #region DataConfigurations
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ReviewDbContext>();

                context.Database.Migrate();
            }
            #endregion

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseCors();
            app.UseMiddleware<GlobalExceptionMiddleware>();
            app.UseMiddleware<RateLimitingMiddleware>();
            app.UseMiddleware<ApplicationKeyMiddleware>(AppKey);
            app.UseMiddleware<EndpointListenerMiddleware>();
            app.UseSerilogRequestLogging();
            app.UseRouting();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Host terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
