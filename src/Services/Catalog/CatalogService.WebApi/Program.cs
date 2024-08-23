using CatalogService.Application;
using CatalogService.Application.Mappers;
using CatalogService.Infostructure;
using CatalogService.WebApi.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

builder.Services.AddAutoMapper(config => config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly())));
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI(opt =>
{// TODO 4
    opt.OAuthClientId("swagger-client-3F9610DD-0032-41FA-92F5-397E6B66AE15");
    opt.OAuthAppName("Swagger UI");
    opt.OAuthClientSecret("swagger-ui-DF669678-66B8-4982-890A-E52F7632A3BA");
    opt.OAuthUsePkce();
});
app.UseHttpsRedirection();
app.UseMiddleware<CustomExceptionHandlerMiddleware>();

app.MapControllers();

var applicationDbContext = app.Services.CreateScope().ServiceProvider.GetRequiredService<CatalogDbContext>();
await applicationDbContext.Database.MigrateAsync();

app.Run();
