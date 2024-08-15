using FluentValidation;
using MassTransit;
using Microsoft.EntityFrameworkCore;
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
builder.Services.AddSwaggerGen();
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
    x.AddConsumer<OrderCreatedConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddMassTransitHostedService();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetService<ShipmentContext>();

    context.Database.Migrate();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowAll");
app.UseAuthorization();
app.UseRouting();
app.MapControllers();

app.Run();