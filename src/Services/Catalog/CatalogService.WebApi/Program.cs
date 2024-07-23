using AutoMapper.Configuration;
using CatalogService.Application;
using CatalogService.Application.Mappers;
using CatalogService.Application.UseCases;
using CatalogService.Application.Validators.Book;
using CatalogService.Domain.Interfaces;
using CatalogService.Infostructure;
using CatalogService.Infostructure.Repositories;
using FluentValidation;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<CatalogDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection")));

builder.Services.AddApplication();
builder.Services.AddInfrastructure();

builder.Services.AddMassTransit(x =>
{
    // Configure RabbitMQ
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq://localhost");
    });
});

builder.Services.AddMassTransitHostedService();
/*
builder.Services.AddAutoMapper(options =>
{
    options.AddProfile<MappingProfile>();
});
builder.Services.AddScoped<BookRepository>();
builder.Services.AddScoped<AuthorRepository>();
builder.Services.AddScoped<CategoryRepository>();
builder.Services.AddScoped<PublisherRepository>();
builder.Services.AddScoped<BookSellerRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IPublisherRepository, PublisherRepository>();
builder.Services.AddScoped<IBookSellerRepository, BookSellerRepository>();

builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateBookCommandValidator>());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateAuthorCommand).Assembly));
*/
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
