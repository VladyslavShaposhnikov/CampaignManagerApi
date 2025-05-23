using CampaignManagerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CampaignManagerApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Seller> Sellers { get; set; }
    public DbSet<Campaign> Campaigns { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<CampaignKeyword> CampaignKeywords { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Seller)
            .WithMany(s => s.Products)
            .HasForeignKey(p => p.SellerId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Campaign>()
            .HasOne(c => c.Product)
            .WithMany(p => p.Campaigns)
            .HasForeignKey(c => c.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CampaignKeyword>()
            .HasOne(k => k.Campaign)
            .WithMany(c => c.Keywords)
            .HasForeignKey(c => c.CampaignId);
        
        // Enum as string
        modelBuilder.Entity<Campaign>()
            .Property(c => c.Status)
            .HasConversion<string>();

    }
}