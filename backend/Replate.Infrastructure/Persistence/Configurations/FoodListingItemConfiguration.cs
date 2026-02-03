using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Replate.Domain.Entities;

namespace Replate.Infrastructure.Persistence.Configurations;

public class FoodListingItemConfiguration : IEntityTypeConfiguration<FoodListingItem>
{
    public void Configure(EntityTypeBuilder<FoodListingItem> builder)
    {
        builder.HasOne(fli => fli.FoodListing)
            .WithMany(fl => fl.FoodListingItems)
            .HasForeignKey(fli => fli.FoodListingId);
    }
}
