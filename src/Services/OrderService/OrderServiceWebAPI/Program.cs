using OrderService.Application.Mappers;
using OrderService.Infrastructure.Consumers;
using OrderService.Infrastructure.Persistence.DataBases;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

//// Configure the HTTP request pipeline.
app.UseApiServices();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

var applicationDbContext = app.Services.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();
await applicationDbContext.Database.MigrateAsync();

app.Run();