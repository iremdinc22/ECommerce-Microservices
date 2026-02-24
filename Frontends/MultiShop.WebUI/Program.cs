using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MultiShop.WebUI.Services.Concrete;
using MultiShop.WebUI.Services.Interfaces;
using IdentityModel.AspNetCore; // dotnet add package IdentityModel.AspNetCore yüklü olmalı

var builder = WebApplication.CreateBuilder(args);

// --- AUTHENTICATION YAPILANDIRMASI ---
// AddAuthentication sadece BİR KEZ çağrılır.
builder.Services.AddAuthentication(options => 
{
    // Varsayılan olarak standart cookie şemasını kullanıyoruz
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
// İLK ÇEREZ: JWT için kullandığın özel çerez şeması
.AddCookie(JwtBearerDefaults.AuthenticationScheme, opt =>
{
    opt.LoginPath = "/Login/Index/";
    opt.LogoutPath = "/Login/LogOut/";
    opt.AccessDeniedPath = "/Pages/AccessDenied/";
    opt.Cookie.HttpOnly = true;
    opt.Cookie.SameSite = SameSiteMode.Strict;
    opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    opt.Cookie.Name = "MultiShopJwt";
})
// İKİNCİ ÇEREZ: Genel oturum çerezi
.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opt =>
{
    opt.LoginPath = "/Login/Index/";
    opt.ExpireTimeSpan = TimeSpan.FromDays(5);
    opt.Cookie.Name = "MultiShopCookie";
    opt.SlidingExpiration = true;
});

// Access Token Yönetimi
builder.Services.AddAccessTokenManagement();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddHttpClient();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Middleware sıralaması
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllerRoute(name: "areas", pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();