using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Replate.Domain.Entities;

namespace Replate.Infrastructure.Persistence.Configurations;

public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> entity)
    {
        entity.HasKey(e => e.Id);
        
        entity.HasIndex(e => e.PublicId)
            .IsUnique();
        
        entity.HasOne(e=> e.Deal)
            .WithMany(d => d.Reservations)
            .HasForeignKey(e => e.DealId)
            .OnDelete(DeleteBehavior.Restrict);
        
        entity.HasOne(e => e.User)
            .WithMany(u => u.Reservations)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.Property(e => e.Id)
            .IsRequired();

    }
}