using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Net;

var builder = WebApplication.CreateBuilder(args);


builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

var corsAllowedHosts = builder.Configuration.GetSection("MraInfrastructure-CORS").Get<string[]>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CORS_POLICY", policyConfig =>
    {
        policyConfig.WithOrigins(corsAllowedHosts)
                    .AllowAnyHeader()
                    .AllowAnyMethod();
    });
});

builder.Services.AddOcelot();



var app = builder.Build();



var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogWarning("Protocol", ServicePointManager.SecurityProtocol);


app.UseHttpsRedirection();
app.UseCors("CORS_POLICY");

// step 3:
app.UseOcelot().Wait();
app.Run();