using Microsoft.EntityFrameworkCore;
using MultiShop.Comment.Context;

var builder = WebApplication.CreateBuilder(args);

// DbContext servisini ekle (connection string'i appsettings.json’dan okur)
builder.Services.AddDbContext<CommentContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// MVC Controllers
builder.Services.AddControllers();

// Swagger (Swashbuckle)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();             
    app.UseSwaggerUI();           
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();