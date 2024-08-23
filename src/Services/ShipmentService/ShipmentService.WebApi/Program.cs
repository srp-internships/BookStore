using FluentValidation;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ShipmentService.Aplication.Common.Mappings;
using ShipmentService.Aplication.CQRS.Shipments.Commands.Update;
using ShipmentService.Aplication.CQRS.Shipments.Queries.GetAll;
using ShipmentService.Aplication.CQRS.Shipments.Queries.GetById;
using ShipmentService.Aplication.Interfaces;
using ShipmentService.Infrastructure.Consumers;
using ShipmentService.Infrastructure.Persistence.DbContexts;
using ShipmentService.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ShipmentContext>(con => con.UseSqlServer(builder.Configuration["ConnectionString"])
                                  .LogTo(Console.Write, LogLevel.Error)
                                  .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
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
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IShipmentRepository, ShipmentRepository>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddAutoMapper(typeof(ShipmentMappings).Assembly);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllShipmentsQueryHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(UpdateShipmentCommandHandler).Assembly));
builder.Services.AddTransient<IValidator<UpdateShipmentCommand>, UpdateShipmentCommandValidator>();
builder.Services.AddTransient<IValidator<GetShipmentByIdQuery>, GetShipmentByIdQueryValidator>();

builder.Services.AddMassTransit(x =>
{
    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter(prefix: "shipment", includeNamespace: false));
    x.AddConsumer<OrderCreatedConsumer>();
    x.AddConsumer<OrderStatusUpdatedConsumer>();
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

builder.Services.AddMassTransitHostedService();

var app = builder.Build();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetService<ShipmentContext>();

    context.Database.Migrate();
}

app.UseSwagger();
app.UseSwaggerUI(opt =>
{// TODO 4
    opt.OAuthClientId("swagger-client-3F9610DD-0032-41FA-92F5-397E6B66AE15");
    opt.OAuthAppName("Swagger UI");
    opt.OAuthClientSecret("swagger-ui-DF669678-66B8-4982-890A-E52F7632A3BA");
    opt.OAuthUsePkce();
});

app.UseCors("AllowAll");
app.UseAuthorization();
app.UseRouting();
app.MapControllers();

app.Run();