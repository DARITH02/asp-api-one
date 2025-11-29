using Microsoft.EntityFrameworkCore;
using WebApplicationApi1.Models;

namespace WebApplicationApi1.Data;

public class AppDbContext : DbContext

{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Product> products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Supplier> Suppliers => Set<Supplier>();
    public DbSet<Purchase> Purchases => Set<Purchase>();
    public DbSet<PurchaseDetails> PurchaseDetails => Set<PurchaseDetails>();
    public DbSet<StockLogs> StockLogs => Set<StockLogs>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // relationship between product and  category
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.Category_id)
            .OnDelete(DeleteBehavior.SetNull);

        // relationship between product and  supplier
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Supplier)
            .WithMany(s => s.Products)
            .HasForeignKey(p => p.Supplier_id)
            .OnDelete(DeleteBehavior.SetNull);


        // relationship between purchase and Supplierc
        modelBuilder.Entity<Purchase>()
            .HasOne(p => p.Supplier)
            .WithMany(s => s.Purchases)
            .HasForeignKey(p => p.SupplierId)
            .OnDelete(DeleteBehavior.SetNull);



        // relationship between purchase detail and product and purchase
        modelBuilder.Entity<PurchaseDetails>()
            .HasOne(p => p.Purchase)
            .WithMany(p => p.Details)
            .OnDelete(DeleteBehavior.SetNull);
        modelBuilder.Entity<PurchaseDetails>()
            .HasOne(p => p.Product)
            .WithMany(p => p.Details)
            .OnDelete(DeleteBehavior.SetNull);

// StockLogs configuration
        modelBuilder.Entity<StockLogs>()
            .ToTable("StockLogs") // ← ensures EF Core uses this exact table name
            .HasOne(sl => sl.Product)
            .WithMany(p => p.Logs)
            .HasForeignKey(sl => sl.ProductId)
            .OnDelete(DeleteBehavior.SetNull);

    }
}