using CCC_Rugby_Web.Components;
using CCC_Rugby_Web.Models;
using CCC_Rugby_Web.Models.Repositories;
using CCC_Rugby_Web.Security;
using CCC_Rugby_Web.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMudServices();

builder.Services.AddDbContext<CCC_DbContext>(opt =>
{
    var connectionString = builder.Configuration.GetConnectionString("CCC_DbContext");
    opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
}); 

builder.Services.AddScoped<UsuarioRepository>();

builder.Services.AddAuthenticationCore();
builder.Services.AddScoped<AuthStateComponent>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<CookieService>();
builder.Services.AddAuthentication()
    .AddScheme<CustomOptions, AuthHandler>("CustomScheme", options => { });
builder.Services.AddScoped<AuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<AuthStateProvider>());
builder.Services.AddCascadingAuthenticationState();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
