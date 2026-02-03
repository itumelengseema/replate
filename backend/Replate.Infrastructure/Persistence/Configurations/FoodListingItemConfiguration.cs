using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Replate.Domain.Entities;

namespace Replate.Infrastructure.Persistence.Configurations;

public class FoodListingItemConfiguration : IEntityTypeConfiguration<FoodListingItem>
{
    public void Configure(EntityTypeBuilder<FoodListingItem> entity)
    {
        entity.ToTable("FoodListingItems");
        entity.HasKey(e => e.Id);

        entity.Property(e => e.Name)
            .HasMaxLength(200)
            .IsRequired();

        entity.Property(e => e.Description)
            .HasMaxLength(500);

        entity.HasOne(e => e.FoodListing)
            .WithMany(f => f.Items)
            .HasForeignKey(e => e.FoodListingId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
