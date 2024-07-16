using System.Numerics;
using Microsoft.EntityFrameworkCore;
using ShipmentService.Infrastructure.Persistence.DbContexts;
using ShipmentService.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ShipmentContext>(con => con.UseSqlServer(builder.Configuration["ConnectionString"])
                                  .LogTo(Console.Write, LogLevel.Error)
                                  .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();



using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetService<ShipmentContext>();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
