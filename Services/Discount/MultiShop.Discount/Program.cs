using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using MultiShop.Discount.Context;
using MultiShop.Discount.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["IdentityServerUrl"]; // IdentityServer URL
        options.Audience  = builder.Configuration["Audience"]; // ApiResource adı
        options.RequireHttpsMetadata = false; // dev ortamı için
    });

builder.Services.AddAuthorization(); // ✅ ekle

// EF Core
builder.Services.AddDbContext<DapperContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Dapper için context enjekte et
builder.Services.AddTransient<DapperContext>();
builder.Services.AddTransient<IDiscountService, DiscountService>();


// Controllers & Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MultiShop.Discount v1");
        c.RoutePrefix = "swagger";
    });
}


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();