using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Replate.Domain.Entities;

namespace Replate.Infrastructure.Persistence.Configurations;

public class VendorProfileConfiguration : IEntityTypeConfiguration<VendorProfile>
{
    public void Configure(EntityTypeBuilder<VendorProfile> entity)
    {
        entity.ToTable("VendorProfile");
        entity.HasKey(e => e.Id);
        entity.HasIndex(e => e.PublicId)
            .IsUnique();
        entity.Property(e => e.PublicId)
            .IsRequired();



        entity.Property(e => e.BusinessName)
            .HasMaxLength(200)
            .IsRequired();

        entity.Property(e => e.Description)
            .HasMaxLength(500);
        
        entity.Property(e => e.LogoUrl)
            .HasMaxLength(500);
        
        
     
        entity.Property(e => e.PhoneNumber)
            .HasMaxLength(50)
            .IsRequired();
        
        entity.Property(e => e.Email)
            .HasMaxLength(100)
            .IsRequired();

        entity.Property(e => e.BusinessHours)
            .HasMaxLength(500);
        
        entity.Property(e => e.CreatedAt)
            .IsRequired();
        entity.Property(e => e.UpdatedAt);
        entity.Property(e => e.IsActive)
            .IsRequired()
            .HasDefaultValue(true);
        
        
        
        
        // Relationships
        entity.HasOne(v => v.User)
            .WithOne(u => u.VendorProfile)
            .HasForeignKey<VendorProfile>(v => v. UserId)
            .OnDelete(DeleteBehavior. Cascade);
            
        entity.HasOne(v => v.VendorAddress)
            .WithOne(a => a.VendorProfile)
            .HasForeignKey<VendorProfile>(v => v.VendorAddressId)
            .OnDelete(DeleteBehavior.SetNull);
            
        entity.HasMany(v => v.Deals)
            .WithOne(d => d.VendorProfile)
            .HasForeignKey(d => d.VendorProfileId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}