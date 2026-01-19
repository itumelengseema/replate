using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Replate.Domain.Entities;

namespace Replate.Infrastructure.Persistence.Configurations;

public class VendorAddressConfiguration :IEntityTypeConfiguration<VendorAddress>
{
    public void Configure(EntityTypeBuilder<VendorAddress> entity)
    {
        entity.ToTable("vendorAddress");
        entity.HasKey(a => a.Id);
        entity.Property(a => a.Street)
            .IsRequired()
            .HasMaxLength(128);

        entity.Property(a => a.Building)
            .HasMaxLength(150);
        
        entity.Property(a => a.City)
            .IsRequired()
            .HasMaxLength(128);
        
        entity.Property(a => a.Province)
            .IsRequired()
            .HasMaxLength(128);
        
        entity.Property(a => a.PostalCode)
            .IsRequired()
            .HasMaxLength(10);
        entity.Property(a => a.Country)
            .IsRequired()
            .HasMaxLength(128);

        entity.Property(a => a.Latitude)
            .IsRequired();
        entity.Property(a => a.Longitude)
            .IsRequired();
        
        entity.Property(a => a.GooglePlaceId)
            .HasMaxLength(200);
        
        entity.HasIndex(a => new  { a.Latitude, a.Longitude });
    }
}