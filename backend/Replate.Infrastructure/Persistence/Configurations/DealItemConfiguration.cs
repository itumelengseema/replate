using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Replate.Domain.Entities;

namespace Replate.Infrastructure.Persistence.Configurations;

public class DealItemConfiguration : IEntityTypeConfiguration<DealItem>
{
    public void Configure(EntityTypeBuilder<DealItem> builder)
    {
        builder.HasOne(di => di.FoodListing)
            .WithMany(fl => fl.DealItems)
            .HasForeignKey(di => di.FoodListingId);
    }
}