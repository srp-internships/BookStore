using CartService.Api;
using CartService.Api.Middlewares;
using CartService.Aplication.Commons.Interfaces;
using CartService.Consumers.Books;
using CartService.Consumers.BookSellers;
using CartService.Infrastructure.Persistence.Contexts;
using CartService.Infrastructure.Repositories;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<CartDbContext>(con => con.UseSqlServer(builder.Configuration["ConnectionString"])
                      .LogTo(Console.Write, LogLevel.Information)
          .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
builder.Services.AddLogging();


#region MassTransit
builder.Services.AddMassTransit(x =>
{
    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter(prefix: "cart", includeNamespace: false));

    x.AddConsumer<BookCreatedConsumer>();
    x.AddConsumer<PriceCreatedConsumer>();
    x.AddConsumer<BookUpdatedConsumer>();
    x.AddConsumer<PriceUpdatedConsumer>();

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
            });

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddMyServices();
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

var app = builder.Build();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetService<CartDbContext>();
#if DEBUG
    if (builder.Environment.IsEnvironment("Test"))
    {
        context.Database.EnsureCreated();
    }
    else
    {
#endif
        context.Database.Migrate();
#if DEBUG
    }
#endif
}

app.UseSwagger();
app.UseSwaggerUI(opt =>
{// TODO 4
    opt.OAuthClientId("swagger-client-3F9610DD-0032-41FA-92F5-397E6B66AE15");
    opt.OAuthAppName("Swagger UI");
    opt.OAuthClientSecret("swagger-ui-DF669678-66B8-4982-890A-E52F7632A3BA");
    opt.OAuthUsePkce();
});

app.UseCors();
app.UseHttpsRedirection();
app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseMiddleware<ApplicationKeyMiddleware>();
app.UseMiddleware<EndpointListenerMiddleware>();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseAuthorization();

app.MapControllers();

var applicationDbContext = app.Services.CreateScope().ServiceProvider.GetRequiredService<CartDbContext>();
await applicationDbContext.Database.MigrateAsync();

app.Run();
