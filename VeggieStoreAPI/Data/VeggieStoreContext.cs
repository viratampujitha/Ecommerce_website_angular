using Microsoft.EntityFrameworkCore;
using VeggieStoreAPI.Models;

namespace VeggieStoreAPI.Data
{
    public class VeggieStoreContext : DbContext
    {
        public VeggieStoreContext(DbContextOptions<VeggieStoreContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Vegetable> Vegetables { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Cart> Carts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Mapping for database column names
            modelBuilder.Entity<Vegetable>()
                .Property(v => v.ImageUrl)
                .HasColumnName("image_url");

            modelBuilder.Entity<Vegetable>()
                .Property(v => v.CreatedAt)
                .HasColumnName("created_at");

            // Relationships
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Vegetable)
                .WithMany()
                .HasForeignKey(oi => oi.VegetableId);

            modelBuilder.Entity<Cart>()
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId);

            modelBuilder.Entity<Cart>()
                .HasOne(c => c.Vegetable)
                .WithMany()
                .HasForeignKey(c => c.VegetableId);
        }
    }
}
