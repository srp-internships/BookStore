using System.Numerics;
using System.Reflection;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShipmentService.Aplication.Common.Mappings;
using ShipmentService.Aplication.CQRS.Shipments.Commands.Create;
using ShipmentService.Aplication.Interfaces;
using ShipmentService.Infrastructure.Persistence.DbContexts;
using ShipmentService.Infrastructure.Repositories;
using ShipmentService.Infrastructure.Services;
using ShipmentService.IntegrationEvent;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ShipmentContext>(con => con.UseSqlServer(builder.Configuration["ConnectionString"])
                                  .LogTo(Console.Write, LogLevel.Error)
                                  .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IShipmentRepository, ShipmentRepository>();
builder.Services.AddScoped<IShipmentService, ShipmentServices>();
builder.Services.AddAutoMapper(typeof(ShipmentMappings));
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateShipmentCommand).Assembly));

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<ShipmentRequestConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq://localhost");

        cfg.ReceiveEndpoint("shipment_queue", e =>
        {
            e.ConfigureConsumer<ShipmentRequestConsumer>(context);
        });
    });
});

builder.Services.AddMassTransitHostedService();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetService<ShipmentContext>();
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
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();