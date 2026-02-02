using System;
using System.Collections.Generic;

namespace Replate.Domain.Entities
{
    public class VendorProfile
    {
        public int Id { get; set; }
        public Guid PublicId { get; set; } = Guid.NewGuid();
        
        // Vendor Information
        public required string BusinessName { get; set; }
        public string?  Description { get; set; }
        public string? LogoUrl { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Email { get; set; }
        
        // Business Hours (stored as JSON or separate entity)
        public string? BusinessHours { get; set; }
        
        // Relationships
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        
        public int?  VendorAddressId { get; set; }
        public VendorAddress?  VendorAddress { get; set; }
        
        // Navigation property
        public ICollection<FoodListing> FoodListings { get; set; } = new List<FoodListing>();
        
        // Audit fields
        public DateTime CreatedAt { get; set; } = DateTime. UtcNow;
        public DateTime?  UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
