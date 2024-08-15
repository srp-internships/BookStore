using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerForOcelot(builder.Configuration);
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
var authentication = "Bearer";
builder.Services.AddAuthentication()
    .AddJwtBearer(authentication, x =>
    {
        x.Authority = "http://localhost:5252";
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
        };
    });
builder.Services.AddOcelot();
builder.Services.AddAuthentication();
var app = builder.Build();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseSwaggerForOcelotUI(option =>
{
    option.PathToSwaggerGenerator = "/swagger/docs";
});
app.UseAuthentication();
app.UseAuthorization();
await app.UseOcelot();

app.Run();
