using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using OrderService.Application.Mappers;
using OrderService.Infrastructure.Consumers;
using OrderService.Infrastructure.Persistence.DataBases;

var builder = WebApplication.CreateBuilder(args);

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
{
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
builder.Services.AddAuthorization();


// Add distributed Redis Cache
builder.Services.AddStackExchangeRedisCache(redisOptions =>
{
    string connection = builder.Configuration
    .GetConnectionString("Redis");
    redisOptions.Configuration = connection;
});

// Add MassTransit 
builder.Services.AddMassTransit(x =>
{
    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter(prefix: "order", includeNamespace: false));
    x.AddConsumer<PaymentStatusUpdatedConsumer>();
    x.AddConsumer<ShipmentStatusUpdatedConsumer>();

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

builder.Services.AddHostedService<MassTransitHostedService>();

builder.Services
    .AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices(builder.Configuration);

builder.Services.AddAutoMapper(typeof(AutoMapperConfiguration).Assembly);

var app = builder.Build();

// TODO 3
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

//// Configure the HTTP request pipeline.
app.UseApiServices();

app.UseSwagger();
app.UseSwaggerUI(opt =>
{// TODO 4
    opt.OAuthClientId("swagger-client-3F9610DD-0032-41FA-92F5-397E6B66AE15");
    opt.OAuthAppName("Swagger UI");
    opt.OAuthClientSecret("swagger-ui-DF669678-66B8-4982-890A-E52F7632A3BA");
    opt.OAuthUsePkce();
});

app.UseHttpsRedirection();

var applicationDbContext = app.Services.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();
await applicationDbContext.Database.MigrateAsync();

app.Run();