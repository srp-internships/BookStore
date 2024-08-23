using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
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
            x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter(prefix: "review", includeNamespace: false));
            x.AddConsumer<BookCreatedConsumer>();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(new Uri(builder.Configuration["RabbitMq:Host"]), h =>
                {
                    h.Username(builder.Configuration["RabbitMq:Username"]);
                    h.Password(builder.Configuration["RabbitMq:Password"]);
                });
                cfg.ConfigureEndpoints(context);
            });
        });
        #endregion

        builder.Services.AddControllers()
            .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {// TODO 1
            string baseUrl = "https://localhost:7167";
            options.AddSecurityDefinition($"AuthToken",
            new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    AuthorizationCode = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri($"{baseUrl}/connect/authorize"),
                        TokenUrl = new Uri($"{baseUrl}/connect/token"),
                        RefreshUrl = new Uri($"{baseUrl}/connect/token"),
                        Scopes = new Dictionary<string, string>
                        {
                                { "openid", "OpenID" },
                                { "profile", "Profile" },
                                { "common_scope", "Web Api" },
                        }
                    }
                }
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                        {
                        new OpenApiSecurityScheme
                        {
                          Reference = new OpenApiReference
                          {
                          Type = ReferenceType.SecurityScheme,
                          Id = $"AuthToken"
                          },
                          Scheme = "oauth2",
                          Name = "Bearer",
                          In = ParameterLocation.Header
                        },
                        new List<string>()
                        }
                });
        });

        builder.Services.AddAuthentication(options =>
        {// TODO 2
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(options =>
            {
                options.Authority = "https://localhost:7167";
                options.Audience = "book_program";
                options.RequireHttpsMetadata = false;
                // TODO setup to use oauth2.0 + Identity server after it has done
            });
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

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            #region DataConfigurations
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ReviewDbContext>();

                context.Database.Migrate();
            }
            #endregion

            app.UseSwagger();

            app.UseSwaggerUI(opt =>
            {// TODO 4
                opt.OAuthClientId("swagger-client-3F9610DD-0032-41FA-92F5-397E6B66AE15");
                opt.OAuthAppName("Swagger UI");
                opt.OAuthClientSecret("swagger-ui-DF669678-66B8-4982-890A-E52F7632A3BA");
                opt.OAuthUsePkce();
            });

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
