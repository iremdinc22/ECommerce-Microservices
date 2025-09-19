using System.Data;
using Microsoft.Data.Sqlite;  // SQLite için gerekli
using Microsoft.EntityFrameworkCore;
using MultiShop.Discount.Entities;

namespace MultiShop.Discount.Context
{
    public class DapperContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DapperContext(DbContextOptions<DapperContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<Coupon> Coupons { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection")
                                       ?? "Data Source=MultiShopDiscountDb.db";
                optionsBuilder.UseSqlite(connectionString);
            }
        }

        // Dapper için bağlantı döndüren metot
        public IDbConnection CreateConnection()
        {
            var cs = _configuration.GetConnectionString("DefaultConnection")
                     ?? "Data Source=MultiShopDiscountDb.db";

            return new SqliteConnection(cs);
        }
    }
}