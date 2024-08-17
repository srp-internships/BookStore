using Asp.Versioning.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PaymentService.Application;
using PaymentService.Infrastructure;
using PaymentService.Infrastructure.Persistence;
using PaymentService.WebApi.Common;
using PaymentService.WebApi.Common.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();

builder.Services.AddOptions<WebApiSettings>()
    .BindConfiguration(WebApiSettings.ConfigurationSection);

// Adding httpcontext accessor to get current httpcontext by DI
builder.Services.AddHttpContextAccessor();

// Adding all services from Application part
builder.Services.AddApplication();

// Adding all services from Infrastructure part
builder.Services.AddInfrastructure(builder.Configuration);

// Adding Serilog
builder.Services.AddSerilogStuff();

// Adding ApiVersioning
builder.Services.AddAPIVersioning();

// Adding authentication and authorization by JWT token
builder.Services.AddAuthenticationByJwtToken();

// Adding MassTransit & RabbitMq
builder.Services.AddMassTransitWithRabbitMq();

var app = builder.Build();

var webApiSettings = app.Services.GetRequiredService<IOptions<WebApiSettings>>().Value;

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || webApiSettings.EnableSwaggerUI)
{
    var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

    app.UseSwagger();
    app.UseSwaggerUI(opt =>
    {
        // build a swagger endpoint for each discovered API version
        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            opt.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
        }
        opt.InjectStylesheet("/swagger-ui/SwaggerDark.css");
    });
}

app.UseCustomExceptionHandlerMiddleware();

app.UseHsts();
app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

var applicationDbContext = app.Services.CreateScope().ServiceProvider.GetRequiredService<AppDbContext>();
await applicationDbContext.Database.MigrateAsync();

app.Run();
