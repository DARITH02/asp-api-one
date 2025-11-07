using Microsoft.EntityFrameworkCore;
using WebApplicationApi1.Models;

namespace WebApplicationApi1.Data;

public class AppDbContext:DbContext

{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<Product> products => Set<Product>();
    public DbSet<Category> Categories=>Set<Category>();
    public DbSet<Supplier> Suppliers=>Set<Supplier>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.Category_id)
            .OnDelete(DeleteBehavior.SetNull);
        
        modelBuilder.Entity<Product>()
            .HasOne(p=>p.Supplier)
            .WithMany(s=>s.Products)
            .HasForeignKey(p=>p.Supplier_id)
            .OnDelete(DeleteBehavior.SetNull);
    }
}