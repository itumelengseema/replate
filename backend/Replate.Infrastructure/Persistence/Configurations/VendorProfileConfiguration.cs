using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Replate.Domain.Entities;

namespace Replate.Infrastructure.Persistence.Configurations;

public class VendorProfileConfiguration : IEntityTypeConfiguration<VendorProfile>
{
    public void Configure(EntityTypeBuilder<VendorProfile> entity)
    {
        entity.HasKey(e => e.Id);
        entity.HasIndex(e=> e.PublicId)
            .IsUnique();



        entity.Property(e => e.BusinessName)
            .HasMaxLength(128)
            .IsRequired();

        entity.Property(e => e.Decscription)
            .HasMaxLength(500);
        
        
     
        entity.Property(e => e.PhoneNumber)
            .HasMaxLength(50);
        
        
        // One-to-one: User <> VendorProfile
        entity.HasOne(e=> e.User)
            .WithMany(u=> u.VendorProfile)
            .HasForeignKey(e=> e.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(e => e.Address)
            .WithOne(a => a.VendorProfile)
            .HasForeignKey<VendorAddress>(e => e.VendorProfileId)
            .OnDelete(DeleteBehavior.Cascade);


    }
}