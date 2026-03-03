using Microsoft.AspNetCore.Authentication.JwtBearer;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// 1. Kimlik Doğrulama Ayarları
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("OcelotAuthenticationScheme", opt =>
    {
        // appsettings.json içinde IdentityServerUrl'in http://localhost:5001 olduğundan emin ol
        opt.Authority = builder.Configuration["IdentityServerUrl"]; 
        opt.Audience = "ResourceOcelot";
        opt.RequireHttpsMetadata = false;
    });

// 2. Ocelot Yapılandırmasını Dosyadan Oku
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

// 3. Ocelot Servislerini Ekle
builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();

// 4. SIRALAMA KRİTİK: Ocelot'tan önce Auth middleware'leri çalışmalı
app.UseAuthentication();
app.UseAuthorization();

// 5. Ocelot Middleware'ini En Sona Koy
await app.UseOcelot();

app.MapGet("/", () => "Hello World!");

app.Run();
