using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Replate.Domain.Entities;

namespace Replate.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.HasKey(u => u.Id);

            entity.HasIndex(u => u.PublicId).IsUnique();
            entity.HasIndex(u => u.FirebaseUid).IsUnique();

            entity.Property(u => u.Email)
                  .IsRequired()
                  .HasMaxLength(256);

            entity.Property(u => u.DisplayName)
                  .IsRequired()
                  .HasMaxLength(150);

            entity.Ignore(u => u.IsVendor);
        }
    }
}
