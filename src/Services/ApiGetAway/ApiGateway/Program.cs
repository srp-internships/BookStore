using ApiGateway;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Net;
using System.Text;
var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

var corsAllowedHosts = builder.Configuration.GetSection("MraInfrastructure-CORS").Get<string[]>();

var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = jwtSettings.Authority;
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Authority,
            IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecurityKey))
        };
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("CORS_POLICY", policyConfig =>
    {
        policyConfig.WithOrigins(corsAllowedHosts)
                    .AllowAnyHeader()
                    .AllowAnyMethod();
    });
});

builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogWarning("Protocol", ServicePointManager.SecurityProtocol);

app.UseHttpsRedirection();
app.UseCors("CORS_POLICY");
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
await app.UseOcelot();
app.Run();
