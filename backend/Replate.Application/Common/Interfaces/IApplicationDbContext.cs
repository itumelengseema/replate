using Microsoft.EntityFrameworkCore;
using Replate.Domain.Entities;

namespace Replate.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<VendorProfile> VendorProfiles { get; }
    DbSet<Deal> Deals { get; }
    DbSet<DealItem> DealItems { get; }
    DbSet<Reservation>  Reservations { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}