using AutoMapper.Configuration;
using CatalogService.Application;
using CatalogService.Application.Mappers;
using CatalogService.Application.UseCases;
using CatalogService.Application.Interfaces;
using CatalogService.Infostructure;
using CatalogService.Infostructure.Repositories;
using CatalogService.WebApi.Middleware;
using FluentValidation;
using MassTransit;
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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<CustomExceptionHandlerMiddleware>();
app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
