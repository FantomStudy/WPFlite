using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFlite.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Shop> Shops { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Shop>().HasData(GetShops());
            modelBuilder.Entity<Product>().HasOne(p => p.Shop)
            .WithMany()
            .HasForeignKey(o => o.ShopId);
            modelBuilder.Entity<Product>().HasData(GetProducts());
            base.OnModelCreating(modelBuilder);
        }
        private Shop[] GetShops()
        {
            return new Shop[]
            {
                new Shop { ShopId = 1, ShopName = "М. Видео", Purpose = "Магазин электроники" },
                new Shop { ShopId = 2, ShopName = "Уютный дворик 56", Purpose = "Магазин деревьев" },
                new Shop { ShopId = 3, ShopName = "Орион", Purpose = "Магазин мебели" }

            };
        }
        private Product[] GetProducts()
        {
            return new Product[]
            {
                new Product { ProductId = 1, Name = "Елка", Description = "Зеленая", Price = 1000.00, Unit = 15, ShopId = 2 },
                new Product { ProductId = 2, Name = "Санки", Description = "Едут", Price = 1200.00, Unit = 152, ShopId = 3 },
                new Product { ProductId = 3, Name = "Гирлянда", Description = "Горит", Price = 500.00, Unit = 1534, ShopId = 1 },
                new Product { ProductId = 4, Name = "Стол", Description = "Стоит", Price = 11300.00, Unit = 125, ShopId = 3 },
                new Product { ProductId = 5, Name = "Компьютер", Description = "Работает", Price = 100230.00, Unit = 5, ShopId = 1 }
            };
        }
    }
}
