using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Replate.Domain.Entities;

namespace Replate.Infrastructure.Persistence.Configurations;

public class DealConfiguration :  IEntityTypeConfiguration<Deal>
{
    public void Configure(EntityTypeBuilder<Deal> builder)
    {
       builder.ToTable("Deals");
            
            builder.HasKey(d => d.Id);
            
            builder.HasIndex(d => d.PublicId)
                .IsUnique();
            
            builder.Property(d => d.PublicId)
                .IsRequired();
            
            builder.Property(d => d. Title)
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
            
            builder.Property(d => d. AvailableQuantity)
                .IsRequired();
            
            builder.Property(d => d.DealType)
                .IsRequired()
                .HasConversion<int>();
            
            builder. Property(d => d.Category)
                .IsRequired()
                .HasConversion<int>();
            
            builder.Property(d => d. AvailableFrom)
                .IsRequired();
            
            builder.Property(d => d.AvailableUntil)
                .IsRequired();
            
            builder. Property(d => d.CreatedAt)
                .IsRequired();
            
            builder.Property(d => d.IsActive)
                .IsRequired()
                .HasDefaultValue(true);
            
            // Relationships
            builder.HasMany(d => d.DealItems)
                .WithOne(di => di.Deal)
                .HasForeignKey(di => di.DealId)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasMany(d => d.Reservations)
                .WithOne(r => r.Deal)
                .HasForeignKey(r => r.DealId)
                .OnDelete(DeleteBehavior.Restrict);
    }
}