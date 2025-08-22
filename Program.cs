using CCC_Rugby_Web.Components;
using CCC_Rugby_Web.Components.Utils;
using CCC_Rugby_Web.Models;
using CCC_Rugby_Web.Models.Repositories;
using CCC_Rugby_Web.Security;
using CCC_Rugby_Web.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using MudBlazor;
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

if (builder.Environment.IsProduction())
{
    var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
    builder.WebHost.UseUrls($"http://0.0.0.0:{port}");
}

// Configurar Data Protection para contenedores
builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo("/tmp/dataprotection-keys"))
    .SetApplicationName("CCC_Rugby_Web");

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMudServices();
MudGlobal.UnhandledExceptionHandler = (exception) => Console.WriteLine(exception);


builder.Services.AddDbContext<CCC_DbContext>(opt =>
{
    var connectionString = (builder.Environment.IsDevelopment()) ? builder.Configuration.GetConnectionString("CCC_DbContext") :
                       GetConnectionString(builder);
    opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddScoped<EntityManager>();
builder.Services.AddScoped<IUtilities, Utilities>();

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

if (builder.Environment.IsProduction())
{
    ConfigureJwtKey(builder);
}


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

// Solo usar HTTPS redirect en desarrollo local
if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseStaticFiles();

// Authentication middleware - IMPORTANTE: En el orden correcto
app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();


app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapControllers();

app.Run();

// Métodos auxiliares
static string GetConnectionString(WebApplicationBuilder builder)
{
    // Prioridad: Variable de entorno -> appsettings
    var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

    if (string.IsNullOrEmpty(connectionString))
    {
        connectionString = builder.Configuration.GetConnectionString("CCC_DbContext");
    }

    if (string.IsNullOrEmpty(connectionString))
    {
        throw new InvalidOperationException("No se encontró una cadena de conexión válida. Configure CONNECTION_STRING como variable de entorno o en appsettings.json");
    }

    return connectionString;
}

static void ConfigureJwtKey(WebApplicationBuilder builder)
{
    var jwtKey = Environment.GetEnvironmentVariable("JWT_KEY");

    if (string.IsNullOrEmpty(jwtKey))
    {
        jwtKey = builder.Configuration["JWT:Key"];
    }

    if (string.IsNullOrEmpty(jwtKey))
    {
        throw new InvalidOperationException("No se encontró una clave JWT válida. Configure JWT_KEY como variable de entorno o en appsettings.json");
    }

    builder.Configuration["JWT:Key"] = jwtKey;
}