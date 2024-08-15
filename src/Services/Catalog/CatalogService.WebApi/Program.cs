using CatalogService.Application;
using CatalogService.Application.Mappers;
using CatalogService.Infostructure;
using CatalogService.WebApi.Middleware;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(config => config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly())));
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<CustomExceptionHandlerMiddleware>();
//app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

var applicationDbContext = app.Services.CreateScope().ServiceProvider.GetRequiredService<CatalogDbContext>();
await applicationDbContext.Database.MigrateAsync();

app.Run();
