using Microsoft.EntityFrameworkCore;
using Replate.Domain.Entities;

namespace Replate.Infrastructure.Persistence
{
    public class ReplateDbContext : DbContext
    {
        public ReplateDbContext(DbContextOptions<ReplateDbContext> options)
            : base(options)
        {

        }

        public DbSet<User> Users => Set<User>();
        public DbSet<VendorProfile> VendorProfiles => Set<VendorProfile>();
        public DbSet<Deal> Deals => Set<Deal>();
        public DbSet<DealItem> DealItems => Set<DealItem>();
        public DbSet<Reservation> Reservations => Set<Reservation>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReplateDbContext).Assembly);
        }
    }
}
