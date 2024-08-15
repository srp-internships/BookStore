using IdentityService.Common.Extensions;
using IdentityService.Components;
using IdentityService.Components.Account;
using IdentityService.Components.IDS;
using IdentityService.Data;
using IdentityService.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Net.Mail;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));

builder.Services.AddScoped(provider =>
{
    var smtpSettings = provider.GetRequiredService<IOptions<SmtpSettings>>().Value;

    var smtpClient = new SmtpClient
    {
        Host = smtpSettings.Host,
        Port = smtpSettings.Port,
        EnableSsl = smtpSettings.EnableSsl,
        Credentials = new System.Net.NetworkCredential(smtpSettings.Username, smtpSettings.Password)
    };

    return smtpClient;
});

builder.Services.AddScoped<IMailService, MailService>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
    })
    .AddIdentityCookies();

var connectionString = builder.Configuration
                              .GetConnectionString("DefaultConnection")
                              ?.Replace("[DataDirectory]", Directory.GetCurrentDirectory())
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddIdentityCore<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<Role>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddIds(builder.Configuration);

builder.Services.AddMassTransitWithRabbitMq();

builder.Services.AddScoped<IEmailSender<User>, IdentityEmailSender>();

var app = builder.Build();

SeedData.EnsureSeedData(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseIdentityServer();
app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();


var applicationDbContext = app.Services.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();
await applicationDbContext.Database.MigrateAsync();
var configDbContext = app.Services.CreateScope().ServiceProvider.GetRequiredService<ConfigurationDbContext>();
await configDbContext.Database.MigrateAsync();
var persistedDbContext = app.Services.CreateScope().ServiceProvider.GetRequiredService<PersistedGrantDbContext>();
await persistedDbContext.Database.MigrateAsync();

app.Run();
