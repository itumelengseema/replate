﻿using Microsoft.EntityFrameworkCore;
using Replate.Application.Interface;
using Replate.Domain.Entities;

namespace Replate.Infrastructure.Persistence
{
    public class ReplateDbContext : DbContext, IApplicationDbContext
    {
        public ReplateDbContext(DbContextOptions<ReplateDbContext> options)
            : base(options)
        {

        }
         // Dbsets
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<VendorProfile> VendorProfiles { get; set; } = null!;
        public DbSet<VendorAddress> VendorAddresses { get; set; } = null!;
        public DbSet<FoodListing> FoodListings { get; set; } = null!;
      
        public DbSet<FoodListingItem> FoodListingItems { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // This automatically finds and applies all IEntityTypeConfiguration implementations in the assembly
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReplateDbContext).Assembly);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateAuditFields();
            return base.SaveChangesAsync(cancellationToken);
        }
        public override int SaveChanges()
        {
            UpdateAuditFields();
            return base.SaveChanges();
        }
        private void UpdateAuditFields()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                var now = DateTime.UtcNow;

                if (entry.State == EntityState.Added)
                {
                    if (entry.Entity.GetType().GetProperty("CreatedAt") != null)
                    {
                        entry.Property("CreatedAt").CurrentValue = now;
                    }
                }

                if (entry.Entity.GetType().GetProperty("UpdatedAt") != null)
                {
                    entry.Property("UpdatedAt").CurrentValue = now;
                }
            }
        }
    }
}
