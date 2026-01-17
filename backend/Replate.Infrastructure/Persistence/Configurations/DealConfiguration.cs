using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Replate.Domain.Entities;

namespace Replate.Infrastructure.Persistence.Configurations;

public class DealConfiguration :  IEntityTypeConfiguration<Deal>
{
    public void Configure(EntityTypeBuilder<Deal> entity)
    {
       entity.HasKey(e => e.Id);
       entity.HasIndex(e=> e.PublicId)
           .IsUnique();
       
       entity.Property(e => e.Title)
           .HasMaxLength(100)
           .IsRequired();

       entity.Property(e => e.Description)
           .HasMaxLength(128);

       entity.Property(e => e.Category)
           .HasConversion<int>();
       
       entity.Property(e=>e.OriginalPrice)
           .HasPrecision(10, 2);
       entity.Property(e=> e.DiscountedPrice)
           .HasPrecision(10, 2);
       entity.Property(e => e.QuantityAvailable)
           .IsRequired();
       entity.Property(e=> e.IsActive)
           .HasDefaultValue(true);
       
       entity.Property(e=> e.ExpiryDate)
           .HasDefaultValueSql("CURRENT_TIMESTAMP");
       
       entity.HasOne(e=> e.VendorProfile)
           .WithMany(v => v.Deals)
           .HasForeignKey(e=> e.VendorProfileId)
            .OnDelete(DeleteBehavior.Restrict);


       entity.HasIndex(e => e.ExpiryDate);
       entity.HasIndex(e => e.Category);

    }
}