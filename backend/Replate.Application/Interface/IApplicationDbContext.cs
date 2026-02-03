using Microsoft.EntityFrameworkCore;
using Replate.Domain.Entities;

namespace Replate.Application.Interface;

public interface IApplicationDbContext
{
    // DB Set For all Entities
    
    DbSet<User> Users { get; set; }
    DbSet<VendorProfile> VendorProfiles { get; set; }
    DbSet<VendorAddress> VendorAddresses { get; set; }
    DbSet<FoodListing> FoodListings { get; set; }
    DbSet<FoodListingItem> FoodListingItems { get; set; }
    DbSet<Order> Orders { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) ;
    int SaveChanges();
}