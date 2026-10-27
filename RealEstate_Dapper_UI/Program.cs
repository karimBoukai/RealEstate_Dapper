using Microsoft.AspNetCore.Authentication.Cookies;
using RealEstate_Dapper_UI.Models;
using RealEstate_Dapper_UI.Services;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettingsKey"));

// Add services to the container.
builder.Services.AddHttpClient();
// API offline / transport errors become a 503 response so pages show their empty states instead of crashing.
builder.Services.AddTransient<ApiUnavailableHandler>();
builder.Services.ConfigureHttpClientDefaults(http => http.AddHttpMessageHandler<ApiUnavailableHandler>());

builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opt =>
    {
        opt.LoginPath = "/Login/Index";
        opt.LogoutPath = "/Login/Logout";
        opt.AccessDeniedPath = "/Login/Index";

        opt.Cookie.HttpOnly = true;

        // ✅ IMPORTANT: allow cookie to be sent on normal navigations
        opt.Cookie.SameSite = SameSiteMode.Lax;

        opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
        opt.Cookie.Name = "RealEstateJwt";
    });

builder.Services.AddAuthorization();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ILoginServices, LoginServices>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Friendly page for empty 4xx/5xx responses (e.g. NotFound() when a property can't be loaded).
app.UseStatusCodePagesWithReExecute("/Home/StatusCode/{0}");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "property",
        pattern: "property/{slug}/{id}",
        defaults: new { controller = "Property", action = "PropertySingle" });

    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Default}/{action=Index}/{id?}");
});

app.Run();
