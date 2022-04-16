using System;
using System.Threading.Tasks;
using ITunes.UrlShortener.WebUI;
using ITunes.UrlShortener.WebUI.Helper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddHttpContextAccessor();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(x => { x.LoginPath = "/admin/auth/login"; });

builder.Services.AddScoped<CustomCookieAuthenticationEvents>();
builder.Services.AddHttpClient();

builder.Services.AddScoped(typeof(IApiHelper<>), typeof(ApiHelper<>));
builder.Services.AddSingleton<ISecurityTokenHandler, SecurityTokenHandler>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "UrlRedirect",
    pattern: "/{id}",
    defaults: new { controller = "Home", action = "Index" }
);
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "MyArea",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);

app.Run();


namespace ITunes.UrlShortener.WebUI
{
    public class CustomCookieAuthenticationEvents : CookieAuthenticationEvents
    {
        public override async Task ValidatePrincipal(CookieValidatePrincipalContext context)
        {
            var userPrincipal = context.Principal;
            context.Properties.ExpiresUtc = DateTime.UtcNow.AddHours(24);
            context.Properties.IsPersistent = true;
        }
    }
}