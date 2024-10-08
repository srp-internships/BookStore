using AnalyticsService.Infrastructure.Data;
using AnalyticsService.Infrastructure.MassTransit;
using AnalyticsService.Infrastructure.Repositories;
using AnalyticsService.Application.Interfaces;
using AnalyticsServiceApi;
using Microsoft.EntityFrameworkCore;
using AnalyticsService.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMassTransitConfiguration();


builder.Services.AddControllers(options => options.Filters.Add<ApiExceptionHandlerFilter>());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddDbContext<AnalyticsDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IBookSaleRepository, BookSaleRepository>();
builder.Services.AddScoped<IBookSaleService, BookSaleService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var applicationDbContext = app.Services.CreateScope().ServiceProvider.GetRequiredService<AnalyticsDbContext>();
await applicationDbContext.Database.MigrateAsync();

app.Run();