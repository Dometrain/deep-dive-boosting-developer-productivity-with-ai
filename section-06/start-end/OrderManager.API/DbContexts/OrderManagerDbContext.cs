using Microsoft.EntityFrameworkCore;
using OrderManager.API.Entities;

namespace OrderManager.API.DbContexts;

public class OrderManagerDbContext(DbContextOptions<OrderManagerDbContext> options) : DbContext(options)
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderLine> OrderLines { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Vendor> Vendors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>()
            .ToTable("Orders")
            .Property(o => o.Title)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<Order>()
            .Property(o => o.Description)
            .HasMaxLength(300);

        modelBuilder.Entity<Order>()
            .Property(o => o.Id)
            .UseIdentityColumn();

        modelBuilder.Entity<Order>()
            .Property(o => o.OrderTotal)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<OrderLine>()
            .ToTable("OrderLines")
            .Property(ol => ol.Details)
            .HasMaxLength(300);

        modelBuilder.Entity<OrderLine>()
            .Property(ol => ol.Id)
            .UseIdentityColumn();

        modelBuilder.Entity<OrderLine>()
            .Property(ol => ol.Price)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Product>()
            .ToTable("Products")
            .Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<Product>()
            .Property(p => p.Description)
            .HasMaxLength(300);

        modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Product>()
            .Property(p => p.Id)
            .UseIdentityColumn();

        modelBuilder.Entity<Vendor>()
            .ToTable("Vendors")
            .Property(v => v.Name)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<Vendor>()
            .Property(v => v.Address)
            .HasMaxLength(200);

        modelBuilder.Entity<Vendor>()
            .Property(v => v.Email)
            .IsRequired()
            .HasMaxLength(200);

        modelBuilder.Entity<Vendor>()
            .Property(v => v.Id)
            .UseIdentityColumn();

        // Call the seed method
        OrderManagerDbContextSeed.Seed(modelBuilder);
    }
}
