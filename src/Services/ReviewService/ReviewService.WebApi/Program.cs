using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ReviewService.Application.IRepositories;
using ReviewService.Infrastructure.Consumers;
using ReviewService.Infrastructure.Persistence.Contexts;
using ReviewService.Infrastructure.Repositories;
using ReviewService.Infrastructure.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ReviewDbContext>(con => con.UseSqlServer(builder.Configuration["ConnectionString"])
                      .LogTo(Console.Write, LogLevel.Error)
          .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<BookCreatedConsumer>();

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
    });
});

builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IReviewService, ReviewServices>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBookService, BookService>();

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
    var context = scope.ServiceProvider.GetService<ReviewDbContext>();
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
