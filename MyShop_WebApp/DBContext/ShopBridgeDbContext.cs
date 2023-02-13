using Microsoft.EntityFrameworkCore;
using MyShop_WebApp.Models;

namespace MyShop_WebApp.DBContext
{
    public class ShopBridgeDbContext : DbContext
    {
        public ShopBridgeDbContext(DbContextOptions<ShopBridgeDbContext> options) : base(options)
        {
        }



        public DbSet<Inventory> Inventorys { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.Property(p => p.Name)
                    .HasMaxLength(100)
                    .IsRequired();
                entity.Property(p => p.Description)
                    .IsRequired();
                entity.Property(p => p.Price)
                     .HasPrecision(18, 2)
                     .IsRequired();
                entity.Property(p => p.CreatedAt)
                      .HasDefaultValue(DateTimeOffset.Now.DateTime);
                entity.Property(p => p.UpdatedAt)
                    .ValueGeneratedOnAddOrUpdate();
            });
        }
    }
}
