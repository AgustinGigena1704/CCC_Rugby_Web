using CCC_Rugby_Web.Components;
using CCC_Rugby_Web.Models;
using CCC_Rugby_Web.Models.Repositories;
using CCC_Rugby_Web.Security;
using CCC_Rugby_Web.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

if (builder.Environment.IsDevelopment())
{
    builder.Logging.SetMinimumLevel(LogLevel.Debug);
}

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMudServices();

builder.Services.AddDbContext<CCC_DbContext>(opt =>
{
    var connectionString = builder.Configuration.GetConnectionString("CCC_DbContext");
    opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddScoped<EntityManager>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.MaxDepth = 64;
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

builder.Services.AddScoped(sp =>
{
    var navigationManager = sp.GetRequiredService<NavigationManager>();
    return new HttpClient { BaseAddress = new Uri(navigationManager.BaseUri) };
});

// Repository registration
var repositoryTypes = Assembly.GetExecutingAssembly()
    .GetTypes()
    .Where(t => t.GetCustomAttribute<RepositoryAttribute>() != null && t.IsClass && !t.IsAbstract);

foreach (var repoType in repositoryTypes)
{
    builder.Services.AddScoped(repoType);
}

// HTTP Context Accessor para CookieService
builder.Services.AddHttpContextAccessor();

// Authentication services
builder.Services.AddScoped<AuthStateComponent>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<CookieService>();
builder.Services.AddScoped<AuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<AuthStateProvider>());

// Authentication configuration
builder.Services.AddAuthentication("CustomScheme")
    .AddScheme<CustomOptions, AuthHandler>("CustomScheme", options => { });

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Authentication middleware - IMPORTANTE: En el orden correcto
app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapControllers();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();