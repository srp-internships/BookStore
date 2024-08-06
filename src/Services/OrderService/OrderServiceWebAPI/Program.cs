using OrderService.Application.Mappers;
using OrderService.Infrastructure.Consumers;
using ExchangeType = RabbitMQ.Client.ExchangeType;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddDistributedMemoryCache();
builder.Services.AddStackExchangeRedisCache(redisOptions =>
{
    string connection = builder.Configuration
    .GetConnectionString("Redis");
    redisOptions.Configuration = connection;
});

// Add MassTransit
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<ShipmentStatusUpdatedConsumer>()
        .Endpoint(e => e.Name = builder.Configuration["EventBus:Queues:ShipmentStatusUpdateQueue"]!);
    x.AddConsumer<PaymentStatusUpdatedConsumer>()
        .Endpoint(e => e.Name = builder.Configuration["EventBus:Queues:PaymentStatusUpdateQueue"]!);
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(new Uri(builder.Configuration["RabbitMq:Host"]!), h =>
        {
            h.Username(builder.Configuration["RabbitMq:Username"]!);
            h.Password(builder.Configuration["RabbitMq:Password"]!);
        });

        cfg.ExchangeType = ExchangeType.Fanout;
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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.Run();