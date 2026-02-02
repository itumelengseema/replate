using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Replate.Domain.Entities;

namespace Replate.Infrastructure.Persistence.Configurations;

public class FoodListingConfiguration : IEntityTypeConfiguration<FoodListing>
{
    public void Configure(EntityTypeBuilder<FoodListing> builder)
    {
        builder.ToTable("FoodListings");

        builder.HasKey(d => d.Id);

        builder.HasIndex(d => d.PublicId)
            .IsUnique();

        builder.Property(d => d.PublicId)
            .IsRequired();

        builder.Property(d => d.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(d => d.Description)
            .HasMaxLength(1000);

        builder.Property(d => d.OriginalPrice)
            .IsRequired()
            .HasPrecision(10, 2);

        builder.Property(d => d.DiscountedPrice)
            .IsRequired()
            .HasPrecision(10, 2);

        builder.Property(d => d.AvailableQuantity)
            .IsRequired();

        builder.Property(d => d.FoodListingType)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(d => d.Category)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(d => d.AvailableFrom)
            .IsRequired();

        builder.Property(d => d.AvailableUntil)
            .IsRequired();

        builder.Property(d => d.CreatedAt)
            .IsRequired();

        builder.Property(d => d.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        // Relationships
        builder.HasMany(d => d.DealItems)
            .WithOne(di => di.FoodListing)
            .HasForeignKey(di => di.FoodListingId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(d => d.Reservations)
            .WithOne(r => r.FoodListing)
            .HasForeignKey(r => r.FoodListingId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}