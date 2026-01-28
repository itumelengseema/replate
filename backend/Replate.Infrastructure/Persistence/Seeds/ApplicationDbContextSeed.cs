using Microsoft.EntityFrameworkCore;
using Replate.Domain.Entities;
using Replate.Domain.Enums;

namespace Replate.Infrastructure.Persistence.Seeds
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedAsync(ReplateDbContext context)
        {
            Console.WriteLine("🔍 Checking database state...");
            
            var hasUsers = await context.Users.AnyAsync();
            var hasVendorProfiles = await context.VendorProfiles.AnyAsync();
            var hasVendorAddresses = await context.VendorAddresses.AnyAsync();
            
            Console.WriteLine($"   Users exist: {hasUsers}");
            Console.WriteLine($"   VendorProfiles exist: {hasVendorProfiles}");
            Console.WriteLine($"   VendorAddresses exist: {hasVendorAddresses}");
            
            // Only seed if database is completely empty
            if (hasUsers && hasVendorProfiles && hasVendorAddresses)
            {
                Console.WriteLine("⚠️ Database already seeded. Skipping seeding process.");
                return;
            }
            
            Console.WriteLine("🌱 Starting database seeding...");
            
            //=======================================
            // Seed Users (only if empty)
            //=======================================
            List<User> users;
            if (!hasUsers)
            {
                users = new List<User>
                {
                    new User
                    {
                        PublicId = Guid.NewGuid(),
                        FirebaseUid = "test-firebase-uid-001",
                        Email = "vendor1@test.com",
                        DisplayName = "Test Vendor 1",
                        Role = UserRole.Vendor,
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow
                    },
                    new User
                    {
                        PublicId = Guid.NewGuid(),
                        FirebaseUid = "test-firebase-uid-002",
                        Email = "vendor2@test.com",
                        DisplayName = "Test Vendor 2",
                        Role = UserRole.Vendor,
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow
                    },
                    new User
                    {
                        PublicId = Guid.NewGuid(),
                        FirebaseUid = "test-firebase-uid-003",
                        Email = "customer1@test.com",
                        DisplayName = "Test Customer 1",
                        Role = UserRole.Customer,
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow
                    },
                    new User
                    {
                        PublicId = Guid.NewGuid(),
                        FirebaseUid = "test-firebase-uid-004",
                        Email = "customer2@test.com",
                        DisplayName = "Test Customer 2",
                        Role = UserRole.Customer,
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow
                    }
                };

                await context.Users.AddRangeAsync(users);
                await context.SaveChangesAsync();
                Console.WriteLine($"✅ Seeded {users.Count} users successfully!");
            }
            else
            {
                // Get existing vendor users for profile seeding
                users = await context.Users.Where(u => u.Role == UserRole.Vendor).Take(2).ToListAsync();
                Console.WriteLine($"ℹ️ Users already exist. Using {users.Count} existing vendor users.");
            }
            
            // Check if we have enough vendor users to seed profiles
            if (users.Count < 2)
            {
                Console.WriteLine("⚠️ Not enough vendor users to seed profiles. Need at least 2 vendor users.");
                Console.WriteLine("---------------------------------------");
                return;
            }
            
            //=======================================
            // Seed Vendor Profiles (only if empty)
            //=======================================
            List<VendorProfile> vendorProfiles;
            if (!hasVendorProfiles)
            {
                vendorProfiles = new List<VendorProfile>
                {
                    new VendorProfile
                    {
                        PublicId = Guid.NewGuid(),
                        UserId = users[0].Id, // Vendor 1
                        BusinessName = "Joe's Pizza",
                        Description = "Best pizza in Jozi! Family-owned since 1985.",
                        LogoUrl = null,
                        PhoneNumber = "0118452367",
                        Email = "info@joespizza.com",
                        BusinessHours = "Mon-Fri: 11am-10pm, Sat-Sun: 12pm-11pm",
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow
                    },
                    new VendorProfile
                    {
                        PublicId = Guid.NewGuid(),
                        UserId = users[1].Id, // Vendor 2
                        BusinessName = "Healthy Bites Café",
                        Description = "Organic, healthy food made fresh daily.",
                        LogoUrl = null,
                        PhoneNumber = "0125896342",
                        Email = "hello@healthybites.ca",
                        BusinessHours = "Mon-Sat: 8am-6pm, Sun: Closed",
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow
                    }
                };

                await context.VendorProfiles.AddRangeAsync(vendorProfiles);
                await context.SaveChangesAsync();
                Console.WriteLine($"✅ Seeded {vendorProfiles.Count} Vendor Profiles successfully!");
            }
            else
            {
                vendorProfiles = await context.VendorProfiles.Take(2).ToListAsync();
                Console.WriteLine($"ℹ️ VendorProfiles already exist. Using {vendorProfiles.Count} existing profiles.");
            }
            
            //=======================================
            // Seed Vendor Addresses (only if empty)
            //=======================================
            if (!hasVendorAddresses && vendorProfiles.Count >= 2)
            {
                var vendorAddresses = new List<VendorAddress>
                {
                    new VendorAddress
                    {
                        PublicId = Guid.NewGuid(),
                        Street = "123 Main Street",
                        Building = "Suite 100",
                        City = "Johannesburg",
                        Province = "Gauteng",
                        PostalCode = "1510",
                        Country = "South Africa",
                        Latitude = -26.2041,
                        Longitude = 28.0473,
                        VendorProfileId = vendorProfiles[0].Id,
                        CreatedAt = DateTime.UtcNow
                    },
                    new VendorAddress
                    {
                        PublicId = Guid.NewGuid(),
                        Street = "456 Queen Street West",
                        Building = "", // Empty string instead of null to avoid DB constraint error
                        City = "Pretoria",
                        Province = "Gauteng",
                        PostalCode = "0001",
                        Country = "South Africa",
                        Latitude = -25.7479,
                        Longitude = 28.2293,
                        VendorProfileId = vendorProfiles[1].Id,
                        CreatedAt = DateTime.UtcNow
                    }
                };

                await context.VendorAddresses.AddRangeAsync(vendorAddresses);
                await context.SaveChangesAsync();
                Console.WriteLine($"✅ Seeded {vendorAddresses.Count} Addresses successfully!");
                
                //=======================================
                // Update Vendor Profiles with Address IDs
                //=======================================
                vendorProfiles[0].VendorAddressId = vendorAddresses[0].Id;
                vendorProfiles[1].VendorAddressId = vendorAddresses[1].Id;
                await context.SaveChangesAsync();
                Console.WriteLine("✅ Updated Vendor Profiles with Address IDs!");
            }
            else if (hasVendorAddresses)
            {
                Console.WriteLine("ℹ️ VendorAddresses already exist. Skipping address seeding.");
            }

            // Seed Deals (you can add this once Deal Management is complete)
            // We'll add this in the next step

            Console.WriteLine("✅ Database seeding completed successfully!");
            Console.WriteLine("---------------------------------------");
            
        }
    }
}