using CartService.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using MassTransit;
using System.Text.Json.Serialization;
using CartService.Consumers.Books;
using CartService.Consumers.BookSellers;
using CartService.Api.Middlewares;
using CartService.Api;
using CartService.Aplication.Commons.Interfaces;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<CartDbContext>(con => con.UseSqlServer(builder.Configuration["ConnectionString"])
                      .LogTo(Console.Write, LogLevel.Error)
          .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
builder.Services.AddLogging();


#region MassTransit
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<BookCreatedConsumer>();
    x.AddConsumer<PriceCreatedConsumer>();
    x.AddConsumer<BookUpdatedConsumer>();
    x.AddConsumer<PriceUpdatedConsumer>(); 

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq://localhost", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint("book-created-event-queue", e =>
        {
            e.ConfigureConsumer<BookCreatedConsumer>(context);
        });

        cfg.ReceiveEndpoint("price-created-event-queue", e =>
        {
            e.ConfigureConsumer<PriceCreatedConsumer>(context);
        });

        cfg.ReceiveEndpoint("book-updated-event-queue", e =>
        {
            e.ConfigureConsumer<BookUpdatedConsumer>(context);
        });

        cfg.ReceiveEndpoint("price-updated-event-queue", e =>
        {
            e.ConfigureConsumer<PriceUpdatedConsumer>(context); 
        });
    });
});
#endregion

builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();
app.UseHttpsRedirection();
app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseMiddleware<ApplicationKeyMiddleware>();
app.UseMiddleware<EndpointListenerMiddleware>();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();
