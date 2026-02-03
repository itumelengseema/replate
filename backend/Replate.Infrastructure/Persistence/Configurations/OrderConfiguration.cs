using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Replate.Domain.Entities;

namespace Replate.Infrastructure.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> entity)
    {
        entity.ToTable("Orders");
        entity.HasKey(e => e.Id);
        
        entity.HasIndex(e => e.PublicId)
            .IsUnique();
        
        entity.Property(e => e.PublicId)
            .IsRequired();

        entity.Property(e => e.TotalPrice)
            .HasPrecision(18, 2);

        entity.Property(e => e.Notes)
            .HasMaxLength(500);

        // Relationships
        entity.HasOne(e => e.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(e => e.FoodListing)
            .WithMany(f => f.Orders)
            .HasForeignKey(e => e.FoodListingId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
