using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MultiShop.Comment.Context;

var builder = WebApplication.CreateBuilder(args);

// 1. Kimlik Doğrulama (Authentication) Ayarları
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["IdentityServerUrl"];
        options.Audience = "ResourceComment";
        options.RequireHttpsMetadata = false; 
    });

// 2. Yetkilendirme Servisi
builder.Services.AddAuthorization();

// 3. Veritabanı Bağlantısı (SQL Server)
builder.Services.AddDbContext<CommentContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 4. Controller Servisleri
builder.Services.AddControllers();

// 5. Swagger Ayarları
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// --- HTTP İşlem Hattı (Middleware Pipeline) Yapılandırması ---

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ⚠️ SIRALAMA ÇOK ÖNEMLİ:
app.UseAuthentication(); // Önce kimlik doğrulanır
app.UseAuthorization();  // Sonra yetki kontrol edilir (Eksikti, eklendi ✅)

// ⚠️ API ROTALARINI AKTİF ET:
app.MapControllers();    // Controller'ları URL'ler ile eşleştirir (Eksikti, eklendi ✅)

app.Run();