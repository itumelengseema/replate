using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Replate.Domain.Entities;

namespace Replate.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.ToTable("Users");
            
            entity.HasKey(u => u. Id);
            
            entity.HasIndex(u => u.PublicId)
                .IsUnique();
            
            entity.HasIndex(u => u.FirebaseUid)
                .IsUnique();
            
            entity.HasIndex(u => u.Email)
                .IsUnique();
            
            entity.Property(u => u.PublicId)
                .IsRequired();
            
            entity.Property(u => u.FirebaseUid)
                .IsRequired()
                .HasMaxLength(128);
            
            entity.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);
            
            entity.Property(u => u.DisplayName)
                .HasMaxLength(100);
            
            entity.Property(u => u.PhotoUrl)
                .HasMaxLength(500);
            
            entity.Property(u => u.Role)
                .IsRequired()
                .HasConversion<int>();
            
            entity.Property(u => u.CreatedAt)
                .IsRequired();
            
            entity.Property(u => u.IsActive)
                .IsRequired()
                .HasDefaultValue(true);
            
            // Relationships
            entity.HasMany(u => u.Reservations)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior. Cascade);
        }
    }
}
