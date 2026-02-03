using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Replate.Domain.Entities;

namespace Replate.Infrastructure.Persistence.Configurations;

public class FoodListingConfiguration : IEntityTypeConfiguration<FoodListing>
{
    public void Configure(EntityTypeBuilder<FoodListing> entity)
    {
        entity.ToTable("FoodListings");
        entity.HasKey(e => e.Id);
        
        entity.HasIndex(e => e.PublicId)
            .IsUnique();
        
        entity.Property(e => e.PublicId)
            .IsRequired();

        entity.Property(e => e.Title)
            .HasMaxLength(200)
            .IsRequired();

        entity.Property(e => e.Description)
            .HasMaxLength(1000);

        entity.Property(e => e.OriginalPrice)
            .HasPrecision(18, 2);

        entity.Property(e => e.DiscountedPrice)
            .HasPrecision(18, 2);

        entity.Property(e => e.ImageUrl)
            .HasMaxLength(500);

        // Relationships
        entity.HasOne(e => e.VendorProfile)
            .WithMany(v => v.FoodListings)
            .HasForeignKey(e => e.VendorProfileId)
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasMany(e => e.Items)
            .WithOne(i => i.FoodListing)
            .HasForeignKey(i => i.FoodListingId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
