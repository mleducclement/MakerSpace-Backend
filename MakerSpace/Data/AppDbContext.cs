using MakerSpace.Entities;
using MakerSpace.Models;
using Microsoft.EntityFrameworkCore;

namespace MakerSpace.Data;

public class AppDbContext : DbContext {
   public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

   protected override void OnModelCreating(ModelBuilder modelBuilder) {
      
      modelBuilder.Entity<Product>()
         .Property(p => p.Price)
         .HasPrecision(6, 2);
      
      modelBuilder.Entity<Category>()
         .HasIndex(c => c.Name)
         .IsUnique();
      
      base.OnModelCreating(modelBuilder);
   }

   public DbSet<Product> Products { get; set; }
   public DbSet<Category> Categories { get; set; }
}